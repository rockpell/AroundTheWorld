using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBody : MonoBehaviour, IShipModule
{
    [SerializeField] private int durability;

    void Start()
    {
        GameManager.Instance.ShipBody = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void decreaseDurability(DurabilityEvent durabilityEvent)
    {
        switch(durabilityEvent)
        {
            case DurabilityEvent.COLLIDE_REEF_BODY:
                if (durability > 30)
                    durability -= 30;
                else
                    durability = 0;
                break;
            case DurabilityEvent.COLLIDE_ISLAND_BODY:
                if (durability > 30)
                    durability -= 30;
                else
                    durability = 0;
                break;
            case DurabilityEvent.COLLIDE_PIRATE_BODY:
                if (durability > 10)
                    durability -= 10;
                else
                    durability = 0;
                break;
            case DurabilityEvent.COLLIDE_SHIP_BODY:
                if (durability > 1)
                    durability -= 1;
                else
                    durability = 0;
                break;
            case DurabilityEvent.INSIDETYPOON_BODY:
                if (durability > 1)
                    durability -= 1;
                else
                    durability = 0;
                break;
        }
        if(durability <= 0)
        {
            //여기서 난파엔딩 만들어주거나 호출하면 됨
        }
    }

    public void repairModule(int repairAmound)
    {

    }
    public int Durability
    {
        get { return durability; }
    }
}
