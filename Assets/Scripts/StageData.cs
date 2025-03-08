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
    public int stageKeyCount;
    public int stageRoomCount;
    public List<Room> stageRoomList; // 수량 확인 위해 리스트로 관리
}
