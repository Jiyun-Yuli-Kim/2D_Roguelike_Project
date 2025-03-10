using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class InGameUI : MonoBehaviour, IInitializable
{
    public Action OnMonCountChanged;
    
    [SerializeField] private TextMeshProUGUI _monsterCount;
    [SerializeField] private TextMeshProUGUI _keyCount;

    [SerializeField] private StageDataSetter _stageDataSetter;

    // 플레이어 체력
    private PlayerController _player;
    
    private void OnEnable() => SubscribeEvents();
    private void OnDisable() => UnsubscribeEvents();

    public void SceneInitialize()
    {
        _stageDataSetter = GameManager.Instance.setter;
        _player = GameManager.Instance.player;
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        if (_stageDataSetter != null)
        {
            _stageDataSetter.MonsterCount.Subscribe(SetMonsterCountUI);
            _stageDataSetter.KeyCount.Subscribe(SetKeyCountUI);
        }

        if (_player != null)
        {
            
        }
    }

    private void UnsubscribeEvents()
    {
        _stageDataSetter.MonsterCount.Unsubscribe(SetMonsterCountUI);
        _stageDataSetter.KeyCount.Unsubscribe(SetKeyCountUI);
    }


    public void SetMonsterCountUI(int value)
    {
        _monsterCount.text = value.ToString();
    }

    public void SetKeyCountUI(int value)
    {
        _keyCount.text = value.ToString();
    }
}
