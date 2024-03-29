﻿using System.Collections;
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

    [SerializeField] private Sprite[] shipBodyViewSprite;
    [SerializeField] private Sprite[] shipSailViewSprite;

    private bool isDetilMode = false;

    private Color initColor;
    private Color initTextColor;
    private Color dangerColor;

    void Start()
    {
        initColor = new Color(1, 1, 1, 1);
        initTextColor = new Color(0, 0, 0, 1);
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
            sailText.color = dangerColor;
            shipSailImage.color = dangerColor;
        }
        else
        {
            sailText.color = initTextColor;
            shipSailImage.color = initColor;
        }
    }

    public void setBodyShipText(int maxValue, int nowValue)
    {
        bodyShipText.text = nowValue + "/" + maxValue;

        if (nowValue <= 30)
        {
            bodyShipText.color = dangerColor;
            shipBodyImage.color = dangerColor;
        }
        else
        {
            bodyShipText.color = initTextColor;
            shipBodyImage.color = initColor;
        }
    }

    public void setShipDescriptionText(string text)
    {
        shipDescription.text = text;
    }

    public void setViewImage(YachtType type)
    {
        Vector2 _imageSize = Vector2.zero;
        switch (type)
        {
            case YachtType.DEFAULT:
                shipBodyImage.sprite = shipBodyViewSprite[0];
                shipSailImage.sprite = shipSailViewSprite[0];
                _imageSize = new Vector2(180, 220);
                break;
            case YachtType.TYPE_A:
                shipBodyImage.sprite = shipBodyViewSprite[1];
                shipSailImage.sprite = shipSailViewSprite[1];
                _imageSize = new Vector2(210, 220);
                break;
        }

        shipBodyImage.GetComponent<RectTransform>().sizeDelta = _imageSize;
        shipSailImage.GetComponent<RectTransform>().sizeDelta = _imageSize;
    }
}
