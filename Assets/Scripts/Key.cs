using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour, IPickupable
{
    private void OnEnable()
    {
        GameManager.Instance.setter.KeyCount.Value++;
    }

    public void OnPickup(PlayerController player)
    {
        GameManager.Instance.setter.KeyCount.Value--;
        Destroy(gameObject);
    }
}
