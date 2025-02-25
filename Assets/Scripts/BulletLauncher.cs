using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLauncher : MonoBehaviour
{
    public BulletPool bulletPool;
    public Transform spawnPos; // �ҷ� ���� ��ġ
    public Transform targetPos; //�ҷ� ��ǥ ��ġ
    public Skill curSkill; // ���罺ų

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    // ���������� fixed���� �ϴµ�, ��ǲ�� update���� �ؾ��Ұ� �Ƴ�? �׷�...�Ѿ� �߻�� ��� �Ǵ°ž�

    public void SetBullet() // ���� ��ų�� ���� �ҷ� ����
    {
        bulletPool.bulletPrefab = curSkill.skillBullet;
    }
    public void Shoot() // ��ǲ ���� �޾� �ҷ� �߻�
    {
        targetPos.position = Input.mousePosition;
        Debug.Log("�߻�!");
    }

    public void SetTargetPos()
    {
        if (curSkill.skillName == "SpoonBender")
        {
            // �� ����
            return;
        }
        targetPos.position = Input.mousePosition;
    }
}
