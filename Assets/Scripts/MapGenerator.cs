using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

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

    [SerializeField] Tilemap _tilemap;
    // [SerializeField] Tilemap _floorTilemap;
    // [SerializeField] Tilemap _wallTilemap;

    [SerializeField] Tile _roomTile1; // 방 안쪽 타일
    [SerializeField] Tile _roomTile2; // 방 안쪽 타일 w. 그림자
    [SerializeField] Tile _wallTile; // 경계를 나타내는 타일
    [SerializeField] Tile _outTile; // 외부를 나타내는 타일
    [SerializeField] RuleTile _ruleTile;
    
    // [SerializeField] RuleTile _floorRuleTile;
    // [SerializeField] RuleTile _wallRuleTile;

    // 라인 렌더링
    [SerializeField] LineRenderer _lineRenderer;
    [SerializeField] GameObject _initMap; // 초기 맵
    [SerializeField] GameObject _line; // 공간 분할
    [SerializeField] GameObject _roomLine; // 방 외곽선
    [SerializeField] GameObject _corridorLine;

    private void Start()
    {
        // _mapSize = new Vector2Int(90, 60);
        FillBG();
        Node RootNode = new Node(new RectInt(-_mapSize.x / 2, -_mapSize.y / 2, _mapSize.x, _mapSize.y));
        // DrawRootNode();
        SplitNode(RootNode, 1);
    }

    private void DrawRootNode()
    {
        // 초기 맵 형성, linerenderer로 라인 그리기
        // 영점기준으로 하면 화면 우측 상단에 치우치는 관계로 _mapSize/2를 각각 빼줌
        _lineRenderer.positionCount = 4;
        _lineRenderer.SetPosition(0, new Vector2(0, 0) - _mapSize / 2); // 왼쪽 아래
        _lineRenderer.SetPosition(1, new Vector2(0, _mapSize.y) - _mapSize / 2); // 왼쪽 위
        _lineRenderer.SetPosition(2, new Vector2(_mapSize.x, _mapSize.y) - _mapSize / 2); // 오른쪽 위
        _lineRenderer.SetPosition(3, new Vector2(_mapSize.x, 0) - _mapSize / 2); // 오른쪽 아래
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
            lineRenderer.SetPosition(0, new Vector3(node.nodeRect.xMin, node.nodeRect.yMin + split, 0));
            lineRenderer.SetPosition(1, new Vector3(node.nodeRect.xMin + width, node.nodeRect.yMin + split, 0));
            node.node1 = new Node(new RectInt(node.nodeRect.xMin, node.nodeRect.yMin + split, width, height - split));
            node.node2 = new Node(new RectInt(node.nodeRect.xMin, node.nodeRect.yMin, width, split));
            node.node1.parNode = node;
            node.node2.parNode = node;
            SplitNode(node.node1, depth + 1);
            SplitNode(node.node2, depth + 1);
            DrawLine(node.node1.nodeCenter, node.node2.nodeCenter);

        }

        // divide vertically
        else if (!randomDir)
        {
            node._isHorizontalCut = false;
            int width = node.nodeRect.width;
            int height = node.nodeRect.height;
            int split = Mathf.RoundToInt(width * Random.Range(_minRate, _maxRate));

            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, new Vector3(node.nodeRect.xMin + split, node.nodeRect.yMin, 0));
            lineRenderer.SetPosition(1, new Vector3(node.nodeRect.xMin + split, node.nodeRect.yMin + height, 0));
            node.node1 = new Node(new RectInt(node.nodeRect.xMin, node.nodeRect.yMin, split, height));
            node.node2 = new Node(new RectInt(node.nodeRect.xMin + split, node.nodeRect.yMin, width - split, height));
            node.node1.parNode = node;
            node.node2.parNode = node;
            SplitNode(node.node1, depth + 1);
            SplitNode(node.node2, depth + 1);
            DrawLine(node.node1.nodeCenter, node.node2.nodeCenter);

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

        // roomRect의 xMin과 yMin값을 현재는 nodeCenter에서 roomw/2, roomh/2 만큼을 빼주고 있다.
        // 모든 룸이 가운데 정렬되어 단조로워보인다는 단점이 있다.
        // 이에, 방의 시작 좌표에 랜덤성을 부여하고자 한다.
        int roomX = Mathf.RoundToInt(Random.Range(node.nodeRect.xMin + 1,
            node.nodeRect.xMin + node.nodeRect.width - roomWidth - 1));
        int roomY = Mathf.RoundToInt(Random.Range(node.nodeRect.yMin + 1,
            node.nodeRect.yMin + node.nodeRect.height - roomHeight - 1));

        node.roomRect = new RectInt(roomX, roomY, roomWidth, roomHeight);

        DrawRoom(node);
    }

    // private void DrawCorridor(Node node, int depth)
    // {
    //     if (depth > _maxDepth)
    //     {
    //         return;
    //     }
    //
    //     DrawLine(node.node1.nodeCenter, node.node2.nodeCenter);
    //     DrawCorridor(node.node1, depth + 1);
    //     DrawCorridor(node.node2, depth + 1);
    // }

    private void DrawRoom(Node node)
    {
        // var lineRenderer = Instantiate(_roomLine).GetComponent<LineRenderer>();
        // lineRenderer.useWorldSpace = true; // 빼먹지 말자
        // lineRenderer.positionCount = 4;
        //
        // lineRenderer.SetPosition(0, new Vector3(node.roomRect.xMin, node.roomRect.yMin, 0)); // 왼쪽 아래
        // lineRenderer.SetPosition(1, new Vector3(node.roomRect.xMin, node.roomRect.yMin + node.roomRect.height, 0)); // 왼쪽 위
        // lineRenderer.SetPosition(2, new Vector3(node.roomRect.xMin + node.roomRect.width, node.roomRect.yMin + node.roomRect.height, 0)); // 오른쪽 위
        // lineRenderer.SetPosition(3, new Vector3(node.roomRect.xMin + node.roomRect.width, node.roomRect.yMin, 0)); // 오른쪽 아래

        for (int x = node.roomRect.xMin; x < node.roomRect.xMin + node.roomRect.width; x++)
        {
            for (int y = node.roomRect.yMin; y < node.roomRect.yMin + node.roomRect.height; y++)
            {
                _tilemap.SetTile(new Vector3Int(x, y, 0), _ruleTile);
            }
        }
    }

    private void DrawLine(Vector2Int start, Vector2Int end)
    {
        // var lineRenderer = Instantiate(_corridorLine).GetComponent<LineRenderer>();
        // lineRenderer.useWorldSpace = true;
        // lineRenderer.positionCount = 2;
        //
        // lineRenderer.SetPosition(0, new Vector3(start.x, start.y,0));
        // lineRenderer.SetPosition(1, new Vector3(end.x, end.y,0));

        for (int x = start.x; x < end.x; x++)
        {
            for (int y = start.y - 1; y <= end.y + 2; y++)
            {
                _tilemap.SetTile(new Vector3Int(x, y, 0), _ruleTile);
            }
        }

        for (int y = start.y; y > end.y; y--)
        {
            for (int x = start.x - 1; x <= end.x + 1; x++)
            {
                _tilemap.SetTile(new Vector3Int(x, y, 0), _ruleTile);
            }
        }

    }

    private void FillBG()
    {
        int w = _mapSize.x / 2;
        int h = _mapSize.y / 2;
        for (int i = -w - 10; i < w + 10; i++)
        {
            for (int j = -h - 10; j < h + 10; j++)
            {
                _tilemap.SetTile(new Vector3Int(i, j, 0), _outTile);
            }
        }
    }

    // private void ReplaceTile()
    // {
    //     // 전체 타일을 순회하며 예외처리 진행
    //     // _outTile을 해당하는 외곽 타일로 바꿔주는 방식
    //     // Case 1. 위, 아래, 왼쪽, 오른쪽 외곽선
    // }

    // private int[] CheckNeighbors(Vector2Int tile)
    // {
    //     int[] neighbors = new int[9];
    //     int i = 0;
    //     //0은 outtile, 1은 roomtile
    //     for (int y = tile.y - 1; y <= tile.y + 1; y++)
    //     {
    //         for (int x = tile.x - 1; x <= tile.x + 1; x++)
    //         {
    //             if (_tilemap.GetTile(new Vector3Int(x, y, 0)) == _outTile)
    //             {
    //                 neighbors[i] = 0;
    //             }
    //
    //             else if (_tilemap.GetTile(new Vector3Int(x, y, 0)) == _roomTile1 ||
    //                      _tilemap.GetTile(new Vector3Int(x, y, 0)) == _roomTile2)
    //             {
    //                 neighbors[i] = 1;
    //             }
    //
    //             i++;
    //         }
    //     }
    //     
    //     return neighbors;
    // }
    

    
    
    


}
