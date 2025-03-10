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

    public PlayerController player;
    
    void Awake() => Init();

    private void Init()
    {
        SingletonInit();
        setter = GetComponentInChildren<StageDataSetter>();
    }

    private void SingletonInit()
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
        Debug.Log("Init Singleton GameManager");
    }

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
        // StartCoroutine(Init());
    }

    public void LoadScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }

    // private IEnumerator Init()
    // {
    //     yield return new WaitForSeconds(0.3f);
    //     
    //     // setter.
    //     
    //     // TODO : KimJaeSeong - 초기화 관련 로직에 관한 인터페이스 추가로 주석처리.
    //     // generator.Init(); // 방 생성
    //     
    //     yield return null;
    //     
    //     spawner.Init(setter.curStageData.stageRoomList); // 몬스터, 플레이어, 아이템 스폰
    //     yield return null;
    //     
    //     camController.SetSubject(); // 카메라 추적 대상 설정 
    // }
    
}
