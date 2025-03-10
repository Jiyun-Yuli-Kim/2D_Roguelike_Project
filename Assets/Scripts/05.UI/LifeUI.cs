using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class LifeUI : MonoBehaviour, IInitializable
{
    [SerializeField] private Image[] hearts; 
    
    private PlayerController _player; 
    private int _heartCount;
    private int _halfHeartCount;

    [SerializeField] private Sprite  _fullHeartImage;
    [SerializeField] private Sprite  _halfHeartImage;
    [SerializeField] private Sprite  _emptyHeartImage;
    
    private void Start()
    {
    }

    private void OnEnable()
    {
        Init();
    }

    private void OnDisable()
    {
        _player.PlayerHP.Unsubscribe(OnPlayerHPChanged);
    }

    public void SceneInitialize()
    {
        _player = GameManager.Instance.player;
        _player.PlayerHP.Subscribe(OnPlayerHPChanged);
        Debug.Log(_player);
    }

    public void SetLife(int value)
    {
        _heartCount = value / 2;
        _halfHeartCount = value % 2;
        Debug.Log($"heart : {_heartCount}, halfheart : {_halfHeartCount}");
        SetLifeUI(_heartCount, _halfHeartCount);
    }

    private void SetLifeUI(int heartCount, int halfHeartCount)
    {
        for (int i = 0; i < heartCount; i++)
        {
            hearts[i].sprite = _fullHeartImage;
        }

        for (int i = heartCount; i < 5; i++)
        {
            hearts[i].sprite = _emptyHeartImage;
        }
        
        if (halfHeartCount == 1)
        {
            hearts[heartCount].sprite = _halfHeartImage;
        }
    }
    
    // hp = 5일 때,
    // 5/2 = 2개의 하트 활성화
    // 5%2 = 1개의 반쪽하트 활성화
    // 5/2 = 2개의 빈칸 활성화
    

    private void OnPlayerHPChanged(int value)
    {
        if (value < 0)
        {
            return;
        }

        SetLife(value);
    }

    void Init()
    {
        foreach (var heart in hearts)
        {
            heart.sprite = _fullHeartImage;   
        }
    }
}
