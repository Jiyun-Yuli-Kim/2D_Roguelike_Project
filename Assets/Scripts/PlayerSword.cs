using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : StateBase
{
    public PlayerSword(PlayerController player, Animator animator) : base(player, animator)
    {
        
    }

    public override void OnStateEnter()
    {
        _animator.SetBool("isMoving", true);
    }

    public override void OnStateUpdate()
    {
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
        _animator.SetBool("isMoving", false);
    }

    // private void PlayFootstepSound()
    // {
    //     // 걷기와 뛰기에 따른 발소리 재생
    //     var soundType = isCurDashing? SoundManager.ESFX.SFX_Run : SoundManager.ESFX.SFX_Walk;
    //     SoundManager.Instance.PlaySFX(soundType);
    // }
}
