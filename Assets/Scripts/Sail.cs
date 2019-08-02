﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sail : MonoBehaviour, IShipModule
{
    [SerializeField] private int durability;

    private float currentSpeed;
    [SerializeField] private float decisionSpeed;
    [SerializeField] private float speedValue;

    private float originalDegree;
    private float shipDegree;
    [SerializeField] private GameObject sailModel;
    [SerializeField] private GameObject shipModel;
    [SerializeField] private Wind wind;

    [SerializeField] private Sprite[] sailImage;

    //뭐가 될진 모르지만 일단 선원을 할당할 코드
    [SerializeField] private GameObject crew;

    private bool isControl;
    private bool isRight;
    private bool isSailDown;

    [SerializeField] float sailControlSpeed;

    void Start()
    {
        isControl = true;
        isSailDown = true;
        shipDegree = shipModel.transform.rotation.eulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        checkKeyInput();
        sailDegreeDecision();
        sailSpeedControl();
    }

    private void checkKeyInput()
    {
        //crew에 선원 할당식
        if(crew != null)
        {
            if(Input.GetKeyDown(KeyCode.A))
            {
                if(isControl == true)
                {
                    isControl = false;
                    isRight = false;
                    Debug.Log("Push Down Left Key");
                    originalDegree = shipDegree;
                }
            }
            if(Input.GetKey(KeyCode.A))
            {
                if((isControl == false)&&(isRight == false))
                {
                    if ((shipDegree - originalDegree) % 360 < 45)
                    {
                        shipDegree += sailControlSpeed;
                        shipModel.transform.rotation = Quaternion.Euler(0, 0, shipDegree);
                        Debug.Log("Pushing Left Key");
                    }
                    else
                        Debug.Log("limited");
                }
            }
            if(Input.GetKeyUp(KeyCode.A))
            {
                if ((isControl == false) && (isRight == false))
                {
                    shipDegree = shipDegree % 360;
                    isControl = true;
                    Debug.Log("Push Up Left Key");
                }
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (isControl == true)
                {
                    isControl = false;
                    isRight = true;
                    Debug.Log("Push Down Right Key");
                    originalDegree = shipDegree;
                }
            }
            if (Input.GetKey(KeyCode.D))
            {
                if ((isControl == false) && (isRight == true))
                {
                    if ((originalDegree - shipDegree) % 360 < 45)
                    {
                        shipDegree -= sailControlSpeed;
                        Debug.Log("Pushing Right Key");
                        shipModel.transform.rotation = Quaternion.Euler(0, 0, shipDegree);
                    }
                    else
                        Debug.Log("limited");
                }
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                if ((isControl == false) && (isRight == true))
                {
                    shipDegree = shipDegree % 360;
                    isControl = true;
                    Debug.Log("Push Up Right Key");
                }
            }
            if(Input.GetKeyDown(KeyCode.Space))
            {
                if(isSailDown == false)
                {
                    sailDown();
                }
                else
                {
                    sailUp();
                }
            }
            
        }
    }

    private void sailDown()
    {
        isSailDown = true;
        if(sailImage.Length > 2)
        {
            sailModel.GetComponent<SpriteRenderer>().sprite = sailImage[0];
        }
        Debug.Log("돛 내림!");
    }
    private void sailUp()
    {
        isSailDown = false;
        if (sailImage.Length > 2)
            sailModel.GetComponent<SpriteRenderer>().sprite = sailImage[1];
        Debug.Log("돛 올림!");
    }
    //이거 호출되는거면 그냥 내구도 감소시켜주면 됨
    public void decreaseDurability(DurabilityEvent durabilityEvent)
    {
        switch(durabilityEvent)
        {
            case DurabilityEvent.INSIDETYPOON_SAIL:
                durability -= 1;
                break;
            case DurabilityEvent.INSIDETYPOON_CONTRARYWIND_SAIL:
                durability -= 1;
                break;
        }
    }

    private void sailDegreeDecision()
    {
        float _windDegree = wind.WindDirection;
        if (shipDegree < 0)
            shipDegree += 360;
        if ((shipDegree > (_windDegree + 135) % 360) && (shipDegree < (_windDegree + 225) % 360))
        {
            Debug.Log("It is No-Go zone");
            if(wind.WindSpeedEnumValue == WindSpeed.TYPOON)
            {
                decreaseDurability(DurabilityEvent.INSIDETYPOON_CONTRARYWIND_SAIL);
            }
            else
            {
                decreaseDurability(DurabilityEvent.CONTRARYWIND_SAIL);
            }
            decisionSpeed = 0;
        }
        else
        {
            float _degree = shipDegree - _windDegree;
            if (_degree < 0)
                _degree *= -1;

            if (_degree < 135)
                decisionSpeed = Mathf.Lerp(wind.WindSpeed, 0, _degree / 135);
            else if (_degree > 225)
                decisionSpeed = Mathf.Lerp(0, wind.WindSpeed, (_degree - 225) / 135);
        }
        if(isSailDown == false)
        {
            decisionSpeed = 0;
        }
    }

    private void sailSpeedControl()
    {
        currentSpeed = Mathf.Lerp(currentSpeed, decisionSpeed, Time.deltaTime);
        //currentSpeed를 속도로 전진한다.
        shipModel.transform.position += currentSpeed * shipModel.transform.up * Time.deltaTime;
    }
    public void repairModule(int repairAmount)
    {
        durability += repairAmount;
    }
    public bool IsSailDown
    { get { return isSailDown; } }
}
