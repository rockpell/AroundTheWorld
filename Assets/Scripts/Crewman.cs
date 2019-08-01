using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class CrewmanAbilityWork
{
    private int type;
    private int behavior;
    private int fishing;
    private int repair;
    private int sailing_speed;
    private int food;
    private bool Fishing;
    private bool Repair;
    private bool Drive;
    private bool Sleep;
    private bool Eat;


    public CrewmanAbilityWork()
    {
        type = 0;
        behavior = 10;
        fishing = 10;
        repair = 3;
        sailing_speed = 0;
        food = 4;
        Fishing = false;
        Repair = false;
        Drive = false;
        Sleep = false;
        Eat = false;
    }

    public void settype(int v)
    {
        type = v;
    }
    public int gettype()
    {
        return type;
    }


    public void setAblity(int v1, int v2, int v3, int v4)
    {
        behavior = v1;
        fishing = v2;
        repair = v3;
        sailing_speed = v4;
    }
    public int getbehavior()
    {
        return behavior;
    }
    public int getfishing()
    {
        return fishing;
    }
    public int getrepair()
    {
        return repair;
    }
    public int getsailing_speed()
    {
        return sailing_speed;
    }
    public void setfood(int v)
    {
        food = v;
    }
    public int getfood()
    {
        return food;
    }
    public void setWork(bool v1, bool v2, bool v3, bool v4)
    {
        Fishing = v1;
        Repair = v2;
        Sleep = v3;
        Eat = v4;
    }
    public bool getFishig()
    {
        return Fishing;
    }
    public bool getRepair()
    {
        return Repair;
    }
    public bool getSleep()
    {
        return Sleep;
    }
    public bool getEat()
    {
        return Eat;
    }
    public void setDrive(bool v)
    {
        Drive = v;
    }
    public bool getDrive()
    {
        return Drive;
    }
}

class Captain : CrewmanAbilityWork
{
    public Captain() : base( )
    {
        setAblity(10, 10, 3, 0);
    }
}

class Enginieer : CrewmanAbilityWork
{
    public Enginieer() : base()
    {
        setAblity(10, 10, 2, 0);
    }
}

class Mate : CrewmanAbilityWork
{
    public Mate() : base()
    {
        setAblity(10, 10, 3, 50);
    }
}

class Angler : CrewmanAbilityWork
{
    public Angler() : base()
    {
        setAblity(10, 20, 3, 0);
    }
}

public class Crewman : MonoBehaviour
{
    CrewmanAbilityWork[] crewman;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void makeCrewmanStorage()
    {
        crewman = new CrewmanAbilityWork[5];
        for(int i = 0; i< 5; i++)
        {
            crewman[i] = null;
        }
    }

    public bool makeCrewman(int type)
    {
        int i;
        for (i = 0; i < 5; i++)
        {
            if(crewman[i] == null)
            {
                break;
            }
        }
        if(i >= 5)
        {
            return false;
        }
        switch (type)
        {
            case 0:
                crewman[i].settype(0);
                crewman[i].setAblity(10, 10, 3, 0);
                break;
            case 1:
                crewman[i].settype(1);
                crewman[i].setAblity(10, 10, 2, 0);
                break;
            case 2:
                crewman[i].settype(2);
                crewman[i].setAblity(10, 10, 3, 50);
                break;
            case 3:
                crewman[i].settype(3);
                crewman[i].setAblity(10, 20, 3, 0);
                break;

        }
        return true;
    }
    

    public int? whoDrive()
    {
        for(int i = 0; i < 5; i++)
        {
            if (crewman[i].getDrive())
            {
            return i;

            }
            if (crewman[i] == null)
            {
                break;
            }
        }
        
        return null;
    }
}
