﻿using UnityEngine;
using UnityEngine.UI;

public class CrewUI : MonoBehaviour
{
    [SerializeField] private Image crewImage;
    [SerializeField] private GameObject takeControlMark;
    [SerializeField] private GameObject nowActMark;

    void Start()
    {
        
    }

    public void setCrewImage(Sprite sprite)
    {
        crewImage.sprite = sprite;
    }

    public void toggleTakeControlMark(bool value)
    {
        if(takeControlMark != null)
            takeControlMark.SetActive(value);
    }

    public void setNowActMark(string actName)
    {
        nowActMark.GetComponent<Text>().text = actName;
        nowActMark.SetActive(true);
    }

    public void hideNowActMark()
    {
        nowActMark.SetActive(false);
    }

    public void setHungerGauge(int nowHunger)
    {

    }

    public void setActGauge(int nowAct)
    {

    }
}
