using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    private MapGenerator _mapGen; // 여기서 생성된 맵의 데이터를 받아옴 

    public Transform testPlayer; 
    public List<Node> path;
    
    public Vector2 gridSize;
    public Node[,] grid;
    public float nodeRadius;
    public LayerMask unwalkableMask;

    private int cellCountX, cellCountY;
    private float nodeWidth;

    private void Start()
    {
        // gridSize = GameManager.Instance.generator.mapSize; // MapGenerator에서 맵 크기에 대한 정보 받아옴
        nodeWidth = nodeRadius * 2;
        cellCountX = Mathf.RoundToInt(gridSize.x/nodeWidth);
        cellCountY = Mathf.RoundToInt(gridSize.y/nodeWidth);
        CreateGrid();
    }

    private void CreateGrid()
    {
        grid = new Node[cellCountX, cellCountY]; // 노드들을 담는 2차원 배열로서의 그리드
        Vector3 origin = transform.position - Vector3.right * gridSize.x/2 -  Vector3.up * gridSize.y/2; // 그리드 좌측하단의 기준점

        for (int x = 0; x < cellCountX; x++)
        {
            for (int y = 0; y < cellCountY; y++)
            {
                Vector3 worldPoint = origin + Vector3.right * ( x * nodeWidth + nodeRadius) + Vector3.up * ( y  * nodeWidth + nodeRadius );
                bool walkable = !(Physics2D.OverlapCircle(worldPoint, nodeRadius, unwalkableMask));
                grid [x, y] = new Node(walkable, worldPoint, x, y);
            }
        }
    }

    public Node WorldToNode(Vector3 worldPos) // 플레이어, 목적지에 활용
    {
        var pos = worldPos + Vector3.right * gridSize.x / 2 +  Vector3.up * gridSize.y / 2; // 그리드 원점(좌측 하단) 기준 플레이어 위치를 받기 위해 보정
        int x = Mathf.FloorToInt(pos.x / nodeWidth); // 노드의 너비가 1이 아닐 때도 사용할 수 있도록 보정
        int y = Mathf.FloorToInt(pos.y / nodeWidth);
        Debug.Log(x + "," + y);
        return grid[x, y];
    }

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                int checkX = node.gridX + x; // 이웃 노드의 x좌표
                int checkY = node.gridY + y; // 이웃 노드의 y좌표

                if (checkX >= 0 && checkX < gridSize.x && checkY >= 0 && checkY < gridSize.y) // 그리드 안에 있을 때만 추가
                {
                    neighbours.Add(grid[node.gridX + x, node.gridY + y]);
                }
            }
        }
        return neighbours;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(gridSize.x, gridSize.y, 1));
        if (grid != null)
        {
            Node playerNode = WorldToNode(testPlayer.position);
            foreach (Node n in grid)
            {
                Gizmos.color = (n.isWalkable)? Color.white : Color.red;
                if (playerNode == n)
                {
                    Gizmos.color = Color.cyan;
                }

                if (path != null && path.Contains(n))
                {
                    Gizmos.color = Color.black;
                }

                Gizmos.DrawCube(n.position, Vector3.one * (nodeWidth - 0.1f));
            }
        }
    }
}
