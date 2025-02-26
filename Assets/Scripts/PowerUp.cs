using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : Skill
{
    //public string skillName = "PowerUp";
    //public float bulletCoolTime;
    //public float bulletSpeed;
    //public float bulletDamage;
    //public GameObject skillBullet; // 스킬에 따라 다른 프리팹

    public override void Activate()
    { 
        
    }
    public virtual void Deactivate()
    {
        // 공통 비활성화 로직
    }
}
