using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Monster
{
    [SerializeField] private GameObject _monBullet;
    [SerializeField] private float _monBulletSpeed;

    public override void Attack()
    {
        if (!_isAttacking)
        {
            StartCoroutine(AttackCoroutine());
        }
    }

    private IEnumerator AttackCoroutine()
    {
        _isAttacking = true;
        _anim.SetTrigger("Attack");
        var bullet1 = Instantiate(_monBullet, transform.position, Quaternion.identity);
        var bullet2 = Instantiate(_monBullet, transform.position, Quaternion.identity);
        var bullet3 = Instantiate(_monBullet, transform.position, Quaternion.identity);
        var bullet4 = Instantiate(_monBullet, transform.position, Quaternion.identity);

        bullet1.GetComponent<MonsterBullet>().DirectionalShoot(Vector3.up);
        bullet2.GetComponent<MonsterBullet>().DirectionalShoot(Vector3.down);
        bullet3.GetComponent<MonsterBullet>().DirectionalShoot(Vector3.left);
        bullet4.GetComponent<MonsterBullet>().DirectionalShoot(Vector3.right);

        yield return new WaitForSeconds(_attackCoolTime);
        _isAttacking = false;
    }
}

