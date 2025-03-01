using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner<T> where T : MonoBehaviour
{
    public List<T> objList = new();


    public virtual void Spawn()
    { 
        
    }

}
