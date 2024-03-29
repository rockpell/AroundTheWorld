﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Button bar, wharf, dock, market;
    public Button engineer, mate, angler, plusEngineer, plusMate, plusAngler;
    public Button yacht_a, rafter, plusYacht_a, plusNormalYacht;
    public Button fishing_rod_a, fishing_rod_b, fishing_rod_c, foodup, fooddown, plusFishing_rod_a, plusFishing_rod_b, plusFoodup, plusFooddown;
    public Button _return;
    public Button start;
    public Text LeftFood, Food, Money;

    public GameObject menustart, barselect, wharfselect, dockselect, marketselect;

    private int e_count, m_count, a_count, c_count, f_count;
    private bool _count, a_or_b;

    public CrewmanManager crewman;
    

    // Start is called before the first frame update
    void Start()
    {
        

        menustart.SetActive(true);
        marketselect.SetActive(false);
        wharfselect.SetActive(false);
        dockselect.SetActive(false);
        barselect.SetActive(false);

        textFood();
        textLeftFood();
        textMoney();

        e_count = 0;
        m_count = 0;
        a_count = 0;
        c_count = 0;
        f_count = 0;
        _count = true;

        

        bar.onClick.AddListener(BarOnClick);//술집
        wharf.onClick.AddListener(WharfOnClick);//부두
        dock.onClick.AddListener(DockOnClick);//선착장
        market.onClick.AddListener(MarketOnClick);//시장

        engineer.onClick.AddListener(EngineerOnClick);//엔지니어
        mate.onClick.AddListener(MateOnClick);//항해사
        angler.onClick.AddListener(AnglerOnClick);//강태공

        plusAngler.onClick.AddListener(PlusAnglerOnClick);//강태공 추가
        plusEngineer.onClick.AddListener(PlusEngineerOnClick);//엔지니어 추가
        plusMate.onClick.AddListener(PlusMateOnClick);//항해사 추가

        yacht_a.onClick.AddListener(Yacht_aOnClick);//요트b
        rafter.onClick.AddListener(RafterOnClick);//뗏목

        fishing_rod_a.onClick.AddListener(Fishing_rod_aOnClick);//낚시대a
        fishing_rod_b.onClick.AddListener(Fishing_rod_bOnClick);//낚시대b
        fishing_rod_c.onClick.AddListener(Fishing_rod_cOnClick);//낚시대c
        foodup.onClick.AddListener(FoodUpOnClick);//음식 구매
        fooddown.onClick.AddListener(FoodDownOnClick);//음식 판매

        plusFishing_rod_a.onClick.AddListener(PlusFishing_rod_aOnClick);//낚시대 A 선택
        plusFishing_rod_b.onClick.AddListener(PlusFishing_rod_bOnClick);//낚시대 B 서택
        plusFoodup.onClick.AddListener(PlusFoodupOnClick);//식량 추가
        plusFooddown.onClick.AddListener(PlusFooddownOnClick);// 식량 빼기

        plusYacht_a.onClick.AddListener(PlusYacht_aOnClick);//요트 A 선택
        plusNormalYacht.onClick.AddListener(PlusNormalYachtOnClick);//기본 요트 선택

        start.onClick.AddListener(StartOnClick);//출항

        _return.onClick.AddListener(_ReturnOnClick);//되돌리기

        GameManager.Instance.addYachtHaveList(YachtType.DEFAULT);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void BarOnClick()//술집
    {
        _return.interactable = true;

        engineer.interactable = true;
        mate.interactable = true;
        angler.interactable = true;

        bar.interactable = false;
        wharf.interactable = false;
        dock.interactable = false;
        market.interactable = false;

        menustart.SetActive(false);
        barselect.SetActive(true);


    }

    
    void EngineerOnClick()
    {
        if(GameManager.Instance.InitMoney >= 80)
        {
            if(CrewmanManager.Instance.howManyCrewman() < 4)
            {
                GameManager.Instance.InitMoney = GameManager.Instance.InitMoney - 80;
                GameManager.Instance.addCrewmanHaveList(new Engineer());
                e_count++;
                c_count++;
                textMoney();
            }
           
        }
        
    }
    void MateOnClick()
    {
        if (GameManager.Instance.InitMoney >= 80)
        {
            if (CrewmanManager.Instance.howManyCrewman() < 4)
            {
                GameManager.Instance.InitMoney = GameManager.Instance.InitMoney - 80;
                GameManager.Instance.addCrewmanHaveList(new Mate());
                m_count++;
                c_count++;
                textMoney();
            }
        }

    }
    void AnglerOnClick()
    {
        if (GameManager.Instance.InitMoney >= 80)
        {
            if(CrewmanManager.Instance.howManyCrewman() < 4)
            {
                GameManager.Instance.InitMoney = GameManager.Instance.InitMoney - 80;
                GameManager.Instance.addCrewmanHaveList(new Angler());
                a_count++;
                c_count++;
                textMoney();
            }
        }

    }




    void WharfOnClick()
    {
        if(_count)
        {
            CrewmanManager.Instance.deleteAllCrewman();
            CrewmanManager.Instance.makeCaptain();
            GameManager.Instance.setNowFishingRod(0);
            plusFishing_rod_a.interactable = true;

            _count = false;
        }

        if(e_count > 0 && CrewmanManager.Instance.howManyCrewman() < 4)
        {
            plusEngineer.interactable = true;
        }
        if (m_count > 0 && CrewmanManager.Instance.howManyCrewman() < 4)
        {
            plusMate.interactable = true;
        }
        if (a_count > 0 && CrewmanManager.Instance.howManyCrewman() < 4)
        {
            plusAngler.interactable = true;
        }

        if (GameManager.Instance.isContainFishingRod(FishingRodType.TYPE_B) && a_or_b)
        {
            plusFishing_rod_b.interactable = true;
            plusFishing_rod_a.interactable = false;
        }
        else if(GameManager.Instance.isContainFishingRod(FishingRodType.TYPE_B) && !a_or_b)
        {
            plusFishing_rod_a.interactable = true;
            plusFishing_rod_b.interactable = false;
        }
        else if(!GameManager.Instance.isContainFishingRod(FishingRodType.TYPE_B))
        {
            plusFishing_rod_a.interactable = false;
            plusFishing_rod_b.interactable = false;
        }

        if(GameManager.Instance.LeftFood > 0)
        {
            plusFoodup.interactable = true;
        }
        if (GameManager.Instance.Food > 0)
        {
            plusFooddown.interactable = true;
        }


        bar.interactable = false;
        wharf.interactable = false;
        dock.interactable = false;
        market.interactable = false;

        menustart.SetActive(false);
        wharfselect.SetActive(true);

        start.interactable = true;

        _return.interactable = true;
    }


    void StartOnClick()
    {
        _count = true;
        if (GameManager.Instance.isContainFishingRod(FishingRodType.TYPE_B))
        {
            f_count = 0;
            GameManager.Instance.removeFishingRodHaveList(FishingRodType.TYPE_B);
        }
        SceneManager.LoadScene("InGame");
    }

    void PlusAnglerOnClick()
    {
        if (a_count >= 2 && CrewmanManager.Instance.howManyCrewman() < 4)
        {
            plusAngler.interactable = true;
            CrewmanManager.Instance.makeAngler();
            a_count--;
        }
        if (a_count == 1 && CrewmanManager.Instance.howManyCrewman() < 4)
        {
            plusAngler.interactable = false;
            CrewmanManager.Instance.makeAngler();
            a_count--;
        }
        else
            plusAngler.interactable = false;
    }
    void PlusEngineerOnClick()
    {
        if (e_count >= 2 && CrewmanManager.Instance.howManyCrewman() < 4)
        {
            plusEngineer.interactable = true;
            CrewmanManager.Instance.makeEngineer();
            e_count--;
        }
        if (e_count == 1 && CrewmanManager.Instance.howManyCrewman() < 4)
        {
            plusEngineer.interactable = false;
            CrewmanManager.Instance.makeEngineer();
            e_count--;
        }
        else
            plusEngineer.interactable = false;
    }
    void PlusMateOnClick()
    {
        if (m_count >= 2 && CrewmanManager.Instance.howManyCrewman() < 4)
        {
            plusMate.interactable = true;
            CrewmanManager.Instance.makeMate();
            m_count--;
        }
        if (m_count == 1 && CrewmanManager.Instance.howManyCrewman() < 4)
        {
            plusMate.interactable = false;
            CrewmanManager.Instance.makeMate();
            m_count--;
        }
        else
            plusMate.interactable = false;
    }

    void PlusFishing_rod_aOnClick()
    {
        
        GameManager.Instance.setNowFishingRod(0);
        plusFishing_rod_a.interactable = false;
        if (GameManager.Instance.isContainFishingRod(FishingRodType.TYPE_B))
        {
            plusFishing_rod_b.interactable = true;
        }
    }
    void PlusFishing_rod_bOnClick()
    {
        if (GameManager.Instance.isContainFishingRod(FishingRodType.TYPE_B))
        {
            GameManager.Instance.setNowFishingRod(1);
            plusFishing_rod_b.interactable = false;
            plusFishing_rod_a.interactable = true;
        }
    }

    void PlusFoodupOnClick()
    {
        if (GameManager.Instance.LeftFood > 0)
        {
            GameManager.Instance.LeftFood -= 1;
            GameManager.Instance.Food += 1;
            textFood();
            textLeftFood();
            plusFoodup.interactable = true;
        }
        else
            plusFoodup.interactable = false;
    }
    void PlusFooddownOnClick()
    {
        if (GameManager.Instance.Food > 0)
        {
            GameManager.Instance.LeftFood += 1;
            GameManager.Instance.Food -= 1;
            textFood();
            textLeftFood();
            plusFooddown.interactable = true;
        }
        else
        {
            plusFooddown.interactable = false;
        }
    }

    void PlusYacht_aOnClick()
    {

        if (GameManager.Instance.isContainYacth(YachtType.TYPE_A))
        {

        }
    }
    void PlusNormalYachtOnClick()
    {
        if (!GameManager.Instance.isContainYacth(YachtType.DEFAULT))
        {
            plusNormalYacht.interactable = true;
        }
    }




    void DockOnClick()
    {
        _return.interactable = true;

        yacht_a.interactable = true;
        rafter.interactable = true;

        bar.interactable = false;
        wharf.interactable = false;
        dock.interactable = false;
        market.interactable = false;

        menustart.SetActive(false);
        dockselect.SetActive(true);
    }


    void Yacht_aOnClick()
    {
        if (GameManager.Instance.InitMoney >= 2000 && !GameManager.Instance.isContainYacth(YachtType.TYPE_A))
        {
            GameManager.Instance.addYachtHaveList(YachtType.TYPE_A);
            GameManager.Instance.InitMoney -= 2000;
            textMoney();
        }
            
    }
    void RafterOnClick()
    {

    }



    void MarketOnClick()
    {
        _return.interactable = true;

        fishing_rod_a.interactable = true;
        fishing_rod_b.interactable = true;
        fishing_rod_c.interactable = true;

        fooddown.interactable = true;
        foodup.interactable = true;

        bar.interactable = false;
        wharf.interactable = false;
        dock.interactable = false;
        market.interactable = false;

        menustart.SetActive(false);
        marketselect.SetActive(true);
    }


    void Fishing_rod_aOnClick()
    {

    }
    void Fishing_rod_bOnClick()
    {
        if (GameManager.Instance.InitMoney >= 20 && !GameManager.Instance.isContainFishingRod(FishingRodType.TYPE_B) && f_count == 0)
        {
            GameManager.Instance.addFishingRodHaveList(FishingRodType.TYPE_B);
            GameManager.Instance.InitMoney = GameManager.Instance.InitMoney - 20;
            f_count++;
            textMoney();
        }
    }
    void Fishing_rod_cOnClick()
    {

    }

    void FoodUpOnClick()
    {
        if(GameManager.Instance.InitMoney > 0)
        {
            GameManager.Instance.InitMoney = GameManager.Instance.InitMoney - 1;
            GameManager.Instance.LeftFood += 1;
            textFood();
            textLeftFood();
            textMoney();
        }
    }
    void FoodDownOnClick()
    {
        if(GameManager.Instance.LeftFood > 0)
        {
            GameManager.Instance.InitMoney = GameManager.Instance.InitMoney + 1;
            GameManager.Instance.LeftFood -= 1;
            textFood();
            textLeftFood();
            textMoney();
        }
        
    }




    
    
   
    void _ReturnOnClick()
    {

            

        bar.interactable = true;
        wharf.interactable = true;
        dock.interactable = true;
        market.interactable = true;
        

        _return.interactable = false;
        engineer.interactable = false;
        mate.interactable = false;
        angler.interactable = false;
        
        yacht_a.interactable = false;
        rafter.interactable = false;

        fishing_rod_a.interactable = false;
        fishing_rod_b.interactable = false;
        fishing_rod_c.interactable = false;

        fooddown.interactable = false;
        foodup.interactable = false;

        plusAngler.interactable = false;
        plusEngineer.interactable = false;
        plusFishing_rod_a.interactable = false;
        plusFooddown.interactable = false;
        plusFoodup.interactable = false;
        plusMate.interactable = false;
        plusNormalYacht.interactable = false;
        plusYacht_a.interactable = false;

        menustart.SetActive(true);
        wharfselect.SetActive(false);
        dockselect.SetActive(false);
        marketselect.SetActive(false);
        barselect.SetActive(false);
    }

    public void textLeftFood()
    {
        LeftFood.text = GameManager.Instance.LeftFood + "일치 식량 보유";
    }
    public void textFood()
    {
        Food.text = "사용할" + GameManager.Instance.Food + "일치 식량";
    }
    public void textMoney()
    {
        Money.text = "남은 돈:" + GameManager.Instance.InitMoney ;
    }
}
