using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Board : MonoBehaviour
{
    // cell         위치 r->c 순
    [SerializeField]
    private List<GameObject> cells;

    // block        오브젝트
    //protected IObjectPool<Block> blockPool;

    // 기타 변수
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

    // block 에게 이동할 cell 위치 알려줌
    public virtual GameObject GetCell(int row, int col)
    {
        // TODO

        return null;
    }
}
