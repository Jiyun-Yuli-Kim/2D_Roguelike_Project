using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    private MapGenerator _mapGen; // 여기서 생성된 맵의 데이터를 받아옴 

    public Transform testPlayer; 
    
    public Vector2 gridSize;
    public Node[,] grid;
    public float nodeRadius;
    public LayerMask unwalkableMask;
    
    private List<Node> Open;
    private List<Node> Closed;

    private Node current;
    private Node start;
    private Node target;

    private int cellCountX, cellCountY;
    private float nodeWidth;
    
    private void Awake()
    {
        // Open.Add(start);
    }

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
                grid [x, y] = new Node(walkable, worldPoint);
            }
        }
    }

    public Node GetPlayerCurNode(Vector3 playerPos)
    {
        var pos = playerPos + Vector3.right * gridSize.x / 2 +  Vector3.up * gridSize.y / 2; // 그리드 원점(좌측 하단) 기준 플레이어 위치를 받기 위해 보정
        int x = Mathf.FloorToInt(pos.x / nodeWidth); // 노드의 너비가 1이 아닐 때도 사용할 수 있도록 보정
        int y = Mathf.FloorToInt(pos.y / nodeWidth);
        Debug.Log(x + "," + y);
        return grid[x, y];
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(gridSize.x, gridSize.y, 1));
        if (grid != null)
        {
            Node playerNode = GetPlayerCurNode(testPlayer.position);
            foreach (Node n in grid)
            {
                Gizmos.color = (n.isWalkable)? Color.white : Color.red;
                if (playerNode == n)
                {
                    Gizmos.color = Color.cyan;
                }
                Gizmos.DrawCube(n.position, Vector3.one * (nodeWidth - 0.1f));
            }
        }
    }





    /*
public void FindPath()
{
    while (true) // 일단 조건은 임의로 설정
    {
        if (current == target)
        {
            return;
        }

        current = Open에서 가장 f가 작은 Cell
        Open.Remove(current);
        Closed.Add(current);

        if (current == target)
        {
            return;
        }

        foreach(neighbour in current.neighbours)
        {
            if (!isWalkable || Closed에 있을 시)
            다음 노드로 넘어가기

            if (new path가 최단거리이거나 Open에 없을 시)
            set f of neighbour
            set parent of neighbour to current
            if n is not in Open
            Open.Add(n);
        }
    }
}
*/

}
