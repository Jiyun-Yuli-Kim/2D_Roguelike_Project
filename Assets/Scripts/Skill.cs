using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    public string skillName;
    public float bulletCoolTime;
    public float bulletSpeed;
    public float bulletDamage;
    public GameObject bulletPrefab;
    public SpriteRenderer sRenderer;
    // public Sprite skillBulletImage; // ��ų�� ���� �ٸ� �̹���
    public Animator skillBulletAnimator; 
    public float skillColSize;

    private void Awake()
    {
        sRenderer.sprite = bulletPrefab.GetComponent<Sprite>();
    }

    public abstract void Activate();
    public virtual void Deactivate()
    { 
        // ���� ��Ȱ��ȭ ����
    }
    
    
}
