using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGameButton : MonoBehaviour
{
    public void NewGame()
    {
        GameManager.Instance.StartGame();
        // GameManager.Instance.Init(); // 게임에 필요한 환경 초기화 
    }
}
