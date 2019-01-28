using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OutputData
{
    [SerializeField]
    public int order;

    [SerializeField]
    public int player_id;

    [SerializeField]
    public int gained_experience;

    [SerializeField]
    public int total_experience;

    public OutputData(int order, int player_id, int gained_experience, int total_experience)
    {
        this.order = order;
        this.player_id = player_id;
        this.gained_experience = gained_experience;
        this.total_experience = total_experience;
    }
}
