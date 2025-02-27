using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    public string skillName;
    public float bulletSpeed;
    public float bulletDamage;
    public float bulletCoolTime;
    public CustomPool<Bullet> bulletPool = new(15);
    public GameObject bulletPrefab;

    protected void Awake()
    {
        for (int i = 0; i < bulletPool.size; i++)
        {
            Bullet bullet = Instantiate(bulletPrefab).GetComponent<Bullet>();
            bullet.Init(bulletPool);
            bulletPool.Return(bullet);
        }
    }

    protected void Start()
    {
    }

    public abstract void Activate(BulletLauncher launcher);
    
    public virtual void Deactivate()
    { 
        // ���� ��Ȱ��ȭ ����
    }

    protected void ReturntoPool(Bullet bullet)
    {
        bulletPool.Return(bullet);
    }

}
