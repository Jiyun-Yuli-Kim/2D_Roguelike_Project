using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class Monster : MonoBehaviour
{
    protected string _monName;
    
    [SerializeField] protected float _moveSpeed;
    [SerializeField] protected float _followSpeed;
    [SerializeField] protected float _attackRange;

    [SerializeField] protected float _attackCoolTime;
    [SerializeField] protected float _monsterHP;
    public float MonsterHP
    {
        get => _monsterHP;
        set
        {
            _monsterHP = value;
            CheckMonsterHP();
        }
    }

    protected Rigidbody2D _rb;
    protected Collider2D _col;
    protected bool _isFollowing;
    protected bool _isAttacking;
    protected PlayerController _player;
    
    // protected InGameUI _ui;

    protected SpriteRenderer _spriteRenderer;

    protected Animator _anim; 

    private void Awake()
    {
        // _rb = GetComponent<Rigidbody2D>();
        // Debug.Log(_rb);
        // // _col = GetComponent<Collider2D>();
        // _anim = GetComponent<Animator>();
        // Debug.Log(_anim);
        // _spriteRenderer = GetComponent<SpriteRenderer>();
        // Debug.Log(_spriteRenderer);
    }

    private void OnEnable()
    {
        _rb = GetComponent<Rigidbody2D>();
        Debug.Log(_rb);
        // _col = GetComponent<Collider2D>();
        _anim = GetComponent<Animator>();
        Debug.Log(_anim);
        _spriteRenderer = GetComponent<SpriteRenderer>();
        Debug.Log(_spriteRenderer);
        GameManager.Instance.setter.MonsterCount.Value++;
    }
    
    private void OnDisable()
    {
        GameManager.Instance.setter.MonsterCount.Value--;
    }

    private void Start()
    {
        Move();
    }

    private void Update()
    {
        SetSpriteDirection();
        SetAnimSpeed();
        FollowPlayer();
        
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

    private void SetSpriteDirection() // 스프라이트 이미지의 좌우 반전을 처리한다
    {
        if (_rb.velocity.x < -0.3)
        {
            _spriteRenderer.flipX = true;
        }
        
        else if (_rb.velocity.x > 0.3)
        {
            _spriteRenderer.flipX = false;
        }
    }

    private void FollowPlayer()
    {
        if (!_isFollowing)
        {
            return;
        }

        Vector3 dir = (_player.transform.position - transform.position).normalized;
        _rb.velocity = dir * _followSpeed;
    }

    private void SetAnimSpeed()
    {
        if (_monName == "Orc") // 몬스터의 속도에 따라 애니메이션의 변화가 있는 경우
        {
            _anim.SetFloat("Speed", _rb.velocity.magnitude);
        }
    }

    public void CheckMonsterHP()
    {
        if (MonsterHP <= 0)
        {
            _anim.SetTrigger("Hit");
            Destroy(gameObject, 1f);
        }
    }

    public void GetDamage(float damage)
    {
        _anim.SetTrigger("Hit");
        MonsterHP -= damage; // 불렛의 데미지를 받아와서 적용
        Debug.Log(damage);
        Debug.Log(MonsterHP);
    }

    public abstract void Attack();
}
