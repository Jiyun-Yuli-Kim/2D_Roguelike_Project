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
    public MonsterSpawner monSpawner;
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
        Init();
    }

    public void Init()
    {
        if (generator == null)
        {
            generator = FindAnyObjectByType<MapGenerator>();
            Debug.Log($"Find Generator : {generator}");
        }

        if (monSpawner == null)
        {
            monSpawner = FindAnyObjectByType<MonsterSpawner>();
            Debug.Log($"Find Spawner : {monSpawner}");
        }

        if (generator != null && monSpawner != null)
        {
            generator.GenerateMap();
            generator.GenerateCorridor(setter.curStageData.stageRoomList);
            monSpawner.SpawnMonster(setter.curStageData.stageRoomList);
        }
    }

    public void Reset() // �׽�Ʈ�� �ӽ��Լ�
    {
        setter.curStageData.stageRoomCount = 0;
        setter.curStageData.stageRoomList = null;
        setter.curStageData.stageRoomList = new();

    }

    public void LoadScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }
}
