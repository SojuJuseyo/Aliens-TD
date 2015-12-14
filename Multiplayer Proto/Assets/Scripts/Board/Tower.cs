using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tower : MonoBehaviour
{
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
    public float range;
    //Type of the tower
    public Player_Board.e_tower type;
    //Color upgrade
    [HideInInspector]
    public Player_Board.e_color color = Player_Board.e_color.NONE;
    //Level of the tower
    public int level = 1;
    //Starting point of the beam on the turret
    [HideInInspector]
    public Transform shootPosition;
    //Damage of the turret
    public double damage = 0;
    public double cost = 0;
    //Slow duration of the turret
    private int slowDuration = 0;
    //Poison power
    private int poisonPower = 0;
    //Upgrade / level up script
    private Upgrade script;
    //Radius of the splash
    [HideInInspector]
    public float splashRadius = 1.5f;
    //Declaring the explosion
    public GameObject explosion;

    void Start()
    {
        script = GameObject.Find("Board").GetComponent<Upgrade>();
        shootPosition = transform.FindChild("ShootingPosition");
    }

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

    //fonction appellée pour donner les nouvelles specs de la case à cette tour
    public void UpdateInfos(Player_Board.t_infoSlot newInfos)
    {
        type = newInfos.tower;
        level = newInfos.level;
        color = newInfos.color;
    }

    void GetEnemiesInRadius(float radius)
    {
		GameObject[] enemies;
        //Find all GameObjects
		if (type == Player_Board.e_tower.ANTIAIR)
			enemies = GameObject.FindGameObjectsWithTag("EnemyAir");
		else
        	enemies = GameObject.FindGameObjectsWithTag("Enemy");
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
				Debug.Log("if (dist < radius)");
                Debug.Log(enemy.name + ": " + dist);

                //Declares the line renderer used for the beam
                GameObject go = new GameObject();
                LineRenderer lr = go.AddComponent<LineRenderer>();

                //Draw the beam
                lr.material = new Material(Shader.Find("Particles/Additive"));
                if (color == Player_Board.e_color.BLUE)
                    lr.SetColors(Color.blue, Color.blue);
                else if (color == Player_Board.e_color.RED)
                    lr.SetColors(Color.red, Color.red);
                else if (color == Player_Board.e_color.GREEN)
                    lr.SetColors(Color.green, Color.green);
                lr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                lr.SetVertexCount(2);
                lr.SetWidth(0.2f, 0.2f);
                lr.SetPosition(0, shootPosition.transform.position);
                lr.SetPosition(1, enemy.transform.position);

                ///////////////
                // Explosion //
                ///////////////

                //Get the distance between the turret and the enemy
                if (type == Player_Board.e_tower.SPLASH)
                {
                    GameObject tmpExplosion = Instantiate(explosion, enemy.transform.position, Quaternion.identity) as GameObject;

                    for (int j = 0; j < enemies.Length; j++)
                    {
                        float distanceEnemyEnemy = Vector3.Distance(enemy.transform.position, enemies[j].transform.position);

                        //If the enemy is in the tower's radius
                        if (distanceEnemyEnemy < splashRadius && distanceEnemyEnemy != 0)
                        {
                            Debug.Log("In da mothafocking range : " + enemies[j].name);
                        }
                    }
                    Destroy(tmpExplosion, tmpExplosion.GetComponent<ParticleSystem>().duration);
                }

                /////////
                // End //
                /////////

                //Save the instance of line renderer to be able to delete it after
                lrSave = go;

                //Save the moment when the beam was fired
                lastShot = shootingRate;

                //TODO : Deal damage to the target
				Debug.Log("damageTarget(enemy);");
                damageTarget(enemy);
                break;
            }
        }
    }

    void setNewStats(Upgrade.TurretInfos turretInfos)
    {
        damage = turretInfos.damage;
		Debug.Log("Damage setNewStats " + damage);
        cost = turretInfos.cost;
        if (color == Player_Board.e_color.BLUE)
            slowDuration += 1;
        else if (color == Player_Board.e_color.GREEN)
            poisonPower += 1;
    }

    public void upgrade(Player_Board.e_color newColor)
    {
        if (newColor != Player_Board.e_color.NONE)
            color = newColor;
        else
            ++level;
        if (script == null)
            script = GameObject.Find("Board").GetComponent<Upgrade>();
        setNewStats(script.getNewStats(type, color, level));
    }

    void damageTarget(GameObject target)
    {
        Monster targetScript;
        targetScript = target.GetComponent<Monster>();
        Debug.Log("Damage LE METAL C'EST POUR LES PEDALES : " + damage);
        targetScript.takeDamage(damage, slowDuration, poisonPower);
    }
}
