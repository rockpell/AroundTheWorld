using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SheepStatusUI : MonoBehaviour
{
    [SerializeField] private GameObject shipDetailStatus;
    [SerializeField] private Text sailText;
    [SerializeField] private Text bodyShipText;

    [SerializeField] private Image shipBodyImage;
    [SerializeField] private Image shipSailImage;

    [SerializeField] private Text shipDescription;

    private bool isDetilMode = false;

    private Color initColor;
    private Color dangerColor;

    void Start()
    {
        initColor = new Color(1, 1, 1, 1);
        dangerColor = new Color(1, 0, 0, 1);
    }

    public void toggleDetailMode()
    {
        isDetilMode = !isDetilMode;

        shipDetailStatus.SetActive(isDetilMode);
    }

    public void setSailText(int maxValue, int nowValue)
    {
        sailText.text = nowValue + "/" + maxValue;

        if(nowValue <= 30)
        {
            shipSailImage.color = dangerColor;
        }
        else
        {
            shipSailImage.color = initColor;
        }
    }

    public void setBodyShipText(int maxValue, int nowValue)
    {
        bodyShipText.text = nowValue + "/" + maxValue;

        if (nowValue <= 30)
        {
            shipBodyImage.color = dangerColor;
        }
        else
        {
            shipBodyImage.color = initColor;
        }
    }

    public void setShipDescriptionText(string text)
    {
        shipDescription.text = text;
    }
}
