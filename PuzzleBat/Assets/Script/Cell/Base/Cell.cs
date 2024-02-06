using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField]
    private N_Array[] rcCells;

    public virtual GameObject GetFirstRCCell()
    {
        return rcCells[0].arry[0];
    }

    public virtual GameObject GetRCCell(int row, int col)
    {
        if(row < 0 || row >= rcCells.Length)
        {
            return null;
        }

        if(col < 0 || col >= rcCells[row].arry.Length)
        {
            return null;
        }

        return rcCells[row].arry[col];
    }

    public virtual GameObject GetRCCellByName(string name)
    {
        GameObject findedObj = null;

        foreach(N_Array cell in rcCells)
        {
            foreach(GameObject go in cell.arry)
            {
                if(go.name.Equals(name))
                {
                    findedObj = go;
                    break;
                }
            }
        }

        return findedObj;
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

    public virtual int MaxRowCnt()
    {
        int cnt = 0;

        cnt = rcCells.Length;

        return cnt;
    }

    public virtual int MaxColCnt()
    {
        int cnt = 0;

        foreach(N_Array cell in rcCells)
        {
            cnt = (cnt < cell.arry.Length) ? cell.arry.Length : cnt;
        }

        return cnt;
    }

    public virtual Block GetBlock(int row, int col)
    {
        Transform child = GetRCCell(row, col).transform.GetChild(0);

        return (child == null) ? null : child.GetComponent<Block>();
    }
}
