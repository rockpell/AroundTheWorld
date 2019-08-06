using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance{get{ return instance;}}

    [SerializeField] private int endGenerateCount = 3;
    //private int[] generateCount = new int[4]; // 각 방향 생성 개수, 위쪽 오른쪽 아래쪽 왼쪽 순서
    private int generatedCount;

    private int initMoney = 100; // 초기 자금
    private int nowMoney = 0; // 현재 자금
    private int food = 0; // 현재 식량
    private int leftFood = 0; // 남은 식량

    private bool isGameStart = false;
    private bool isGameEnd = false;

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

    private int reward = 0;

    private GameEnding nowGameEnding = GameEnding.NONE;

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
        yachtHaveList = new List<YachtType>();
        fishingRodHaveList = new List<FishingRodType>();
        crewmenHaveList = new List<Crewman>();
    }

    // Start is called before the first frame update
    void Start()
    {
        nowMoney = initMoney;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (isGameStart)
        {
            sumDeltaTime += Time.deltaTime;
            if (sumDeltaTime > inGameStandardTime)
            {
                sumDeltaTime -= inGameStandardTime;

                bool _isNextDay = calendar.nextTime();

                uiManager.refreshCalendar(calendar);
                uiManager.refreshDayLightUI();
                CrewmanManager.Instance.progressCrew();

                if (_isNextDay)
                {
                    CrewmanManager.Instance.hungryCrewProcess();
                }
            }
        }

        if (isGameStart && !isGameEnd)
        {
            checkEndGame();
        }
    }

    public void gameStart(UIManager uiManager)
    {
        this.uiManager = uiManager;
        isGameStart = true;
        uiManager.refreshCalendar(calendar); // 초기 값 보여주기
    }

    private void checkEndGame()
    {
        if(shipBody.Durability == 0)
        {
            nowGameEnding = GameEnding.SHIPWRECK;
        }

        endGame(nowGameEnding);
    }

    public void endGame(GameEnding gameEnding)
    {
        switch (gameEnding)
        {
            case GameEnding.ARRIVE:
                reward = rewardMoney();
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
            case GameEnding.DEAD:
                UIManager.Instance.endGame(gameEnding);
                break;
        }
        if(gameEnding != GameEnding.NONE)
        {
            isGameEnd = true;
            isGameStart = false;
            nowGameEnding = GameEnding.NONE;
            Time.timeScale = 0; // 나중에 다시 1로 바꿔줘야함
        }
    }

    public void addGenerateCount()
    {
        ++generatedCount;
    }

    public bool isGenerateCountEnd()
    {
        if(generatedCount != 0 && (generatedCount % endGenerateCount == 0))
            return true;
        return false;
    }

    private void initGenerateCount()
    {
        generatedCount = 0;
    }

    public void setNowFishingRod(int level)
    {
        switch (level)
        {
            case 0:
                nowFishingRod = new FishingRodA();
                break;
            case 1:
                nowFishingRod = new FishingRodB();
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
        set 
        {
            food = value;
            if (food < 0)
                food = 0;
        }
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

    public int Reward {
        get { return reward; }
    }

    public GameEnding NowGameEnding {
        get { return nowGameEnding; }
        set { nowGameEnding = value; }
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

    public bool removeFishingRodHaveList(FishingRodType fishingRodType)
    {
        return fishingRodHaveList.Remove(fishingRodType);
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

    public void recoveryTimeScale() // 게임 다시 시작시 UIManager 쪽에서 호출될 함수
    {
        Time.timeScale = 1;
        isGameEnd = false;
        initGenerateCount();
    }

    private int rewardMoney()
    {
        int _result = generatedCount;
        initMoney += _result;
        return _result;
    }
}
