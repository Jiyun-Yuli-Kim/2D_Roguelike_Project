using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLauncher : MonoBehaviour
{
    public CustomPool<Bullet> BulletPool = new(15);
    public Transform spawnPos; // �ҷ� ���� ��ġ
    public Vector3 targetPos; //�ҷ� ��ǥ ��ġ
    public Skill curSkill; // ���罺ų
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

    // ��ų�� ���� �ҷ��� �ٸ� ������� �������� �ϴµ� �̰� ��� ��������(��ų�� ������ƮǮ ������ ����)

    //public void SetBullet() // ���� ��ų�� ���� �ҷ� ����
    //{
    //    bulletPrefab = curSkill.skillBullet;
    //}
    public void Shoot() // ��ǲ ���� �޾� �ҷ� �߻�
    {
        Vector3 targetPos = SetTargetPos();
        // Debug.Log($"��ǥ����(���콺 ��ġ) : {targetPos.x}, {targetPos.y}");
        Bullet bullet = BulletPool.Get();
        bullet.transform.position = transform.position;
        // bullet�� Ȱ��ȭ�� �� �������� �ٲ��ָ� ��.
        var prefab = bullet.GetComponent<GameObject>();
        prefab = bulletPrefab;
        // Debug.Log($"��������(�Ѿ� ��ġ) : {bullet.transform.position.x}, {bullet.transform.position.y}");
        bullet.ToTarget(targetPos);
    }

    // ��Ȱ��ȭ�� ���?

    public void Track()
    { 
    
    }

    public Vector3 SetTargetPos()
    {
        //if (curSkill.skillName == "SpoonBender")
        //{
        //    // �� ����
        //    return Vector2.zero;
        //}
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;
        return mouseWorldPos;
    }
}
