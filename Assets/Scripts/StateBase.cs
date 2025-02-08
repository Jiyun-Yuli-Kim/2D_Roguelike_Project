using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateBase
{
    public PlayerController _controller;
    public Animator _animator;
    
    // 생성자를 통해 PlayerController, Animator를 가질 것을 강제함.
    public StateBase(PlayerController player, Animator animator)
    {
        _controller = player;
        _animator = animator;
    }

    public virtual void OnStateEnter()
    {
    }

    public virtual void OnStateExit()
    {
    }
    
    public virtual void OnStateUpdate()
    {
    }
}
