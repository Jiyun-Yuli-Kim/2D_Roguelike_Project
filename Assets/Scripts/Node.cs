using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node // 왜 안붙였을까?
{
    public Node parNode;
    public Node node1;
    public Node node2;
    public RectInt nodeRect;
    public Vector2Int nodeCenter;
    public Room room;
    public bool _isHorizontalCut; // true면 위아래로 나눔, false면 양옆으로 나눔

    public Node(RectInt NodeRect)
    {
        this.nodeRect = NodeRect;
        this.nodeCenter = new Vector2Int(NodeRect.xMin + NodeRect.width/2, NodeRect.yMin + NodeRect.height/2);
    }
}
