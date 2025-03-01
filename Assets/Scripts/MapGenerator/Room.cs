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

    // MapGenerator���� ȣ���ϴ� �Լ���, �� ������ ���������� ������
    public void CreateSpawnArea()
    {
        //�������迭�� x��ǥ�� roomRect.xMin+1 ~ roomRect.xMin+1 + (roomRect.width-2)
        //�������迭�� y��ǥ�� roomRect.yMin+1 ~ roomRect.yMin+1 + (roomRect.height-3)
        row = roomRect.height - 3; // ���� 3��, �Ʒ��� 1���� ��Ÿ�� ���� ����
        col = roomRect.width - 2; // �翷 1���� ��Ÿ�� ���� ����
        spawnArea = new int[row, col];
        //Debug.Log($"�������� �� : {spawnArea.GetLength(1)}, �� : {spawnArea.GetLength(0)}");

        //for (int r = 0; r < row; r++)
        //{
        //    for (int c = 0; c < col; c++)
        //    { 
        //        spawnArea[r, c] = 0;
        //    }
        //}
    }
}
