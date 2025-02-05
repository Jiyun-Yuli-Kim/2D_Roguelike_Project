using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] Vector2Int _mapSize; // 기본 맵 사이즈
    [SerializeField] int _maxDepth = 4; // 트리의 높이 -> 방 16개
    [SerializeField] float _minRate; // 이등분 할 때의 최소비율
    [SerializeField] float _maxRate; // 이등분 할 때의 최대비율
    [SerializeField] float _roomMinRate; // 방 생성 시의 최소비율
    [SerializeField] float _roomMaxRate; // 방 생성 시의 최대비율
    [SerializeField] float _divideRate;
    [SerializeField] float _minArea;
    [SerializeField] Tile _roomTile; // 방 안쪽 타일
    [SerializeField] Tile _wallTile; // 경계를 나타내는 타일
    [SerializeField] Tile _outTile; // 외부를 나타내는 타일
    
    // 라인 렌더링
    [SerializeField] LineRenderer _lineRenderer;
    [SerializeField] GameObject _initMap; // 초기 맵
    [SerializeField] GameObject _line; // 공간 분할
    [SerializeField] GameObject _roomLine; // 방 외곽선

    private void Start()
    {
        _mapSize = new Vector2Int(90,60);
        Node RootNode = new Node(new RectInt(-_mapSize.x/2,-_mapSize.y/2,_mapSize.x,_mapSize.y));
        DrawRootNode();
        SplitNode(RootNode, 1);
    }

    private void DrawRootNode()
    {
        // 초기 맵 형성, linerenderer로 라인 그리기
        // 영점기준으로 하면 화면 우측 상단에 치우치는 관계로 _mapSize/2를 각각 빼줌
        _lineRenderer.positionCount = 4;
        _lineRenderer.SetPosition(0, new Vector2(0, 0) - _mapSize/2); // 왼쪽 아래
        _lineRenderer.SetPosition(1, new Vector2(0, _mapSize.y) - _mapSize/2); // 왼쪽 위
        _lineRenderer.SetPosition(2, new Vector2(_mapSize.x, _mapSize.y) - _mapSize/2); // 오른쪽 위
        _lineRenderer.SetPosition(3, new Vector2(_mapSize.x, 0) - _mapSize/2); // 오른쪽 아래
    }
    
    private void SplitNode(Node node, int depth)
    {
        if (depth > _maxDepth)
        {
            // node._isLeafNode = true;
            GenerateRoom(node);
            return;
        }

        if (node.nodeRect.width * node.nodeRect.height < _minArea)
        {
            GenerateRoom(node);
            // node._isLeafNode = true;
            return;
        }

        var lineRenderer = Instantiate(_line).GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = true;

        bool randomDir = Random.value > 0.5f; // 기본적으로는 자르는 방향을 랜덤하게
        
        if (node.nodeRect.width / node.nodeRect.height > _divideRate) // 가로/세로 비가 특정 비율 이상이면 가로로 자르기
        {
            randomDir = false;
        }

        else if (node.nodeRect.height / node.nodeRect.width > _divideRate) // 세로/가로 비가 특정 비율 이상이면 세로로 자르기
        {
            randomDir = true;
        }

        // divide horizontally
        if (randomDir)
        {
            node._isHorizontalCut = true;
            int height = node.nodeRect.height;
            int width = node.nodeRect.width;
            int split = Mathf.RoundToInt(height * Random.Range(_minRate, _maxRate));
            
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, new Vector3(node.nodeRect.xMin, node.nodeRect.yMin+split, 0));
            lineRenderer.SetPosition(1, new Vector3(node.nodeRect.xMin+width, node.nodeRect.yMin+split, 0));
            node.node1 = new Node(new RectInt(node.nodeRect.xMin, node.nodeRect.yMin+split, width, height-split));
            node.node2 = new Node(new RectInt(node.nodeRect.xMin, node.nodeRect.yMin, width, split));
            node.node1.parNode = node;
            node.node2.parNode = node;
            SplitNode(node.node1, depth+1);
            SplitNode(node.node2, depth+1);
        }
        
        // divide vertically
        else if (!randomDir)
        {
            node._isHorizontalCut = false;
            int width = node.nodeRect.width;
            int height = node.nodeRect.height;
            int split = Mathf.RoundToInt(width * Random.Range(_minRate, _maxRate));
            
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, new Vector3(node.nodeRect.xMin+split, node.nodeRect.yMin, 0));
            lineRenderer.SetPosition(1, new Vector3(node.nodeRect.xMin+split, node.nodeRect.yMin+height, 0));
            node.node1 = new Node(new RectInt(node.nodeRect.xMin, node.nodeRect.yMin, split, height));
            node.node2 = new Node(new RectInt(node.nodeRect.xMin+split, node.nodeRect.yMin, width-split, height));
            node.node1.parNode = node;
            node.node2.parNode = node;
            SplitNode(node.node1, depth+1);
            SplitNode(node.node2, depth+1);
        }
    }

   /*
    1. 지금 생성한 노드라 리프노드라면, 방을 만들어준다. 
    - 이에 대한 판정은 어떻게 할 것인지?
    - 현재 로직상, maxDepth 도달 이전에 리프노드가 생성되기도 함 
    - 불변수 활용 : isLeafNode
    - 아니면 아예 처음의 판정부에서 만들어버린다면?
        -> 어차피 이게 마지막 노드인지 확인하는 로직이니까
     
     방 만들기 로직
     노드의 width, height를 가져온다
     여기서 랜덤하게 방의 width, height를 뽑는다
     노드센터를 기준으로 (x-w/2, y-h/2)가 룸의 RectInt의 xMin, yMin이 된다
    */

   private void GenerateRoom(Node node)
   {
       int roomWidth = Mathf.RoundToInt(node.nodeRect.width * Random.Range(_roomMinRate, _roomMaxRate));
       int roomHeight = Mathf.RoundToInt(node.nodeRect.height * Random.Range(_roomMinRate, _roomMaxRate));
       node.roomRect = new RectInt(Mathf.RoundToInt(node.nodeCenter.x - roomWidth/2), Mathf.RoundToInt(node.nodeCenter.y - roomHeight/2), roomWidth, roomHeight);
       Debug.Log($"GenerateRoom - Room Position: ({node.roomRect.xMin}, {node.roomRect.yMin}) Size: {node.roomRect.width}x{node.roomRect.height}");

       DrawRoom(node);
   }

   private void DrawRoom(Node node)
   {
       var lineRenderer = Instantiate(_roomLine).GetComponent<LineRenderer>();
       Debug.Log(lineRenderer);
       lineRenderer.useWorldSpace = true; // 빼먹지 말자
       lineRenderer.positionCount = 4;
       Debug.Log($"Draw 가로길이 : {node.nodeRect.width}, {node.roomRect.width}");
       Debug.Log($"Draw 세로길이 : {node.nodeRect.height}, {node.roomRect.height}");

       lineRenderer.SetPosition(0, new Vector3(node.roomRect.xMin, node.roomRect.yMin, 0)); // 왼쪽 아래
       lineRenderer.SetPosition(1, new Vector3(node.roomRect.xMin, node.roomRect.yMin + node.roomRect.height, 0)); // 왼쪽 위
       lineRenderer.SetPosition(2, new Vector3(node.roomRect.xMin + node.roomRect.width, node.roomRect.yMin + node.roomRect.height, 0)); // 오른쪽 위
       lineRenderer.SetPosition(3, new Vector3(node.roomRect.xMin + node.roomRect.width, node.roomRect.yMin, 0)); // 오른쪽 아래
   }


}
