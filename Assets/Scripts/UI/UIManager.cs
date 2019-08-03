﻿using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Canvas myCanvas;
    [SerializeField] private GameObject eventMenu;
    [SerializeField] private GameObject selectMenu;

    [SerializeField] private CrewUI[] crewUIs;
    [SerializeField] private CrewStatusUI[] crewStatusUIs;
    [SerializeField] private GameObject crewHighlight;
    [SerializeField] private EtcStatusUI etcStatusUI;
    [SerializeField] private SheepStatusUI sheepStatusUI;
    [SerializeField] private MessageUI messageUI;

    [SerializeField] private Text calendarDate;
    [SerializeField] private Text calendarTime;
    [SerializeField] private Text calendarDDay;

    [SerializeField] private Sprite captineSprite; // 선장
    [SerializeField] private Sprite engineerSprite; // 엔지니어
    [SerializeField] private Sprite mateSprite; // 항해사
    [SerializeField] private Sprite anglerSprite; // 강태공

    private int selectCrewIndex = -1; // 선택된 선원 번호(왼쪽부터 0번)

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.gameStart(this);
        initUI();
    }

    public void outrangeClick()
    {
        hideSelectMenu();
    }

    public void showSelectMenu(int index)
    {
        Vector2 _size = selectMenu.GetComponent<RectTransform>().sizeDelta;

        CanvasScaler scaler = myCanvas.GetComponentInParent<CanvasScaler>();
        selectMenu.GetComponent<RectTransform>().anchoredPosition 
            = new Vector2(Input.mousePosition.x * scaler.referenceResolution.x / Screen.width - scaler.referenceResolution.x / 2 + _size.x/2,
            Input.mousePosition.y * scaler.referenceResolution.y / Screen.height - scaler.referenceResolution.y / 2 + _size.y/2);

        selectMenu.SetActive(true);
        showCrewHighlight(index);

        selectCrewIndex = index;
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
        crewUIs[selectCrewIndex].setNowActMark("낚시");
        crewStatusUIs[selectCrewIndex].setCrewNowActText("낚시");
        hideSelectMenu();

        showMessage("낚시!");
    }

    public void repairButton()
    {
        crewUIs[selectCrewIndex].setNowActMark("수리");
        crewStatusUIs[selectCrewIndex].setCrewNowActText("수리");
        hideSelectMenu();
    }

    public void eatButton()
    {
        crewUIs[selectCrewIndex].setNowActMark("식사");
        crewStatusUIs[selectCrewIndex].setCrewNowActText("식사");
        hideSelectMenu();
    }

    public void sleepButton()
    {
        crewUIs[selectCrewIndex].setNowActMark("수면");
        crewStatusUIs[selectCrewIndex].setCrewNowActText("수면");
        hideSelectMenu();
    }

    public void takeControlButton()
    {
        appointTakeControlCrew();
        hideSelectMenu();
    }

    private void appointTakeControlCrew()
    {
        for(int i = 0; i < crewUIs.Length; i++)
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
    }

    private void initUI()
    {
        /*CrewmanAbilityWork[] crewData*/

        // 선원 정보를 이용하여 UI 갱신

        //for (int i = 0; i < crewData.Length; i++)
        //{
        //    crewUIs[i].setCrewImage();
        //    crewStatusUIs[i].setCrewImage();

        //    crewUIs[i].setActGauge();
        //    crewUIs[i].setHungerGauge();
        //    crewStatusUIs[i].setLeftActText();
        //    crewStatusUIs[i].setHungerText();
        //    crewStatusUIs[i].setCrewJobText();

        //    crewUIs[i].gameObject.SetActive(true);
        //    crewStatusUIs[i].gameObject.SetActive(true);
        //}

        //sheepStatusUI.setShipDescriptionText("요트 설명 적힐곳");
    }

    private void refreshCrewUI(int crewIndex)
    {
        // 선원 데이터를 가져와서 UI 갱신

        Crewman _crew = CrewmanManager.Instance.getCrewman(crewIndex);

        //crewUIs[crewIndex].setNowActMark("행동");
        crewUIs[crewIndex].setActGauge(_crew.getbehavior());
        crewUIs[crewIndex].setHungerGauge(_crew.getfull());

        //crewStatusUIs[crewIndex].setCrewNowActText("행동");
        crewStatusUIs[crewIndex].setLeftActText(10, _crew.getbehavior());
        crewStatusUIs[crewIndex].setHungerText(4, _crew.getbehavior());
    }

    private void refreshEtcUI()
    {
        etcStatusUI.setFishingRodText(GameManager.Instance.getNowFishingRod().Durability);
        etcStatusUI.setFoodText(GameManager.Instance.Food);
    }

    private void refreshSheepStatusUI()
    {
        sheepStatusUI.setSailText(100, 99); // 돛 내구도
        sheepStatusUI.setBodyShipText(100, 98); // 선체 내구도
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

    public void refreshCalendar(Calendar calendar)
    {
        calendarDate.text = calendar.year + "년 " + calendar.month + "월 " + calendar.day + "일";
        calendarTime.text = calendar.time + "시";
        calendarDDay.text = "D+" + calendar.dday;
    }

    private void showMessage(string text)
    {
        messageUI.enqueueMessage(text);
    }
}
