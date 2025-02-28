using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageDataSetter : MonoBehaviour
{
    private int StageCount;
    public StageData[] stageDatas;
    public StageData curStageData;

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

}
