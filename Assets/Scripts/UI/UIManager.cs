using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Canvas myCanvas;
    [SerializeField] private GameObject eventMenu;
    [SerializeField] private GameObject selectMenu;

    [SerializeField] private CrewUI[] crewUIs;
    [SerializeField] private GameObject crewHighlight;

    private int selectCrewIndex = -1; // 선택된 선원 번호(왼쪽부터 0번)

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void outrangeClick()
    {
        hideSelectMenu();
        hideCrewHighlight();
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
        hideSelectMenu();
    }

    public void repairButton()
    {
        crewUIs[selectCrewIndex].setNowActMark("수리");
        hideSelectMenu();
    }

    public void eatButton()
    {
        crewUIs[selectCrewIndex].setNowActMark("식사");
        hideSelectMenu();
    }

    public void sleepButton()
    {
        crewUIs[selectCrewIndex].setNowActMark("수면");
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
}
