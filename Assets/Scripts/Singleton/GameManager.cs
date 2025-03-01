using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    // public SceneChanger sceneChanger; // ��� �� ���� ������ �ߴ���
    public StageDataSetter setter;
    public MapGenerator generator;

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

    private void Start()
    {
        if (generator == null)
        { 
            generator = FindAnyObjectByType<MapGenerator>();
            Debug.Log($"Find Generator : {generator}");
        }
        if (generator != null)
        {
            generator.GenerateMap();
        }
    }

    public void LoadScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }
}
