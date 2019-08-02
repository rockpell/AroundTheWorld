using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainMenue : MonoBehaviour
{
    public Button bar, wharf, dock, market;
    public Button engineer, mate, angler;
    public Button yacht_b, rafter;
    public Button fishing_rod_a, fishing_rod_b, fishing_rod_c;
    public Button _return;
    public Button start;
    public GameManager gamemanager;

    // Start is called before the first frame update
    void Start()
    {
        bar.onClick.AddListener(BarOnClick);//술집
        wharf.onClick.AddListener(WharfOnClick);//부두
        dock.onClick.AddListener(DockOnClick);//선착장
        market.onClick.AddListener(MarketOnClick);//시장

        engineer.onClick.AddListener(EngineerOnClick);//엔지니어
        mate.onClick.AddListener(MateOnClick);//항해사
        angler.onClick.AddListener(AnglerOnClick);//강태공

        yacht_b.onClick.AddListener(Yacht_bOnClick);//요트b
        rafter.onClick.AddListener(RafterOnClick);//뗏목

        fishing_rod_a.onClick.AddListener(Fishing_rod_aOnClick);//낚시대a
        fishing_rod_b.onClick.AddListener(Fishing_rod_bOnClick);//낚시대b
        fishing_rod_c.onClick.AddListener(Fishing_rod_cOnClick);//낚시대c

        start.onClick.AddListener(StartOnClick);//출항

        _return.onClick.AddListener(_ReturnOnClick);//되돌리기
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BarOnClick()
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

    void WharfOnClick()
    {
        _return.interactable = true;

        start.interactable = true;

        bar.interactable = false;
        wharf.interactable = false;
        dock.interactable = false;
        market.interactable = false;
    }

    void DockOnClick()
    {
        _return.interactable = true;

        yacht_b.interactable = true;
        rafter.interactable = true;

        bar.interactable = false;
        wharf.interactable = false;
        dock.interactable = false;
        market.interactable = false;
    }
    void MarketOnClick()
    {
        _return.interactable = true;

        fishing_rod_a.interactable = true;
        fishing_rod_b.interactable = true;
        fishing_rod_c.interactable = true;

        bar.interactable = false;
        wharf.interactable = false;
        dock.interactable = false;
        market.interactable = false;

    }
    void EngineerOnClick()
    {
    }
    void MateOnClick()
    {

    }
    void AnglerOnClick()
    {

    }
    void Yacht_bOnClick()
    {

    }
    void RafterOnClick()
    {

    }
    void Fishing_rod_aOnClick()
    {

    }
    void Fishing_rod_bOnClick()
    {

    }
    void Fishing_rod_cOnClick()
    {

    }
    void StartOnClick()
    {

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
        yacht_b.interactable = false;
        rafter.interactable = false;
        fishing_rod_a.interactable = false;
        fishing_rod_b.interactable = false;
        fishing_rod_c.interactable = false;
    }
}
