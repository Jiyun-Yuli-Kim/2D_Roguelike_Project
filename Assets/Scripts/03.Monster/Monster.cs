using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster : MonoBehaviour
{
    [SerializeField] protected float _moveSpeed;
    [SerializeField] protected float _followSpeed;
    [SerializeField] protected float _attackRange;

    [SerializeField] protected float _attackCoolTime;
    [SerializeField] protected float _monsterHP;

    protected Rigidbody2D _rb;
    protected Collider2D _col;
    protected bool _isFollowing;
    protected bool _isAttacking;
    protected PlayerController _player;

    protected SpriteRenderer _spriteRenderer;

    protected Animator _anim; 

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<Collider2D>();
        _anim = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        Move();
    }

    private void Update()
    {
        if (_monsterHP <= 0)
        {
            _anim.SetTrigger("Hit");
            Destroy(gameObject);
        }

        if (_rb.velocity.x < -0.3)
        {
            _spriteRenderer.flipX = true;
        }
        
        else if (_rb.velocity.x > 0.3)
        {
            _spriteRenderer.flipX = false;
        }

        if (HasParameter(_anim, "Speed"))
        {
            _anim.SetFloat("Speed", _rb.velocity.magnitude);
        }

        if (_isFollowing)
        { 
            Vector3 dir = (_player.transform.position - transform.position).normalized;
            _rb.velocity = dir * _followSpeed;
        }
        
        if(_player != null &&
           (transform.position - _player.transform.position).magnitude < _attackRange)
        {
            Attack();    
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        { 
            _player = collision.gameObject.GetComponent<PlayerController>();
            _isFollowing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _isFollowing = false;
            _player = null;

            Move();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            _anim.SetTrigger("Hit");
            _monsterHP-=collision.gameObject.GetComponent<Bullet>().bulletDamage;
        }
    }

    public virtual void Move()
    {
        StartCoroutine(MoveRandomly());
    }

    private IEnumerator MoveRandomly()
    {
        while (true)
        {
            if (_isFollowing)
            {
                break;
            }
            // 랜덤 방향과 랜덤 초를 받는다
            float randSec = Random.Range(0.5f, 3);
            Vector3 randDir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized;
            _rb.velocity = randDir * _moveSpeed;
            yield return new WaitForSeconds(randSec);
        }
    }

    public abstract void Attack();

    private bool HasParameter(Animator anim, string name) // 이거 사용자 정의 함수로 따로 보관하자
    {
        bool hasParameter = false;

        foreach (AnimatorControllerParameter param in anim.parameters)
        {
            if (param.name == name)
            { 
                hasParameter = true;
            }
        }

        return hasParameter;
    }


}
