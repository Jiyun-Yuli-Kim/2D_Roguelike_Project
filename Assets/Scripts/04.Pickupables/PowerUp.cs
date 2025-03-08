using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : Skill
{
    private void Awake()
    {
        skillName = "PowerUp";
    }
 
    public override void Activate(BulletLauncher launcher)
    {
        launcher.curSkill = this;
        launcher.bulletPool = launcher.PBulletPool; 
        launcher.coolTime = bulletCoolTime;
    }

}
