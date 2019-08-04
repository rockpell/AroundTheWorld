using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sail : MonoBehaviour, IShipModule
{
    [SerializeField] private int durability;

    private float currentSpeed;
    private float decisionSpeed;

    private float originalDegree;
    private float shipDegree;
    [SerializeField] private float limitAngle;
    [SerializeField] private GameObject sailModel;
    [SerializeField] private GameObject shipModel;
    [SerializeField] private Wind wind;

    [SerializeField] private Sprite[] sailImage;

    //뭐가 될진 모르지만 일단 선원을 할당할 코드
    [SerializeField] private Crewman driveCrew;

    private bool isControl;
    private bool isRight;
    private bool isSailDown;
    private bool isContrary;

    private float currentTime;
    [SerializeField] private float decreaseTime;

    [SerializeField] float sailControlSpeed;

    void Start()
    {
        isControl = true;
        isSailDown = true;
        shipDegree = shipModel.transform.rotation.eulerAngles.z;
        GameManager.Instance.Sail = this;
    }

    // Update is called once per frame
    void Update()
    {
        checkKeyInput();
        sailDegreeDecision();
        sailSpeedControl();
        shipDegree = shipModel.transform.rotation.eulerAngles.z;
    }

    private void checkKeyInput()
    {
        //crew에 선원 할당식
        if((driveCrew != null))
        {
            if(Input.GetKeyDown(KeyCode.A) && (driveCrew.getbehavior() > 0))
            {
                if(isControl == true)
                {
                    isControl = false;
                    isRight = false;
                    originalDegree = shipDegree;
                    driveCrew.setbehavior(driveCrew.getbehavior() - 1);
                }
            }
            if(Input.GetKey(KeyCode.A))
            {
                if((isControl == false)&&(isRight == false))
                {
                    if (shipDegree < originalDegree)
                        shipDegree += 360;
                    if ((shipDegree - originalDegree) % 360 < limitAngle)
                    {
                        shipDegree += sailControlSpeed;
                        shipModel.transform.rotation = Quaternion.Euler(0, 0, shipDegree);
                    }
                    else
                    { }
                }
            }
            if(Input.GetKeyUp(KeyCode.A))
            {
                if ((isControl == false) && (isRight == false))
                {
                    shipDegree = shipDegree % 360;
                    isControl = true;
                }
            }
            if (Input.GetKeyDown(KeyCode.D) && (driveCrew.getbehavior() > 0))
            {
                if (isControl == true)
                {
                    isControl = false;
                    isRight = true;
                    originalDegree = shipDegree;
                    driveCrew.setbehavior(driveCrew.getbehavior() - 1);
                }
            }
            if (Input.GetKey(KeyCode.D))
            {
                if ((isControl == false) && (isRight == true))
                {
                    if (originalDegree < shipDegree)
                        originalDegree += 360;
                    if ((originalDegree - shipDegree)%360 < limitAngle)
                    {
                        shipDegree -= sailControlSpeed;
                        shipModel.transform.rotation = Quaternion.Euler(0, 0, shipDegree);
                    }
                    else
                    { }
                }
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                if ((isControl == false) && (isRight == true))
                {
                    shipDegree = shipDegree % 360;
                    isControl = true;
                }
            }
            if(Input.GetKeyDown(KeyCode.Space) && (driveCrew.getbehavior() >= 2))
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
        if(sailImage.Length > 1)
        {
            sailModel.GetComponent<SpriteRenderer>().sprite = sailImage[0];
            driveCrew.setbehavior(driveCrew.getbehavior() - 2);
        }
    }
    private void sailUp()
    {
        isSailDown = false;
        if (sailImage.Length > 1)
        {
            sailModel.GetComponent<SpriteRenderer>().sprite = sailImage[1];
            driveCrew.setbehavior(driveCrew.getbehavior() - 2);
        }
    }
    //이거 호출되는거면 그냥 내구도 감소시켜주면 됨
    public void decreaseDurability(DurabilityEvent durabilityEvent)
    {
        if (durability < 1)
            return;
        switch(durabilityEvent)
        {
            case DurabilityEvent.INSIDETYPOON_SAIL:
                durability -= 1;
                break;
            case DurabilityEvent.INSIDETYPOON_CONTRARYWIND_SAIL:
                durability -= 1;
                break;
            case DurabilityEvent.CONTRARYWIND_SAIL:
                durability -= 1;
                break;
        }
    }

    private void sailDegreeDecision()
    {
        currentTime += Time.deltaTime;
        float _windDegree = wind.WindDirection;
        if (shipDegree < 0)
            shipDegree += 360;
        if ((shipDegree > (_windDegree + 135) % 360) && (shipDegree < (_windDegree + 225) % 360))
        {
            isContrary = true;
            if(durability > 0)
            {
                if(wind.WindSpeedEnumValue != WindSpeed.TYPOON)
                {
                    if(currentTime > decreaseTime)
                    {
                        decreaseDurability(DurabilityEvent.CONTRARYWIND_SAIL);
                        currentTime = 0;
                    }
                }
            }
            decisionSpeed = 0;
        }
        else
        {
            isContrary = false;
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
        if(durability > 0)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, decisionSpeed, Time.deltaTime);
            //currentSpeed를 속도로 전진한다.
            //driveCrew의 항해속도 보너스를 적용시킴
            if(driveCrew != null)
                currentSpeed = currentSpeed + (currentSpeed * (driveCrew.getsailing_speed() / 100));
            shipModel.transform.position += currentSpeed * shipModel.transform.up * 0.016f;
        }
    }
    public void repairModule(int repairAmount)
    {
        if ((durability + repairAmount) > 100)
            durability = 100;
        else
            durability += repairAmount;
    }
    public bool IsSailDown
    { get { return isSailDown; } }
    public bool IsContrary
    { get { return isContrary; } }
    public Crewman DriveCrew
    {
        get { return driveCrew; }
        set { driveCrew = value; }
    }
    public int Durability
    {
        get { return durability; }
    }
    public float CurrentSpeed
    { get { return currentSpeed; } }
}
