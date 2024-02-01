using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField]
    private N_Array[] rcCells;

    public GameObject GetRCCell(int row, int col)
    {
        if(row < 0 || row > rcCells.Length)
        {
            return null;
        }

        if(col < 0 || col > rcCells[row].arry.Length)
        {
            return null;
        }

        return rcCells[row].arry[col];
    }

    public int Count()
    {
        int cnt = 0;

        foreach(N_Array cell in rcCells)
        {
            cnt += cell.arry.Length;
        }

        return cnt;
    }
}
