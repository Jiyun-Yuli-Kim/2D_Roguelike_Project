using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell // 방 생성에서 이미 노드 클래스 사용하여 여기서는 Cell로 명명
{
    public bool isWalkable; // 지나갈 수 있는 노드인지 여부
    public Vector3 position; //
    public float g; // 시작 노드부터 현재 노드까지의 최단거리
    public float h; // 휴리스틱 : 도착 노드까지의 최단거리 추정치
    public List<Cell> Neighbours; // 이웃 노드 8개 리스트
    public List<Cell> Path; // 예상경로
    
    public float f 
    {
        get { return g + h; }
    }

    public Cell(bool  Walkable, Vector3 Pos, float G, float H)
    {
        isWalkable = Walkable;
        position = Pos;
    }

}