using UnityEngine;

public class Match
{


    #region Member Variables

    [SerializeField]
    private Player m_playerOne;

    [SerializeField]
    private Player m_playerTwo;

    [SerializeField]
    private Tournament.SurfaceType m_surfaceType;

    [SerializeField]
    private Tournament.TournamentType m_tournamentType;

    #endregion


    public Match(Player firstPlayer, Player secondPlayer, Tournament.SurfaceType surfaceType, Tournament.TournamentType tournamentType)
    {
        m_playerOne = firstPlayer;
        m_playerTwo = secondPlayer;
        m_surfaceType = surfaceType;
        m_tournamentType = tournamentType;
    }


    #region Public Methods

    /// <summary>
    /// Starts the match and returns the winner player.
    /// </summary>
    /// <returns>Winner player.</returns>
    public Player Winner()
    {
        SetPlayerPoints(m_playerOne, m_playerTwo, m_surfaceType);

        float randomShot = Random.Range(0.0f, 1.0f);
        float playerOneWinningPossibility = (float)m_playerOne.MatchPoint / (float)(m_playerOne.MatchPoint + m_playerTwo.MatchPoint);

        if (m_tournamentType == Tournament.TournamentType.Elimination)
        {
            if (randomShot <= playerOneWinningPossibility)
            {
                m_playerOne.GainExperience(20);
                m_playerTwo.GainExperience(10);
                return m_playerOne;
            }
            else
            {
                m_playerTwo.GainExperience(20);
                m_playerOne.GainExperience(10);
                return m_playerTwo;
            }
        }
        else
        {
            if (randomShot <= playerOneWinningPossibility)
            {
                m_playerOne.GainExperience(10);
                m_playerTwo.GainExperience(1);
                return m_playerOne;
            }
            else
            {
                m_playerTwo.GainExperience(10);
                m_playerOne.GainExperience(1);
                return m_playerTwo;
            }
        }
    }

    /// <summary>
    /// Plays the match.
    /// </summary>
    public void Play()
    {
        SetPlayerPoints(m_playerOne, m_playerTwo, m_surfaceType);

        float randomShot = Random.Range(0.0f, 1.0f);
        float playerOneWinningPossibility = (float)m_playerOne.MatchPoint / (float)(m_playerOne.MatchPoint + m_playerTwo.MatchPoint);

        if (m_tournamentType == Tournament.TournamentType.Elimination)
        {
            if (randomShot <= playerOneWinningPossibility)
            {
                m_playerOne.GainExperience(20);
                m_playerTwo.GainExperience(10);
            }
            else
            {
                m_playerTwo.GainExperience(20);
                m_playerOne.GainExperience(10);
            }
        }
        else
        {
            if (randomShot <= playerOneWinningPossibility)
            {
                m_playerOne.GainExperience(10);
                m_playerTwo.GainExperience(1);
            }
            else
            {
                m_playerTwo.GainExperience(10);
                m_playerOne.GainExperience(1);
            }
        }
    }

    #endregion


    #region Utility

    /// <summary>
    /// Calculates match points of the players.
    /// </summary>
    /// <param name="playerOne">First player.</param>
    /// <param name="playerTwo">Second player.</param>
    /// <param name="surfaceType">Tournament surface type.</param>
    void SetPlayerPoints(Player playerOne, Player playerTwo, Tournament.SurfaceType surfaceType)
    {
        playerOne.ResetMatchPoint();
        playerTwo.ResetMatchPoint();

        playerOne.GainMatchPoint(1);
        if (playerOne.Hand == Player.HandType.Left)
            playerOne.GainMatchPoint(2);

        playerTwo.GainMatchPoint(1);
        if (playerTwo.Hand == Player.HandType.Left)
            playerTwo.GainMatchPoint(2);

        if (playerOne.Experience > playerTwo.Experience)
            playerOne.GainMatchPoint(3);
        else if (playerOne.Experience < playerTwo.Experience)
            playerTwo.GainMatchPoint(3);

        if (surfaceType == Tournament.SurfaceType.Clay)
        {
            if (playerOne.SurfaceSkillSet.ClayExp > playerTwo.SurfaceSkillSet.ClayExp)
                playerOne.GainMatchPoint(4);
            else if (playerOne.SurfaceSkillSet.ClayExp < playerTwo.SurfaceSkillSet.ClayExp)
                playerTwo.GainMatchPoint(4);
        }
        else if (surfaceType == Tournament.SurfaceType.Grass)
        {
            if (playerOne.SurfaceSkillSet.GrassExp > playerTwo.SurfaceSkillSet.GrassExp)
                playerOne.GainMatchPoint(4);
            else if (playerOne.SurfaceSkillSet.GrassExp < playerTwo.SurfaceSkillSet.GrassExp)
                playerTwo.GainMatchPoint(4);
        }
        else if (surfaceType == Tournament.SurfaceType.Hard)
        {
            if (playerOne.SurfaceSkillSet.HardExp > playerTwo.SurfaceSkillSet.HardExp)
                playerOne.GainMatchPoint(4);
            else if (playerOne.SurfaceSkillSet.HardExp < playerTwo.SurfaceSkillSet.HardExp)
                playerTwo.GainMatchPoint(4);
        }
    }

    #endregion


}
