using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "Data/StageData")]
public class StageData : ScriptableObject
{
    //발자국 소리
    //bgm
    public int stageNo;
    public string stageName;
    public RuleTile stageRuletile;
    public int stageMonsterCount;
    public List<GameObject> stageMonsterList;
    public int stageRoomCount;
    public List<Room> stageRoomList; // 수량 확인 위해 리스트로 관리
    // 각 스테이지의 총 몬스터를 이렇게 관리하는건 ok. 그럼 각 방마다는 어떻게 할랭?

}
