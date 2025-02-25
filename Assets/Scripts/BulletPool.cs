using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    private int poolSize;
    public GameObject bulletPrefab;
    BulletPool(int PoolSize)
    { 
        poolSize = PoolSize;
    }
}
