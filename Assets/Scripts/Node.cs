using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Node parNode;
    public Node node1;
    public Node node2;
    public RectInt nodeRect;
    public bool _isHorizontalCut; // true면 위아래로 나눔, false면 양옆으로 나눔
    
    public Node(RectInt nodeRect)
    {
        this.nodeRect = nodeRect;
    }
}
