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
        StartCoroutine(ReturnBullet());
    }

    private IEnumerator ReturnBullet() // 총알이 플레이어에 충돌했을 때의 루틴
    {
        _anim.SetTrigger("OnDestroy");
        yield return new WaitForSeconds(0.1f);
        
        if (_coll!=null && _coll.gameObject.CompareTag("Player"))
        {
            _coll.gameObject.GetComponent<PlayerController>().TakeDamage(1);
        }
        
        Destroy(gameObject);
    }

    public void Shoot(Vector3 target)
    {
        Vector3 direction = (target - this.transform.position).normalized;
        _rb.velocity = direction*_speed;
    }
    
    public void DirectionalShoot(Vector3 direction)
    {
        _rb.velocity = direction*_speed;
    }
    
}
