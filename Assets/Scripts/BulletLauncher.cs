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

    private PlayerController _player;

    private void Awake()
    {
    }

    private void Start()
    {
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
        bullet.ToTarget(transform.position, targetPos);
        Debug.Log("발사!");
    }

    // 비활성화는 어떻게?

    public void Track()
    { 
    
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
