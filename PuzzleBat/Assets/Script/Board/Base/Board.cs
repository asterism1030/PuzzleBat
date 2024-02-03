using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private List<Block> selectedBlocks = new List<Block>(2);

    #region MonoBehaviour
    public void Start()
    {
        Fill();
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);

            if (hit.collider == null)
            {
                return;
            }

            Block block = hit.transform.GetComponent<Block>();

            if (block.IsSelected == true)
            {
                block.ToggleBlockSelect();
                selectedBlocks.Remove(block);

                return;
            }

            selectedBlocks.Add(block);
            block.ToggleBlockSelect();

            if(selectedBlocks.Count == 2)
            {
                // TODO) Swap 조건 (같은 Row 이거나 Col)

                Swap(selectedBlocks.First<Block>(), selectedBlocks.Last<Block>());
                // TODO) Match


                /*
                foreach (Block sb in selectedBlocks)
                {
                    sb.ToggleBlockSelect();
                }

                selectedBlocks.Clear();
                */
            }

            
        }
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

        for (int i = 0; i < rowCnt; i++)
        {
            for (int j = 0; j < colCnt; j++)
            {
                GameObject rc = cell.GetRCCell(i, j);

                if (rc == null)
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
        //// First
        // 상

        // 하

        // 좌

        // 우

        //// Last
        // 상

        // 하

        // 좌

        // 우
    }

    public virtual void Clear()
    {

    }

    public virtual void Shuffle()
    {

    }

    public virtual void Swap(Block block1, Block block2)
    {
        Transform block1Transform = block1.transform;
        Transform block1Parent = block1.transform.parent;

        block1.transform.SetParent(block2.transform.parent);
        block2.transform.SetParent(block1Parent);

        block1.transform.DOMove(block2.transform.position, 0.5f);
        block2.transform.DOMove(block1Transform.position, 0.5f);
    }
    #endregion
}
