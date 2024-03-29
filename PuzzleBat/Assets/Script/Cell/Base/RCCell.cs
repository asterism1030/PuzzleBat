using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RCCell : MonoBehaviour
{
    private int row = -1;
    private int col = -1;

    private bool isRefillPoint = false;

    // getter setter
    public int Row { get { return row; } set { row = value; } }
    public int Col { get { return col; } set { col = value; } }
    public bool IsRefillPoint { get { return isRefillPoint; } set { isRefillPoint = value; } }

    // Action / Func
    public Action<RCCell> RCCellDelegate;

    public RCCell GetUpCell()
    {
        return transform.parent.GetComponent<Cell>().GetRCCell(row - 1, col);
    }

    public RCCell GetDownCell()
    {
        return transform.parent.GetComponent<Cell>().GetRCCell(row + 1, col);
    }

    public RCCell GetLeftCell()
    {
        return transform.parent.GetComponent<Cell>().GetRCCell(row, col - 1);
    }

    public RCCell GetRightCell()
    {
        return transform.parent.GetComponent<Cell>().GetRCCell(row, col + 1);
    }

    public Block GetBlock()
    {
        int childCnt = transform.childCount;

        return (childCnt == 0) ? null : transform.GetChild(0).GetComponent<Block>();
    }

    public bool IsFilled()
    {
        int childCnt = transform.childCount;

        return (childCnt == 0) ? false : true;
    }

    public void RequestRefill()
    {
        if(isRefillPoint == true && IsFilled() == false)
        {
            RCCellDelegate(this);
        }
    }
}
