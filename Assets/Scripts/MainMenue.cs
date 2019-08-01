using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainMenue : MonoBehaviour
{
    public Button bar, wharf, dock, market;

    // Start is called before the first frame update
    void Start()
    {
        bar.onClick.AddListener(BarOnClick);
        wharf.onClick.AddListener(WharfOnClick);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BarOnClick()
    {

    }

    void WharfOnClick()
    {

    }

    void DockOnClick()
    {

    }
    void MarketOnClick()
    {

    }
}
