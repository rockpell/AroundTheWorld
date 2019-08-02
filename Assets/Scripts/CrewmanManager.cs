using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CrewmanManager : MonoBehaviour
{
    List<Crewman> crewmanList = new List<Crewman>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()   
    {
        
    }

    public bool makeCaptain()//선장 생성
    {
        if (crewmanList.Count == 0)
        {
            crewmanList.Add(new Captain());
            crewmanList[0].setindex(crewmanList.Count);
            return true;
        }
        return false;
    }

    public bool makeEnginieer(int type)//엔지니어 생성 단,선원이 4명 이하일때
    {
        if(crewmanList.Count < 4)
        {
            crewmanList.Add(new Enginieer());
            crewmanList[crewmanList.Count + 1].setindex(crewmanList.Count+1);
            return true;
        }
        return false;
        
    }
    public bool makeMate(int type)//항해사 생성 단,선원이 4명 이하일때
    {
        if (crewmanList.Count < 4)
        {
            crewmanList.Add(new Mate());
            crewmanList[crewmanList.Count + 1].setindex(crewmanList.Count + 1);
            return true;
        }
        return false;

    }
    public bool makeAngler(int type)//강태공 생성 단,선원이 4명 이하일때
    {
        if (crewmanList.Count < 4)
        {
            crewmanList.Add(new Angler());
            crewmanList[crewmanList.Count + 1].setindex(crewmanList.Count + 1);
            return true;
        }
        return false;

    }
    

    public Crewman whoDrive()// 선원중 누가 항해를 하는지
    {
        for(int i = 0; i < crewmanList.Count; i++)
        {
            crewmanList[i].getDrive();
            if (crewmanList[i].getDrive())
            {
                return crewmanList[i];
            }
        }
        
        return null;
    }

    public void crewDrive()
    {

    }

    public bool actingCheck(Crewman crewman)//선원이 행동을 하는지, 행동을 하면 false, 안하면 true
    {
        if( Acting.NOTHING == crewman.whatActing())
        {
            return true;
        }
        return false;
    }

}
