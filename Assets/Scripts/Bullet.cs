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
        Vector3 direction = target - this.transform.position;
        Debug.Log($"발사방향 : {direction.x},{direction.y}");
        _rb.velocity = direction*bulletSpeed;
    }
}
