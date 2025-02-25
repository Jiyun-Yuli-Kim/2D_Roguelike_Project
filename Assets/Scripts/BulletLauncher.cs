using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLauncher : MonoBehaviour
{
    public BulletPool bulletPool;
    public Transform spawnPos; // �ҷ� ���� ��ġ
    public Transform targetPos; //�ҷ� ��ǥ ��ġ
    public Skill curSkill; // ���罺ų

    public void SetBullet() // ���� ��ų�� ���� �ҷ� ����
    {
        bulletPool.bulletPrefab = curSkill.skillBullet;
    }
    public void Shoot() // ��ǲ ���� �޾� �ҷ� �߻�
    { 
        
    }
}
