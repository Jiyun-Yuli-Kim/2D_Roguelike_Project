using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public StageDataSetter setter;
    public MapGenerator generator;
    public Spawner spawner;
    public CamController camController;

    void Awake()
    {
        if (Instance != null)
        { 
            Destroy(gameObject);
        }

        else if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    
    // public void Init() // 스테이지 변경시마다 시행하는 로직
    // {
        // {
        //     generator = FindAnyObjectByType<MapGenerator>();
        //     Debug.Log($"Find Generator : {generator}");
        // }
        //
        // if (spawner == null)
        // {
        //     spawner = FindAnyObjectByType<Spawner>();
        //     Debug.Log($"Find Spawner : {spawner}");
        // }
        //
        // if (camController == null)
        // {
        //     camController = FindAnyObjectByType<CamController>();
        //     Debug.Log($"Find CamController : {camController}");
        // }
        
        // if (generator != null && spawner != null && camController != null)
        // {
            // generator.Init(); // 방 생성
            // spawner.SpawnMonster(setter.curStageData.stageRoomList); // 몬스터 스폰
            // spawner.SpawnPlayerAndGoal(setter.curStageData.stageRoomList); // 플레이어 및 목적지 스폰
            // camController.SetSubject(); // 카메라 추적 대상 설정
        // }
    // }

    public void Reset() // 테스트용 임시함수
    {
        setter.curStageData.stageRoomCount = 0;
        setter.curStageData.stageRoomList = null;
        setter.curStageData.stageRoomList = new();
        spawner.DestroyMonsters();
        spawner.DestroyPlayerAndGoal();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
        // fake loading
        StartCoroutine(Init());
    }

    public void LoadScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }

    private IEnumerator Init()
    {
        yield return new WaitForSeconds(0.3f);
        
        generator.Init(); // 방 생성
        yield return null;
        
        spawner.Init(setter.curStageData.stageRoomList); // 몬스터, 플레이어, 아이템 스폰
        yield return null;
        
        camController.SetSubject(); // 카메라 추적 대상 설정 
    }
}
