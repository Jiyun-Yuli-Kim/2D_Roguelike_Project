using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreiKugel : Skill
{
    private void Awake()
    {
        skillName = "FreiKugel";
    }
    public override void Activate(BulletLauncher launcher)
    {
        //launcher.bulletPool = bulletPool;
        launcher.coolTime = launcher.curSkill.bulletCoolTime;
    }
}
