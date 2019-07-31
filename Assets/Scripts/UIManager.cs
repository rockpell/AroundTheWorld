using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Canvas myCanvas;
    [SerializeField] private GameObject eventMenu;
    [SerializeField] private GameObject selectMenu;

    [SerializeField] private GameObject[] crews;
    [SerializeField] private GameObject crewHighlight;

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
        selectMenu.SetActive(false);
        hideCrewHighlight();
    }

    public void showSelectMenu(int index)
    {
        Vector2 _size = selectMenu.GetComponent<RectTransform>().sizeDelta;

        CanvasScaler scaler = myCanvas.GetComponentInParent<CanvasScaler>();
        selectMenu.GetComponent<RectTransform>().anchoredPosition 
            = new Vector2(Input.mousePosition.x * scaler.referenceResolution.x / Screen.width - scaler.referenceResolution.x / 2 + _size.x/2,
            Input.mousePosition.y * scaler.referenceResolution.y / Screen.height - scaler.referenceResolution.y / 2 + _size.y/2);

        selectMenu.SetActive(true);
        showCrewHighlight(index);
    }

    private void showCrewHighlight(int index)
    {
        if(crews[index] != null)
        {
            if(crewHighlight != null)
            {
                crewHighlight.transform.position = crews[index].transform.position;
                crewHighlight.SetActive(true);
            }
        }
    }

    private void hideCrewHighlight()
    {
        if (crewHighlight != null)
        {
            crewHighlight.SetActive(false);
        }
    }
}
