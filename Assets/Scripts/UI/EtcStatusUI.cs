using UnityEngine;
using UnityEngine.UI;

public class EtcStatusUI : MonoBehaviour
{
    [SerializeField] private Text foodText;
    [SerializeField] private Text fishingRodText;

    private Color initColor;
    private Color dangerColor;

    void Start()
    {
        initColor = new Color(0, 0, 0, 1);
        dangerColor = new Color(1, 0, 0, 1);
    }

    public void setFoodText(int foodCount)
    {
        foodText.text = foodCount.ToString();
    }

    public void setFishingRodText(int fishingRodCount)
    {
        if(fishingRodCount <= 30)
        {
            fishingRodText.color = dangerColor;
        }
        else
        {
            fishingRodText.color = initColor;
        }

        fishingRodText.text = fishingRodCount.ToString();
    }
}
