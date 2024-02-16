using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> refillPointCell;

    [SerializeField]
    private N_Array[] rcCells;

    void Start()
    {
        Init();
    }

    public void Init()
    {
        int rowCnt = MaxRowCnt();
        int colCnt = MaxColCnt();

        for (int i = 0; i < rowCnt; i++)
        {
            for (int j = 0; j < colCnt; j++)
            {
                if (GetRCCell(i, j) == null)
                {
                    continue;
                }

                RCCell rc = GetRCCell(i, j).GetComponent<RCCell>();

                rc.Row = i;
                rc.Col = j;
            }
        }

        foreach(GameObject go in refillPointCell)
        {
            RCCell rc = go.GetComponent<RCCell>();

            if(rc == null)
            {
                continue;
            }

            rc.IsRefillPoint = true;
        }
    }

    public virtual RCCell GetRCCell(int row, int col)
    {
        if(row < 0 || row >= rcCells.Length)
        {
            return null;
        }

        if(col < 0 || col >= rcCells[row].arry.Length)
        {
            return null;
        }

        return rcCells[row].arry[col].GetComponent<RCCell>();
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

}
