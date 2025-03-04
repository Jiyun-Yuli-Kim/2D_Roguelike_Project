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
    public Vector3 moveDirection { get; private set; }
    public Vector3 orientation { get; private set; }

    public BulletLauncher launcher;

    public Action OnPlayerSpawned;
            
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<Animator>();
        orientation = new Vector3(0, -1, 0);
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
        moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"),0).normalized;
        if (moveDirection == Vector3.zero)
        {
            _rb.velocity = Vector3.zero;
            return;
        }

        orientation = moveDirection; // Input이 있을 때만 플레이어의 방향 갱신

        _rb.velocity =  moveDirection * _playerSpeed;
        _playerAnimator.SetFloat("MoveX", _rb.velocity.x);
        _playerAnimator.SetFloat("MoveY", _rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 스킬 획득
        if (collision.gameObject.CompareTag("Skill"))
        {
            if (collision.gameObject.name == "PowerUp" && launcher.curSkillName != "PowerUp") // 현재 스킬이 PowerUp이 아닐 때에만 돌입
            {
                launcher.curSkill.Deactivate(launcher);
                launcher.powerUpSkil.Activate(launcher);
            }

            else if (collision.gameObject.name == "FreiKugel" && launcher.curSkillName != "FreiKugel") // 이름으로 판정하므로 이름 설정시 주의!
            {
                launcher.curSkill.Deactivate(launcher);
                collision.gameObject.GetComponent<Skill>().Activate(launcher);
            }

            Destroy(collision.gameObject);
        }
    }
}
