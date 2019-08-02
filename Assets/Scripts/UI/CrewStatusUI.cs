using UnityEngine;
using UnityEngine.UI;

public class CrewStatusUI : MonoBehaviour
{
    [SerializeField] private Image crewImage;
    [SerializeField] private Text crewJobText;
    [SerializeField] private Text crewNowActText;

    [SerializeField] private Text leftActText;
    [SerializeField] private Text hungerText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void setCrewImage(Sprite sprite)
    {
        crewImage.sprite = sprite;
    }

    public void setCrewJobText(string job)
    {
        crewJobText.text = job;
    }

    public void setCrewNowActText(string nowAct)
    {
        crewNowActText.text = nowAct;
    }

    public void setLeftActText(int maxActCount, int nowActCount)
    {
        leftActText.text = nowActCount + "/" + maxActCount;
    }

    public void setHungerText(int maxHungerCount, int nowHungerCount)
    {
        hungerText.text = nowHungerCount + "/" + maxHungerCount;
    }
}
