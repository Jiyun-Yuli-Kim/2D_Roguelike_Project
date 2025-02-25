using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    public string skillName;
    public float bulletFrequency;
    public float bulletSpeed;
    public float bulletDamage;
    public GameObject skillBullet; // 스킬에 따라 다른 프리팹

    public abstract void Activate();
    public virtual void Deactivate()
    { 
        // 공통 비활성화 로직
    }
    
    
}
