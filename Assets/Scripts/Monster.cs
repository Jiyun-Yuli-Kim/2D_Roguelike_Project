using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public virtual void Move()
    {
        StartCoroutine(MoveRandomly());
    }

    private IEnumerator MoveRandomly()
    {
        // 랜덤 방향과 랜덤 초를 받는다
        float randSec = Random.Range(1, 5);
        Vector3 randDir = new Vector3(Random.Range(1, 5), Random.Range(1, 5), 0);
        yield return new WaitForSeconds(randSec);
    }
}
