using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLauncher : MonoBehaviour
{
    public CustomPool<Bullet> bulletPool;
    public Transform spawnPos; // �ҷ� ���� ��ġ
    private Vector3 targetPos; //�ҷ� ��ǥ ��ġ
    public string curSkillName;

    public float attackRange = 55;
    public float coolTime; // �Ѿ� �߻� �� ��Ÿ�� ���� 
    public GameObject bulletPrefab;

    public float radius;
    private LayerMask _targetLayer; 

    private PlayerController _player;

    public CustomPool<Bullet> DBulletPool = new(15); // ����Ʈ ��ų
    public GameObject DBulletPrefab;
    public CustomPool<Bullet> PBulletPool = new(15); // �Ŀ��� ��ų
    public GameObject PBulletPrefab;
    public CustomPool<Bullet> FBulletPool = new(15); // ��ź ��ų
    public GameObject FBulletPrefab;

    public Skill curSkill; // ���罺ų
    private DefaultSkill defaultSkill;
    public PowerUp powerUpSkill;
    public FreiKugel freiKugelSkill;

    private void Awake()
    {
        for (int i = 0; i < DBulletPool.size; i++)
        {
            Bullet bullet = Instantiate(DBulletPrefab).GetComponent<Bullet>();
            bullet.Init(DBulletPool);
            DBulletPool.Return(bullet);
        }
        for (int i = 0; i < PBulletPool.size; i++)
        {
            Bullet bullet = Instantiate(PBulletPrefab).GetComponent<Bullet>();
            bullet.Init(PBulletPool);
            PBulletPool.Return(bullet);
        }
        for (int i = 0; i < FBulletPool.size; i++)
        {
            Bullet bullet = Instantiate(FBulletPrefab).GetComponent<Bullet>();
            bullet.Init(FBulletPool);
            FBulletPool.Return(bullet);
        }

        powerUpSkill = GetComponentInChildren<PowerUp>();
        freiKugelSkill = GetComponentInChildren<FreiKugel>();
        Debug.Log(powerUpSkill);
        Debug.Log(freiKugelSkill);
    }

    private void Start()
    {
        _targetLayer = LayerMask.GetMask("Monster");
        // defaultSkill = GetComponent<DefaultSkill>();
        _player = GetComponentInParent<PlayerController>();
        bulletPool = DBulletPool;
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

        targetPos = SetTargetPos(); // ���콺 ���� ��ġ�� ������ǥ�� ������
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

        if (curSkill.skillName == "FreiKugel")
        {
            Collider2D[] monsters = Physics2D.OverlapCircleAll(targetPos, radius, _targetLayer); // ���콺 ���� Ư�� ���� ���� ���� ��� ��ȯ
            if (monsters.Length > 0)
            {
                bullet.target = GetNearestMonster(monsters);
            }
        }

        bullet.ToTarget(transform.position, targetPos);
    }

    // ��Ȱ��ȭ�� ���?

    public Monster GetNearestMonster(Collider2D[] cols)
    {
        Monster[] mons = new Monster[cols.Length];

        for (int i = 0; i < cols.Length; i++)
        {
            mons[i] = cols[i].GetComponent<Monster>();
            Debug.Log(mons[i]);
        }

        Monster Mon = mons[0];
        float distance = (new Vector3(1000, 1000, 0) - transform.position).magnitude; // ������ ū ���� ����

        for (int i = 0; i < mons.Length; i++)
        {
            if ((mons[i].transform.position - targetPos).magnitude < distance)
            { 
                distance = (mons[i].transform.position - targetPos).magnitude; // �� ���� ��
                Mon = mons[i];
            }
        }
        return Mon;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
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

    public void GetPowerUp()
    {
        curSkill = powerUpSkill;
        curSkill.Activate(this);
        bulletPool = PBulletPool;
    }

    public void GetFreiKugel()
    {
        curSkill = freiKugelSkill;
        curSkill.Activate(this);
        bulletPool = FBulletPool;
    }
}
