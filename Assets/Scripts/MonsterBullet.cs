using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBullet : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Collider2D _col;
    private Collision2D _coll;

    private Animator _anim;
    [SerializeField]  private float _speed;
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<Collider2D>();
        _anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _coll = collision;
        StartCoroutine(DestroyBullet());
    }

    private IEnumerator DestroyBullet()
    {
        _anim.SetTrigger("OnDestroy");
        yield return new WaitForSeconds(0.2f);
        
        if (_coll.gameObject.CompareTag("Player"))
        {
            // 피격 판정은 플레이어 자체에서?
        }
        
        Destroy(gameObject);
    }

    public void Shoot(Vector3 target)
    {
        Vector3 direction = (target - this.transform.position).normalized;
        _rb.velocity = direction*_speed;
    }
    
}
