using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLauncher : MonoBehaviour
{
    public CustomPool<Bullet> BulletPool = new(15);
    public Transform spawnPos; // 불렛 생성 위치
    public Vector3 targetPos; //불렛 목표 위치
    public Skill curSkill; // 현재스킬
    public GameObject bulletPrefab;

    private void Start()
    {
        for (int i = 0; i < BulletPool.size; i++) 
        {
            Bullet bullet= Instantiate(bulletPrefab).GetComponent<Bullet>();
            bullet.OnBulletHitWall += Return;
            bullet.OnBulletHitEnemy += Return;
            BulletPool.Return(bullet);
        }
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
    }

    void Return(Bullet bullet)
    {
        BulletPool.Return(bullet);
    }

    // 스킬에 따라 불렛이 다른 양상으로 움직여야 하는데 이걸 어떻게 구현할지(스킬과 오브젝트풀 연동의 문제)

    //public void SetBullet() // 현재 스킬에 따른 불렛 설정
    //{
    //    bulletPrefab = curSkill.skillBullet;
    //}
    public void Shoot() // 인풋 직접 받아 불렛 발사
    {
        Vector3 targetPos = SetTargetPos();
        // Debug.Log($"목표지점(마우스 위치) : {targetPos.x}, {targetPos.y}");
        Bullet bullet = BulletPool.Get();
        bullet.transform.position = transform.position;
        // bullet이 활성화될 때 프리팹을 바꿔주면 됨.
        var prefab = bullet.GetComponent<GameObject>();
        prefab = bulletPrefab;
        // Debug.Log($"시작지점(총알 위치) : {bullet.transform.position.x}, {bullet.transform.position.y}");
        bullet.ToTarget(targetPos);
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
