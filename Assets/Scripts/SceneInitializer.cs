using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInitializer : MonoBehaviour, IInitializable
{
    [SerializeField] private List<GameObject> _initializables = new();

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        _initializables[0] = GameManager.Instance.setter.gameObject;
        
        foreach (GameObject i in _initializables)
        {
            Debug.Log(i.name);
            i.GetComponent<IInitializable>().SceneInitialize();
        }
    }
    
    public void SceneInitialize()
    {
    }
}
