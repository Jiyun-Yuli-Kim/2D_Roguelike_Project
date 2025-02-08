using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBow : StateBase
{
    public PlayerBow(PlayerController player, Animator animator) : base(player, animator)
    {
        
    }

    public override void OnStateEnter()
    {
        Debug.Log("PlayerBow Entered");
    }

    public override void OnStateUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
            Debug.Log("Bow Attack!");
        }
            // // 사운드 재생 로직
            // footstepTimer -= Time.deltaTime;
            // if (footstepTimer <= 0)
            // {
            //     PlayFootstepSound();
            //     footstepTimer = isCurDashing? 0.3f : 0.5f; // 대시 상태와 걷기 상태의 발소리 간격
            // }
    }

    public override void OnStateExit()
    {
    }

    private void Attack()
    {
        _animator.SetTrigger("Bow");
    }

    // private void PlayFootstepSound()
    // {
    //     // 걷기와 뛰기에 따른 발소리 재생
    //     var soundType = isCurDashing? SoundManager.ESFX.SFX_Run : SoundManager.ESFX.SFX_Walk;
    //     SoundManager.Instance.PlaySFX(soundType);
    // }
}
