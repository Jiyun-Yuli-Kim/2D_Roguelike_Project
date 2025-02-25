using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLauncher : MonoBehaviour
{
    public BulletPool bulletPool;
    public Transform spawnPos; // 불렛 생성 위치
    public Transform targetPos; //불렛 목표 위치
    public Skill curSkill; // 현재스킬

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    // 물리연산은 fixed에서 하는데, 인풋은 update에서 해야할거 아냐? 그럼...총알 발사는 어떻게 되는거야

    public void SetBullet() // 현재 스킬에 따른 불렛 설정
    {
        bulletPool.bulletPrefab = curSkill.skillBullet;
    }
    public void Shoot() // 인풋 직접 받아 불렛 발사
    {
        targetPos.position = Input.mousePosition;
        Debug.Log("발사!");
    }

    public void SetTargetPos()
    {
        if (curSkill.skillName == "SpoonBender")
        {
            // 적 추적
            return;
        }
        targetPos.position = Input.mousePosition;
    }
}
