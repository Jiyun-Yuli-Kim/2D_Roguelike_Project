using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && GameManager.Instance.setter.KeyCount.Value == 0)
        {
            _animator.SetBool("Open", true); // 상자 열림 이펙트
            Invoke("OpenWinPopup",1.3f); // 1.3초 딜레이
        }
    }

    private void OpenWinPopup()
    {
        Time.timeScale = 0; // 게임 정지
        GameManager.Instance.player.OnPlayerWin?.Invoke(); // 승리 팝업 오픈
    }
}
