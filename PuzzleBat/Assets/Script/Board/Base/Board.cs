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

    public virtual void Init()
    {
        totalCellCnt = cell.Count();
        blockPool.Pool.Clear();
    }

    // TODO ���� ����
    public void Start()
    {
        Fill();
    }

    public virtual void Fill()
    {
        // TODO ������ �ۼ�, �׽�Ʈ �ڵ�
        GameObject rc11 = cell.GetRCCell(1, 1);
        GameObject block = blockPool.Pool.Get();
        
        block.transform.SetParent(rc11.transform);
        // ??? ��°�� ������ ������ �ȵ�¡..? ����??
        // TODO �����..
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

    // block ���� �̵��� cell ��ġ �˷���
    public virtual GameObject GetCell(int row, int col)
    {
        // TODO

        return null;
    }
}
