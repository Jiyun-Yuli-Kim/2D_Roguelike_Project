using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    // public SceneChanger sceneChanger; // 얘는 왜 굳이 오픈을 했는지
    public StageDataSetter setter;
    public MapGenerator generator;
    public MonsterSpawner monSpawner;
    void Awake()
    {
        if (Instance != null)
        { 
            Destroy(gameObject); // 왜 this 아니고 오브젝트를 지울지 잘 생각해보기
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

    public void Reset() // 테스트용 임시함수
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
