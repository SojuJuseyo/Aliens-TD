using UnityEngine;
using System.Collections;

public class Upgrade : MonoBehaviour
{
    public double damage;
    public int cost;

    public struct TurretInfos
    {
		public double damage;
        public int cost;
    }

    public TurretInfos getNewStats(Player_Board.e_tower type, Player_Board.e_color color, int level)
    {
        TurretInfos turretInfos;

        switch (type)
        {
            case Player_Board.e_tower.STANDARD:
                turretInfos = standardTower(color, level);
                break;
            case Player_Board.e_tower.ANTIAIR:
                turretInfos = antiaerialTower(color, level);
                break;
            case Player_Board.e_tower.GATLING:
                turretInfos = gatlingTower(color, level);
                break;
            case Player_Board.e_tower.MELEE:
                turretInfos = meleeTower(color, level);
                break;
            case Player_Board.e_tower.SNIPER:
                turretInfos = sniperTower(color, level);
                break;
            case Player_Board.e_tower.SPLASH:
                turretInfos = mortarTower(color, level);
                break;
            default:
                turretInfos.damage = damage;
                turretInfos.cost = cost;
                break;
        }
		Debug.Log ("turretInfos damage VERIF = " + turretInfos.damage);
        return (turretInfos);
    }

    public TurretInfos standardTower(Player_Board.e_color color, int level)
    {
        TurretInfos turretInfos;

        switch (color)
        {
            case Player_Board.e_color.BLUE:
            {
                switch (level)
                {
                        case 1:
                            turretInfos.damage = damage * 1.1;
                            turretInfos.cost = cost * 2;
                            return (turretInfos);
                        case 2:
                            turretInfos.damage = damage * 1.2;
                            turretInfos.cost = cost * 4;
                            return (turretInfos);
                        case 3:
                            turretInfos.cost = cost * 8;
                            turretInfos.damage = damage * 1.3;
                            return (turretInfos);
                        case 4:
                            turretInfos.damage = damage * 1.4;
                            turretInfos.cost = cost * 16;
                            return (turretInfos);
                        case 5:
                            turretInfos.damage = damage * 1.5;
                            turretInfos.cost = cost * 32;
                            return (turretInfos);
                }
                break;
            }
            case Player_Board.e_color.GREEN:
                {
                    switch (level)
                    {
                        case 1:
                            turretInfos.damage = damage;
                            turretInfos.cost = cost * 2;
                            return (turretInfos);
                        case 2:
                            turretInfos.damage = damage;
                            turretInfos.cost = cost * 4;
                            return (turretInfos);
                        case 3:
                            turretInfos.cost = cost * 8;
                            turretInfos.damage = damage;
                            return (turretInfos);
                        case 4:
                            turretInfos.damage = damage;
                            turretInfos.cost = cost * 16;
                            return (turretInfos);
                        case 5:
                            turretInfos.damage = damage;
                            turretInfos.cost = cost * 32;
                            return (turretInfos);
                    }
                    break;
                }
            case Player_Board.e_color.RED:
                {
                    switch (level)
                    {
                        case 1:
                            turretInfos.damage = damage * 1.25;
                            turretInfos.cost = cost * 2;
                            return (turretInfos);
                        case 2:
                            turretInfos.damage = damage * 1.5;
                            turretInfos.cost = cost * 4;
                            return (turretInfos);
                        case 3:
                            turretInfos.damage = damage * 1.75;
                            turretInfos.cost = cost * 8;
                            return (turretInfos);
                        case 4:
                            turretInfos.damage = damage * 2;
                            turretInfos.cost = cost * 16;
                            return (turretInfos);
                        case 5:
                            turretInfos.damage = damage * 3;
                            turretInfos.cost = cost * 32;
                            return (turretInfos);
                    }
                    break;
                }
        }
		Debug.Log ("damage VERIF 1 = " + damage);
        turretInfos.damage = damage;
		Debug.Log ("damage VERIF 2 = " + damage);
        turretInfos.cost = cost;
        return (turretInfos);
    }

    public TurretInfos antiaerialTower(Player_Board.e_color color, int level)
    {
        TurretInfos turretInfos;

        switch (color)
        {
            case Player_Board.e_color.BLUE:
                {
                    switch (level)
                    {
                        case 1:
                            turretInfos.damage = damage * 1.1;
                            turretInfos.cost = cost * 4;
                            return (turretInfos);
                        case 2:
                            turretInfos.damage = damage * 1.2;
                            turretInfos.cost = cost * 8;
                            return (turretInfos);
                        case 3:
                            turretInfos.cost = cost * 16;
                            turretInfos.damage = damage * 1.3;
                            return (turretInfos);
                        case 4:
                            turretInfos.damage = damage * 1.4;
                            turretInfos.cost = cost * 28;
                            return (turretInfos);
                        case 5:
                            turretInfos.damage = damage * 1.5;
                            turretInfos.cost = cost * 40;
                            return (turretInfos);
                    }
                    break;
                }
            case Player_Board.e_color.GREEN:
                {
                    switch (level)
                    {
                        case 1:
                            turretInfos.damage = damage;
                            turretInfos.cost = cost * 4;
                            return (turretInfos);
                        case 2:
                            turretInfos.damage = damage;
                            turretInfos.cost = cost * 8;
                            return (turretInfos);
                        case 3:
                            turretInfos.cost = cost * 16;
                            turretInfos.damage = damage;
                            return (turretInfos);
                        case 4:
                            turretInfos.damage = damage;
                            turretInfos.cost = cost * 28;
                            return (turretInfos);
                        case 5:
                            turretInfos.damage = damage;
                            turretInfos.cost = cost * 40;
                            return (turretInfos);
                    }
                    break;
                }
            case Player_Board.e_color.RED:
                {
                    switch (level)
                    {
                        case 1:
                            turretInfos.damage = damage * 1.25;
                            turretInfos.cost = cost * 4;
                            return (turretInfos);
                        case 2:
                            turretInfos.damage = damage * 1.5;
                            turretInfos.cost = cost * 8;
                            return (turretInfos);
                        case 3:
                            turretInfos.damage = damage * 1.75;
                            turretInfos.cost = cost * 16;
                            return (turretInfos);
                        case 4:
                            turretInfos.damage = damage * 2;
                            turretInfos.cost = cost * 28;
                            return (turretInfos);
                        case 5:
                            turretInfos.damage = damage * 3;
                            turretInfos.cost = cost * 40;
                            return (turretInfos);
                    }
                    break;
                }
        }

        turretInfos.damage = damage;
        turretInfos.cost = cost * 2;
        return (turretInfos);
    }

    public TurretInfos mortarTower(Player_Board.e_color color, int level)
    {
        TurretInfos turretInfos;

        switch (color)
        {
            case Player_Board.e_color.BLUE:
                {
                    switch (level)
                    {
                        case 1:
                            turretInfos.damage = damage * 1.05;
                            turretInfos.cost = cost * 8;
                            return (turretInfos);
                        case 2:
                            turretInfos.damage = damage * 1.10;
                            turretInfos.cost = cost * 16;
                            return (turretInfos);
                        case 3:
                            turretInfos.damage = damage * 1.15;
                            turretInfos.cost = cost * 28;
                            return (turretInfos);
                        case 4:
                            turretInfos.damage = damage * 1.2;
                            turretInfos.cost = cost * 40;
                            return (turretInfos);
                        case 5:
                            turretInfos.damage = damage * 1.25;
                            turretInfos.cost = cost * 60;
                            return (turretInfos);
                    }
                    break;
                }
            case Player_Board.e_color.GREEN:
                {
                    switch (level)
                    {
                        case 1:
                            turretInfos.damage = damage;
                            turretInfos.cost = cost * 8;
                            return (turretInfos);
                        case 2:
                            turretInfos.damage = damage;
                            turretInfos.cost = cost * 16;
                            return (turretInfos);
                        case 3:
                            turretInfos.cost = cost * 28;
                            turretInfos.damage = damage;
                            return (turretInfos);
                        case 4:
                            turretInfos.damage = damage;
                            turretInfos.cost = cost * 40;
                            return (turretInfos);
                        case 5:
                            turretInfos.damage = damage;
                            turretInfos.cost = cost * 60;
                            return (turretInfos);
                    }
                    break;
                }
            case Player_Board.e_color.RED:
                {
                    switch (level)
                    {
                        case 1:
                            turretInfos.damage = damage * 1.1;
                            turretInfos.cost = cost * 8;
                            return (turretInfos);
                        case 2:
                            turretInfos.damage = damage * 1.2;
                            turretInfos.cost = cost * 16;
                            return (turretInfos);
                        case 3:
                            turretInfos.damage = damage * 1.3;
                            turretInfos.cost = cost * 28;
                            return (turretInfos);
                        case 4:
                            turretInfos.damage = damage * 1.5;
                            turretInfos.cost = cost * 40;
                            return (turretInfos);
                        case 5:
                            turretInfos.damage = damage * 1.75;
                            turretInfos.cost = cost * 60;
                            return (turretInfos);
                    }
                    break;
                }
        }

        turretInfos.damage = damage;
        turretInfos.cost = cost * 4;
        return (turretInfos);
    }

    public TurretInfos meleeTower(Player_Board.e_color color, int level)
    {
        TurretInfos turretInfos;

        switch (color)
        {
            case Player_Board.e_color.BLUE:
                {
                    switch (level)
                    {
                        case 1:
                            turretInfos.damage = damage * 1.1;
                            turretInfos.cost = cost * 4;
                            return (turretInfos);
                        case 2:
                            turretInfos.damage = damage * 1.2;
                            turretInfos.cost = cost * 8;
                            return (turretInfos);
                        case 3:
                            turretInfos.cost = cost * 16;
                            turretInfos.damage = damage * 1.3;
                            return (turretInfos);
                        case 4:
                            turretInfos.damage = damage * 1.4;
                            turretInfos.cost = cost * 28;
                            return (turretInfos);
                        case 5:
                            turretInfos.damage = damage * 1.5;
                            turretInfos.cost = cost * 40;
                            return (turretInfos);
                    }
                    break;
                }
            case Player_Board.e_color.GREEN:
                {
                    switch (level)
                    {
                        case 1:
                            turretInfos.damage = damage;
                            turretInfos.cost = cost * 4;
                            return (turretInfos);
                        case 2:
                            turretInfos.damage = damage;
                            turretInfos.cost = cost * 8;
                            return (turretInfos);
                        case 3:
                            turretInfos.cost = cost * 16;
                            turretInfos.damage = damage;
                            return (turretInfos);
                        case 4:
                            turretInfos.damage = damage;
                            turretInfos.cost = cost * 28;
                            return (turretInfos);
                        case 5:
                            turretInfos.damage = damage;
                            turretInfos.cost = cost * 40;
                            return (turretInfos);
                    }
                    break;
                }
            case Player_Board.e_color.RED:
                {
                    switch (level)
                    {
                        case 1:
                            turretInfos.damage = damage * 1.25;
                            turretInfos.cost = cost * 4;
                            return (turretInfos);
                        case 2:
                            turretInfos.damage = damage * 1.5;
                            turretInfos.cost = cost * 8;
                            return (turretInfos);
                        case 3:
                            turretInfos.damage = damage * 1.75;
                            turretInfos.cost = cost * 16;
                            return (turretInfos);
                        case 4:
                            turretInfos.damage = damage * 2;
                            turretInfos.cost = cost * 28;
                            return (turretInfos);
                        case 5:
                            turretInfos.damage = damage * 3;
                            turretInfos.cost = cost * 40;
                            return (turretInfos);
                    }
                    break;
                }
        }

        turretInfos.damage = damage;
        turretInfos.cost = cost * 2;
        return (turretInfos);
    }

    public TurretInfos sniperTower(Player_Board.e_color color, int level)
    {
        TurretInfos turretInfos;

        switch (color)
        {
            case Player_Board.e_color.BLUE:
                {
                    switch (level)
                    {
                        case 1:
                            turretInfos.damage = damage * 2.1;
                            turretInfos.cost = cost * 8;
                            return (turretInfos);
                        case 2:
                            turretInfos.damage = damage * 2.2;
                            turretInfos.cost = cost * 16;
                            return (turretInfos);
                        case 3:
                            turretInfos.damage = damage * 2.3;
                            turretInfos.cost = cost * 28;
                            return (turretInfos);
                        case 4:
                            turretInfos.damage = damage * 2.4;
                            turretInfos.cost = cost * 40;
                            return (turretInfos);
                        case 5:
                            turretInfos.damage = damage * 2.5;
                            turretInfos.cost = cost * 60;
                            return (turretInfos);
                    }
                    break;
                }
            case Player_Board.e_color.GREEN:
                {
                    switch (level)
                    {
                        case 1:
                            turretInfos.damage = damage;
                            turretInfos.cost = cost * 8;
                            return (turretInfos);
                        case 2:
                            turretInfos.damage = damage;
                            turretInfos.cost = cost * 16;
                            return (turretInfos);
                        case 3:
                            turretInfos.cost = cost * 28;
                            turretInfos.damage = damage;
                            return (turretInfos);
                        case 4:
                            turretInfos.damage = damage;
                            turretInfos.cost = cost * 40;
                            return (turretInfos);
                        case 5:
                            turretInfos.damage = damage;
                            turretInfos.cost = cost * 60;
                            return (turretInfos);
                    }
                    break;
                }
            case Player_Board.e_color.RED:
                {
                    switch (level)
                    {
                        case 1:
                            turretInfos.damage = damage * 2.25;
                            turretInfos.cost = cost * 8;
                            return (turretInfos);
                        case 2:
                            turretInfos.damage = damage * 2.5;
                            turretInfos.cost = cost * 16;
                            return (turretInfos);
                        case 3:
                            turretInfos.damage = damage * 2.75;
                            turretInfos.cost = cost * 28;
                            return (turretInfos);
                        case 4:
                            turretInfos.damage = damage * 3;
                            turretInfos.cost = cost * 40;
                            return (turretInfos);
                        case 5:
                            turretInfos.damage = damage * 4;
                            turretInfos.cost = cost * 60;
                            return (turretInfos);
                    }
                    break;
                }
        }

        turretInfos.damage = damage * 2;
        turretInfos.cost = cost * 4;
        return (turretInfos);
    }

    public TurretInfos gatlingTower(Player_Board.e_color color, int level)
    {
        TurretInfos turretInfos;

        switch (color)
        {
            case Player_Board.e_color.BLUE:
                {
                    switch (level)
                    {
                        case 1:
                            turretInfos.damage = damage * 0.45;
                            turretInfos.cost = cost * 6;
                            return (turretInfos);
                        case 2:
                            turretInfos.damage = damage * 0.5;
                            turretInfos.cost = cost * 12;
                            return (turretInfos);
                        case 3:
                            turretInfos.damage = damage * 0.55;
                            turretInfos.cost = cost * 24;
                            return (turretInfos);
                        case 4:
                            turretInfos.damage = damage * 0.6;
                            turretInfos.cost = cost * 36;
                            return (turretInfos);
                        case 5:
                            turretInfos.damage = damage * 0.65;
                            turretInfos.cost = cost * 50;
                            return (turretInfos);
                    }
                    break;
                }
            case Player_Board.e_color.GREEN:
                {
                    switch (level)
                    {
                        case 1:
                            turretInfos.damage = damage;
                            turretInfos.cost = cost * 6;
                            return (turretInfos);
                        case 2:
                            turretInfos.damage = damage;
                            turretInfos.cost = cost * 12;
                            return (turretInfos);
                        case 3:
                            turretInfos.cost = cost * 24;
                            turretInfos.damage = damage;
                            return (turretInfos);
                        case 4:
                            turretInfos.damage = damage;
                            turretInfos.cost = cost * 36;
                            return (turretInfos);
                        case 5:
                            turretInfos.damage = damage;
                            turretInfos.cost = cost * 50;
                            return (turretInfos);
                    }
                    break;
                }
            case Player_Board.e_color.RED:
                {
                    switch (level)
                    {
                        case 1:
                            turretInfos.damage = damage * 0.55;
                            turretInfos.cost = cost * 6;
                            return (turretInfos);
                        case 2:
                            turretInfos.damage = damage * 0.7;
                            turretInfos.cost = cost * 12;
                            return (turretInfos);
                        case 3:
                            turretInfos.damage = damage * 0.85;
                            turretInfos.cost = cost * 24;
                            return (turretInfos);
                        case 4:
                            turretInfos.damage = damage * 1;
                            turretInfos.cost = cost * 36;
                            return (turretInfos);
                        case 5:
                            turretInfos.damage = damage * 1.25;
                            turretInfos.cost = cost * 50;
                            return (turretInfos);
                    }
                    break;
                }
        }

        turretInfos.damage = damage * 0.4;
        turretInfos.cost = cost * 3;
        return (turretInfos);
    }
}
