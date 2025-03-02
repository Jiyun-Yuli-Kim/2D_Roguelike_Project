using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLauncher : MonoBehaviour
{
    public CustomPool<Bullet> bulletPool;
    public Transform spawnPos; // 불렛 생성 위치
    public Vector3 targetPos; //불렛 목표 위치
    public Skill curSkill; // 현재스킬
    public float attackRange = 55;
    private DefaultSkill defaultSkill;
    public float coolTime; // 총알 발사 후 쿨타임 측정 
    public GameObject bulletPrefab;

    public float radius;
    private LayerMask _targetLayer; 

    private PlayerController _player;

    private void Awake()
    {
    }

    private void Start()
    {
        _targetLayer = LayerMask.GetMask("Enemy");
        // defaultSkill = GetComponent<DefaultSkill>();
        _player = GetComponentInParent<PlayerController>();
        bulletPool = curSkill.bulletPool;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //if (curSkill.skillName == "SpoonBender")
            //{
            //    Track();
            //}
            Shoot();
        }

        coolTime += Time.deltaTime;
    }

    // 스킬에 따라 불렛이 다른 양상으로 움직여야 하는데 이걸 어떻게 구현할지(스킬과 오브젝트풀 연동의 문제)

    //public void SetBullet() // 현재 스킬에 따른 불렛 설정
    //{
    //    bulletPrefab = curSkill.skillBullet;
    //}
    public void Shoot() // 인풋 직접 받아 불렛 발사
    {
        if (coolTime < curSkill.bulletCoolTime)
        {
            return;
        }

        Vector3 targetPos = SetTargetPos(); // 마우스 현재 위치를 월드좌표로 가져옴
        Vector3 attackDir = targetPos - _player.transform.position; // 플레이어 기준 공격 방향
        float curAngle = Vector3.SignedAngle(_player.orientation, attackDir, _player.orientation);
        //Debug.Log(_player.orientation);
        //Debug.Log(attackDir);
        //Debug.Log(curAngle);

        // 각도를 계산해서 공격범위 밖의 입력은 무시
        if (curAngle > attackRange)
        {
            Debug.Log("공격범위 밖입니다.");
            return;
        }

        coolTime = 0;
        Bullet bullet = bulletPool.Get();
        bullet.transform.position = transform.position; // 현재 플레이어 위치에 불렛 활성화
        
        if (curSkill.name == "FreiKugel")
        {
            bullet.target = GetNearestMonster();
        }

        bullet.ToTarget(transform.position, targetPos);
        Debug.Log("발사!");
    }

    // 비활성화는 어떻게?

    public Monster GetNearestMonster()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, radius, _targetLayer); // 특정 범위 내의 적을 모두 반환
        Monster[] mons = new Monster[cols.Length];

        for (int i = 0; i < cols.Length; i++)
        {
            mons[i] = cols[i].GetComponent<Monster>();
        }

        Monster Mon = mons[0];
        float distance = (new Vector3(1000, 1000, 0) - transform.position).magnitude; // 임의의 큰 값을 성정

        for (int i = 0; i < mons.Length; i++)
        {
            if ((mons[i].transform.position - transform.position).magnitude < distance)
            { 
                distance = (mons[i].transform.position - transform.position).magnitude; // 더 작은 값
                Mon = mons[i];
            }
        }
        return Mon;
    }

    public Vector3 SetTargetPos()
    {
        //if (curSkill.skillName == "SpoonBender")
        //{
        //    // 적 추적
        //    return Vector2.zero;
        //}
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;
        return mouseWorldPos;
    }
}
