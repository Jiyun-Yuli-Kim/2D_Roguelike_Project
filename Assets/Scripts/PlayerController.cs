using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _playerSpeed;
    private Animator _playerAnimator;
    private Rigidbody2D _rb;
    private Vector2 _moveDirection;
    public BulletLauncher launcher;
            
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    GetGun();
        //}

        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    GetSpear();
        //}

        //if (Input.GetMouseButtonDown(0))
        //{
        //    PlayerAttack();
        //}
    }

    void FixedUpdate()
    {
        _playerAnimator.SetFloat("Speed", _rb.velocity.magnitude);
        _moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        if (_moveDirection == Vector2.zero)
        {
            _rb.velocity = Vector2.zero;
            return;
        }

        _rb.velocity =  _moveDirection * _playerSpeed;
        _playerAnimator.SetFloat("MoveX", _rb.velocity.x);
        _playerAnimator.SetFloat("MoveY", _rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Skill"))
        { 
            launcher.curSkill = gameObject.GetComponent<Skill>();
            Destroy(collision.gameObject);
        }
    }
}
