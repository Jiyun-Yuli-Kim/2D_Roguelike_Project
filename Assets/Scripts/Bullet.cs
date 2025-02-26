using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    public float bulletDamage;
    private Rigidbody2D _rb;
    public Action<Bullet> OnBulletHitWall;
    public Action<Bullet> OnBulletHitEnemy;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("벽에 충돌");
            OnBulletHitWall?.Invoke(this);
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            OnBulletHitEnemy?.Invoke(this);
        }
    }

    public void ToTarget(Vector3 target)
    {
        Vector3 direction = (target - this.transform.position).normalized;
        Debug.Log($"발사방향 : {direction.x},{direction.y}");
        _rb.velocity = direction*bulletSpeed;
    }
}
