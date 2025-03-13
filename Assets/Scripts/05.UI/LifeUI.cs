using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class LifeUI : MonoBehaviour
{
    [SerializeField] private Image[] hearts; 
    
    private PlayerController _player; 

    [SerializeField] private Sprite  _fullHeartImage;
    [SerializeField] private Sprite  _halfHeartImage;
    [SerializeField] private Sprite  _emptyHeartImage;

    private void OnEnable()
    {
        // if (_player != null)
        // {
        //     _player.PlayerHP.Subscribe(OnPlayerHPChanged);
        // }
    }

    private void OnDisable()
    {
        // _player.PlayerHP.Unsubscribe(OnPlayerHPChanged);
    }

    // public void Init()
    // {
    // }

    public void SetLife(int value)
    {
        int _heartCount = value / 2;
        bool _hasHalfHeart = (value % 2==1);
        // Debug.Log($"heart : {_heartCount}, halfheart : {_hasHalfHeart}");
        SetLifeUI(_heartCount, _hasHalfHeart);
    }

    private void SetLifeUI(int heartCount, bool hasHalfHeart)
    {
        for (int i = 0; i < heartCount; i++)
        {
            hearts[i].sprite = _fullHeartImage;
        }

        for (int i = heartCount; i < 5; i++)
        {
            hearts[i].sprite = _emptyHeartImage;
        }

        if (hasHalfHeart)
        {
            hearts[heartCount].sprite = _halfHeartImage;
        }
    }
    
    public void OnPlayerHPChanged(int value)
    {
        if (value < 0)
        {
            return;
        }

        SetLife(value);
    }

    // void Init()
    // {
    //     foreach (var heart in hearts)
    //     {
    //         heart.sprite = _fullHeartImage;   
    //     }
    // }
}
