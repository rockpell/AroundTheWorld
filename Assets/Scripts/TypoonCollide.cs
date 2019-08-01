using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypoonCollide : MonoBehaviour
{
    [SerializeField] private Sail sail;
    [SerializeField] private ShipBody shipBody;

    private Wind wind;

    private float[] currentTimes;
    //
    [SerializeField] private float[] decisionTimes;
    void Start()
    {
        wind = GameObject.Find("Wind").GetComponent<Wind>();
        currentTimes = new float[decisionTimes.Length];
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger Enter");
        if(wind != null)
        {
            wind.RefreshTime = wind.OriginRefreshTime / 2;
            wind.IsTypoon = true;
        }
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Trigger Stay");
        for (int i = 0; i < currentTimes.Length; i++)
        {
            currentTimes[i] += Time.deltaTime;
        }
        //2초당 1감소, 돛 내렸으면 감소 없음
        if(currentTimes[0] > decisionTimes[0])
        {
            //돛을 내렸는지 판정
            sail.decreaseDurability(DurabilityEvent.INSIDETYPOON_SAIL);
            currentTimes[0] = 0;
        }
        //1초당 1감소, 돛 내렸으면 감소 없음
        if (currentTimes[1] > decisionTimes[1])
        {
            //돛 방향이 지금 방향이랑 비교해서 역풍인지
            sail.decreaseDurability(DurabilityEvent.INSIDETYPOON_CONTRARYWIND_SAIL);
            currentTimes[1] = 0;
        }
        //3초당 1감소
        if(currentTimes[2] > decisionTimes[2])
        {
            shipBody.decreaseDurability(DurabilityEvent.INSIDETYPOON_BODY);
            currentTimes[2] = 0;
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Trigger Exit");
        wind.RefreshTime = wind.OriginRefreshTime;
        wind.IsTypoon = false;
    }
    void Update()
    {
        
    }
}
