public class Crewman//선원의 능력과 행동을 가진 클래스
{
    private int type;//선원의 종류 ( 0 - 선장, 1 - 엔지니어, 2 - 항해사, 3 - 강태공)
    private int behavior;// 행동력
    private int fishing;//낚시능력
    private int repair;//수리할때 필요한 행동력
    private int sailing_speed;//항해속도
    private int full;//포만감
    private int index;//선원 순서
    private int time;//행동할때의 시간
    private Acting acting_type;//행동종류
    private bool Fishing;//낚시유무
    private bool Repair;//수리유무
    private bool Drive;//항해유무
    private bool Sleep;//잠유무
    private bool Eat;//식사유무


    public Crewman(int type)
    {
        this.type = type;
        behavior = 10;
        fishing = 10;
        repair = 3;
        sailing_speed = 0;
        full = 4;
        index = -1;
        time = -1;
        Fishing = false;
        Repair = false;
        Drive = false;
        Sleep = false;
        Eat = false;
        acting_type = Acting.NOTHING;
    }

    public int gettype()
    {
        return type;
    }

    public void setindex(int index)
    {
        this.index = index;
    }
    public int getindex()
    {
        return index;
    }


    public void setAblity(int behavior, int fishing, int repair, int sailing_speed)
    {
        this.behavior = behavior;
        this.fishing = fishing;
        this.repair = repair;
        this.sailing_speed = sailing_speed;
    }
    public void setbehavior(int behavior)
    {
        if (behavior <= 10&& behavior >= 0)
        {
            this.behavior = behavior;
        }
        
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
    public void setfull(int minus)
    {
        full = full - minus;
    }
    public int getfull()
    {
        return full;
    }
    public void setFishing(bool Fishing)
    {
        this.Fishing = Fishing;
    }
    public void setRepair(bool Repair)
    {
        this.Repair = Repair;
    }
    public void setSleep(bool Sleep)
    {
        this.Sleep = Sleep;
    }
    public void setEat(bool Eat)
    {
        this.Eat = Eat;
    }
    public void setDrive(bool Drive)
    {
        this.Drive = Drive;
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

    public bool getDrive()
    {
        return Drive;
    }
    public Acting whatActing()//선원이 무엇을 행동하는지?
    {
        acting_type = Acting.NOTHING;

        if (Fishing == true) // 낚시중
        {
            acting_type = Acting.FISHING;
        }
        else if (Repair == true) // 수리중
        {
            acting_type = Acting.REPAIR;
        }
        else if (Drive == true) // 항해중
        {
            acting_type = Acting.DRIVE;
        }
        else if (Sleep == true) // 숙면중
        {
            acting_type = Acting.SLEEP;
        }
        else if (Eat == true) // 먹는중
        {
            acting_type = Acting.EAT;
        }

        return acting_type;
    }

    public void settime(int time)
    {
        this.time = time;
    }
    public int gettime()
    {
        return time;
    }
}

public class Captain : Crewman//선장 클래스
{
    public Captain() : base(0)
    {
        setAblity(10, 0, 3, 0);
    }
}

public class Engineer : Crewman//엔지니어 클래스
{
    public Engineer() : base(1)
    {
        setAblity(10, 0, 2, 0);
    }
}

class Mate : Crewman//항해사 클래스
{
    public Mate() : base(2)
    {
        setAblity(10, 0, 3, 50);
    }
}

class Angler : Crewman//강태공 클래스
{
    public Angler() : base(3)
    {
        setAblity(10, 10, 3, 0);
    }
}