using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Monster
{
    public override void Attack()
    {
        _anim.SetTrigger("Attack");
        _player.playerHP--;
    }
}
