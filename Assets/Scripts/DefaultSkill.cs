using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultSkill : Skill
{
    //public string skillName = "FreiKugel"; // 마탄의사수에나오는....뭐 그런건데 그냥 전공 살리려고 넣어봤,,,,
    //public float bulletCoolTime;
    //public float bulletSpeed;
    //public float bulletDamage;
    //public GameObject skillBullet;

    //private void Awake()
    //{
    //    for (int i = 0; i < bulletPool.size; i++)
    //    {
    //        Bullet bullet = Instantiate(bulletPrefab).GetComponent<Bullet>();
    //        bullet.OnBulletHitWall += ReturntoPool;
    //        bullet.OnBulletHitEnemy += ReturntoPool;
    //        bulletPool.Return(bullet);
    //    }
    //}

    public override void Activate()
    {
        // 구현
    }
}
