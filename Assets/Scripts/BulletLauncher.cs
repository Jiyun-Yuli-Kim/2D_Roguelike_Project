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
        bullet.ToTarget(transform.position, targetPos);
        Debug.Log("�߻�!");
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
