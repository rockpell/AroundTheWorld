using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CrewmanManager : MonoBehaviour
{
    private static CrewmanManager instance;
    public static  CrewmanManager Instance { get { return instance; } }
    private GameManager gamemanager;
    private Calendar calendar;
    private int howmany;



    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
    }

    List<Crewman> crewmanList = new List<Crewman>();

    // Start is called before the first frame update
    void Start()
    {
        calendar = GameManager.Instance.Calendar;
    }

    // Update is called once per frame
    void Update()   
    {
        
    }

    public bool makeCaptain()//선장 생성
    {
        if (crewmanList.Count == 0)
        {
            crewmanList.Add(new Captain());
            crewmanList[crewmanList.Count-1].setindex(crewmanList.Count-1);
            return true;
        }
        return false;
    }

    public bool makeEngineer()//엔지니어 생성 단,선원이 4명 이하일때
    {
        if(crewmanList.Count < 4)
        {
            crewmanList.Add(new Engineer());
            crewmanList[crewmanList.Count-1].setindex(crewmanList.Count-1);
            return true;
        }
        return false;
        
    }
    public bool makeMate()//항해사 생성 단,선원이 4명 이하일때
    {
        if (crewmanList.Count < 4)
        {
            crewmanList.Add(new Mate());
            crewmanList[crewmanList.Count-1].setindex(crewmanList.Count-1);
            return true;
        }
        return false;

    }
    public bool makeAngler()//강태공 생성 단,선원이 4명 이하일때
    {
        if (crewmanList.Count < 4)
        {
            crewmanList.Add(new Angler());
            crewmanList[crewmanList.Count-1].setindex(crewmanList.Count-1);
            return true;
        }
        return false;

    }
    
    public Crewman getCrewman(int index)//index입력하면 그값에 맞는 선원 출력
    {
        if(index < 4)
        {
            return crewmanList[index];
        }
        return null;
    }

    public void dieCrewman(Crewman crewman)//해당되는 crewman삭제
    {
        crewmanList.RemoveAt(crewman.getindex());
        for(int i = 0; i< crewmanList.Count; i++)
        {
            crewmanList[i].setindex(i);
        }

        UIManager.Instance.initUI(); // 선원 변경 되었기때문에 UI 초기화 필요
    }

    //public Crewman whoDrive()// 선원중 누가 항해를 하는지
    //{
    //    for(int i = 0; i < crewmanList.Count; i++)
    //    {
    //        crewmanList[i].getDrive();
    //        if (crewmanList[i].getDrive())
    //        {
    //            return crewmanList[i];
    //        }
    //    }
        
    //    return null;
    //}

    public Crewman whoDrive()
    {
        for (int i = 0; i < crewmanList.Count; i++)
        {
            if (crewmanList[i].getActingType() == Acting.DRIVE)
                return crewmanList[i];
        }

        return null;
    }

    //public bool crewDrive(Crewman crewman)//항해시키기
    //{
    //    if (whoDrive() == null && actingCheck(crewman))
    //    {
    //        crewman.setDrive(true);
    //        crewman.setActingType(Acting.DRIVE);
    //        return true;
    //    }
    //    else if(whoDrive() != null && actingCheck(crewman))
    //    {
    //        crewDriveStop(whoDrive());
    //        crewman.setDrive(true);
    //        crewman.setActingType(Acting.DRIVE);
    //        return true;
    //    }
    //    return false;
    //}

    public void crewDriveStop(Crewman crewman)//항해그만두기
    {
        if (crewman.getDrive())
        {
            crewman.setDrive(false);
        }
    }

    public bool crewDrive(Crewman crewman)
    {
        for(int i = 0; i < crewmanList.Count; i++)
        {
            if (crewmanList[i].getActingType() == Acting.DRIVE)
                crewmanList[i].setActingType(Acting.NOTHING);
        }

        crewman.setActingType(Acting.DRIVE);
        GameManager.Instance.Sail.DriveCrew = crewman;

        return true;
    }

    public bool crewmanSleep(Crewman crewman)//재우기
    {
        int behavior;
        int time = -1;
        if (actingCheck(crewman))
        {
            notifyStealDrive(crewman);

            if (7 <= calendar.time && calendar.time < 19)
            {
                crewman.setSleep(true);
                crewman.setActingType(Acting.SLEEP);
                behavior = crewman.getbehavior() + 5;
                if(behavior > 10)
                {
                    behavior = 10;
                }
                crewman.setbehavior(behavior);
                time = calendar.time + 4;
                if(time >= 24)
                {
                    time -= 24;
                }
            }
            else
            {
                crewman.setSleep(true);
                crewman.setActingType(Acting.SLEEP);
                crewman.setbehavior(10);
                time = calendar.time + 6;
                if (time >= 24)
                {
                    time -= 24;
                }
            }
            crewman.settime(time);
            return true;

        }
        return false;
    }

    public bool crewmanEat(Crewman crewman)//식사
    {
        if (actingCheck(crewman))
        {
            notifyStealDrive(crewman);

            if (GameManager.Instance.Food > 0)
            {
                if (crewman.getfull() < 4)
                {
                    GameManager.Instance.Food -= 1;
                    crewman.setfull(crewman.getfull() + 1);
                    return true;
                }
                UIManager.Instance.showMessage("배가 부릅니다.");
            }
            else
            {
                UIManager.Instance.showMessage("식량이 부족합니다.");
            }

            return false;
        }
        return false;

    }

    public bool crewmanRepair(Crewman crewman)// 수리하기
    {
        if (actingCheck(crewman))
        {
            //if (crewman.getbehavior() >= 2)
            //{
            //    if (crewman.gettype() == 1)
            //    {
            //        crewman.setbehavior(crewman.getbehavior() - 2);
            //        return true;
            //    }
            //    else if (crewman.getbehavior() >= 3)
            //    {
            //        crewman.setbehavior(crewman.getbehavior() - 3);
            //        return true;
            //    }
            //}

            notifyStealDrive(crewman);

            if (crewman.gettype() == 1)
            {
                if (crewman.getbehavior() >= 2)
                {
                    crewman.setbehavior(crewman.getbehavior() - 2);
                    UIManager.Instance.showMessage("배가 수리되었습니다.");
                    return true;
                }
            }
            else if (crewman.getbehavior() >= 3)
            {
                crewman.setbehavior(crewman.getbehavior() - 3);
                UIManager.Instance.showMessage("배가 수리되었습니다.");
                return true;
            }
            UIManager.Instance.showMessage("행동력이 부족합니다.");
        }
        return false;
    }

    public bool crewmanFishing(Crewman crewman)//낚시하기
    {
        int time = -1;
        if (actingCheck(crewman))
        {
            notifyStealDrive(crewman);

            if (crewman.getbehavior() >= 1)
            {
                if (GameManager.Instance.getNowFishingRod().Durability > 0)
                {
                    crewman.setFishing(true);
                    crewman.setActingType(Acting.FISHING);
                    crewman.setbehavior(crewman.getbehavior() - 1);

                    GameManager.Instance.getNowFishingRod().Durability -= 2;

                    time = calendar.time + 1;

                    if (time >= 24)
                    {
                        time -= 24;
                    }

                    crewman.settime(time);
                    return true;
                }
                UIManager.Instance.showMessage("낚시대의 내구도가 부족합니다.");
            }
            UIManager.Instance.showMessage("행동력이 부족합니다.");
        }

        return false;
    }

    public void crewmanWakeUpCount(Crewman crewman)//시간이 되면 깨우기
    {
        if(crewman.gettime() == calendar.time)
        {
            crewman.setSleep(false);
            crewman.setActingType(Acting.NOTHING);
        }
    }

    public bool crewmanFishingCount(Crewman crewman)//시간이 되면 낚시 그만두기 true면 그만 false면 계속
    {
        if (crewman.gettime() == calendar.time)
        {
            crewman.setFishing(false);
            crewman.setActingType(Acting.NOTHING);

            if (crewmanFishingYes(crewman, GameManager.Instance.getNowFishingRod()))
            {
                GameManager.Instance.Food += 2;
                UIManager.Instance.showMessage("낚시 성공! \n식량 2를 획득하였습니다.");
                return true;
            }
            else
            {
                UIManager.Instance.showMessage("낚시 실패!");
                return false;
            }
        }
        return false;
        
    }

    public bool crewmanFishingYes(Crewman crewman, FishingRod fishingRod)//낚시 성공 실패
    {
        if (Random.Range(0.0f, 100.0f) < (crewman.getfishing() + fishingRod.FishingProbability))
        {
            return true;
        }
        else
            return false;
    }

    public bool actingCheck(Crewman crewman)//행동을 하는지, 행동을 하면 false, 안하면 true
    {
        //if( Acting.NOTHING == crewman.whatActing())
        //{
        //    return true;
        //}

        if (crewman.getActingType() == Acting.NOTHING || crewman.getActingType() == Acting.DRIVE)
            return true;

        UIManager.Instance.showMessage("다른 행동을 하는중입니다.");
        return false;
    }

    public int howManyCrewman()//선원의 수
    {
        return crewmanList.Count;
    }

    public void progressCrew()
    {
        for(int i = 0; i < crewmanList.Count; i++)
        {
            if (crewmanList[i].getActingType() == Acting.SLEEP)
            {
                crewmanWakeUpCount(crewmanList[i]);
            }
            else if(crewmanList[i].getActingType() == Acting.FISHING)
            {
                crewmanFishingCount(crewmanList[i]);
            }
        }
    }

    public void hungryCrewProcess()
    {
        for (int i = 0; i < crewmanList.Count; i++)
        {
            if(crewmanList[i].getfull() > 0)
            {
                crewmanList[i].setfull(crewmanList[i].getfull() - 1);
            }
            else
            {
                dieCrewman(crewmanList[i]);
            }
        }
    }

    public void notifyStealDrive(Crewman crewman)
    {
        crewman.setActingType(Acting.NOTHING);
        GameManager.Instance.Sail.DriveCrew = null;
    }
}
