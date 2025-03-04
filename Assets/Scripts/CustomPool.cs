using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomPool<T> where T : MonoBehaviour
{
    private Stack<T> pool = new();
    public int size { get; private set; }

    public CustomPool(int Size)
    { 
        size = Size;
    }

    public T Get()
    {
        // stack의 현재 크기가 0보다 크다면
        if (pool.Count > 0)
        {
            T target = pool.Pop();
            target.gameObject.SetActive(true);
            return target;
        }

        else 
        {
            Debug.Log("스택이 비었습니다.");
            return null;
        }
    }

    public void Return(T target)
    {
        // 스택의 현재 크기가 사이즈를 넘지 않는다면
        // 비활성화
        // 스택에 넣기
        if (pool.Count < size)
        {
            target.gameObject.SetActive(false);
            pool.Push(target);
        }

        else
        {
            Debug.Log("스택이 꽉 찼습니다.");
        }
    }
}
