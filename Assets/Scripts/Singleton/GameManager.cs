using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    // public SceneChanger sceneChanger; // 얘는 왜 굳이 오픈을 했는지
    public StageDataSetter setter;

    void Awake()
    {
        if (Instance != null)
        { 
            Destroy(gameObject); // 왜 this 아니고 오브젝트를 지울지 잘 생각해보기
        }

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void LoadScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }
}
