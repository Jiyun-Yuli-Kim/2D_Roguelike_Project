using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public bool isWalkable; // 지나갈 수 있는 노드인지 여부
    public Vector3 position; // 노드의 중심좌표
    public int gridX,  gridY; // 노드의 기준좌표(좌측아래)
    public int gCost; // 시작 노드부터 현재 노드까지의 최단거리
    public int hCost; // 휴리스틱 : 도착 노드까지의 최단거리 추정치
    public List<Node> Neighbours; // 이웃 노드 8개 리스트
    public List<Node> Path; // 예상경로
    
    public Node parent; 
    
    public float fCost 
    {
        get { return gCost + hCost; }
    }

    public Node(bool  Walkable, Vector3 Pos,  int GridX, int GridY)
    {
        isWalkable = Walkable;
        position = Pos;
        gridX = GridX;
        gridY = GridY;
    }

}