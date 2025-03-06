using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    private MapGenerator _mapGen; // 여기서 생성된 맵의 데이터를 받아옴 

    private List<Cell> Open;
    private List<Cell> Closed;

    private Cell current;
    private Cell start;
    private Cell target;

    private void Awake()
    {
        Open.Add(start);
    }

    /*
    private void Foo()
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
