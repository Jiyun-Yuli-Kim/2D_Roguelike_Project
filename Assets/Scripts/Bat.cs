using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Monster
{
    public override void Attack()
    {
        _anim.SetTrigger("Attack");
        _player.playerHP--;
    }
}
