using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FreiKugelBullet : Bullet
{
    //public float bulletSpeed;
    //public float bulletDamage;
    //public float bulletCoolTime;
    //private CustomPool<Bullet> bulletPool;
    //public Animator bulletAnimator;

    //private Rigidbody2D _rb;
    //private Collider2D _col;
    private Vector3 _origin;
    private bool _isActive;
    private float _estTime; // 타겟까지 날아가는데 걸릴 것으로 추정되는 시간
    private float _curTime;

    //public Monster target;



    void Update()
    {
        if (!_isActive)
        {
            return;
        }

        _curTime += Time.deltaTime;
        
        if (_curTime > _estTime / 3)
        {
            Vector3 targetDir = (target.transform.position - _origin).normalized;
            //Vector3 targetDir = (new Vector3(27.16f, 26.17f, 0) - _origin).normalized;
            Vector3 newDir = targetDir + _origin;
            _rb.velocity = newDir * bulletSpeed;
        }
    }

    public override void ToTarget(Vector3 origin, Vector3 mousePos)
    {
        _isActive = true;
        _origin = origin.normalized;
        Vector3 initDir = (mousePos - this.transform.position).normalized; // 마우스 인풋에 따른 목표방향
        _rb.velocity = initDir * bulletSpeed;
        _estTime = (target.transform.position - origin).magnitude / bulletSpeed;
        //_estTime = (new Vector3(27.16f, 26.17f, 0) - origin).magnitude / bulletSpeed;


    }
}
