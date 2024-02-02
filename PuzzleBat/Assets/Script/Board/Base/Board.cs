using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Board : MonoBehaviour
{
    // cell
    [SerializeField]
    private Cell cell;

    // block
    [SerializeField]
    private BaseObjectPool blockPool;

    // ��Ÿ ����
    private int totalCellCnt = 0;

    #region MonoBehaviour
    public void Start()
    {
        Fill();
    }
    #endregion

    #region EtcFunc
    public virtual void Init()
    {
        totalCellCnt = cell.Count();
        blockPool.Pool.Clear();
    }

    public virtual void Fill()
    {
        int rowCnt = cell.MaxRowCnt();
        int colCnt = cell.MaxColCnt();

        for(int i = 0; i < rowCnt; i++)
        {
            for(int j = 0; j < colCnt; j++)
            {
                GameObject rc = cell.GetRCCell(i, j);

                if(rc == null)
                {
                    continue;
                }

                GameObject block = blockPool.Pool.Get();

                block.transform.SetParent(rc.transform);
                block.transform.position = rc.transform.position;
            }
        }
    }

    public virtual void Refill()
    {

    }

    public virtual void Match()
    {

    }

    public virtual void Clear()
    {

    }

    public virtual void Shuffle()
    {
        
    }

    // block ���� �̵��� cell ��ġ �˷���
    public virtual GameObject GetCell(int row, int col)
    {
        // TODO

        return null;
    }
    #endregion
}
