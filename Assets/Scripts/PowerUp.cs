using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : Skill
{
    public override void Activate(BulletLauncher launcher)
    {
        //launcher.bulletPool = bulletPool;
        launcher.coolTime = launcher.curSkill.bulletCoolTime;
    }

}
