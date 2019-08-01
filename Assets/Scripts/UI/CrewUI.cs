using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrewUI : MonoBehaviour
{
    [SerializeField] private GameObject takeControlMark;
    [SerializeField] private GameObject nowActMark;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
