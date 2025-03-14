using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageDataSetter : MonoBehaviour, IInitializable
{
    private int StageCount =3 ;
    public StageData[] stageDatas;
    public StageData curStageData;
    [SerializeField] private InGameUI _ui;
    
    public List<GameObject> stageMonsterList;
    public List<Room> stageRoomList; // 수량 확인 위해 리스트로 관리
    public int stageRoomCount;

    
    public ObservableProperty<int> MonsterCount;
    public ObservableProperty<int> KeyCount;
    
    //public int stageNo;
    //public string stageName;
    //public RuleTile stageRuletile;
    //public int stageMonsterCount;
    //public List<GameObject> stageMonsterList;
    //public List<Room> stageRoomList;

    private void Awake()
    {
        stageRoomList = new();
    }

    public void SceneInitialize()
    {
        // int stagenum = Random.Range(0, stageDatas.Length);
        // curStageData = stageDatas[stagenum];
        curStageData = stageDatas[0];
        stageRoomList = new();
        MonsterCount = new ObservableProperty<int>(0); 
        KeyCount = new ObservableProperty<int>(0);
        SoundManager.Instance.PlayBGM((EBGMs)curStageData.stageNo);
    }
   
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
