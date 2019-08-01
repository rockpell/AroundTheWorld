using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShipModule
{
    void decreaseDurability(DurabilityEvent durabilityEvent);
    void repairModule(int repairAmound);
}
