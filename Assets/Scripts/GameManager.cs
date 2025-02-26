using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameManager Instance { get; private set; }
    public SceneChanger sceneChanger; // ��� �� ���� ������ �ߴ���

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

}
