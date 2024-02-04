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

            if (block.IsSelected == true || selectedBlocks.Contains(block))
            {
                block.ToggleBlockSelect();
                selectedBlocks.Remove(block);

                return;
            }

            selectedBlocks.Add(block);
            block.ToggleBlockSelect();

            if (selectedBlocks.Count == 2)
            {
                // Swap (같은 Row 이거나 Col)
                List<int> block1RC = selectedBlocks[0].GetRowCol();
                List<int> block2RC = selectedBlocks[1].GetRowCol();

                if (block1RC[0] == block2RC[0] || block1RC[1] == block2RC[1])
                {
                    Swap(selectedBlocks[0], selectedBlocks[1]);

                    // TODO) Match
                }

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

                Block block = blockPool.Pool.Get().GetComponent<Block>();

                block.Put(rc);
            }
        }
    }

    public virtual void Refill()
    {

    }

    public virtual void Match(int row, int col)
    {
        List<Block> matched = new List<Block>();

        // 상 -> 하
        int upRange = (row - 2) < 0 ? 0 : row - 2;
        int downRange = (row + 2) >= cell.MaxRowCnt() ? cell.MaxRowCnt() - 1 : row + 2;

        for(int i = upRange; i <= downRange; i++)
        {
            // TODO) 아래 코드 캡슐화 하면 깔끔할 것 같다
            Block rcB = cell.GetRCCell(i, col).transform.GetChild(0).GetComponent<Block>();

            // TODO) 한 줄에서 가장 긴 연속되는 같은 블록
        }


        // 좌 -> 우



    }

    public virtual void Clear()
    {

    }

    public virtual void Shuffle()
    {

    }

    public virtual void Swap(Block block1, Block block2)
    {
        GameObject block1Parent = block1.transform.parent.gameObject;

        block1.Move(block2.transform.parent.gameObject);
        block2.Move(block1Parent);
    }
    #endregion
}
