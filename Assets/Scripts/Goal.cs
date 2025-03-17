using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private Animator _animator;
    private InGameUI _inGameUI;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _inGameUI = FindObjectOfType<InGameUI>();
        Debug.Log(_inGameUI);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (GameManager.Instance.setter.KeyCount.Value == 0)
            {
                _animator.SetBool("Open", true); // 상자 열림 이펙트
                Invoke("OpenWinPopup",1.3f); // 1.3초 딜레이
            }
            else if (GameManager.Instance.setter.KeyCount.Value > 0)
            {
                // 남은 열쇠 수 띄우기
                _inGameUI.ShowLeftKeyCount();
            }
        }
    }

    private void OpenWinPopup()
    {
        Time.timeScale = 0; // 게임 정지
        Cursor.visible = true;
        GameManager.Instance.player.OnPlayerWin?.Invoke(); // InGameUI상에서 승리 팝업 오픈
        SoundManager.Instance.StopBGM();
        SoundManager.Instance.PlaySFX(ESFXs.WinSFX);
    }
}
