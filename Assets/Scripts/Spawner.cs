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

    private void Start()
    {
        // 본게임에서는 awake로 이동
        GameManager.Instance.spawner = this;

    }

    public void SpawnPlayerAndGoal(List<Room> rooms)
    {
        player = Instantiate(playerPrefab, new Vector3(rooms[0].roomCenter.x, rooms[0].roomCenter.y, 0), Quaternion.Euler(0, 0, 0));
        goal = Instantiate(goalPrefab, new Vector3(rooms[rooms.Count-1].roomCenter.x, rooms[rooms.Count - 1].roomCenter.y, 0), Quaternion.Euler(0, 0, 0));
    }

    public void SpawnMonster(List<Room> rooms)
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            int index = Random.Range(0, monPatternList.Count);
            monSpawnList.Add(Instantiate(monPatternList[index], new Vector3(rooms[i].roomCenter.x, rooms[i].roomCenter.y, 0), Quaternion.Euler(0, 0, 0)));
        }
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
