using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance{get{ return instance;}}

    [SerializeField] private int endGenerateCount = 3;
    private int[] generateCount = new int[4]; // 각 방향 생성 개수, 위쪽 오른쪽 아래쪽 왼쪽 순서

    private int initMoney = 100; // 초기 자금
    private int nowMoney = 0; // 현재 자금
    private int food = 0; // 현재 식량
    private int leftFood = 0; // 남은 식량

    private bool isGameStart = false;

    private List<YachtType> yachtHaveList;
    private List<FishingRodType> fishingRodHaveList;
    private List<Crewman> crewmenHaveList; // 배에 안탄 선원, 배에 타고 있는 선원 포함

    private UIManager uiManager;
    private Calendar calendar;
    private FishingRod nowFishingRod;
    private Sail sail;
    private ShipBody shipBody;
    
    private float inGameStandardTime = 3; // 현실 시간 기준 게임 내의 한시간
    private float sumDeltaTime;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
        calendar = new Calendar();
    }

    // Start is called before the first frame update
    void Start()
    {
        nowMoney = initMoney;

        yachtHaveList = new List<YachtType>();
        fishingRodHaveList = new List<FishingRodType>();
        crewmenHaveList = new List<Crewman>();

        setNowFishingRod(0);

        CrewmanManager.Instance.makeCaptain();
        CrewmanManager.Instance.makeEngineer();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameStart)
        {
            sumDeltaTime += Time.deltaTime;
            if (sumDeltaTime > inGameStandardTime)
            {
                sumDeltaTime -= inGameStandardTime;

                bool _isNextDay = calendar.nextTime();

                uiManager.refreshCalendar(calendar);
                CrewmanManager.Instance.progressCrew();

                if (_isNextDay)
                {
                    CrewmanManager.Instance.hungryCrewProcess();
                }
            }
        }
    }

    public void gameStart(UIManager uiManager)
    {
        this.uiManager = uiManager;
        isGameStart = true;
        uiManager.refreshCalendar(calendar); // 초기 값 보여주기
    }

    public void endGame(GameEnding gameEnding)
    {
        switch (gameEnding)
        {
            case GameEnding.ARRIVE:
                UIManager.Instance.endGame(gameEnding);
                break;
            case GameEnding.SHIPWRECK: // 난파엔딩
                UIManager.Instance.endGame(gameEnding);
                break;
            case GameEnding.PIRATE:
                UIManager.Instance.endGame(gameEnding);
                break;
            case GameEnding.HUNGRY:
                UIManager.Instance.endGame(gameEnding);
                break;
        }
    }

    public int getGenerateCount(int index)
    {
        return generateCount[index];
    }

    public void addGenerateCount(int index, int value)
    {
        generateCount[index] += value;
    }

    public bool isGenerateCountEnd()
    {
        if (generateCount[0] > endGenerateCount || generateCount[1] > endGenerateCount ||
                generateCount[2] > endGenerateCount || generateCount[3] > endGenerateCount)
            return true;
        return false;
    }

    public void setNowFishingRod(int level)
    {
        switch (level)
        {
            case 0:
                nowFishingRod = new FishingRodA();
                break;
        }
    }

    public FishingRod getNowFishingRod()
    {
        return nowFishingRod;
    }

    public int InitMoney {
        get { return initMoney; }
        set { initMoney = value; }
    }

    public int NowMoney {
        get { return nowMoney; }
        set { nowMoney = value; }
    }

    public int Food {
        get { return food; }
        set { food = value; }
    }

    public int LeftFood {
        get { return leftFood; }
        set { leftFood = value; }
    }

    public Calendar Calendar {
        get { return calendar; }
    }

    public Sail Sail {
        get { return sail; }
        set { sail = value; }
    }

    public ShipBody ShipBody {
        get { return shipBody; }
        set { shipBody = value; }
    }

    public void addYachtHaveList(YachtType yachtType)
    {
        yachtHaveList.Add(yachtType);
    }

    public bool isContainYacth(YachtType yachtType)
    {
        return yachtHaveList.Contains(yachtType);
    }

    public void addFishingRodHaveList(FishingRodType fishingRodType)
    {
        fishingRodHaveList.Add(fishingRodType);
    }

    public bool isContainFishingRod(FishingRodType fishingRodType)
    {
        return fishingRodHaveList.Contains(fishingRodType);
    }

    public void addCrewmanHaveList(Crewman crewman)
    {
        crewmenHaveList.Add(crewman);
    }

    public Crewman getFromCrewmanHaveList(int index)
    {
        if(crewmenHaveList.Count <= index)
        {
            return null;
        }
        return crewmenHaveList[index];
    }

    public bool removeCrewmanHaveList(Crewman crewman)
    {
        return crewmenHaveList.Remove(crewman);
    }

    public bool removeAtCrewmanHaveList(int index)
    {
        if (crewmenHaveList.Count <= index)
        {
            return false;
        }
        crewmenHaveList.RemoveAt(index);
        return true;
    }

    public void repairSail()
    {
        sail.repairModule(30);
    }

    public void repairBody()
    {
        shipBody.repairModule(30);
    }
}
