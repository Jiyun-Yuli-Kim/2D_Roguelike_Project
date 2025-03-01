using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    public RectInt roomRect;
    public Vector2Int roomCenter;
    public int[,] spawnArea;
    private int row;
    private int col;

    public void SetCenter()
    {
        roomCenter = new Vector2Int(roomRect.xMin + roomRect.width / 2, roomRect.yMin + roomRect.height / 2);
    }

    // MapGenerator에서 호출하는 함수로, 방 생성시 스폰영역을 생성함
    public void CreateSpawnArea()
    {
        //이차원배열의 x좌표는 roomRect.xMin+1 ~ roomRect.xMin+1 + (roomRect.width-2)
        //이차원배열의 y좌표는 roomRect.yMin+1 ~ roomRect.yMin+1 + (roomRect.height-3)
        row = roomRect.height - 3; // 위에 3개, 아래에 1개의 룰타일 영역 제외
        col = roomRect.width - 2; // 양옆 1개의 룰타일 영역 제외
        spawnArea = new int[row, col];
        //Debug.Log($"스폰영역 행 : {spawnArea.GetLength(1)}, 열 : {spawnArea.GetLength(0)}");

        //for (int r = 0; r < row; r++)
        //{
        //    for (int c = 0; c < col; c++)
        //    { 
        //        spawnArea[r, c] = 0;
        //    }
        //}
    }
}
