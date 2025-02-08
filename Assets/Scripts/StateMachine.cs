using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public enum StateType
    {
        PDefault, PSword, PBow 
    }
    
    public StateBase CurrentState;
    private PlayerController _playerController;
    private Animator _animator;
    
    private List<StateBase> _states = new();

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        PlayerDefault playerDefault = new PlayerDefault(this._playerController, this._animator);
        PlayerSword playerSword = new PlayerSword(this._playerController, this._animator);
        //PlayerBow playerBow = new PlayerBow(this._playerController, this._animator);
        //AddState(playerDefault, playerSword, playerBow);
        
        OnChangeState(StateType.PDefault);
    }

    private void Update()
    {
        CurrentState.OnStateUpdate();
    }

    public void AddState(params StateBase[] states)
    {
        foreach (var state in states)
        {
            _states.Add(state);
        }
    }

    public void OnChangeState(StateType type)
    {
        if (0 <= (int)type && (int)type < _states.Count)
        {
            CurrentState?.OnStateExit();
            // Debug.Log($"현재상태 {CurrentState}에서 나감");
            CurrentState = _states[(int)type];
            CurrentState.OnStateEnter();
            // Debug.Log($"다음상태 {CurrentState}(으)로 돌입");

        }
    }
}
