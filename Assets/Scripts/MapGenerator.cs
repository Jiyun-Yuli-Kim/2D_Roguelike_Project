using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] Vector2Int _mapSize; // 기본 맵 사이즈
    private int _curDepth = 1;
    [SerializeField] int _maxDepth = 30; // 트리의 높이 -> 방 16개
    [SerializeField] float _minRate; // 이등분할 때의 최소비율
    [SerializeField] float _maxRate; // 이등분할 때의 최대비율
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
        Partition(RootNode);
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

    private void GenerateMap()
    {
        for (int i = 0; i < _maxDepth; i++)
        {
            
        }
        
        /*
        1. 가로 세로 중 더 긴 것을 구해 로컬변수 maxLength에 저장
         1) 이 때, Node._isHorizontalCut에 가로로 잘랐는지 여부 저장
        2. maxLength를 RandomRange에 의해 나눔
         1) 기준좌표(왼쪽아래)의 x좌표에 maxLength * RandomRange 더한 값을 기준으로 가름
         2) 렌더링: (기준좌표.x + maxLength * RandomRange, 기준좌표.y)와 (기준좌표.x + maxLength * RandomRange, 기준좌표.y+minLength)
        3. 노드 연결
         1) 자식노드와 부모노드 서로 연결하기
        */
    }

    private void Partition(Node node)
    {
        if (_curDepth >= _maxDepth)
        {
            return;
        }
        
        var lineRenderer = Instantiate(_line).GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = true;
        lineRenderer.positionCount = 2;
        _curDepth++;

        if (node.nodeRect.height > node.nodeRect.width)
        {
            node._isHorizontalCut = true;
            int maxLength = node.nodeRect.height;
            int minLength = node.nodeRect.width;
            int split = Mathf.RoundToInt(maxLength * Random.Range(_minRate, _maxRate));
            Debug.Log(split);
            
            lineRenderer.SetPosition(0, new Vector3(node.nodeRect.xMin, node.nodeRect.yMin+split, 0));
            lineRenderer.SetPosition(1, new Vector3(node.nodeRect.xMin+minLength, node.nodeRect.yMin+split, 0));
            node.node1 = new Node(new RectInt(node.nodeRect.xMin, node.nodeRect.yMin+split, minLength, maxLength-split));
            node.node2 = new Node(new RectInt(node.nodeRect.xMin, node.nodeRect.yMin, minLength, split));
            node.node1.parNode = node;
            node.node2.parNode = node;
            Partition(node.node1);
            Partition(node.node2);
        }
        
        else if (node.nodeRect.width >= node.nodeRect.height)
        {
            node._isHorizontalCut = false;
            int maxLength = node.nodeRect.width;
            int minLength = node.nodeRect.height;
            int split = Mathf.RoundToInt(maxLength * Random.Range(_minRate, _maxRate));
            Debug.Log(split);
            
            lineRenderer.SetPosition(0, new Vector3(node.nodeRect.xMin+split, node.nodeRect.yMin, 0));
            lineRenderer.SetPosition(1, new Vector3(node.nodeRect.xMin+split, node.nodeRect.yMin+minLength, 0));
            node.node1 = new Node(new RectInt(node.nodeRect.xMin, node.nodeRect.yMin, split, minLength));
            node.node2 = new Node(new RectInt(node.nodeRect.xMin+split, node.nodeRect.yMin, maxLength-split, minLength));
            node.node1.parNode = node;
            node.node2.parNode = node;
            Partition(node.node1);
            Partition(node.node2);
        }
    }


}
