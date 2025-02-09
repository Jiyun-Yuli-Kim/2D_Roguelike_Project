using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _playerSpeed;
    private Animator _playerAnimator;
    private Rigidbody2D _rb;
    private Vector2 _moveDirection;
    private StateMachine _stateMachine;
    public bool hasBow;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _stateMachine = GetComponent<StateMachine>();
        _playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        _moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        _playerAnimator.SetFloat("Speed", _rb.velocity.magnitude);
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeWeapon();
        }
    }

    void FixedUpdate()
    {
        if (_moveDirection == Vector2.zero)
        {
            _rb.velocity = Vector2.zero;
            return;
        }

        _rb.velocity =  _moveDirection * _playerSpeed;
        _playerAnimator.SetFloat("MoveX", _rb.velocity.x);
        _playerAnimator.SetFloat("MoveY", _rb.velocity.y);
    }

    void ChangeWeapon()
    {
        if (!hasBow) // 활 스킬을 습득하지 못했을 때 
        {
            return;
        }

        if (hasBow && _stateMachine.CurrentState is PlayerSword) //현재 기본상태(소드)일때
        {
            _stateMachine.OnChangeState(StateMachine.StateType.PBow);
            return;
        }

        if (hasBow && _stateMachine.CurrentState is PlayerBow)
        {
            _stateMachine.OnChangeState(StateMachine.StateType.PSword);
        }
    }

}
