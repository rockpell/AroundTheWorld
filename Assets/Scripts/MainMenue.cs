using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenue : MonoBehaviour
{
    public Button bar, wharf, dock, market;
    public Button engineer, mate, angler, plusEngineer, plusMate, plusAngler;
    public Button yacht_a, rafter, plusYacht_a, plusNormalYacht;
    public Button fishing_rod_a, fishing_rod_b, fishing_rod_c, foodup, fooddown, plusFishing_rod_a, plusFoodup, plusFooddown;
    public Button _return;
    public Button start;
    public Text LeftFood;
    public Text Food;

    private int e_count, m_count, a_count, c_count;

    public CrewmanManager crewman;

    private FishingRodA FishingRodA;

    // Start is called before the first frame update
    void Start()
    {
        textFood();
        textLeftfood();

        e_count = 0;
        m_count = 0;
        a_count = 0;
        c_count = 0;

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
    }

    
    void EngineerOnClick()
    {
        if(GameManager.Instance.InitMoney >= 80)
        {
            if(crewman.howManyCrewman() < 4)
            {
                GameManager.Instance.InitMoney = GameManager.Instance.InitMoney - 80;
                GameManager.Instance.addCrewmanHaveList(new Engineer());
                e_count++;
                c_count++;
            }
           
        }
        
    }
    void MateOnClick()
    {
        if (GameManager.Instance.InitMoney >= 80)
        {
            if (crewman.howManyCrewman() < 4)
            {
                GameManager.Instance.InitMoney = GameManager.Instance.InitMoney - 80;
                GameManager.Instance.addCrewmanHaveList(new Mate());
                m_count++;
                c_count++;
            }
        }

    }
    void AnglerOnClick()
    {
        if (GameManager.Instance.InitMoney >= 80)
        {
            if(crewman.howManyCrewman() < 4)
            {
                GameManager.Instance.InitMoney = GameManager.Instance.InitMoney - 80;
                GameManager.Instance.addCrewmanHaveList(new Angler());
                a_count++;
                c_count++;
            }
        }

    }




    void WharfOnClick()
    {
        _return.interactable = true;

        start.interactable = true;
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

        if (GameManager.Instance.isContainFishingRod(FishingRodType.TYPE_A))
        {
            plusFishing_rod_a.interactable = true;
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
    }


    void StartOnClick()
    {
        //SceneManager.LoadScene("SampleScene");
    }

    void PlusAnglerOnClick()
    {
        if (a_count > 0 && CrewmanManager.Instance.howManyCrewman() < 4)
        {
            plusAngler.interactable = true;
            CrewmanManager.Instance.makeAngler();
            a_count--;
        }
        else
            plusAngler.interactable = false;
    }
    void PlusEngineerOnClick()
    {
        if (e_count > 0 && CrewmanManager.Instance.howManyCrewman() < 4)
        {
            plusEngineer.interactable = true;
            CrewmanManager.Instance.makeEngineer();
            e_count--;
        }
        else
            plusEngineer.interactable = false;
    }
    void PlusMateOnClick()
    {
        if (m_count > 0 && CrewmanManager.Instance.howManyCrewman() < 4)
        {
            plusMate.interactable = true;
            CrewmanManager.Instance.makeMate();
            m_count--;
        }
        else
            plusMate.interactable = false;
    }

    void PlusFishing_rod_aOnClick()
    {
        plusFishing_rod_a.interactable = false;
    }

    void PlusFoodupOnClick()
    {
        if (GameManager.Instance.LeftFood > 0)
        {
            GameManager.Instance.LeftFood -= 1;
            GameManager.Instance.Food += 1;
            textFood();
            textLeftfood();
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
            textLeftfood();
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
        if (GameManager.Instance.isContainYacth(YachtType.DEFAULT))
        {

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
    }


    void Yacht_aOnClick()
    {
        if (GameManager.Instance.InitMoney >= 2000)
        {
            GameManager.Instance.InitMoney -= 2000;
            GameManager.Instance.addYachtHaveList(YachtType.TYPE_A);
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

    }


    void Fishing_rod_aOnClick()
    {
        if(GameManager.Instance.InitMoney > 5)
        {
            GameManager.Instance.InitMoney = GameManager.Instance.InitMoney- FishingRodA.Price;
            GameManager.Instance.addFishingRodHaveList(FishingRodType.TYPE_A);
        }

    }
    void Fishing_rod_bOnClick()
    {

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
            textLeftfood();
        }
    }
    void FoodDownOnClick()
    {
        if(GameManager.Instance.LeftFood > 0)
        {
            GameManager.Instance.InitMoney = GameManager.Instance.InitMoney + 1;
            GameManager.Instance.LeftFood -= 1;
            textFood();
            textLeftfood();
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
    }

    public void textLeftfood()
    {
        LeftFood.text = GameManager.Instance.LeftFood + "일치 식량 보유";
    }
    public void textFood()
    {
        Food.text = "사용할" + GameManager.Instance.Food + "일치 식량";
    }
}
