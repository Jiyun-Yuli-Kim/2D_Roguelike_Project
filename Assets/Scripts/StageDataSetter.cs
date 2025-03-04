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

    //public void SetMonsterCount()
    //{
    //    int monCountPerRoom = curStageData.stageMonsterCount / 10; // 일단 적당히 10으로 나누기
    //    int extraMons = curStageData.stageMonsterCount - monCountPerRoom * curStageData.stageRoomCount; // 남은 마리수 구하기
    //    // 남은 마리수에 대해 랜덤분배
    //}





    // 방의 개수 확인 ex.6
    // 각 방에 몬스터 몇마리씩 분배할지 확인
    // - 현 스테이지 몬스터가 총 55마리라면, 54/6으로 할것인지?
    // - 몬스터 50마리, 방 7개라면, 50/7 = 7이고, 나머지는 버릴것인지? 아니면 그냥 50%7을 1번방에 더한다거나
    // 아니면 70퍼만 일정하게 스폰하고 나머지는 랜덤하게 넣는다던지?
    // - 몬스터 50마리, 방 7개라면 35/7 해서 각방에 5마리씩 스폰하고, 나머지 15마리는 랜덤하게 배치 
    // 50마리, 6개:8 7개:7 8개:6
}
