using UnityEngine;
using UnityEngine.UI;

public class EtcStatusUI : MonoBehaviour
{
    [SerializeField] private Text foodText;
    [SerializeField] private Text fishingRodText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void setFoodText(int foodCount)
    {
        foodText.text = foodCount.ToString();
    }

    public void setFishingRodText(int fishingRodCount)
    {
        fishingRodText.text = fishingRodCount.ToString();
    }
}
