using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigator : MonoBehaviour, IPickupable
{
    private Grid grid;
        
    private List<Node> Open;
    private List<Node> Closed;

    private Node current;
    private Node start;
    private Node target;
    
    public Transform seeker, goal;

    private void Awake()
    {
        grid = GetComponent<Grid>(); // 임시로 이렇게 진행
    }

    private void Update()
    {
        FindPath(seeker.position, goal.position);
    }

    public void FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Node startNode = grid.WorldToNode(startPos);
        Node targetNode = grid.WorldToNode(targetPos);

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node CurNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                // f값이 가장 작은 노드를 탐색하겠다. 만약 f값이 같다면 h값이 더 작은 노드를 탐색하겠다.
                if (openSet[i].fCost < CurNode.fCost || openSet[i].fCost == CurNode.fCost && openSet[i].hCost < CurNode.hCost)
                {
                    CurNode = openSet[i];
                }
            }
            
            openSet.Remove(CurNode);
            closedSet.Add(CurNode);

            if (CurNode == targetNode)
            {
                RetraceNode(startNode, targetNode);
                return;
            }

            foreach (Node neighbour in grid.GetNeighbours(CurNode))
            {
                if (!neighbour.isWalkable || closedSet.Contains(neighbour)) // 걸을 수 없거나 이미 탐색되었다면 건너뛴다.
                {
                    continue;
                }
                
                int newGCostToNeighbour = CurNode.gCost + GetDistance(CurNode, neighbour);
                if (newGCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newGCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = CurNode;

                    if (!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                    }
                }
            }
            

        }

    }

    private int GetDistance(Node a, Node b)
    {
        int moveX = Mathf.Abs(a.gridX - b.gridX);
        int moveY = Mathf.Abs(a.gridY - b.gridY);
        
        if (moveX > moveY)
        {
            moveX -= moveY;
            return moveX * 10 + moveY * 14;
        }

        else if (moveX < moveY)
        {
            moveY -= moveX;
            return moveY * 10 + moveX * 14;
        }
        
        else // moveX == moveY (위의 케이스에 포함되지만 가독성을 위해 명시)
        {
            return moveX * 14;
        }
    }

    private void RetraceNode(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        
        path.Reverse();
        grid.path = path;
    }

    public void OnPickup()
    {
        
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

    if (current == target) 왜 여기서 할까요? 가장짧으니까 얘가 curNode -> 어 근데 타겟이네? -> 종료
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
