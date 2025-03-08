using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] Vector2Int _mapSize; // 기본 맵 사이즈
    [SerializeField] int _maxDepth; // 트리의 높이
    [SerializeField] float _minRate; // 이등분 할 때의 최소비율
    [SerializeField] float _maxRate; // 이등분 할 때의 최대비율
    [SerializeField] float _roomMinRate; // 방 생성 시 노드 대비 최소비율
    [SerializeField] float _roomMaxRate; // 방 생성 시 노드 대비 최대비율
    [SerializeField] float _divideRate; // 랜덤 분할 시 기준이 되는 가로/세로 혹은 세로/가로 비율의 최대값.
    [SerializeField] int _minAreaHeight;
    [SerializeField] int _minAreaWidth; // 노드의 최소 넓이. 이것보다 작은 노드는 생성되지 않음.

    [SerializeField] Tilemap _tilemap;
    
    [SerializeField] Tile _outTile; // 외부를 나타내는 타일
    [SerializeField] RuleTile _ruleTile;
    
    [SerializeField] Tilemap _minimap;
    [SerializeField] RuleTile  _MMOutTile;
    [SerializeField] RuleTile _MMInTile;

    // 라인 렌더링
    //[SerializeField] LineRenderer _lineRenderer;
    //[SerializeField] GameObject _initMap; // 초기 맵
    //[SerializeField] GameObject _line; // 공간 분할
    //[SerializeField] GameObject _roomLine; // 방 외곽선
    //[SerializeField] GameObject _corridorLine;

    private void Awake()
    {
    }

    private void Start()
    {
        GameManager.Instance.generator = this;
        // Init();
    }

    public void Init()
    {
        GenerateMap(); // 방 생성
        GenerateCorridor(GameManager.Instance.setter.curStageData.stageRoomList); // 복도 생성
    }

    public void GenerateMap()
    {
        // _mapSize = new Vector2Int(90, 60);
        FillBG();
        FillMinimapBG(); // 렌더링할 미니맵
        BSPNode rootBspNode = new BSPNode(new RectInt(-_mapSize.x / 2, -_mapSize.y / 2, _mapSize.x, _mapSize.y));
        // DrawRootNode();
        SplitNode(rootBspNode, 0);
        // 각 방에 대해 스폰할 몬스터 개수 설정
        // 방 생성 완료 후 실행될 로직이므로 일단 여기...
        // GameManager.Instance.setter.SetMonsterCount();
    }
    

    //private void DrawRootNode()
    //{
    //    // 초기 맵 형성, linerenderer로 라인 그리기
    //    // 영점기준으로 하면 화면 우측 상단에 치우치는 관계로 _mapSize/2를 각각 빼줌
    //    _lineRenderer.positionCount = 4;
    //    _lineRenderer.SetPosition(0, new Vector2(0, 0) - _mapSize / 2); // 왼쪽 아래
    //    _lineRenderer.SetPosition(1, new Vector2(0, _mapSize.y) - _mapSize / 2); // 왼쪽 위
    //    _lineRenderer.SetPosition(2, new Vector2(_mapSize.x, _mapSize.y) - _mapSize / 2); // 오른쪽 위
    //    _lineRenderer.SetPosition(3, new Vector2(_mapSize.x, 0) - _mapSize / 2); // 오른쪽 아래
    //}

    private void SplitNode(BSPNode bspNode, int depth)
    {
        //var lineRenderer = Instantiate(_line).GetComponent<LineRenderer>();
        //lineRenderer.useWorldSpace = true;

        bool isVerticallyCut = Random.value > 0.5f; // 기본적으로는 자르는 방향을 랜덤하게

        if (bspNode.nodeRect.width / bspNode.nodeRect.height > _divideRate) // 가로/세로 비가 특정 비율 이상이면 가로로 자르기
        {
            isVerticallyCut = false;
        }

        else if (bspNode.nodeRect.height / bspNode.nodeRect.width > _divideRate) // 세로/가로 비가 특정 비율 이상이면 세로로 자르기
        {
            isVerticallyCut = true;
        }

        // 세로분할
        if (isVerticallyCut)
        {
            if (depth > _maxDepth)
            {
                return;
            }

            bspNode._isHorizontalCut = true;
            int height = bspNode.nodeRect.height;
            int width = bspNode.nodeRect.width;
            int split = Mathf.RoundToInt(height * Random.Range(_minRate, _maxRate));

            //lineRenderer.positionCount = 2;
            //lineRenderer.SetPosition(0, new Vector3(node.nodeRect.xMin, node.nodeRect.yMin + split, 0));
            //lineRenderer.SetPosition(1, new Vector3(node.nodeRect.xMin + width, node.nodeRect.yMin + split, 0));

            // 자식 노드 생성 및 현재 노드를 부모노드로 설정
            bspNode.node1 = new BSPNode(new RectInt(bspNode.nodeRect.xMin, bspNode.nodeRect.yMin + split, width, height - split));
            bspNode.node2 = new BSPNode(new RectInt(bspNode.nodeRect.xMin, bspNode.nodeRect.yMin, width, split));
            bspNode.node1.ParBspNode = bspNode;
            bspNode.node2.ParBspNode = bspNode;

            // 현재 노드가 리프 노드인지 확인하고, 리프노드라면 방 생성
            if (depth == _maxDepth)
            {
                // Debug.Log($"노드 중심 : {node.nodeCenter.x}, {node.nodeCenter.y}");

                // 노드의 높이나 너비가 최소 기준보다 작으면 방 생성 건너뛰기
                if (bspNode.nodeRect.width < _minAreaWidth + 2 || bspNode.nodeRect.height < _minAreaHeight +3)
                {
                    bspNode = null;
                    return;
                }

                GenerateRoom(bspNode);
                // DrawLine(node.nodeCenter, node.parNode.nodeCenter);

                bspNode._hasRoom = true;

                if (bspNode.ParBspNode.node1._hasRoom && bspNode.ParBspNode.node2._hasRoom)
                {
                    // DrawLine(node.parNode.node1.nodeCenter, node.parNode.node2.nodeCenter);
                    return;
                }

            }

            // 노드 중심끼리 연결하여 복도 생성
            //if (depth < 2)
            //{
            //    DrawLine(node.node1.nodeCenter, node.node2.nodeCenter);
            //}

            SplitNode(bspNode.node1, depth + 1);
            SplitNode(bspNode.node2, depth + 1);
        }


        // 가로분할
        else if (!isVerticallyCut)
        {
            if (depth > _maxDepth)
            {
                return;
            }
            bspNode._isHorizontalCut = false;
            int width = bspNode.nodeRect.width;
            int height = bspNode.nodeRect.height;
            int split = Mathf.RoundToInt(width * Random.Range(_minRate, _maxRate));

            //lineRenderer.positionCount = 2;
            //lineRenderer.SetPosition(0, new Vector3(node.nodeRect.xMin + split, node.nodeRect.yMin, 0));
            //lineRenderer.SetPosition(1, new Vector3(node.nodeRect.xMin + split, node.nodeRect.yMin + height, 0));
            bspNode.node1 = new BSPNode(new RectInt(bspNode.nodeRect.xMin, bspNode.nodeRect.yMin, split, height));
            bspNode.node2 = new BSPNode(new RectInt(bspNode.nodeRect.xMin + split, bspNode.nodeRect.yMin, width - split, height));
            bspNode.node1.ParBspNode = bspNode;
            bspNode.node2.ParBspNode = bspNode;

            // 현재 노드가 리프 노드인지 확인하고, 리프노드라면 방 생성
            if (depth == _maxDepth)
            {
                //Debug.Log($"노드 중심 : {node.nodeCenter.x}, {node.nodeCenter.y}");

                // 노드의 높이나 너비가 최소 기준보다 작으면 방 생성 건너뛰기
                if (bspNode.nodeRect.width < _minAreaWidth + 2 || bspNode.nodeRect.height < _minAreaHeight + 3)
                {
                    bspNode = null;
                    return;
                }

                GenerateRoom(bspNode);
                // DrawLine(node.nodeCenter, node.parNode.nodeCenter);

                bspNode._hasRoom = true;

                if (bspNode.ParBspNode.node1._hasRoom && bspNode.ParBspNode.node2._hasRoom)
                {
                    // DrawLine(node.parNode.node1.nodeCenter, node.parNode.node2.nodeCenter);
                    return;
                }

            }

            // 노드 중심끼리 연결하여 복도 생성
            //if (depth < 2)
            //{
            //    DrawLine(node.node1.nodeCenter, node.node2.nodeCenter);
            //}

            SplitNode(bspNode.node1, depth + 1);
            SplitNode(bspNode.node2, depth + 1);

        }
    }

    // 지금 생성한 노드라 리프노드라면, 방을 만들어준다.
    // - 현재 로직상, maxDepth 도달 이전에 리프노드가 생성되기도 함
    //  1) maxDepth일 때
    //  2) 더 작은 방을 생성할 수 없을 때

    // - 방 만들기 로직
    //  1) 노드의 width, height를 가져온다
    //  2) 여기서 랜덤하게 방의 width, height를 뽑는다
    //  3) 노드센터를 기준으로 (x-w/2, y-h/2)가 룸의 RectInt의 xMin, yMin이 된다

    // 노드 안에 방을 생성한다.
    private void GenerateRoom(BSPNode bspNode)
    {
        //Debug.Log("RoomGen");

        bspNode.room = new Room();

        int roomWidth = Mathf.RoundToInt(bspNode.nodeRect.width * Random.Range(_roomMinRate, _roomMaxRate));
        if (roomWidth < _minAreaWidth)
        {
            roomWidth = _minAreaWidth; // 방의 최소 너비 보장
        }
        int roomHeight = Mathf.RoundToInt(bspNode.nodeRect.height * Random.Range(_roomMinRate, _roomMaxRate));
        if (roomHeight < _minAreaHeight)
        {
            roomHeight = _minAreaHeight; // 방의 최소 높이 보장
        }

        // roomRect의 xMin과 yMin값을 현재는 nodeCenter에서 roomw/2, roomh/2 만큼을 빼주고 있다.
        // 모든 룸이 가운데 정렬되어 단조로워보인다는 단점이 있다.
        // 이에, 방의 시작 좌표에 랜덤성을 부여하고자 한다.
        int roomX = Mathf.RoundToInt(Random.Range(bspNode.nodeRect.xMin + 1,
            bspNode.nodeRect.xMin + bspNode.nodeRect.width - roomWidth - 1));
        int roomY = Mathf.RoundToInt(Random.Range(bspNode.nodeRect.yMin + 1,
            bspNode.nodeRect.yMin + bspNode.nodeRect.height - roomHeight - 1));

        bspNode.room.roomRect = new RectInt(roomX, roomY, roomWidth, roomHeight);
        bspNode.room.SetCenter();
        //Debug.Log($"방 중심 : {node.room.roomCenter.x}, {node.room.roomCenter.y}");
        bspNode.room.CreateSpawnArea();

        //Debug.Log($"시작점 x좌표 : {node.room.roomRect.xMin}, 시작점 y좌표 : {node.room.roomRect.xMin}," +
        //    $"너비 : {node.room.roomRect.width}, 높이 : {node.room.roomRect.height}");

        // 현재 스테이지 내의 방 개수를 확인하기 위함
        GameManager.Instance.setter.curStageData.stageRoomList.Add(bspNode.room);
        GameManager.Instance.setter.curStageData.stageRoomCount++;

        DrawRoom(bspNode);
        DrawMinimapRoom(bspNode);

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

    private void DrawRoom(BSPNode bspNode)
    {
        // var lineRenderer = Instantiate(_roomLine).GetComponent<LineRenderer>();
        // lineRenderer.useWorldSpace = true; // 빼먹지 말자
        // lineRenderer.positionCount = 4;
        //
        // lineRenderer.SetPosition(0, new Vector3(node.roomRect.xMin, node.roomRect.yMin, 0)); // 왼쪽 아래
        // lineRenderer.SetPosition(1, new Vector3(node.roomRect.xMin, node.roomRect.yMin + node.roomRect.height, 0)); // 왼쪽 위
        // lineRenderer.SetPosition(2, new Vector3(node.roomRect.xMin + node.roomRect.width, node.roomRect.yMin + node.roomRect.height, 0)); // 오른쪽 위
        // lineRenderer.SetPosition(3, new Vector3(node.roomRect.xMin + node.roomRect.width, node.roomRect.yMin, 0)); // 오른쪽 아래

        for (int x = bspNode.room.roomRect.xMin; x < bspNode.room.roomRect.xMin + bspNode.room.roomRect.width; x++)
        {
            for (int y = bspNode.room.roomRect.yMin; y < bspNode.room.roomRect.yMin + bspNode.room.roomRect.height; y++)
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
        if (start.x >= end.x && start.y >= end.y)
        {
            for (int x = end.x; x < start.x; x++)
            {
                for (int y = end.y - 1; y <= start.y + 2; y++) // 위아래 경계 타일 3개에 대한 오프셋
                {
                    _tilemap.SetTile(new Vector3Int(x, y, 0), _ruleTile);
                }
            }

            for (int y = end.y; y < start.y; y++)
            {
                for (int x = end.x - 1; x <= start.x + 1; x++) // 양옆 경계 타일 2개에 대한 오프셋
                {
                    _tilemap.SetTile(new Vector3Int(x, y, 0), _ruleTile);
                }
            }
        }
        
        if (start.x >= end.x && start.y < end.y)
        {
            for (int x = end.x; x < start.x; x++)
            {
                for (int y = start.y - 1; y <= end.y + 2; y++) // 위아래 경계 타일 3개에 대한 오프셋
                {
                    _tilemap.SetTile(new Vector3Int(x, y, 0), _ruleTile);
                }
            }

            for (int y = start.y; y < end.y; y++)
            {
                for (int x = end.x - 1; x <= start.x + 1; x++) // 양옆 경계 타일 2개에 대한 오프셋
                {
                    _tilemap.SetTile(new Vector3Int(x, y, 0), _ruleTile);
                }
            }
        }
        
        if (start.x < end.x && start.y >= end.y)
        {
            for (int x = start.x; x < end.x; x++)
            {
                for (int y = end.y - 1; y <= start.y + 2; y++) // 위아래 경계 타일 3개에 대한 오프셋
                {
                    _tilemap.SetTile(new Vector3Int(x, y, 0), _ruleTile);
                }
            }

            for (int y = end.y; y < start.y; y++)
            {
                for (int x = start.x - 1; x <= end.x + 1; x++) // 양옆 경계 타일 2개에 대한 오프셋
                {
                    _tilemap.SetTile(new Vector3Int(x, y, 0), _ruleTile);
                }
            }
        }
        
        if (start.x < end.x && start.y < end.y)
        {
            for (int x = start.x; x < end.x; x++)
            {
                for (int y = start.y - 1; y <= end.y + 2; y++) // 위아래 경계 타일 3개에 대한 오프셋
                {
                    _tilemap.SetTile(new Vector3Int(x, y, 0), _ruleTile);
                }
            }

            for (int y = end.y; y > start.y; y--)
            {
                for (int x = start.x - 1; x <= end.x + 1; x++) // 양옆 경계 타일 2개에 대한 오프셋
                {
                    _tilemap.SetTile(new Vector3Int(x, y, 0), _ruleTile);
                }
            }
        }
    }

    // 배경 타일을 채움
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

    // 방 생성 완료 후, 복도 따로 생성 
    public void GenerateCorridor(List<Room> rooms)
    {

        for (int i = 0; i < rooms.Count - 1; i++)
        {
            DrawCorridor(rooms[i], rooms[i + 1]);
            //DrawLine(new Vector2Int(rooms[i].roomCenter.x, rooms[i].roomCenter.y), new Vector2Int(rooms[i + 1].roomCenter.x, rooms[i].roomCenter.y));
            //DrawLine(new Vector2Int(rooms[i + 1].roomCenter.x, rooms[i].roomCenter.y), new Vector2Int(rooms[i + 1].roomCenter.x, rooms[i+1].roomCenter.y));
        }
        // DrawCorridor(rooms[1], rooms[4]);
        // DrawCorridor(rooms[0], rooms[rooms.Count - 1]); // 끝방과 첫방 연결
    }

    void DrawCorridor(Room room1, Room room2)
    {
        int x1 = room1.roomCenter.x;
        int x2 = room2.roomCenter.x;
        int y1 = room1.roomCenter.y;
        int y2 = room2.roomCenter.y;
        
        DrawLine(new Vector2Int(x1, y1), new Vector2Int(x2, y1));
        DrawLine(new Vector2Int(x2, y1), new Vector2Int(x2, y2));
        
        DrawMinimapLine(new Vector2Int(x1, y1), new Vector2Int(x2, y1));
        DrawMinimapLine(new Vector2Int(x2, y1), new Vector2Int(x2, y2));

        //for (int i = x1; i < x2; i++)
        //{
        //    for (int j = y1 - 1; j <= y2 + 2; j++) // 위아래 경계 타일 3개에 대한 오프셋
        //    {
        //        _tilemap.SetTile(new Vector3Int(i, j, 0), _ruleTile);
        //    }
        //}

        //for (int y = start.y; y > end.y; y--)
        //{
        //    for (int x = start.x - 1; x <= end.x + 1; x++) // 양옆 경계 타일 2개에 대한 오프셋
        //    {
        //        _tilemap.SetTile(new Vector3Int(x, y, 0), _ruleTile);
        //    }
        //}
    }

    // 미니맵 배경 타일을 채움
    private void FillMinimapBG()
    {
        int w = _mapSize.x / 2;
        int h = _mapSize.y / 2;
        for (int i = -w - 10; i < w + 10; i++)
        {
            for (int j = -h - 10; j < h + 10; j++)
            {
                _minimap.SetTile(new Vector3Int(i, j, 0), _MMOutTile);
            }
        }
    }
    
    private void DrawMinimapRoom(BSPNode bspNode)
    {
        for (int x = bspNode.room.roomRect.xMin; x < bspNode.room.roomRect.xMin + bspNode.room.roomRect.width; x++)
        {
            for (int y = bspNode.room.roomRect.yMin; y < bspNode.room.roomRect.yMin + bspNode.room.roomRect.height; y++)
            {
                _minimap.SetTile(new Vector3Int(x, y, 0), _MMInTile);
            }
        }
    }
    
    private void DrawMinimapLine(Vector2Int start, Vector2Int end)
    {
        // var lineRenderer = Instantiate(_corridorLine).GetComponent<LineRenderer>();
        // lineRenderer.useWorldSpace = true;
        // lineRenderer.positionCount = 2;
        //
        // lineRenderer.SetPosition(0, new Vector3(start.x, start.y,0));
        // lineRenderer.SetPosition(1, new Vector3(end.x, end.y,0));

        for (int x = start.x; x < end.x; x++)
        {
            for (int y = start.y; y <= end.y; y++)
            {
                _minimap.SetTile(new Vector3Int(x, y, 0), _MMInTile);
            }
        }

        for (int y = start.y; y > end.y; y--)
        {
            for (int x = start.x; x <= end.x; x++) 
            {
                _minimap.SetTile(new Vector3Int(x, y, 0), _MMInTile);
            }
        }
    }
}
 