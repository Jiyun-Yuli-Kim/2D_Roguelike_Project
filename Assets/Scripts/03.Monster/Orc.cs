using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc : Monster
{
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
        _player.TakeDamage(1);
        yield return new WaitForSeconds(_attackCoolTime);
        _isAttacking = false;
    }
}
