using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public abstract class Skill : MonoBehaviour, IPickupable
{
    public string skillName;
    public float bulletSpeed;
    public float bulletDamage;
    public float bulletCoolTime;

    public void OnPickup(PlayerController player)
    {
        Activate(player.launcher);
        Destroy(gameObject);
    }
    
    public abstract void Activate(BulletLauncher launcher);

    public virtual void Deactivate(BulletLauncher launcher)
    { 
        // 공통 비활성화 로직
    }
    
}
