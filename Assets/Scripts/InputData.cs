using UnityEngine;

public class InputData{

    [SerializeField]
    private Player[] players;

    [SerializeField]
    private Tournament[] tournaments;

    public InputData(Player[] players, Tournament[] tournaments)
    {
        this.players = players;
        this.tournaments = tournaments;
    }

    public Player[] Players
    {
        get { return players; }
    }

    public Tournament[] Tournaments
    {
        get { return tournaments; }
    }
}
