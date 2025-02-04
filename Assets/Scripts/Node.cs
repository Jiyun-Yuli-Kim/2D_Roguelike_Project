using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Node parNode;
    public Node node1;
    public Node node2;
    public RectInt nodeRect;

    public Node(RectInt nodeRect)
    {
        this.nodeRect = nodeRect;
    }
}
