using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

[Preserve]
public class Navigator : Item
{
    void Awake()
    {
        itemName = "Navigator";
    }

    public override void OnPickup()
    {
        GameManager.Instance.spawner.pathFinder.isPickedUp = true;
        SoundManager.Instance.PlaySFX(ESFXs.GetItemSFX);
        Destroy(gameObject);
    }
}
