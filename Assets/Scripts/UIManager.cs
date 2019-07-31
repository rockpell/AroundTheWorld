using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject eventMenu;
    [SerializeField] private GameObject selectMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void outrangeClick()
    {
        Debug.Log("out!");
        selectMenu.SetActive(false);
    }

    public void showSelectMenu()
    {
        selectMenu.SetActive(true);
    }
}
