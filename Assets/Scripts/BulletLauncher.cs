using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLauncher : MonoBehaviour
{
    public BulletPool bulletPool;
    public Transform spawnPos; // 불렛 생성 위치
    public Transform targetPos; //불렛 목표 위치
    public Skill curSkill; // 현재스킬

    public void SetBullet() // 현재 스킬에 따른 불렛 설정
    {
        bulletPool.bulletPrefab = curSkill.skillBullet;
    }
    public void Shoot() // 인풋 직접 받아 불렛 발사
    { 
        
    }
}
