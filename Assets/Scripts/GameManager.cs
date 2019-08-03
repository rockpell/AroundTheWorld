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
    private int food = 0; // 식량

    private bool isGameStart = false;

    private UIManager uiManager;
        
    private Calendar calendar;

    private FishingRod nowFishingRod;
    // 낚시대
    // 요트
    // 선원

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
                calendar.nextTime();
                uiManager.refreshCalendar(calendar);
            }
        }
    }

    public void gameStart(UIManager uiManager)
    {
        this.uiManager = uiManager;
        isGameStart = true;
        uiManager.refreshCalendar(calendar); // 초기 값 보여주기
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

    public Calendar Calendar {
        get { return calendar; }
    }
}
