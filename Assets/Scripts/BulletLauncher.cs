using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLauncher : MonoBehaviour
{
    public CustomPool<Bullet> bulletPool;
    public Transform spawnPos; // �ҷ� ���� ��ġ
    public Vector3 targetPos; //�ҷ� ��ǥ ��ġ
    public Skill curSkill; // ���罺ų
    [SerializeField] private Skill defaultSkill;
    public float coolTime; // �Ѿ� �߻� �� ��Ÿ�� ���� 
    public GameObject bulletPrefab;

    private void Awake()
    {
        curSkill = defaultSkill;
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

    // ��ų�� ���� �ҷ��� �ٸ� ������� �������� �ϴµ� �̰� ��� ��������(��ų�� ������ƮǮ ������ ����)

    //public void SetBullet() // ���� ��ų�� ���� �ҷ� ����
    //{
    //    bulletPrefab = curSkill.skillBullet;
    //}
    public void Shoot() // ��ǲ ���� �޾� �ҷ� �߻�
    {
        if (coolTime < curSkill.bulletCoolTime)
        {
            return;
        }

        coolTime = 0;
        Vector3 targetPos = SetTargetPos();
        // Debug.Log($"��ǥ����(���콺 ��ġ) : {targetPos.x}, {targetPos.y}");
        Bullet bullet = bulletPool.Get();
        bullet.transform.position = transform.position;
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
