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
            //Debug.Log($"현재시각 : {_curTime}");
            Vector3 targetDir = (target.transform.position - this.transform.position).normalized;
            Vector3 straightDir = (target.transform.position - _origin).normalized;
            Vector3 newDir = targetDir + straightDir;
            _rb.velocity = newDir * bulletSpeed;
        }
    }

    public override void ToTarget(Vector3 origin, Vector3 mousePos)
    {
        _isActive = true;
        _origin = origin;
        _curTime = 0;
        Vector3 initDir = (mousePos - this.transform.position).normalized; // 마우스 인풋에 따른 목표방향
        _rb.velocity = initDir * bulletSpeed;
        _estTime = MathF.Abs((target.transform.position - origin).magnitude) / bulletSpeed;
        //Debug.Log($"기준시각 : {_estTime/2}");
    }
}
