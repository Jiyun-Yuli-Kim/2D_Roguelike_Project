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
    // private StateMachine _stateMachine;
    public bool hasGun; // UI 연동
    public bool hasSpear; // UI 연동
    // public List<IWeapon> Weapons;
    public IWeapon _weapon;
    public Weapons _weapons;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        // _stateMachine = GetComponent<StateMachine>();
        _playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        _playerAnimator.SetFloat("Speed", _rb.velocity.magnitude);
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GetGun();
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GetSpear();
        }

        if (Input.GetMouseButtonDown(0))
        {
            PlayerAttack();
        }
    }

    void FixedUpdate()
    {
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

    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter");
        if (other.gameObject.CompareTag("Gun"))
        {
            Debug.Log("총 줍줍");
            hasGun = true;
            GetGun();
            Destroy(other.gameObject, 0.5f);
        }
        
        else if (other.gameObject.CompareTag("Spear"))
        {
            Debug.Log("창 줍줍");
            hasSpear = true;
            GetSpear();
            Destroy(other.gameObject, 0.5f);
        }
    }

    private void GetGun()
    {
        if (!hasGun)
        {
            return;
        }

        _weapons = Weapons.Gun;
    }

    private void GetSpear()
    {
        if (!hasSpear)
        {
            return;
        }
        
        _weapons = Weapons.Spear;
    }

    void PlayerAttack()
    {
        if (!hasGun && !hasSpear)
        {
            return;
        }

        if (_weapons == Weapons.Gun)
        {
            _playerAnimator.SetTrigger("Shoot");
        }
        
        else if (_weapons == Weapons.Spear)
        {
            _playerAnimator.SetTrigger("Spear");
        }
    }

    // void ChangeWeapon()
    // {
    //     if (!hasBow) // 활 스킬을 습득하지 못했을 때 
    //     {
    //         return;
    //     }
    //
    //     if (hasBow && _stateMachine.CurrentState is PlayerSword) //현재 기본상태(소드)일때
    //     {
    //         _stateMachine.OnChangeState(StateMachine.StateType.PBow);
    //         return;
    //     }
    //
    //     if (hasBow && _stateMachine.CurrentState is PlayerBow)
    //     {
    //         _stateMachine.OnChangeState(StateMachine.StateType.PSword);
    //     }
    // }
}
