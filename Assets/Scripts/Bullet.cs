using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    public float bulletDamage;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void ToTarget(Vector3 target)
    {
        _rb.velocity = (target - new Vector3(this.transform.position.x, this.transform.position.y))*bulletSpeed;
    }
}
