using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigator : Item
{
    void Awake()
    {
        name = "Navigator";
    }

    public override void OnPickup()
    {
        GameManager.Instance.spawner.pathFinder.isPickedUp = true;
        SoundManager.Instance.PlaySFX(ESFXs.GetItemSFX);
        Destroy(gameObject);
    }
}
