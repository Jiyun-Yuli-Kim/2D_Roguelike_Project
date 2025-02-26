using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGameButton : MonoBehaviour
{
    public void NewGame()
    {
        GameManager.Instance.LoadScene(1);
    }
}
