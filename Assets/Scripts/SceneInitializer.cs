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
            i.GetComponent<IInitializable>().SceneInitialize();
        }
    }
    
    // TODO: KimJaeSeong - 초기화를 담당하는 객체 또한 초기화 작업이 필요할 수 있으므로 인터페이스 추가해놨음. 삭제해도 됨
    public void SceneInitialize()
    {
    }
}
