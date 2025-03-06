using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    public bool isWalkable;
    public Vector3 position;
    public float g;
    public float h;
    public List<Cell> Neighbours;
 
    
    
}