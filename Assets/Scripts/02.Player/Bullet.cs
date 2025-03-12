using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    public float bulletDamage;
    public float bulletCoolTime;
    protected CustomPool<Bullet> bulletPool;
    public Animator bulletAnimator;

    protected Rigidbody2D _rb;
    protected Collider2D _col;

    private Collision2D _coll;

    public Monster target;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<Collider2D>();
        bulletAnimator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _coll = collision;
        StartCoroutine(ReturnBullet());
    }

    private IEnumerator ReturnBullet()
    {
        bulletAnimator.SetTrigger("OnDestroy");
        yield return new WaitForSeconds(0.1f);

        if (_coll.gameObject.CompareTag("Wall") || _coll.gameObject.CompareTag("Enemy"))
        {
            bulletPool.Return(this);
            target = null;
        }
    }

    public virtual void ToTarget(Vector3 origin, Vector3 target)
    {
        Vector3 direction = (target - this.transform.position).normalized;
        _rb.velocity = direction*bulletSpeed;
    }

    public void Init(CustomPool<Bullet> BulletPool)
    {
        bulletPool = BulletPool;
    }
}
