using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BSPNode // 생성자를 사용하기 위해 MonoBehaviour 상속받지 않음
{
    public BSPNode ParBspNode;
    public BSPNode node1;
    public BSPNode node2;
    public RectInt nodeRect;
    public Vector2Int nodeCenter;
    public Room room;
    public bool _isHorizontalCut; // true면 위아래로 나눔, false면 양옆으로 나눔
    public bool _hasRoom;

    public BSPNode(RectInt NodeRect)
    {
        this.nodeRect = NodeRect;
        this.nodeCenter = new Vector2Int(NodeRect.xMin + NodeRect.width/2, NodeRect.yMin + NodeRect.height/2);
    }
}
