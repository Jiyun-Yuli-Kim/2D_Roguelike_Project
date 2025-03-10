using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageDataSetter : MonoBehaviour, IInitializable
{
    private int StageCount =3 ;
    public StageData[] stageDatas;
    public StageData curStageData;
    [SerializeField] private InGameUI _ui;



    public ObservableProperty<int> MonsterCount;
    public ObservableProperty<int> KeyCount;
    
    //public int stageNo;
    //public string stageName;
    //public RuleTile stageRuletile;
    //public int stageMonsterCount;
    //public List<GameObject> stageMonsterList;
    //public int stageRoomCount;
    //public List<Room> stageRoomList;

    private void Awake()
    {
        // stageDatas = new StageData[StageCount];
        curStageData.stageRoomList = new();
        curStageData.stageMonsterCount = 0;
        curStageData.stageKeyCount = 3;
    }

    public void SceneInitialize()
    {
        // int stagenum = Random.Range(0, stageDatas.Length);
        // curStageData = stageDatas[stagenum];
        curStageData = stageDatas[0];

        curStageData.stageRoomList = new();
        MonsterCount = new ObservableProperty<int>(0); 
        KeyCount = new ObservableProperty<int>(3);
    }

    public void StageDataInit(int stageNo)
    {
        curStageData = stageDatas[stageNo];
        curStageData.stageRoomList = new();
        curStageData.stageMonsterCount = 0;
        curStageData.stageKeyCount = 3;
    }
   
    // //TODO : TestCode
    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Z))
    //     {
    //         MonsterCount.Value++;
    //     }
    //     if (Input.GetKeyDown(KeyCode.X))
    //     {
    //         KeyCount.Value++;
    //     }
    // }
}
