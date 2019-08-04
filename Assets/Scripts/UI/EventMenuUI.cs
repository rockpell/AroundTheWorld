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
        int _childCount = saveCrewEventMenu.transform.childCount;

        for(int i = 0; i < _childCount; i++)
        {
            saveCrewEventMenu.transform.GetChild(i).gameObject.SetActive(false);
        }

        for(int i = 0; i < CrewmanManager.Instance.howManyCrewman(); i++)
        {
            saveCrewEventMenu.transform.GetChild(i).gameObject.SetActive(true);
        }

        eventText.text = "구조에 보낼 선원을 선택하세요";
        saveCrewEventMenu.SetActive(true);
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
        int _randNum = Random.Range(0, 10);

        if(_randNum < 9)
        {
            if(CrewmanManager.Instance.howManyCrewman() < 4)
            {
                if (_randNum < 3)
                {
                    CrewmanManager.Instance.makeEngineer();
                    UIManager.Instance.showMessage("엔지니어가 합류합니다");
                }
                else if (_randNum < 6)
                {
                    CrewmanManager.Instance.makeAngler();
                    UIManager.Instance.showMessage("강태공이 합류합니다");
                }
                else
                {
                    CrewmanManager.Instance.makeMate();
                    UIManager.Instance.showMessage("항해사가 합류합니다");
                }
            }
            else
            {
                UIManager.Instance.showMessage("선원을 구조했지만 태울자리가 없어서 보내줬습니다...");
            }
        }
        else
        {
            CrewmanManager.Instance.dieCrewman(CrewmanManager.Instance.getCrewman(index));
            UIManager.Instance.showMessage("선원이 사망하였습니다");

            if (!CrewmanManager.Instance.isAliveCaptain())
            {
                GameManager.Instance.NowGameEnding = GameEnding.DEAD;
            }
        }

        UIManager.Instance.initUI();
        cancelButton();
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
