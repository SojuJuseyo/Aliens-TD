using UnityEngine;
using System.Collections;

public class Upgrade : MonoBehaviour
{
    public double damage;

    public double getNewStats(Player_Board.e_tower type, Player_Board.e_color color, int level)
    {
        double newDamage;
        switch (type)
        {
            case Player_Board.e_tower.STANDARD:
                newDamage = standardTower(color, level);
                break;
            case Player_Board.e_tower.ANTIAIR:
                newDamage = antiaerialTower(color, level);
                break;
            case Player_Board.e_tower.GATLING:
                newDamage = gatlingTower(color, level);
                break;
            case Player_Board.e_tower.MELEE:
                newDamage = meleeTower(color, level);
                break;
            case Player_Board.e_tower.SNIPER:
                newDamage = sniperTower(color, level);
                break;
            case Player_Board.e_tower.SPLASH:
                newDamage = mortarTower(color, level);
                break;
            default:
                newDamage = damage;
                break;
        }
        return (newDamage);
    }

    double standardTower(Player_Board.e_color color, int level)
    {
        switch (color)
        {
            case Player_Board.e_color.BLUE:
            {
                switch (level)
                {
                        case 1:
                            return (damage * 1.1);
                        case 2:
                            return (damage * 1.2);
                        case 3:
                            return (damage * 1.3);
                        case 4:
                            return (damage * 1.4);
                        case 5:
                            return (damage * 1.5);
                }
                break;
            }
            case Player_Board.e_color.GREEN:
                {
                    switch (level)
                    {
                        case 1:
                            return (damage);
                        case 2:
                            return (damage);
                        case 3:
                            return (damage);
                        case 4:
                            return (damage);
                        case 5:
                            return (damage);
                    }
                    break;
                }
            case Player_Board.e_color.RED:
                {
                    switch (level)
                    {
                        case 1:
                            return (damage * 1.25);
                        case 2:
                            return (damage * 1.5);
                        case 3:
                            return (damage * 1.75);
                        case 4:
                            return (damage * 2);
                        case 5:
                            return (damage * 3);
                    }
                    break;
                }
        }
        return (damage);
    }

    double antiaerialTower(Player_Board.e_color color, int level)
    {
        switch (color)
        {
            case Player_Board.e_color.BLUE:
                {
                    switch (level)
                    {
                        case 1:
                            return (damage * 1.1);
                        case 2:
                            return (damage * 1.2);
                        case 3:
                            return (damage * 1.3);
                        case 4:
                            return (damage * 1.4);
                        case 5:
                            return (damage * 1.5);
                    }
                    break;
                }
            case Player_Board.e_color.GREEN:
                {
                    switch (level)
                    {
                        case 1:
                            return (damage);
                        case 2:
                            return (damage);
                        case 3:
                            return (damage);
                        case 4:
                            return (damage);
                        case 5:
                            return (damage);
                    }
                    break;
                }
            case Player_Board.e_color.RED:
                {
                    switch (level)
                    {
                        case 1:
                            return (damage * 1.25);
                        case 2:
                            return (damage * 1.5);
                        case 3:
                            return (damage * 1.75);
                        case 4:
                            return (damage * 2);
                        case 5:
                            return (damage * 3);
                    }
                    break;
                }
        }
        return (damage);
    }

    double mortarTower(Player_Board.e_color color, int level)
    {
        switch (color)
        {
            case Player_Board.e_color.BLUE:
                {
                    switch (level)
                    {
                        case 1:
                            return (damage * 1.05);
                        case 2:
                            return (damage * 1.1);
                        case 3:
                            return (damage * 1.15);
                        case 4:
                            return (damage * 1.2);
                        case 5:
                            return (damage * 1.25);
                    }
                    break;
                }
            case Player_Board.e_color.GREEN:
                {
                    switch (level)
                    {
                        case 1:
                            return (damage);
                        case 2:
                            return (damage);
                        case 3:
                            return (damage);
                        case 4:
                            return (damage);
                        case 5:
                            return (damage);
                    }
                    break;
                }
            case Player_Board.e_color.RED:
                {
                    switch (level)
                    {
                        case 1:
                            return (damage * 1.1);
                        case 2:
                            return (damage * 1.2);
                        case 3:
                            return (damage * 1.3);
                        case 4:
                            return (damage * 1.5);
                        case 5:
                            return (damage * 1.75);
                    }
                    break;
                }
        }
        return (damage);
    }

    double meleeTower(Player_Board.e_color color, int level)
    {
        switch (color)
        {
            case Player_Board.e_color.BLUE:
                {
                    switch (level)
                    {
                        case 1:
                            return (damage * 1.1);
                        case 2:
                            return (damage * 1.2);
                        case 3:
                            return (damage * 1.3);
                        case 4:
                            return (damage * 1.4);
                        case 5:
                            return (damage * 1.5);
                    }
                    break;
                }
            case Player_Board.e_color.GREEN:
                {
                    switch (level)
                    {
                        case 1:
                            return (damage);
                        case 2:
                            return (damage);
                        case 3:
                            return (damage);
                        case 4:
                            return (damage);
                        case 5:
                            return (damage);
                    }
                    break;
                }
            case Player_Board.e_color.RED:
                {
                    switch (level)
                    {
                        case 1:
                            return (damage * 1.25);
                        case 2:
                            return (damage * 1.5);
                        case 3:
                            return (damage * 1.75);
                        case 4:
                            return (damage * 2);
                        case 5:
                            return (damage * 3);
                    }
                    break;
                }
        }
        return (damage);
    }

    double sniperTower(Player_Board.e_color color, int level)
    {
        switch (color)
        {
            case Player_Board.e_color.BLUE:
                {
                    switch (level)
                    {
                        case 1:
                            return (damage * 2.1);
                        case 2:
                            return (damage * 2.2);
                        case 3:
                            return (damage * 2.3);
                        case 4:
                            return (damage * 2.4);
                        case 5:
                            return (damage * 2.5);
                    }
                    break;
                }
            case Player_Board.e_color.GREEN:
                {
                    switch (level)
                    {
                        case 1:
                            return (damage);
                        case 2:
                            return (damage);
                        case 3:
                            return (damage);
                        case 4:
                            return (damage);
                        case 5:
                            return (damage);
                    }
                    break;
                }
            case Player_Board.e_color.RED:
                {
                    switch (level)
                    {
                        case 1:
                            return (damage * 2.25);
                        case 2:
                            return (damage * 2.5);
                        case 3:
                            return (damage * 2.75);
                        case 4:
                            return (damage * 3);
                        case 5:
                            return (damage * 4);
                    }
                    break;
                }
        }
        return (damage * 2);
    }

    double gatlingTower(Player_Board.e_color color, int level)
    {
        switch (color)
        {
            case Player_Board.e_color.BLUE:
                {
                    switch (level)
                    {
                        case 1:
                            return (damage * 0.45);
                        case 2:
                            return (damage * 0.5);
                        case 3:
                            return (damage * 0.55);
                        case 4:
                            return (damage * 0.6);
                        case 5:
                            return (damage * 0.65);
                    }
                    break;
                }
            case Player_Board.e_color.GREEN:
                {
                    switch (level)
                    {
                        case 1:
                            return (damage);
                        case 2:
                            return (damage);
                        case 3:
                            return (damage);
                        case 4:
                            return (damage);
                        case 5:
                            return (damage);
                    }
                    break;
                }
            case Player_Board.e_color.RED:
                {
                    switch (level)
                    {
                        case 1:
                            return (damage * 0.55);
                        case 2:
                            return (damage * 0.7);
                        case 3:
                            return (damage * 0.85);
                        case 4:
                            return (damage * 1);
                        case 5:
                            return (damage * 1.25);
                    }
                    break;
                }
        }
        return (damage * 0.4);
    }
}
