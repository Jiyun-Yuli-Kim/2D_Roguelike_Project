using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public List<GameObject> monPatternList;

    private void Start()
    {
        // �����ӿ����� awake�� �̵�
        
    }

    public void SpawnMonster(List<Room> rooms)
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            int index = Random.Range(0, monPatternList.Count);
            Instantiate(monPatternList[index], new Vector3(rooms[i].roomCenter.x, rooms[i].roomCenter.y, 0), Quaternion.Euler(0, 0, 0));
        }
    }
}
