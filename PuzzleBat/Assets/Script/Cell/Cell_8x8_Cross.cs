using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell_8x8_Cross : Cell
{
    public override RCCell GetRCCell(int row, int col)
    {
        if((row == 0 && col == 0) || (row == 0 && col == 7)
            || (row == 7 && col == 0) || (row == 7 && col == 7))
        {
            return null;
        }

        int correctinR = row;
        int correctinC = col;

        if(row == 0 || row == 7)
        {
            correctinC -= 1;
        }

        return base.GetRCCell(correctinR, correctinC);
    }

    public override int MaxRowCnt()
    {
        return 8;
    }

    public override int MaxColCnt()
    {
        return 8;
    }

}
