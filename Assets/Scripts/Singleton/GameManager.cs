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
    public Spawner spawner;
    public CamController camController;

    void Awake()
    {
        if (Instance != null)
        { 
            Destroy(gameObject); // �� this �ƴϰ� ������Ʈ�� ������ �� �����غ���
        }

        else if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        Init(); // Ÿ��Ʋ������ ���ӸŴ����� �ִµ�, �̰� ���⿡ �ϴ°� �´���
    }

    public void Init() // �������� ����ø��� �����ϴ� ����
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
            generator.GenerateMap(); // �� ����
            generator.GenerateCorridor(setter.curStageData.stageRoomList); // ���� ����
            spawner.SpawnMonster(setter.curStageData.stageRoomList); // ���� ����
            spawner.SpawnPlayerAndGoal(setter.curStageData.stageRoomList); // �÷��̾� �� ������ ����
            camController.SetSubject(); // ī�޶� ���� ��� ����
        }
    }

    public void Reset() // �׽�Ʈ�� �ӽ��Լ�
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
