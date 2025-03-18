using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public bool isWalkable; // 지나갈 수 있는 노드인지 여부
    public Vector3 position; // 노드의 중심좌표
    public float g; // 시작 노드부터 현재 노드까지의 최단거리
    public float h; // 휴리스틱 : 도착 노드까지의 최단거리 추정치
    public List<Node> Neighbours; // 이웃 노드 8개 리스트
    public List<Node> Path; // 예상경로
    
    public float f 
    {
        get { return g + h; }
    }

    public Node(bool  Walkable, Vector3 Pos)
    {
        isWalkable = Walkable;
        position = Pos;
    }

}