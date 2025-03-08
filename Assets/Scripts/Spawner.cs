using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> monPatternList;
    private List<GameObject> monSpawnList = new();
    public GameObject playerPrefab;
    public GameObject player;
    public GameObject goalPrefab;
    private GameObject goal;
    public GameObject keyPrefab;
    private GameObject key;
    private List<GameObject> keySpawnList = new();

    private void Awake()
    {
    }

    private void Start()
    {
        GameManager.Instance.spawner = this;
        // Init();
    }

    public void Init(List<Room> rooms)
    {
        SpawnPlayer(rooms); // 플레이어 스폰
        SpawnGoal(rooms); // 목적지 스폰
        SpawnMonster(rooms); // 몬스터 스폰
        SpawnKey(rooms); // 열쇠 스폰
    }

    public void SpawnPlayer(List<Room> rooms)
    {
        player = Instantiate(playerPrefab, new Vector3(rooms[0].roomCenter.x+0.35f, rooms[0].roomCenter.y, 0), Quaternion.Euler(0, 0, 0));
    }
    
    public void SpawnGoal(List<Room> rooms)
    {
        goal = Instantiate(goalPrefab, new Vector3(rooms[rooms.Count-1].roomCenter.x, rooms[rooms.Count - 1].roomCenter.y, 0), Quaternion.Euler(0, 0, 0));
    }

    public void SpawnMonster(List<Room> rooms)
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            int index = Random.Range(0, monPatternList.Count);
            monSpawnList.Add(Instantiate(monPatternList[index], new Vector3(rooms[i].roomCenter.x+0.35f, rooms[i].roomCenter.y, 0), Quaternion.Euler(0, 0, 0)));
        }
    }

    public void SpawnKey(List<Room> rooms)
    {
        int room1 = Random.Range(1, rooms.Count-1);
        int room2;
        int room3;

        do
        {
            room2 = Random.Range(1, rooms.Count-1);
        } while (room2 == room1);

        do
        {
            room3 = Random.Range(1, rooms.Count-1);
        } while (room3 == room1 || room3 == room2);

        keySpawnList.Add(Instantiate(keyPrefab, new Vector3(rooms[room1].roomCenter.x + 5, rooms[room1].roomCenter.y + 5, 0),
            Quaternion.Euler(0, 0, 0)));
        keySpawnList.Add(Instantiate(keyPrefab, new Vector3(rooms[room2].roomCenter.x - 5, rooms[room2].roomCenter.y - 5, 0),
            Quaternion.Euler(0, 0, 0)));
        keySpawnList.Add(Instantiate(keyPrefab, new Vector3(rooms[room3].roomCenter.x + 5, rooms[room3].roomCenter.y - 5, 0),
            Quaternion.Euler(0, 0, 0)));
    }


    public void DestroyPlayerAndGoal()
    {
        Destroy(player);
        Destroy(goal);
    }

    public void DestroyMonsters()
    {
        foreach (GameObject mon in monSpawnList)
        {
            Destroy(mon);
        }
    }

}
