using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using UnityEngine.PlayerLoop;

public class FreiKugelBullet : Bullet
{
    //public float bulletSpeed;
    //public float bulletDamage;
    //public float bulletCoolTime;
    //private CustomPool<Bullet> bulletPool;
    //public Animator bulletAnimator;

    //private Rigidbody2D _rb;
    //private Collider2D _col;
    private bool _isActive;
    private float _estTime; // 타겟까지 날아가는데 걸릴 것으로 추정되는 시간
    private float _curTime; // 불렛이 활성화된 시간

    //public Monster target;


    private void OnEnable()
    {
        _isActive = true;
        ResetStat();
    }

    private void OnDisable()
    {
        _isActive = false;
        ResetStat();
    }

    void Update()
    {
        _curTime += Time.deltaTime;
    }

    private void ResetStat()
    {
        _curTime = 0;
    }


    public override void ToTarget(Vector3 origin, Vector3 mousePos)
    {
        if (target == null)
        {
            _rb.velocity = mousePos.normalized * bulletSpeed;
        }

        else if (target != null)
        {
            StartCoroutine(SetBulletPos(origin, mousePos));
        }
    }

    private IEnumerator SetBulletPos(Vector3 origin, Vector3 mousePos)
    {
        Vector3 initDir = (mousePos - this.transform.position).normalized; // 마우스 인풋에 따른 목표방향
        _rb.velocity = initDir * bulletSpeed;
        _estTime = MathF.Abs((target.transform.position - origin).magnitude) / bulletSpeed;
        
        yield return new WaitForSeconds(_estTime/3);
        
        while (_isActive&&target!=null)
        {
            Vector3 targetDir = (target.transform.position - this.transform.position).normalized;
            Vector3 straightDir = (target.transform.position - origin).normalized;
            Vector3 newDir = targetDir + straightDir;
            _rb.velocity = newDir * bulletSpeed;
            yield return null;
        }
    }
}
