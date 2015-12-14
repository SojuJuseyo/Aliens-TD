using UnityEngine;
using System.Collections;

public class UpgradeMonsters : MonoBehaviour
{
    //Stats
    public int health;
    public int speed;
    public int armor;
    //Percentages
    public int slowResist;
    public int poisonResist;
    //Money money money
    public int cost;
    public int incomeIncrease;

    public struct Stats
    {
        //Stats
        public float health;
        public float speed;
        public double armor;
        //Percentages
        public float slowResist;
        public float poisonResist;
        //Money money money
        public int cost;
        public int incomeIncrease;
    }

	public Stats getNewStats(MonstersSelection.e_enemy type, int level)
    {
        Stats newStats;

        switch (type)
        {
			case MonstersSelection.e_enemy.MONSTER1:
                newStats = monster1(level);
                break;
			case MonstersSelection.e_enemy.MONSTER2:
                newStats = monster2(level);
                break;
			case MonstersSelection.e_enemy.MONSTER3:
                newStats = monster3(level);
                break;
			case MonstersSelection.e_enemy.MONSTER4:
                newStats = monster4(level);
                break;
			case MonstersSelection.e_enemy.MONSTER5:
                newStats = monster5(level);
                break;
			case MonstersSelection.e_enemy.MONSTER6:
                newStats = monster6(level);
                break;
			case MonstersSelection.e_enemy.MONSTER7:
                newStats = monster7(level);
                break;
			case MonstersSelection.e_enemy.MONSTER8:
                newStats = monster8(level);
                break;
			case MonstersSelection.e_enemy.MONSTER9:
                newStats = monster9(level);
                break;
            default:
                newStats.health = health;
                newStats.speed = speed;
                newStats.armor = armor;
                newStats.slowResist = slowResist;
                newStats.poisonResist = poisonResist;
                newStats.cost = cost;
                newStats.incomeIncrease = incomeIncrease;
                break;
        }
        return (newStats);
    }

    public Stats monster1(int level)
    {
        Stats newStats = new Stats();

        switch (level)
        {
            case 1:
                newStats.health = health * 1;
                newStats.speed = speed * 1;
                newStats.armor = armor + (armor * 0 / 100);
                newStats.slowResist = slowResist + (slowResist * 0 / 100);
                newStats.poisonResist = poisonResist + (poisonResist * 0 / 100);
                newStats.cost = cost * 1;
                newStats.incomeIncrease = incomeIncrease * 1;
                break;
            case 2:
                newStats.health = health * 2;
                newStats.speed = (float)(speed * 1.25);
                newStats.armor = armor + (armor * 5 / 100);
                newStats.slowResist = slowResist + (slowResist * 5 / 100);
                newStats.poisonResist = poisonResist + (poisonResist * 5 / 100);
                newStats.cost = cost * 2;
                newStats.incomeIncrease = incomeIncrease * 2;
                break;
            case 3:
                newStats.health = health * 4;
                newStats.speed = (float)(speed * 1.5);
                newStats.armor = armor + (armor * 10 / 100);
                newStats.slowResist = slowResist + (slowResist * 10 / 100);
                newStats.poisonResist = poisonResist + (poisonResist * 10 / 100);
                newStats.cost = cost * 4;
                newStats.incomeIncrease = incomeIncrease * 4;
                break;
        }
        return (newStats);
    }

    public Stats monster2(int level)
    {
        Stats newStats = new Stats();

        switch (level)
        {
            case 1:
                newStats.health = health * 2;
                newStats.speed = speed * 1;
                newStats.armor = armor + (armor * 0 / 100);
                newStats.slowResist = slowResist + (slowResist * 0 / 100);
                newStats.poisonResist = poisonResist + (poisonResist * 0 / 100);
                newStats.cost = cost * 2;
                newStats.incomeIncrease = incomeIncrease * 2;
                break;
            case 2:
                newStats.health = health * 4;
                newStats.speed = (float)(speed * 1.25);
                newStats.armor = armor + (armor * 10 / 100);
                newStats.slowResist = slowResist + (slowResist * 5 / 100);
                newStats.poisonResist = poisonResist + (poisonResist * 5 / 100);
                newStats.cost = cost * 4;
                newStats.incomeIncrease = incomeIncrease * 4;
                break;
            case 3:
                newStats.health = health * 8;
                newStats.speed = (float)(speed * 1.5);
                newStats.armor = armor + (armor * 15 / 100);
                newStats.slowResist = slowResist + (slowResist * 10 / 100);
                newStats.poisonResist = poisonResist + (poisonResist * 10 / 100);
                newStats.cost = cost * 8;
                newStats.incomeIncrease = incomeIncrease * 8;
                break;
        }
        return (newStats);
    }

    public Stats monster3(int level)
    {
        Stats newStats = new Stats();

        switch (level)
        {
            case 1:
                newStats.health = health * 2;
                newStats.speed = (float)(speed * 1.25);
                newStats.armor = armor + (armor * 10 / 100);
                newStats.slowResist = slowResist + (slowResist * 0 / 100);
                newStats.poisonResist = poisonResist + (poisonResist * 0 / 100);
                newStats.cost = cost * 4;
                newStats.incomeIncrease = incomeIncrease * 4;
                break;
            case 2:
                newStats.health = health * 4;
                newStats.speed = (float)(speed * 1.5);
                newStats.armor = armor + (armor * 20 / 100);
                newStats.slowResist = slowResist + (slowResist * 5 / 100);
                newStats.poisonResist = poisonResist + (poisonResist * 5 / 100);
                newStats.cost = cost * 8;
                newStats.incomeIncrease = incomeIncrease * 8;
                break;
            case 3:
                newStats.health = health * 8;
                newStats.speed = (float)(speed * 1.75);
                newStats.armor = armor + (armor * 30 / 100);
                newStats.slowResist = slowResist + (slowResist * 10 / 100);
                newStats.poisonResist = poisonResist + (poisonResist * 10 / 100);
                newStats.cost = cost * 16;
                newStats.incomeIncrease = incomeIncrease * 16;
                break;
        }
        return (newStats);
    }

    public Stats monster4(int level)
    {
        Stats newStats = new Stats();

        switch (level)
        {
            case 1:
                newStats.health = (float)(health * 2.5);
                newStats.speed = (float)(speed * 0.75);
                newStats.armor = armor + (armor * 25 / 100);
                newStats.slowResist = slowResist + (slowResist * 25 / 100);
                newStats.poisonResist = poisonResist + (poisonResist * -10 / 100);
                newStats.cost = cost * 6;
                newStats.incomeIncrease = incomeIncrease * 6;
                break;
            case 2:
                newStats.health = health * 5;
                newStats.speed = (float)(speed * 0.75);
                newStats.armor = armor + (armor * 30 / 100);
                newStats.slowResist = slowResist + (slowResist * 30 / 100);
                newStats.poisonResist = poisonResist + (poisonResist * -5 / 100);
                newStats.cost = cost * 12;
                newStats.incomeIncrease = incomeIncrease * 12;
                break;
            case 3:
                newStats.health = health * 10;
                newStats.speed = (float)(speed * 0.75);
                newStats.armor = armor + (armor * 35 / 100);
                newStats.slowResist = slowResist + (slowResist * 35 / 100);
                newStats.poisonResist = poisonResist + (poisonResist * -5 / 100);
                newStats.cost = cost * 24;
                newStats.incomeIncrease = incomeIncrease * 24;
                break;
        }
        return (newStats);
    }

    public Stats monster5(int level)
    {
        //AERIAL
        Stats newStats = new Stats();

        switch (level)
        {
            case 1:
                newStats.health = (float)(health * 1.5);
                newStats.speed = (float)(speed * 1.5);
                newStats.armor = armor + (armor * 0 / 100);
                newStats.slowResist = slowResist + (slowResist * -25 / 100);
                newStats.poisonResist = poisonResist + (poisonResist * 50 / 100);
                newStats.cost = cost * 8;
                newStats.incomeIncrease = incomeIncrease * 8;
                break;
            case 2:
                newStats.health = health * 3;
                newStats.speed = (float)(speed * 1.75);
                newStats.armor = armor + (armor * 0 / 100);
                newStats.slowResist = slowResist + (slowResist * -20 / 100);
                newStats.poisonResist = poisonResist + (poisonResist * 60 / 100);
                newStats.cost = cost * 16;
                newStats.incomeIncrease = incomeIncrease * 16;
                break;
            case 3:
                newStats.health = health * 6;
                newStats.speed = speed * 2;
                newStats.armor = armor + (armor * 0 / 100);
                newStats.slowResist = slowResist + (slowResist * -10 / 100);
                newStats.poisonResist = poisonResist + (poisonResist * 75 / 100);
                newStats.cost = cost * 32;
                newStats.incomeIncrease = incomeIncrease * 32;
                break;
        }
        return (newStats);
    }

    public Stats monster6(int level)
    {
        Stats newStats = new Stats();

        switch (level)
        {
            case 1:
                newStats.health = health * 2;
                newStats.speed = speed * 4;
                newStats.armor = armor + (armor * -50 / 100);
                newStats.slowResist = slowResist + (slowResist * -25 / 100);
                newStats.poisonResist = poisonResist + (poisonResist * -25 / 100);
                newStats.cost = cost * 12;
                newStats.incomeIncrease = incomeIncrease * 12;
                break;
            case 2:
                newStats.health = health * 4;
                newStats.speed = (float)(speed * 4.5);
                newStats.armor = armor + (armor * -45 / 100);
                newStats.slowResist = slowResist + (slowResist * -20 / 100);
                newStats.poisonResist = poisonResist + (poisonResist * -20 / 100);
                newStats.cost = cost * 24;
                newStats.incomeIncrease = incomeIncrease * 24;
                break;
            case 3:
                newStats.health = health * 8;
                newStats.speed = speed * 5;
                newStats.armor = armor + (armor * -45 / 100);
                newStats.slowResist = slowResist + (slowResist * -20 / 100);
                newStats.poisonResist = poisonResist + (poisonResist * -20 / 100);
                newStats.cost = cost * 48;
                newStats.incomeIncrease = incomeIncrease * 48;
                break;
        }
        return (newStats);
    }

    public Stats monster7(int level)
    {
        Stats newStats = new Stats();

        switch (level)
        {
            case 1:
                newStats.health = (float)(health * 3.5);
                newStats.speed = (float)(speed * 1.5);
                newStats.armor = armor + (armor * 15 / 100);
                newStats.slowResist = slowResist + (slowResist * 0 / 100);
                newStats.poisonResist = poisonResist + (poisonResist * 50 / 100);
                newStats.cost = cost * 20;
                newStats.incomeIncrease = incomeIncrease * 20;
                break;
            case 2:
                newStats.health = (float)(health * 6.5);
                newStats.speed = (float)(speed * 1.75);
                newStats.armor = armor + (armor * 25 / 100);
                newStats.slowResist = slowResist + (slowResist * 0 / 100);
                newStats.poisonResist = poisonResist + (poisonResist * 65 / 100);
                newStats.cost = cost * 40;
                newStats.incomeIncrease = incomeIncrease * 40;
                break;
            case 3:
                newStats.health = (float)(health * 12.5);
                newStats.speed = speed * 2;
                newStats.armor = armor + (armor * 40 / 100);
                newStats.slowResist = slowResist + (slowResist * 0 / 100);
                newStats.poisonResist = poisonResist + (poisonResist * 80 / 100);
                newStats.cost = cost * 80;
                newStats.incomeIncrease = incomeIncrease * 80;
                break;
        }
        return (newStats);
    }

    public Stats monster8(int level)
    {
        Stats newStats = new Stats();

        switch (level)
        {
            case 1:
                newStats.health = health * 4;
                newStats.speed = speed * 1;
                newStats.armor = armor + (armor * 10 / 100);
                newStats.slowResist = slowResist + (slowResist * 75 / 100);
                newStats.poisonResist = poisonResist + (poisonResist * 0 / 100);
                newStats.cost = cost * 30;
                newStats.incomeIncrease = incomeIncrease * 30;
                break;
            case 2:
                newStats.health = (float)(health * 7.5);
                newStats.speed = (float)(speed * 1.2);
                newStats.armor = armor + (armor * 20 / 100);
                newStats.slowResist = slowResist + (slowResist * 80 / 100);
                newStats.poisonResist = poisonResist + (poisonResist * 10 / 100);
                newStats.cost = cost * 60;
                newStats.incomeIncrease = incomeIncrease * 60;
                break;
            case 3:
                newStats.health = health * 15;
                newStats.speed = (float)(speed * 1.5);
                newStats.armor = armor + (armor * 30 / 100);
                newStats.slowResist = slowResist + (slowResist * 85 / 100);
                newStats.poisonResist = poisonResist + (poisonResist * 20 / 100);
                newStats.cost = cost * 120;
                newStats.incomeIncrease = incomeIncrease * 120;
                break;
        }
        return (newStats);
    }

    public Stats monster9(int level)
    {
        Stats newStats = new Stats();

        switch (level)
        {
            case 1:
                newStats.health = health * 10;
                newStats.speed = (float)(speed * 0.25);
                newStats.armor = armor + (armor * 30 / 100);
                newStats.slowResist = slowResist + (slowResist * 30 / 100);
                newStats.poisonResist = poisonResist + (poisonResist * 30 / 100);
                newStats.cost = cost * 50;
                newStats.incomeIncrease = incomeIncrease * 50;
                break;
            case 2:
                newStats.health = health * 20;
                newStats.speed = (float)(speed * 0.30);
                newStats.armor = armor + (armor * 40 / 100);
                newStats.slowResist = slowResist + (slowResist * 40 / 100);
                newStats.poisonResist = poisonResist + (poisonResist * 40 / 100);
                newStats.cost = cost * 100;
                newStats.incomeIncrease = incomeIncrease * 100;
                break;
            case 3:
                newStats.health = health * 50;
                newStats.speed = (float)(speed * 0.35);
                newStats.armor = armor + (armor * 50 / 100);
                newStats.slowResist = slowResist + (slowResist * 50 / 100);
                newStats.poisonResist = poisonResist + (poisonResist * 50 / 100);
                newStats.cost = cost * 200;
                newStats.incomeIncrease = incomeIncrease * 200;
                break;
        }
        return (newStats);
    }
}
