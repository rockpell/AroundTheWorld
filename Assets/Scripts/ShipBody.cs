using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBody : MonoBehaviour, IShipModule
{
    [SerializeField] private int durability;

    void Start()
    {
        
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
                durability -= 30;
                break;
            case DurabilityEvent.COLLIDE_ISLAND_BODY:
                durability -= 30;
                break;
            case DurabilityEvent.COLLIDE_PIRATE_BODY:
                durability -= 10;
                break;
            case DurabilityEvent.COLLIDE_SHIP_BODY:
                durability -= 1;
                break;
            case DurabilityEvent.INSIDETYPOON_BODY:
                durability -= 1;
                break;
        }
    }

    public void repairModule(int repairAmound)
    {

    }
}
