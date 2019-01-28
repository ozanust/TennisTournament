using UnityEngine;

public class Player
{

    #region Inner Classes

    [System.Serializable]
    public class Skill
    {

        #region Member Variables

        [SerializeField]
        private int clay;

        [SerializeField]
        private int grass;

        [SerializeField]
        private int hard;

        #endregion


        public Skill(int clay, int grass, int hard)
        { 
            this.clay = clay;
            this.grass = grass;
            this.hard = hard;
        }


        #region Properties

        public int ClayExp
        {
            get { return clay; }
        }

        public int GrassExp
        {
            get { return grass; }
        }

        public int HardExp
        {
            get { return hard; }
        }

        #endregion
    }

    #endregion


    #region Enumerables

    public enum HandType
    {
        Left,
        Right
    }

    #endregion


    #region Member Variables

    [SerializeField]
    private int m_id;

    [SerializeField]
    private int m_experience;

    [SerializeField]
    private HandType m_hand;

    [SerializeField]
    private Skill m_skills;

    [SerializeField]
    private int m_matchPoint = 0;

    [SerializeField]
    private int m_gainedExperience = 0;

    #endregion


    public Player(int id, int experience, HandType hand, Skill skills)
    {
        m_id = id;
        m_experience = experience;
        m_hand = hand;
        m_skills = skills;
    }


    #region Properties

    public int ID
    {
        get { return m_id; }
    }

    public int Experience
    {
        get { return m_experience; }
    }

    public HandType Hand
    {
        get { return m_hand; }
    }

    public Skill SurfaceSkillSet
    {
        get { return m_skills; }
    }

    public int MatchPoint
    {
        get { return m_matchPoint; }
    }

    public int BaseExperience
    {
        get { return (m_experience - m_gainedExperience); }
    }

    public int GainedExperience
    {
        get { return m_gainedExperience; }
    }

    #endregion


    #region Public Methods

    public void GainExperience(int gainedExperience)
    {
        m_experience += gainedExperience;
        m_gainedExperience += gainedExperience;
    }

    public void GainMatchPoint(int gainedPoint)
    {
        m_matchPoint += gainedPoint;
    }

    public void ResetMatchPoint()
    {
        m_matchPoint = 0;
    }

    #endregion


}
