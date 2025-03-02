using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLauncher : MonoBehaviour
{
    public CustomPool<Bullet> bulletPool;
    public Transform spawnPos; // �ҷ� ���� ��ġ
    public Vector3 targetPos; //�ҷ� ��ǥ ��ġ
    public Skill curSkill; // ���罺ų
    public float attackRange = 55;
    private DefaultSkill defaultSkill;
    public float coolTime; // �Ѿ� �߻� �� ��Ÿ�� ���� 
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

        Vector3 targetPos = SetTargetPos(); // ���콺 ���� ��ġ�� ������ǥ�� ������
        Vector3 attackDir = targetPos - _player.transform.position; // �÷��̾� ���� ���� ����
        float curAngle = Vector3.SignedAngle(_player.orientation, attackDir, _player.orientation);
        //Debug.Log(_player.orientation);
        //Debug.Log(attackDir);
        //Debug.Log(curAngle);

        // ������ ����ؼ� ���ݹ��� ���� �Է��� ����
        if (curAngle > attackRange)
        {
            Debug.Log("���ݹ��� ���Դϴ�.");
            return;
        }

        coolTime = 0;
        Bullet bullet = bulletPool.Get();
        bullet.transform.position = transform.position; // ���� �÷��̾� ��ġ�� �ҷ� Ȱ��ȭ
        
        if (curSkill.name == "FreiKugel")
        {
            bullet.target = GetNearestMonster();
        }

        bullet.ToTarget(transform.position, targetPos);
        Debug.Log("�߻�!");
    }

    // ��Ȱ��ȭ�� ���?

    public Monster GetNearestMonster()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, radius, _targetLayer); // Ư�� ���� ���� ���� ��� ��ȯ
        Monster[] mons = new Monster[cols.Length];

        for (int i = 0; i < cols.Length; i++)
        {
            mons[i] = cols[i].GetComponent<Monster>();
        }

        Monster Mon = mons[0];
        float distance = (new Vector3(1000, 1000, 0) - transform.position).magnitude; // ������ ū ���� ����

        for (int i = 0; i < mons.Length; i++)
        {
            if ((mons[i].transform.position - transform.position).magnitude < distance)
            { 
                distance = (mons[i].transform.position - transform.position).magnitude; // �� ���� ��
                Mon = mons[i];
            }
        }
        return Mon;
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
