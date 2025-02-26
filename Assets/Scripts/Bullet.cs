using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    public float bulletDamage;
    private SpriteRenderer _renderer;
    public Animator bulletAnimator;
    public float colSize;

    private Rigidbody2D _rb;
    private Collider2D _col;

    public Action<Bullet> OnBulletHitWall;
    public Action<Bullet> OnBulletHitEnemy;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            OnBulletHitWall?.Invoke(this);
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            OnBulletHitEnemy?.Invoke(this);
        }
    }

    public void SetAppearance(Skill skill)
    {
        _renderer.sprite = skill.sRenderer.sprite;
        bulletAnimator = skill.skillBulletAnimator;
        var col = _col as CircleCollider2D;
        col.radius = colSize;
    }

    public void ToTarget(Vector3 target)
    {
        Vector3 direction = (target - this.transform.position).normalized;
        // Debug.Log($"�߻���� : {direction.x},{direction.y}");
        _rb.velocity = direction*bulletSpeed;
    }
}
