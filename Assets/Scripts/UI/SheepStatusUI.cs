using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SheepStatusUI : MonoBehaviour
{
    [SerializeField] private GameObject shipDetailStatus;
    [SerializeField] private Text sailText;
    [SerializeField] private Text bodyShipText;
    [SerializeField] private Text shipDescription;

    private bool isDetilMode = false;

    void Start()
    {
        
    }

    public void toggleDetailMode()
    {
        isDetilMode = !isDetilMode;

        shipDetailStatus.SetActive(isDetilMode);
    }

    public void setSailText(int maxValue, int nowValue)
    {
        sailText.text = nowValue + "/" + maxValue;
    }

    public void setBodyShipText(int maxValue, int nowValue)
    {
        bodyShipText.text = nowValue + "/" + maxValue;
    }

    public void setShipDescriptionText(string text)
    {
        shipDescription.text = text;
    }
}
