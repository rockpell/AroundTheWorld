using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CrewmanManager : MonoBehaviour
{
    private static CrewmanManager instance;
    public static  CrewmanManager Instance { get { return instance; } }
    private Calendar calendar;
    private int[] time;



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
        time = new int[4];
        for(int i = 0; i < 4; i++)
        {
            time[i] = -1;
        }
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
            crewmanList[crewmanList.Count].setindex(crewmanList.Count);
            return true;
        }
        return false;
    }

    public bool makeEngineer(int type)//엔지니어 생성 단,선원이 4명 이하일때
    {
        if(crewmanList.Count < 4)
        {
            crewmanList.Add(new Engineer());
            crewmanList[crewmanList.Count].setindex(crewmanList.Count);
            return true;
        }
        return false;
        
    }
    public bool makeMate(int type)//항해사 생성 단,선원이 4명 이하일때
    {
        if (crewmanList.Count < 4)
        {
            crewmanList.Add(new Mate());
            crewmanList[crewmanList.Count].setindex(crewmanList.Count);
            return true;
        }
        return false;

    }
    public bool makeAngler(int type)//강태공 생성 단,선원이 4명 이하일때
    {
        if (crewmanList.Count < 4)
        {
            crewmanList.Add(new Angler());
            crewmanList[crewmanList.Count].setindex(crewmanList.Count);
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
    }

    public Crewman whoDrive()// 선원중 누가 항해를 하는지
    {
        for(int i = 0; i < crewmanList.Count; i++)
        {
            crewmanList[i].getDrive();
            if (crewmanList[i].getDrive())
            {
                return crewmanList[i];
            }
        }
        
        return null;
    }

    public bool crewDrive(Crewman crewman)//항해시키기
    {
        if (whoDrive() == null && actingCheck(crewman))
        {
            crewman.setDrive(true);
            return true;
        }
        return false;
    }

    public void crewDriveStop(Crewman crewman)//항해그만두기
    {
        if (crewman.getDrive())
        {
            crewman.setDrive(false);
        }
    }

    public bool crewmanSleep(Crewman crewman, Calendar calendar)//재우기
    {
        int time = -1;
        if (actingCheck(crewman))
        {
            if(7 <= calendar.time && calendar.time < 19)
            {
                crewman.setDrive(true);
                time = calendar.time + 4;
                if(time >= 24)
                {
                    time -= 24;
                }
            }
            else
            {
                crewman.setDrive(true);
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

    public bool crewmanEat(Crewman crewman)//밥먹이기
    {
        if (actingCheck(crewman))
        {
            crewman.setEat(true);
            return true;
        }
        return false;

    }
    public bool crewmanFishing(Crewman crewman)//낚시하기
    {
        int time = -1;
        if (actingCheck(crewman))
        {
            crewman.setFishing(true);

            time = calendar.time + 1;

            if (time >= 24)
            {
                time -= 24;
            }

            crewman.settime(time);
            return true;

        }
        return false;
    }

    public bool crewmanRepair(Crewman crewman)// 수리하기
    {
        if (actingCheck(crewman))
        {
            crewman.setRepair(true);
            return true;
        }
        return false;
    }

    public bool actingCheck(Crewman crewman)//행동을 하는지, 행동을 하면 false, 안하면 true
    {
        if( Acting.NOTHING == crewman.whatActing())
        {
            return true;
        }
        return false;
    }
    /*
    public void crewmanWakeUpCount(Crewman crewman)//시간이 되면 깨우기
    {
        if(crewman.gettime() == calendar.time)
        {
            crewman.setSleep(false);
        }
    }
    */
    public void crewmanWkeUpCount(Crewman crewman)
    {
        int time = crewman.gettime();
        if (time > 0)
        {
            crewman.settime(time - 1);
        }
        else if (time == 0)
        {
            crewman.settime(time - 1);
            crewman.setSleep(false);
        }
    }
     

    public void crewmanCount(Crewman crewman)//시간이 되면 낚시 그만두기
    {
        if (crewman.gettime() == calendar.time)
        {
            crewman.setFishing(false);
        }
    }
}
