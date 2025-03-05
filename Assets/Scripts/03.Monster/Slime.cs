using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Monster
{
    [SerializeField] private GameObject _monBullet;
    [SerializeField] private float _monBulletSpeed;

    public override void Attack()
    {
        if (!_isAttacking)
        {
            Debug.Log("슬라임 공격!");
            StartCoroutine(AttackCoroutine());
        }
    }

    private IEnumerator AttackCoroutine()
    {
        _isAttacking = true;
        _anim.SetTrigger("Attack");
        var bullet = Instantiate(_monBullet, transform.position, Quaternion.identity);
        Debug.Log(bullet);
        bullet.GetComponent<MonsterBullet>().Shoot(_player.transform.position);
        yield return new WaitForSeconds(_attackCoolTime);
        _isAttacking = false;
    }
}
