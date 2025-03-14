using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "StageData", menuName = "Data/StageData")]
public class StageData : ScriptableObject
{
    //발자국 소리
    //bgm
    public int stageNo;
    public string stageName;
    public RuleTile stageRuletile;
    public Tile stageOutTile; // 외부를 나타내는 타일

}
