using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sail : MonoBehaviour, IShipModule
{
    [SerializeField] private int durability;
    private float speed;
    private float originalDegree;
    [SerializeField] private float sailDegree;
    [SerializeField] private GameObject sailModel;

    //뭐가 될진 모르지만 일단 선원을 할당할 코드
    [SerializeField] private GameObject crew;

    private bool isControl;
    private bool isRight;
    private bool isSailDown;

    [SerializeField] float sailControlSpeed;

    void Start()
    {
        isControl = true;
    }

    // Update is called once per frame
    void Update()
    {
        checkKeyInput();
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
                    originalDegree = sailDegree;
                }
            }
            if(Input.GetKey(KeyCode.A))
            {
                if((isControl == false)&&(isRight == false))
                {
                    if ((sailDegree - originalDegree) % 360 < 45)
                    {
                        sailDegree += sailControlSpeed;
                        sailModel.transform.rotation = Quaternion.Euler(0, 0, sailDegree);
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
                    sailDegree = sailDegree % 360;
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
                    originalDegree = sailDegree;
                }
            }
            if (Input.GetKey(KeyCode.D))
            {
                if ((isControl == false) && (isRight == true))
                {
                    if ((originalDegree - sailDegree) % 360 < 45)
                    {
                        sailDegree -= sailControlSpeed;
                        Debug.Log("Pushing Right Key");
                        sailModel.transform.rotation = Quaternion.Euler(0, 0, sailDegree);
                    }
                    else
                        Debug.Log("limited");
                }
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                if ((isControl == false) && (isRight == true))
                {
                    sailDegree = sailDegree % 360;
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
        Debug.Log("돛 내림!");
    }
    private void sailUp()
    {
        isSailDown = false;
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

    public void repairModule(int repairAmount)
    {
        durability += repairAmount;
    }
    public bool IsSailDown
    { get { return isSailDown; } }
}
