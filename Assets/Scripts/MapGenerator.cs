using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] Vector2Int _mapSize; // 기본 맵 사이즈
    [SerializeField] int _maxDepth = 4; // 트리의 높이 -> 방 16개
    [SerializeField] float _minRate; // 이등분할 때의 최소비율
    [SerializeField] float _maxRate; // 이등분할 때의 최대비율
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

    private void SplitNode(Node node, int depth)
    {
        if (depth > _maxDepth)
        {
            return;
        }

        if (node.nodeRect.width * node.nodeRect.height < _minArea)
        {
            return;
        }

        var lineRenderer = Instantiate(_line).GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = true;

        bool randomDir = Random.value > 0.5f; // 기본적으로는 자르는 방향을 랜덤하게
        
        if (node.nodeRect.width / node.nodeRect.height > _divideRate) // 가로/세로 비가 
        {
            randomDir = false;
        }

        else if (node.nodeRect.height / node.nodeRect.width > _divideRate)
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
            
            Debug.Log($"{node.node1.nodeRect.width}, {node.node1.nodeRect.height}");
            Debug.Log($"{node.node2.nodeRect.width}, {node.node2.nodeRect.height}");

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
            
            Debug.Log($"{node.node1.nodeRect.width}, {node.node1.nodeRect.height}");
            Debug.Log($"{node.node2.nodeRect.width}, {node.node2.nodeRect.height}");
        }
    }

    // private void DevideHorizontal()
    // {
    //     
    // }
    //
    // private void DevideVertical()
    // {
    //     
    // }


}
