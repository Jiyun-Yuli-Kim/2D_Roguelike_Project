using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    public string skillName;
    public float bulletFrequency;
    public float bulletSpeed;
    public float bulletDamage;
    public GameObject skillBullet; // ��ų�� ���� �ٸ� ������

    public abstract void Activate();
    public virtual void Deactivate()
    { 
        // ���� ��Ȱ��ȭ ����
    }
    
    
}
