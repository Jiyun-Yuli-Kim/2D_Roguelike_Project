using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageDataSetter : MonoBehaviour
{
    private int StageCount;
    public StageData[] stageDatas;
    public StageData curStageData;

    //public int stageNo;
    //public string stageName;
    //public RuleTile stageRuletile;
    //public int stageMonsterCount;
    //public List<GameObject> stageMonsterList;
    //public int stageRoomCount;
    //public List<Room> stageRoomList;

    private void Awake()
    {
        stageDatas = new StageData[StageCount];
        curStageData.stageRoomList = new();
    }

    public void StageDataInit(int stageNo)
    {
        curStageData = stageDatas[stageNo];
        curStageData.stageRoomList = new();
    }

    public void SetMonsterCount()
    {
        int monCountPerRoom = curStageData.stageMonsterCount / 10; // �ϴ� ������ 10���� ������
        int extraMons = curStageData.stageMonsterCount - monCountPerRoom * curStageData.stageRoomCount; // ���� ������ ���ϱ�
        // ���� �������� ���� �����й�
    }





    // ���� ���� Ȯ�� ex.6
    // �� �濡 ���� ����� �й����� Ȯ��
    // - �� �������� ���Ͱ� �� 55�������, 54/6���� �Ұ�����?
    // - ���� 50����, �� 7�����, 50/7 = 7�̰�, �������� ����������? �ƴϸ� �׳� 50%7�� 1���濡 ���Ѵٰų�
    // �ƴϸ� 70�۸� �����ϰ� �����ϰ� �������� �����ϰ� �ִ´ٴ���?
    // - ���� 50����, �� 7����� 35/7 �ؼ� ���濡 5������ �����ϰ�, ������ 15������ �����ϰ� ��ġ 
    // 50����, 6��:8 7��:7 8��:6
}
