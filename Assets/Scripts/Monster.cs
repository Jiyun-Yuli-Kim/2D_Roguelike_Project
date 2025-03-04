using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _followSpeed;

    private Rigidbody2D _rb;
    private Collider2D _col;
    private bool _isFollowing;
    private PlayerController _player;

    private SpriteRenderer _spriteRenderer;

    private Animator _anim; 

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<Collider2D>();
        Debug.Log("0");
        _anim = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        Move();
    }

    private void Update()
    {
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
