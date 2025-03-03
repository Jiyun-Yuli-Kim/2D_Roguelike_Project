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
            //Debug.Log($"����ð� : {_curTime}");
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
        Vector3 initDir = (mousePos - this.transform.position).normalized; // ���콺 ��ǲ�� ���� ��ǥ����
        _rb.velocity = initDir * bulletSpeed;
        _estTime = MathF.Abs((target.transform.position - origin).magnitude) / bulletSpeed;
        //Debug.Log($"���ؽð� : {_estTime/2}");
    }
}
