using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneTemplate;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Timeline;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _playerSpeed;
    private Animator _playerAnimator;
    private Rigidbody2D _rb;
    public Vector3 moveDirection { get; private set; }
    public Vector3 orientation { get; private set; }

    [SerializeField] private int _maxHp = 10;
    public ObservableProperty<int> PlayerHP;
    
    public BulletLauncher launcher;
    
    private void Awake() => Init();

    private void OnEnable()
    {
        GameManager.Instance.player = this;
        PlayerHP.Value = 10;
        PlayerHP.Subscribe(CheckPlayerHP);
    }
    
    private void OnDisable()
    {
        GameManager.Instance.player = null;
        PlayerHP.Unsubscribe(CheckPlayerHP);
    }

    void FixedUpdate()
    {
        // TODO : 입력 및 애니메이션 관련 로직은 프레임마다 검사하고 처리하던지
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

    // private void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if (collision.gameObject.CompareTag("MonsterBullet"))
    //     {
    //         TakeDamage(1);
    //         
    //         //playerHP--;
    //         // 피격 애니메이션 또는 이펙트 추가
    //     }
    // }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var pickupable = collision.GetComponent<IPickupable>();
        
        // 스킬 획득
        if (pickupable != null)
        {
            if (collision.gameObject.CompareTag("Skill"))
            {
                if (collision.gameObject.name == "PowerUp" &&
                    launcher.curSkillName != "PowerUp") // 현재 스킬이 PowerUp이 아닐 때에만 돌입
                {
                    launcher.curSkill.Deactivate(launcher);
                    launcher.powerUpSkill.Activate(launcher);
                }

                else if (collision.gameObject.name == "FreiKugel" &&
                         launcher.curSkillName != "FreiKugel") // 이름으로 판정하므로 이름 설정시 주의!
                {
                    launcher.curSkill.Deactivate(launcher);
                    launcher.freiKugelSkill.Activate(launcher);
                }

                Destroy(collision.gameObject);
            }
            
            // 열쇠 획득
            else if (collision.gameObject.CompareTag("Key"))
            {
                
            }
        }
    }

    private void Update()
    {
        SetMove();
    }

    private Vector3 GetNormalizedDirection()
    {
        // 임시로 둔 값임
        Vector3 dir = Vector3.zero;

        return dir;
    }

    private void SetMove()
    {
        Vector3 dir = GetNormalizedDirection();
        
        // 입력이 된건 맞는지 검사하고 != Vector3.zero
        
        // velocity set 로직
        
        // 애니메이션 변경
    }

    public void TakeDamage(int damage)  // Monster에서 호출됨
    {
        PlayerHP.Value -= damage;
        // Debug.Log(PlayerHP.Value);
    }

    private void CheckPlayerHP(int hp)
    {
        if (PlayerHP.Value <= 0)
        {
            PlayerCharacterDie();
        }
        
        // TODO: 체력 변화에 따른 이벤트가 있다면 여기서 추가하면 됨
        // Ex> 체력이 가득차면 특정 효과를 켠다던지
        //      체력이 50%이하로 떨어지면 힘들어하는 이펙트를 켠다던지
    }

    private void PlayerCharacterDie()
    {
        _playerAnimator.SetBool("Die", true);
    }
    
    private void Init()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<Animator>();
        orientation = new Vector3(0, -1, 0);
        PlayerHP = new ObservableProperty<int>(_maxHp);
    }
}
