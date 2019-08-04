using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Canvas myCanvas;
    [SerializeField] private GameObject eventMenu;
    [SerializeField] private GameObject selectMenu;
    [SerializeField] private GameObject endingPanel;

    [SerializeField] private CrewUI[] crewUIs;
    [SerializeField] private CrewStatusUI[] crewStatusUIs;
    [SerializeField] private GameObject crewHighlight;
    [SerializeField] private EtcStatusUI etcStatusUI;
    [SerializeField] private SheepStatusUI sheepStatusUI;
    [SerializeField] private MessageUI messageUI;
    [SerializeField] private EventMenuUI eventMenuUI;

    [SerializeField] private Text calendarDate;
    [SerializeField] private Text calendarTime;
    [SerializeField] private Text calendarDDay;

    [SerializeField] private Sprite captineSprite; // 선장
    [SerializeField] private Sprite engineerSprite; // 엔지니어
    [SerializeField] private Sprite mateSprite; // 항해사
    [SerializeField] private Sprite anglerSprite; // 강태공

    private int selectCrewIndex = -1; // 선택된 선원 번호(왼쪽부터 0번)

    private static UIManager instance;
    public static UIManager Instance { get { return instance; } }

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(this.gameObject);
            return;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.gameStart(this);
        initUI();
    }

    void Update()
    {
        refreshUI();
    }

    public void outrangeClick()
    {
        hideSelectMenu();
    }

    public void showSelectMenu(int index)
    {
        Crewman _crew = CrewmanManager.Instance.getCrewman(index);
        
        if(_crew.getActingType() == Acting.NOTHING || _crew.getActingType() == Acting.DRIVE)
        {
            Vector2 _size = selectMenu.GetComponent<RectTransform>().sizeDelta;

            CanvasScaler scaler = myCanvas.GetComponentInParent<CanvasScaler>();
            selectMenu.GetComponent<RectTransform>().anchoredPosition
                = new Vector2(Input.mousePosition.x * scaler.referenceResolution.x / Screen.width - scaler.referenceResolution.x / 2 + _size.x / 2,
                Input.mousePosition.y * scaler.referenceResolution.y / Screen.height - scaler.referenceResolution.y / 2 + _size.y / 2);

            selectMenu.SetActive(true);
            showCrewHighlight(index);

            selectCrewIndex = index;
        }
    }

    private void hideSelectMenu()
    {
        selectMenu.SetActive(false);
        hideCrewHighlight();
    }

    private void showCrewHighlight(int index)
    {
        if(crewUIs[index] != null)
        {
            if(crewHighlight != null)
            {
                crewHighlight.transform.position = crewUIs[index].transform.position;
                crewHighlight.SetActive(true);
            }
        }
    }

    private void hideCrewHighlight()
    {
        if (crewHighlight != null)
        {
            crewHighlight.SetActive(false);
        }
    }

    public void fishingButton()
    {
        if (CrewmanManager.Instance.crewmanFishing(CrewmanManager.Instance.getCrewman(selectCrewIndex)))
        {
            crewUIs[selectCrewIndex].setNowActMark("낚시");
            crewStatusUIs[selectCrewIndex].setCrewNowActText("낚시");
        }
        else
        {
            Debug.Log("낚시 불가!");
        }

        hideSelectMenu();
    }

    public void repairButton()
    {
        CrewmanManager.Instance.slectCrewmanRepair(CrewmanManager.Instance.getCrewman(selectCrewIndex));
        eventMenuUI.gameObject.SetActive(true);
        eventMenuUI.showRepairMenu(selectCrewIndex);

        hideSelectMenu();
    }

    public void eatButton()
    {
        if (CrewmanManager.Instance.crewmanEat(CrewmanManager.Instance.getCrewman(selectCrewIndex)))
        {
            crewUIs[selectCrewIndex].setNowActMark("식사");
            crewStatusUIs[selectCrewIndex].setCrewNowActText("식사");
        }
        else
        {
            Debug.Log("식사 불가!");
        }
        hideSelectMenu();
    }

    public void sleepButton()
    {
        if(CrewmanManager.Instance.crewmanSleep(CrewmanManager.Instance.getCrewman(selectCrewIndex)))
        {
            crewUIs[selectCrewIndex].setNowActMark("수면");
            crewStatusUIs[selectCrewIndex].setCrewNowActText("수면");
        }
        else
        {
            Debug.Log("수면 불가!");
        }
        hideSelectMenu();
    }

    public void takeControlButton()
    {
        if (CrewmanManager.Instance.crewDrive(CrewmanManager.Instance.getCrewman(selectCrewIndex)))
        {
            crewUIs[selectCrewIndex].setNowActMark("항해");
            crewStatusUIs[selectCrewIndex].setCrewNowActText("항해");
            //GameManager.Instance.Sail.DriveCrew = CrewmanManager.Instance.getCrewman(selectCrewIndex);
            //appointTakeControlCrew();
        }
        else
        {
            Debug.Log("항해 불가!");
        }
        
        hideSelectMenu();
    }

    private void appointTakeControlCrew()
    {
        for (int i = 0; i < crewUIs.Length; i++)
        {
            if (i == selectCrewIndex) continue;
            crewUIs[i].toggleTakeControlMark(false);
        }
        crewUIs[selectCrewIndex].toggleTakeControlMark(true);
    }

    public void hideNowActMark(int crewIndex) // 해당 선원의 행동 표시를 지우는 함수
    {
        crewUIs[crewIndex].hideNowActMark();
        crewStatusUIs[selectCrewIndex].setCrewNowActText("활동중");
    }

    private void refreshUI()
    {
        refreshEtcUI();
        refreshSheepStatusUI();

        for (int i = 0; i < CrewmanManager.Instance.howManyCrewman(); i++)
        {
            Crewman _crewman = CrewmanManager.Instance.getCrewman(i);

            refreshCrewUI(i);
        }
    }

    public void initUI()
    {
        // 선원 정보를 이용하여 UI 갱신
        
        for(int i = 0; i < crewUIs.Length; i++)
        {
            crewUIs[i].gameObject.SetActive(false);
            crewStatusUIs[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < CrewmanManager.Instance.howManyCrewman(); i++)
        {
            Crewman _crewman = CrewmanManager.Instance.getCrewman(i);

            crewUIs[i].setCrewImage(getCrewImage(_crewman));
            crewStatusUIs[i].setCrewImage(getCrewImage(_crewman));

            refreshCrewUI(i);

            crewUIs[i].gameObject.SetActive(true);
            crewStatusUIs[i].gameObject.SetActive(true);
        }

        //sheepStatusUI.setShipDescriptionText("요트 설명 적힐곳");
    }

    private void refreshCrewUI(int crewIndex)
    {
        // 선원 데이터를 가져와서 UI 갱신

        Crewman _crew = CrewmanManager.Instance.getCrewman(crewIndex);

        crewUIs[crewIndex].setNowActMark(actingEnumToString(_crew.getActingType()));
        crewUIs[crewIndex].setActGauge(_crew.getbehavior());
        crewUIs[crewIndex].setHungerGauge(_crew.getfull());

        crewStatusUIs[crewIndex].setCrewNowActText(actingEnumToString(_crew.getActingType()));
        crewStatusUIs[crewIndex].setLeftActText(10, _crew.getbehavior());
        crewStatusUIs[crewIndex].setHungerText(4, _crew.getfull());
        crewStatusUIs[crewIndex].setCrewJobText(getCrewJob(_crew));
    }

    private void refreshEtcUI()
    {
        etcStatusUI.setFishingRodText(GameManager.Instance.getNowFishingRod().Durability);
        etcStatusUI.setFoodText(GameManager.Instance.Food);
    }

    private void refreshSheepStatusUI()
    {
        sheepStatusUI.setSailText(100, GameManager.Instance.Sail.Durability); // 돛 내구도
        sheepStatusUI.setBodyShipText(100, GameManager.Instance.ShipBody.Durability); // 선체 내구도
    }

    private Sprite getCrewImage(Crewman crewData) 
    {
        if(crewData is Captain)
        {
            return captineSprite;
        }
        else if(crewData is Engineer)
        {
            return engineerSprite;
        }
        else if(crewData is Mate)
        {
            return mateSprite;
        }
        else if(crewData is Angler)
        {
            return anglerSprite;
        }

        return null;
    }

    private string getCrewJob(Crewman crewData)
    {
        if (crewData is Captain)
        {
            return "선장";
        }
        else if (crewData is Engineer)
        {
            return "엔지니어";
        }
        else if (crewData is Mate)
        {
            return "항해사";
        }
        else if (crewData is Angler)
        {
            return "강태공";
        }

        return null;
    }

    public void refreshCalendar(Calendar calendar)
    {
        calendarDate.text = calendar.year + "년 " + calendar.month + "월 " + calendar.day + "일";
        calendarTime.text = calendar.time + "시";
        calendarDDay.text = "D+" + calendar.dday;
    }

    public void showMessage(string text)
    {
        messageUI.enqueueMessage(text);
    }

    private string actingEnumToString(Acting acting)
    {
        switch (acting)
        {
            case Acting.FISHING:
                return "낚시";
            case Acting.REPAIR:
                return "수리";
            case Acting.SLEEP:
                return "수면";
            case Acting.EAT:
                return "식사";
            case Acting.DRIVE:
                return "항해";

        }

        return null;
    }

    public void endGame(GameEnding gameEnding)
    {
        Text _endText = endingPanel.transform.GetChild(0).GetComponent<Text>();
        endingPanel.SetActive(true);
        switch (gameEnding)
        {
            case GameEnding.ARRIVE:
                _endText.text = "세계일주 하였습니다!\n 명성이 올라 " + GameManager.Instance.Reward + "의 돈을 얻습니다.";
                break;
            case GameEnding.SHIPWRECK: // 난파엔딩
                _endText.text = "난파되었습니다";
                break;
            case GameEnding.PIRATE:
                _endText.text = "해적에게 전부 약탈당했습니다";
                break;
            case GameEnding.HUNGRY:
                _endText.text = "배고파서 죽었습니다";
                break;
            case GameEnding.DEAD:
                _endText.text = "선장이 죽었습니다";
                break;
        }
    }

    public void showSaveCrewEvent()
    {
        eventMenuUI.gameObject.SetActive(true);
        eventMenuUI.showSaveCrewMenu();
    }

    public void restartButton()
    {
        GameManager.Instance.recoveryTimeScale();
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenuScene");
    }
}
