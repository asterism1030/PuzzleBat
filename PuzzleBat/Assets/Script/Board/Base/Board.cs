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

    // 기타 변수
    private int totalCellCnt = 0;

    public virtual void Init()
    {
        totalCellCnt = cell.Count();
        blockPool.Pool.Clear();
    }

    // TODO 지울 예정
    public void Start()
    {
        Fill();
    }

    public virtual void Fill()
    {
        // TODO 나머지 작성, 테스트 코드
        GameObject rc11 = cell.GetRCCell(1, 1);
        GameObject block = blockPool.Pool.Get();
        
        block.transform.SetParent(rc11.transform);
        // ??? 어째서 포지션 변경이 안되징..? 와이??
        // TODO 디버깅..
        block.transform.position = Vector3.zero;
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
