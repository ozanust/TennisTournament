using UnityEngine;

public class Tournament
{
    #region Enumerables

    public enum SurfaceType
    {
        Clay = 0,
        Grass = 1,
        Hard = 2
    }

    public enum TournamentType
    {
        Elimination = 0,
        League = 1
    }

    #endregion


    #region Member Variables

    [SerializeField]
    private int m_id;

    [SerializeField]
    private SurfaceType m_surfaceType;

    [SerializeField]
    private TournamentType m_type;

    #endregion


    public Tournament(int id, SurfaceType surface, TournamentType type)
    {
        m_id = id;
        m_surfaceType = surface;
        m_type = type;
    }


    #region Properties

    public int ID
    {
        get { return m_id; }
    }

    public SurfaceType TournamentSurfaceType
    {
        get { return m_surfaceType; }
    }

    public TournamentType Type
    {
        get { return m_type; }
    }

    #endregion
}
