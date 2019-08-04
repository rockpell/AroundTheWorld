using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventMenuUI : MonoBehaviour
{
    [SerializeField] private Text eventText;

    [SerializeField] private GameObject saveCrewEventMenu;
    [SerializeField] private GameObject tradeEventMenu;
    [SerializeField] private GameObject repairMenu;

    private int selectCrewIndex = -1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void showRepairMenu(int crewIndex)
    {
        selectCrewIndex = crewIndex;
        eventText.text = "수리 할 부분을 선택하세요";
        repairMenu.SetActive(true);
    }

    public void showSaveCrewMenu()
    {

    }

    public void repairSailButton()
    {
        if (GameManager.Instance.Sail.Durability >= 100)
        {
            UIManager.Instance.showMessage("내구도가 이미 최대치입니다");
        }
        else
        {
            if(selectCrewIndex == -1)
            {
                UIManager.Instance.showMessage("선원이 선택되지 않았습니다.");
            }
            else
            {
                bool _isRepair = CrewmanManager.Instance.crewmanRepair(CrewmanManager.Instance.getCrewman(selectCrewIndex));
                if (_isRepair)
                {
                    GameManager.Instance.repairSail();
                    UIManager.Instance.showMessage("돛을 수리하였습니다");
                }
            }
            
        }
        cancelButton();
    }

    public void repairBodyButton()
    {
        if (GameManager.Instance.Sail.Durability >= 100)
        {
            UIManager.Instance.showMessage("내구도가 이미 최대치입니다");
        }
        else
        {
            if (selectCrewIndex == -1)
            {
                UIManager.Instance.showMessage("선원이 선택되지 않았습니다.");
            }
            else
            {
                bool _isRepair = CrewmanManager.Instance.crewmanRepair(CrewmanManager.Instance.getCrewman(selectCrewIndex));
                if (_isRepair)
                {
                    GameManager.Instance.repairBody();
                    UIManager.Instance.showMessage("선체를 수리하였습니다");
                }
            }

        }

        cancelButton();
    }

    public void selectCrewButton(int index)
    {

    }

    public void tradeButton()
    {

    }

    public void cancelButton()
    {
        repairMenu.SetActive(false);
        saveCrewEventMenu.SetActive(false);
        tradeEventMenu.SetActive(false);
        this.gameObject.SetActive(false);
    }
}
