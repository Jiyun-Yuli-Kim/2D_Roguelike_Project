using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : Skill
{
    public override void Activate(BulletLauncher launcher)
    {
        launcher.curSkill = this;
        launcher.bulletPool = launcher.PBulletPool; 
        launcher.coolTime = this.bulletCoolTime;
    }

}
