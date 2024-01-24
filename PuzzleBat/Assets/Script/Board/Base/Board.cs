using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Board : MonoBehaviour
{
    // cell         ��ġ r->c ��
    [SerializeField]
    private List<GameObject> cells;

    // block        ������Ʈ
    //protected IObjectPool<Block> blockPool;

    // ��Ÿ ����
    private int totalCellCnt = 0;

    public virtual void Init(int cellCnt)
    {
        this.totalCellCnt = cellCnt;

        for(int i = 0; i < cellCnt; i++)
        {
            //blockPool.Get();
        }
    }

    public virtual void Fill()
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
}
