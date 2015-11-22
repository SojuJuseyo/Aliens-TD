using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tower : MonoBehaviour {

    //Type of game object that represents the enemy
	[HideInInspector]
    public const string enemyType = "Enemy";
    //Variable used to store the beam that is displayed on screen
	[HideInInspector]
    private GameObject lrSave;
    //Time in seconds between two shots (cooldown of the turret)
	[HideInInspector]
    public float shootingRate;
    //Duration of the shooting animation (how long is a beam displayed on screen)
	[HideInInspector]
    public float shotDuration;
    //Time since the last shot
	[HideInInspector]
    private float lastShot = 0f;
    //Time since the last shot
	[HideInInspector]
    public float range;
    //Type of the tower
	[HideInInspector]
    public Player_Board.e_tower type;
    //Color upgrade
	[HideInInspector]
	public Player_Board.e_color color = Player_Board.e_color.NONE;
    //Level of the tower
	[HideInInspector]
    public int level = 1;
    //Starting point of the beam on the turret
	[HideInInspector]
    public GameObject shootPosition;
    //Damage of the turret
	[HideInInspector]
    public uint damage = 0;
    //Slow duration of the turret
	[HideInInspector]
    private uint slowDuration = 0;
    //Poison power
	[HideInInspector]
    private uint poisonPower = 0;
    //Upgrade / level up script
    //private Upgrade script = GameObject.Find("GameData").GetComponent<Upgrade>();
    //Radius of the splash
	[HideInInspector]
    public float splashRadius = 1.5f;

    void Update()
    {
        //If a beam as been fired, decrease the cooldown of the turret's next shot
        if (lastShot > 0)
            lastShot -= Time.deltaTime;

        //If a beam as been displayed on screen for enough time, delete it
        if (lastShot < shootingRate - shotDuration)
            Destroy(lrSave);
        
        //If the turret's cooldown is at 0, search for new targets
        if (lastShot <= 0)
            GetEnemiesInRadius(range);
	}

	//fonction appellé pour donner les nouvelles specs de la case à cette tour
	public void UpdateInfos(Player_Board.t_infoSlot newInfos){
		type = newInfos.tower;
		level = newInfos.level;
		color = newInfos.color;
	}

    void GetEnemiesInRadius(float radius)
    {
        //Find all GameObjects
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0)
            return;

        for (uint i = 0; i < enemies.Length; ++i)
        {
            //Contains the enemy we will shoot
            GameObject enemy = enemies[i];

            //Get the distance between the turret and the enemy
            float dist = Vector3.Distance(transform.position, enemy.transform.position);

            //If the enemy is in the tower's radius
            if (dist < radius)
            {
                Debug.Log(enemy.name + ": " + dist);

                //Declares the line renderer used for the beam
                GameObject go = new GameObject();
                LineRenderer lr = go.AddComponent<LineRenderer>();

                //Draw the beam
                lr.material.color = Color.red;
                lr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                lr.SetVertexCount(2);
                lr.SetWidth(0.2f, 0.2f);
                lr.SetPosition(0, shootPosition.transform.position);
                lr.SetPosition(1, enemy.transform.position);

                ////////////
                // Explosion
                ////////////
                //Get the distance between the turret and the enemy

                if (type == Player_Board.e_tower.SPLASH)
                {
                    GameObject explosion = Instantiate(Resources.Load("Prefabs/ParticleSystemExplosion", typeof(GameObject))) as GameObject;
                    explosion.transform.position = enemy.transform.position;

                    for (int j = 0; j < enemies.Length; j++)
                    {
                        float distanceEnemyEnemy = Vector3.Distance(enemy.transform.position, enemies[j].transform.position);

                        //If the enemy is in the tower's radius
                        if (distanceEnemyEnemy < splashRadius && distanceEnemyEnemy != 0)
                        {
                            Debug.Log("In da mothafocking range : " + enemies[j].name);
                        }
                    }

                    Destroy(explosion, explosion.GetComponent<ParticleSystem>().duration);
                }

                //////////
                /// End
                //////////

                //Save the instance of line renderer to be able to delete it after
                lrSave = go;

                //Save the moment when the beam was fired
                lastShot = shootingRate;

                //TODO : Deal damage to the target
                //damageTarget(child);
                break;
            }
        }
    }

    void    setNewStats(Dictionary<string, uint> stats)
    {
        damage = stats["damage"];
        if (color == Player_Board.e_color.BLUE)
            slowDuration = stats["slowDuration"];
        else if (color == Player_Board.e_color.GREEN)
            poisonPower = stats["poisonPower"];
    }
	
}
