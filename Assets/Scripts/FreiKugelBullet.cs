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
    private float _estTime; // Ÿ�ٱ��� ���ư��µ� �ɸ� ������ �����Ǵ� �ð�
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
        Vector3 initDir = (mousePos - this.transform.position).normalized; // ���콺 ��ǲ�� ���� ��ǥ����
        _rb.velocity = initDir * bulletSpeed;
        _estTime = (target.transform.position - origin).magnitude / bulletSpeed;
        //_estTime = (new Vector3(27.16f, 26.17f, 0) - origin).magnitude / bulletSpeed;


    }
}
