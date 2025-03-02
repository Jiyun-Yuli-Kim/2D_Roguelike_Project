//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class MonsterSpawner : MonoBehaviour
//{
//    public List<GameObject> monPatternList;
//    public List<GameObject> monSpawnList;

//    private void Start()
//    {
//        // 본게임에서는 awake로 이동
        
//    }

//    public void SpawnMonster(List<Room> rooms)
//    {
//        for (int i = 0; i < rooms.Count; i++)
//        {
//            int index = Random.Range(0, monPatternList.Count);
//            monSpawnList.Add(Instantiate(monPatternList[index], new Vector3(rooms[i].roomCenter.x, rooms[i].roomCenter.y, 0), Quaternion.Euler(0, 0, 0)));
//        }
//    }

//    public void DestroyMonsters(List<GameObject> monList)
//    {
//        foreach (GameObject mon in monList)
//        {
//            Destroy(mon);
//        }
//    }
//}
