using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour, IInitializable
{
    [SerializeField] private List<GameObject> monPatternList;
    [SerializeField] private List<GameObject> itemList; // 하트, 무적, 길찾기
    [SerializeField] private List<GameObject> skillList; // PowerUP, FreiKugel
    
    private List<GameObject> monSpawnList = new();
    public PlayerController playerPrefab;
    // public PlayerController player;
    public GameObject goalPrefab;
    private GameObject goal;
    public GameObject keyPrefab;
    private GameObject key;
    private List<GameObject> keySpawnList = new();
    
    private void Start()
    {
        GameManager.Instance.spawner = this;
    }

    public void SceneInitialize()
    {
        Init(GameManager.Instance.setter.stageRoomList);
    }

    public void Init(List<Room> rooms)
    {
        SpawnPlayer(rooms); // 플레이어 스폰
        SpawnGoal(rooms); // 목적지 스폰
        SpawnMonster(rooms); // 몬스터 스폰
        SpawnKey(rooms); // 열쇠 스폰
        SpawnSkill(rooms); // 스킬 스폰
        SpawnItem(rooms); // 아이템 스폰
    }
    
    public void SpawnPlayer(List<Room> rooms)
    {
        GameManager.Instance.player = Instantiate(playerPrefab, new Vector3(rooms[0].roomCenter.x+0.35f, rooms[0].roomCenter.y, 0), Quaternion.Euler(0, 0, 0));
    }
    
    public void SpawnGoal(List<Room> rooms)
    {
        // goal 변수는 테스트용. 추후 삭제
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
        List<int> randRooms = GetRandomRooms(rooms.Count-2, 3); // 첫방과 마지막방 제외
        // foreach (int room in randRooms)
        // {
        //     Debug.Log(room);
        // }

        keySpawnList.Add(Instantiate(keyPrefab, new Vector3(rooms[randRooms[0]+1].roomCenter.x + 1.2f, rooms[randRooms[0]+1].roomCenter.y + 1.2f, 0),
            Quaternion.Euler(0, 0, 0)));
        keySpawnList.Add(Instantiate(keyPrefab, new Vector3(rooms[randRooms[1]+1].roomCenter.x + 1.2f, rooms[randRooms[1]+1].roomCenter.y - 1.2f, 0),
            Quaternion.Euler(0, 0, 0)));
        keySpawnList.Add(Instantiate(keyPrefab, new Vector3(rooms[randRooms[2]+1].roomCenter.x + 1.2f, rooms[randRooms[2]+1].roomCenter.y - 1.2f, 0),
            Quaternion.Euler(0, 0, 0)));
    }

    public void SpawnSkill(List<Room> rooms)
    {
        List<int> randRooms = GetRandomRooms(rooms.Count-2, 2); // 첫방과 마지막방 제외
        Instantiate(skillList[0], new Vector3(rooms[randRooms[0]+1].roomCenter.x - 2f, rooms[randRooms[0]+1].roomCenter.y + 2f, 0),
        Quaternion.Euler(0, 0, 0));
        Instantiate(skillList[1], new Vector3(rooms[randRooms[1]+1].roomCenter.x - 2f, rooms[randRooms[1]+1].roomCenter.y - 2f, 0), 
            Quaternion.Euler(0, 0, 0));
    }
    
    public void SpawnItem(List<Room> rooms)
    {
        List<int> randRooms = GetRandomRooms(rooms.Count-2, 2); // 첫방과 마지막방 제외
        Debug.Log(randRooms[0]);
        Debug.Log(randRooms[1]);
        Instantiate(itemList[0], new Vector3(rooms[randRooms[0]+1].roomCenter.x + 2f, rooms[randRooms[0]+1].roomCenter.y - 2f, 0),
            Quaternion.Euler(0, 0, 0));
        Instantiate(itemList[0], new Vector3(rooms[randRooms[1]+1].roomCenter.x + 2f, rooms[randRooms[1]+1].roomCenter.y + 2f, 0), 
            Quaternion.Euler(0, 0, 0));
    }

    private List<int> GetRandomRooms(int roomCount, int resultCount)
    {
        List<int> rooms = new();
        for (int i = 0; i < roomCount; i++)
        {
            rooms.Add(i);
        }
        Shuffle(rooms);
        return rooms.GetRange(0, resultCount);
    }

    private List<int> Shuffle(List<int> numbers)
    {
        for (int i = 0; i < numbers.Count; i++)
        {
            int r = Random.Range(0, numbers.Count);
            (numbers[i], numbers[r]) = (numbers[r], numbers[i]);
        }
        return numbers;
    }

    public void DestroyPlayerAndGoal()
    {
        Destroy(GameManager.Instance.player.gameObject);
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
