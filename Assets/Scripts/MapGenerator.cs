using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] Vector2Int _mapSize = new Vector2Int(60,40); // 기본 맵 사이즈
    [SerializeField] int _maxDepth = 4; // 트리의 높이 -> 방 16개
    [SerializeField] float _minRate; // 이등분할 때의 최소비율
    [SerializeField] float _maxRate; // 이등분할 때의 최대비율
    [SerializeField] Tile _roomTile; // 방 안쪽 타일
    [SerializeField] Tile _wallTile; // 경계를 나타내는 타일
    [SerializeField] Tile _outTile; // 외부를 나타내는 타일

    private bool _isHorizontalCut; // true면 위아래로 나눔, false면 양옆으로 나눔
    
    // 라인 렌더링
    [SerializeField] LineRenderer _lineRenderer;
    [SerializeField] GameObject _outLine; // 초기 맵
    [SerializeField] GameObject _line; // 공간 분할
    [SerializeField] GameObject _roomLine; // 방 외곽선

    private void Start()
    {
        GenerateMap();
    }

    private void GenerateMap()
    {
        // 초기 맵 형성, linerenderer로 라인 그리기
        _lineRenderer.positionCount = 4;
        _lineRenderer.SetPosition(0, new Vector2(0, 0) - _mapSize/2);
        _lineRenderer.SetPosition(1, new Vector2(0, _mapSize.y) - _mapSize/2);
        _lineRenderer.SetPosition(2, new Vector2(_mapSize.x, _mapSize.y) - _mapSize/2);
        _lineRenderer.SetPosition(3, new Vector2(_mapSize.x, 0) - _mapSize/2);
        // 첫번째 노드를 _minRate~_maxRate 사이의 랜덤 수로 갈라주기
        //  
        for (int i = 0; i < _maxDepth; i++)
        {
            
        }
    }
}
