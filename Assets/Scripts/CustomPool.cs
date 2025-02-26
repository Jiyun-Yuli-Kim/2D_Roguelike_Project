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
        // stack�� ���� ũ�Ⱑ 0���� ũ�ٸ�
        if (pool.Count > 0)
        {
            T target = pool.Pop();
            target.gameObject.SetActive(true);
            return target;
        }

        else 
        {
            Debug.Log("������ ������ϴ�.");
            return null;
        }
    }

    public void Return(T target)
    {
        // ������ ���� ũ�Ⱑ ����� ���� �ʴ´ٸ�
        // ��Ȱ��ȭ
        // ���ÿ� �ֱ�
        if (pool.Count < size)
        {
            target.gameObject.SetActive(false);
            pool.Push(target);
        }

        else
        {
            Debug.Log("������ �� á���ϴ�.");
        }
    }
}
