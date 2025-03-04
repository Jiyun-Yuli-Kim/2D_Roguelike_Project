using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultSkill : Skill
{
    public override void Activate(BulletLauncher launcher)
    {
        launcher.curSkill = this;
        launcher.bulletPool = launcher.DBulletPool;
        launcher.coolTime = this.bulletCoolTime;
    }
}
