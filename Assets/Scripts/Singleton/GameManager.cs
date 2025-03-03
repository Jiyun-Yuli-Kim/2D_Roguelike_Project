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
    public Spawner spawner;
    public CamController camController;

    void Awake()
    {
        if (Instance != null)
        { 
            Destroy(gameObject); // 왜 this 아니고 오브젝트를 지울지 잘 생각해보기
        }

        else if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        Init(); // 타이틀씬부터 게임매니저가 있는데, 이걸 여기에 하는게 맞는지
    }

    public void Init() // 스테이지 변경시마다 시행하는 로직
    {
        if (generator == null)
        {
            generator = FindAnyObjectByType<MapGenerator>();
            Debug.Log($"Find Generator : {generator}");
        }

        if (spawner == null)
        {
            spawner = FindAnyObjectByType<Spawner>();
            Debug.Log($"Find Spawner : {spawner}");
        }

        if (camController == null)
        {
            camController = FindAnyObjectByType<CamController>();
            Debug.Log($"Find CamController : {camController}");
        }

        if (generator != null && spawner != null && camController != null)
        {
            generator.GenerateMap(); // 방 생성
            generator.GenerateCorridor(setter.curStageData.stageRoomList); // 복도 생성
            spawner.SpawnMonster(setter.curStageData.stageRoomList); // 몬스터 스폰
            spawner.SpawnPlayerAndGoal(setter.curStageData.stageRoomList); // 플레이어 및 목적지 스폰
            camController.SetSubject(); // 카메라 추적 대상 설정
        }
    }

    public void Reset() // 테스트용 임시함수
    {
        setter.curStageData.stageRoomCount = 0;
        setter.curStageData.stageRoomList = null;
        setter.curStageData.stageRoomList = new();
        spawner.DestroyMonsters();
        spawner.DestroyPlayerAndGoal();
    }

    public void LoadScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }
}
