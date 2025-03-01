using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "Data/StageData")]
public class StageData : ScriptableObject
{
    //���ڱ� �Ҹ�
    //bgm
    public int stageNo;
    public string stageName;
    public RuleTile stageRuletile;
    public int stageMonsterCount;
    public List<GameObject> stageMonsterList;
    public int stageRoomCount;
    public List<Room> stageRoomList;
    // �� ���������� �� ���͸� �̷��� �����ϴ°� ok. �׷� �� �渶�ٴ� ��� �ҷ�?

}
