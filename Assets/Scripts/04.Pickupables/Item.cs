using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour, IPickupable
{
    protected string name;

    public abstract void OnPickup();
}
