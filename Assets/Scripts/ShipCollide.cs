using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCollide : MonoBehaviour
{
    [SerializeField] private ShipBody shipBody;
    public void OnCollisionEnter2D(Collision2D collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Reef":
                shipBody.decreaseDurability(DurabilityEvent.COLLIDE_REEF_BODY);
                break;
            case "Island":
                shipBody.decreaseDurability(DurabilityEvent.COLLIDE_ISLAND_BODY);
                break;
            case "Ship":
                shipBody.decreaseDurability(DurabilityEvent.COLLIDE_SHIP_BODY);
                break;
            case "Pirate":
                shipBody.decreaseDurability(DurabilityEvent.COLLIDE_PIRATE_BODY);
                break;
        }
    }
}
