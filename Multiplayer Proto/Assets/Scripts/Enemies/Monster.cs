using UnityEngine;
using System.Collections;
using Pathfinding;

public class Monster : MonoBehaviour {

    //Type of the monster
	public MonstersSelection.e_enemy type;
    //Stats
    public float formerHealth;
    public float formerSpeed;
    public double armor;
    //Percentages
    public float slowResist;
    public float poisonResist;
    //Money money money
    public int cost;
    public int incomeIncrease;
    //Upgrade / level up script
    private UpgradeMonsters script;

    private Seeker m_seeker;
	private Transform m_nexus;

	private int m_currentPoint;
	private Vector3 m_dir;
	private Path m_path;

	private MonstersSelection.e_enemy m_typeMonster;
	private Player_Board.e_player m_player;

	private float m_health;
	private float m_speed;

    private System.DateTime poisonStart;
    private System.DateTime poisonLastDamage;
    private System.DateTime poisonEnd;
    private System.DateTime slowEnd;
    private double poisonDamage;
    private double poisonDamageVal;

    void Start() {
		m_currentPoint = 0;
		m_seeker = GetComponent<Seeker> ();
		if (m_player == Player_Board.e_player.PLAYER1)
			m_nexus = GameObject.Find ("PLAYER2-NEXUS").transform;
		else
			m_nexus = GameObject.Find ("PLAYER1-NEXUS").transform;
		UpdatePath ();
	}

	public void setPlayerBoard(Player_Board.e_player p){
		m_player = p;
	}

	void FixedUpdate()
    {
        if (m_path != null)
        {
			if (m_currentPoint < m_path.vectorPath.Count)
            {
				if (m_seeker.IsDone() == true) {
					transform.position = Vector3.Lerp (transform.position, m_path.vectorPath [m_currentPoint], Time.deltaTime * m_speed);
					transform.LookAt(m_path.vectorPath [m_currentPoint]);
				}
				if (Vector3.Distance (transform.position, m_path.vectorPath [m_currentPoint]) < 1f)
					++m_currentPoint;
			}
            else
            {
				Debug.Log ("Monster blocked");
			}
		}
        if (System.DateTime.Compare(System.DateTime.Now, slowEnd) >= 0 && m_speed != formerSpeed)
            m_speed = formerSpeed;
        if (poisonDamage != 0)
        {
            if (poisonLastDamage == poisonStart || System.DateTime.Compare(System.DateTime.Now.AddSeconds(1), poisonLastDamage) > 0)
            {
                poisonLastDamage = System.DateTime.Now;
                m_health -= (float)poisonDamageVal;
            }
            if (System.DateTime.Compare(System.DateTime.Now, poisonEnd) >= 0)
            {
                poisonDamage = 0;
                poisonDamageVal = 0;
            }
        }
		Debug.Log ("Health monster = " + m_health);
	}

	public void UpdatePath () {
		if (m_seeker == null)
			m_seeker = GetComponent<Seeker> ();
		if (m_nexus == null) {
			if (m_player == Player_Board.e_player.PLAYER1)
				m_nexus = GameObject.Find ("PLAYER2-NEXUS").transform;
			else
				m_nexus = GameObject.Find ("PLAYER1-NEXUS").transform;
		}
		m_seeker.StartPath (gameObject.transform.position, m_nexus.position, OnPathComplete);
		m_currentPoint = 0;
	}

    public void takeDamage(double damage, int slowDuration, int poisonPower)
    {
        if (slowDuration != 0)
        {
            slowEnd = System.DateTime.Now.AddSeconds(slowDuration);
            m_speed = formerSpeed / 2 * slowResist;
            Debug.Log("Slow!");
        }
        if (poisonPower != 0)
        {
            poisonStart = System.DateTime.Now;
            poisonLastDamage = poisonStart;
            poisonEnd = System.DateTime.Now.AddSeconds(poisonPower);
            poisonDamage = formerHealth / 10 * poisonResist;
            poisonDamageVal = poisonDamage / 5; 
            Debug.Log("Poison!");
        }
        if (damage > 0)
            Debug.Log("Damage!");
        m_health -= (int)damage;
        if (m_health <= 0)
            Destroy(this.gameObject);
    }

    void OnPathComplete (Path p) {
		m_path = p;
	}

    public void setNewStats(UpgradeMonsters.Stats newStats)
    {
        formerHealth = newStats.health;
        m_health = formerHealth;
        formerSpeed = newStats.speed;
        m_speed = formerSpeed;
        armor = newStats.armor;
        slowResist = newStats.slowResist;
        poisonResist = newStats.poisonResist;
        cost = newStats.cost;
        incomeIncrease = newStats.incomeIncrease;
    }

    public void upgrade(int level)
    {
        if (script == null) {
			script = GameObject.Find ("Board").GetComponent<UpgradeMonsters> ();
			setNewStats (script.getNewStats (type, level));
		}
    }
}