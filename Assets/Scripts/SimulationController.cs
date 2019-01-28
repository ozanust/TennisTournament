using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class SimulationController : MonoBehaviour
{


    #region Member Variables

    private string m_inputFileName = "input.json";
    private string m_outputFileName = "output.json";
    private string m_inputFilePath;
    private string m_outputFilePath;
    private string m_inputData = string.Empty;

    private Stack<Player> m_playerStack;

    private Player[] m_allPlayers;
    private Tournament[] m_tournaments;

    #endregion 


    private void Start()
    {
        m_inputFilePath = Path.Combine(Application.streamingAssetsPath, m_inputFileName);
        m_outputFilePath = Path.Combine(Application.streamingAssetsPath, m_outputFileName);

        if (LoadInputAndParseDataFromFile(m_inputFilePath))
        {
            for (int i = 0; i < m_tournaments.Length; i++)
            {
                PlayTournament(m_tournaments[i]);
            }

            List<Player> players = m_allPlayers.OrderBy(player => player.Experience).ThenBy(player => player.BaseExperience).Reverse().ToList();
            List<OutputData> results = new List<OutputData>();

            for (int i = 0; i < players.Count; i++)
            {
                results.Add(new OutputData(i + 1, players[i].ID, players[i].GainedExperience, players[i].Experience));
            }

            string json = JsonConvert.SerializeObject(results, Formatting.Indented);
            File.WriteAllText(m_outputFilePath, json);
        }
    }

	/// <summary>
	/// Loads game settings from input file to string as data. Returns true if parsing and loading the data is a success.
	/// </summary>
	/// <param name="inputFile">Absolute path of the input.json file.</param>
	bool LoadInputAndParseDataFromFile (string inputFile)
	{
        bool fileSuccessfullyRead = false;

        try
        {
            m_inputData = File.ReadAllText(inputFile);
            ParseInputData(m_inputData);
            fileSuccessfullyRead = true;
        }
        catch (System.Exception ex)
        {
            fileSuccessfullyRead = false;
            Debug.LogWarning(ex.Message);
        }

        return fileSuccessfullyRead;
	}

    /// <summary>
    /// Gets the player and tournament list from loaded input data.
    /// </summary>
    /// <param name="inputData">Loaded input data.</param>
    void ParseInputData(string inputData)
    {
        InputData data = JsonConvert.DeserializeObject<InputData>(inputData);

        if (Mathf.IsPowerOfTwo(data.Players.Length))
        {
            m_allPlayers = data.Players;
            m_tournaments = data.Tournaments;
        }
        else
        {
            Debug.LogWarning("Player count is not power of two.");
            return;
        }
    }

    /// <summary>
    /// Plays the all tournament matches.
    /// </summary>
    /// <param name="tournament"></param>
    void PlayTournament(Tournament tournament)
    {
        if(tournament.Type == Tournament.TournamentType.League)
        {
            List<Match> matches = new List<Match>();
            for (int i = 0; i < m_allPlayers.Length - 1; i++)
            {
                for(int j = i + 1; j < m_allPlayers.Length; j++)
                {
                    Match match = new Match(m_allPlayers[i], m_allPlayers[j], tournament.TournamentSurfaceType, tournament.Type);
                    matches.Add(match);
                }
            }

            int remainedMatchCount = 0;
            while((remainedMatchCount = matches.Count) > 0)
            {
                int iMatch = Random.Range(0, remainedMatchCount);
                Match match = matches[iMatch];
                matches.RemoveAt(iMatch);
                match.Play();
            }
        }
        else
        {
            PlayRound(m_allPlayers, tournament);
        }
    }

    /// <summary>
    /// Plays all rounds of the tournament until 1 player left as winner. Is a recursive function.
    /// </summary>
    /// <param name="playerList">Players that attended to the round.</param>
    /// <param name="tournament"></param>
    void PlayRound(Player[] playerList, Tournament tournament)
    {
        List<Match> roundMatches = RoundMatches(playerList, tournament);
        List<Player> nextRoundPlayers = new List<Player>();

        for (int i = 0; i < roundMatches.Count; i++)
        {
            nextRoundPlayers.Add(roundMatches[i].Winner());
        }

        if(nextRoundPlayers.Count > 1)
            PlayRound(nextRoundPlayers.ToArray(), tournament);
    }

    /// <summary>
    /// Prepares all round matches via blind pick from player list.
    /// </summary>
    /// <param name="playerList">Players that attended to the round.</param>
    /// <param name="tournament"></param>
    /// <returns></returns>
    List<Match> RoundMatches(Player[] playerList, Tournament tournament)
    {
        int matchCount = playerList.Length / 2;
        List<Match> matches = new List<Match>();
        List<Player> tempPlayers = new List<Player>(playerList);

        for (int i = 0; i < matchCount; i++)
        {
            int iFirstPlayer = UnityEngine.Random.Range(0, tempPlayers.Count);
            Player firstPlayer = tempPlayers[iFirstPlayer];
            tempPlayers.RemoveAt(iFirstPlayer);

            int iSecondPlayer = UnityEngine.Random.Range(0, tempPlayers.Count);
            Player secondPlayer = tempPlayers[iSecondPlayer];
            tempPlayers.RemoveAt(iSecondPlayer);

            matches.Add(new Match(firstPlayer, secondPlayer, tournament.TournamentSurfaceType, tournament.Type));
        }

        tempPlayers = null;

        return matches;
    }
}
