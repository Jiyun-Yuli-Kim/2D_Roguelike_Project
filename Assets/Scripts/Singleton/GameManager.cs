using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    // public SceneChanger sceneChanger; // ��� �� ���� ������ �ߴ���

    void Awake()
    {
        if (Instance != null)
        { 
            Destroy(gameObject); // �� this �ƴϰ� ������Ʈ�� ������ �� �����غ���
        }

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void LoadScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }
}
