using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : Item
{
    private void Awake()
    {
        name = "Heart";
    }

    public override void OnPickup()
    {
        if (GameManager.Instance.player.PlayerHP.Value >= 8) // MaxHP 초과하지 않도록
        {
            GameManager.Instance.player.PlayerHP.Value = 10;
        }
        else if (GameManager.Instance.player.PlayerHP.Value % 2 == 1) // 반쪽짜리 하트가 있다면, 하트를 1.5개 채움
        {
            GameManager.Instance.player.PlayerHP.Value += 3;
        }

        else if (GameManager.Instance.player.PlayerHP.Value % 2 == 0)
        {
            GameManager.Instance.player.PlayerHP.Value += 2;
        }
    }
}
