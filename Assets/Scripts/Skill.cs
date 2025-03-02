using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    public string skillName;
    public float bulletSpeed;
    public float bulletDamage;
    public float bulletCoolTime;

    protected void Awake()
    {

    }

    protected void Start()
    {
    }

    public abstract void Activate(BulletLauncher launcher);
    
    public virtual void Deactivate()
    { 
        // ���� ��Ȱ��ȭ ����
    }

    //protected void ReturntoPool(Bullet bullet)
    //{
    //    bulletPool.Return(bullet);
    //}

}
