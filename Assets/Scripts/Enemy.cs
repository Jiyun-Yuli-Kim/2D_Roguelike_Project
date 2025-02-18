using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string enemyName;
    public int maxHp;
    public int curHp;
    
    
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AttackTarget(other.gameObject);
        }
    }

    void AttackTarget(GameObject target)
    {
        
    }
}
