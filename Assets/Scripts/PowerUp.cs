using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : Skill
{
    //public string skillName;
    //public float bulletCoolTime;
    //public float bulletSpeed;
    //public float bulletDamage;
    //public Animator skillBulletAnimator;
    //public float skillColSize;
    //public CustomPool<Bullet> bulletPool;
    //public GameObject bulletPrefab;

    private void Awake()
    {
        for (int i = 0; i < bulletPool.size; i++)
        {
            Bullet bullet = Instantiate(bulletPrefab).GetComponent<Bullet>();
            bullet.OnBulletHitWall += ReturntoPool;
            bullet.OnBulletHitEnemy += ReturntoPool;
            bulletPool.Return(bullet);
        }
    }

    public override void Activate()
    { 
        
    }

}
