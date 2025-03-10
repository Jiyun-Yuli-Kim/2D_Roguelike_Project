using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInitializable
{
    /// <summary>
    /// 씬 초기화 작업 시 호출되어야 할 기능
    /// </summary>
    public void SceneInitialize();
}
