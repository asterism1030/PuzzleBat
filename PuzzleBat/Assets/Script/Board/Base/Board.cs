using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEngine;
using UnityEngine.Pool;

public class Board : MonoBehaviour
{
    // cell
    [SerializeField]
    private Cell cell;

    // block
    [SerializeField]
    private BlockPool blockPool;

    // Action / Func
    public Action<bool> BlockDelegate;

    // ��Ÿ ����
    private int totalCellCnt = 0;
    private List<Block> selectedBlocks = new List<Block>(2);

    #region MonoBehaviour
    public void Start()
    {
        Init();
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

            // TODO) Select ���� �Լ� �и�
            Select(block);
            
        }
    }
    #endregion

    #region EtcFunc
    public virtual void Init()
    {
        totalCellCnt = cell.Count();
        blockPool.Clear();

        int rowCnt = cell.MaxRowCnt();
        int colCnt = cell.MaxColCnt();

        for (int i = 0; i < rowCnt; i++)
        {
            for (int j = 0; j < colCnt; j++)
            {
                RCCell rc = cell.GetRCCell(i, j);

                if (rc == null)
                {
                    continue;
                }

                Block block = blockPool.Get();

                block.Put(rc);
            }
        }
    }


    public virtual void Refill()
    {
        // TODO) ���̻��� Refill �� �ʿ� ���� ��� Block �鿡�� �˸�
        
    }

    public virtual void Select(Block block)
    {
        if (block.IsSelected == true || selectedBlocks.Contains(block))
        {
            block.ToggleBlockSelect();
            selectedBlocks.Remove(block);

            return;
        }

        selectedBlocks.Add(block);
        block.ToggleBlockSelect();

        // TODO) test
        //Debug.Log(block.GetRCCell().GetDownCell().Row + ", " + block.GetRCCell().GetDownCell().Col);

        if (selectedBlocks.Count == 2)
        {
            // Swap (���� Row �̰ų� Col)
            List<int> block1RC = new List<int>{ selectedBlocks[0].Row, selectedBlocks[0].Col };
            List<int> block2RC = new List<int> { selectedBlocks[1].Row, selectedBlocks[1].Col };

            if (Mathf.Abs(block1RC[0] - block2RC[0]) + Mathf.Abs(block1RC[1] - block2RC[1]) == 1)
            {
                Swap(selectedBlocks[0], selectedBlocks[1]);

                List<Block> matched = Match(block1RC[0], block1RC[1]);
                matched.AddRange(Match(block2RC[0], block2RC[1]));

                // TODO) matched sorting (Ŭ���� ��� �߽�����)
                if(matched.Count != 0)
                {
                    Clear(matched);
                }
                else
                {
                    // TODO) 0.3f ������ �� ����
                    Swap(selectedBlocks[0], selectedBlocks[1]);
                }
            }

            foreach (Block sb in selectedBlocks)
            {
                sb.ToggleBlockSelect();
            }

            selectedBlocks.Clear();
        }
    }

    public virtual List<Block> Match(int row, int col)
    {
        List<Block> result = new List<Block>();

        List<Block> matched = new List<Block>();

        // �� -> ��
        int upRange = (row - 2) < 0 ? 0 : row - 2;
        int downRange = (row + 3) > cell.MaxRowCnt() ? cell.MaxRowCnt() : row + 3;

        for(int i = upRange; i < downRange; i++)
        {
            if (cell.GetRCCell(i, col) == null || cell.GetRCCell(i, col).GetBlock() == null)
            {
                break;
            }

            Block rcB = cell.GetRCCell(i, col).GetBlock();

            if (matched.Count() != 0 && matched[0].GetBlockType() != rcB.GetBlockType())
            {
                if (matched.Count() < 3)
                {
                    matched.Clear();
                }
                else
                {
                    break;
                }
            }

            matched.Add(rcB);
        }

        if(matched.Count >= 3)
        {
            result.AddRange(matched);
        }

        // �� -> ��
        matched.Clear();

        upRange = (col - 2) < 0 ? 0 : col - 2;
        downRange = (col + 3) > cell.MaxColCnt() ? cell.MaxColCnt() : col + 3;

        for (int i = upRange; i < downRange; i++)
        {
            if(cell.GetRCCell(row, i) == null || cell.GetRCCell(row, i).GetBlock() == null)
            {
                break;
            }

            Block rcB = cell.GetRCCell(row, i).GetBlock();

            if (matched.Count() != 0 && matched[0].GetBlockType() != rcB.GetBlockType())
            {
                if(matched.Count() < 3)
                {
                    matched.Clear();
                }
                else
                {
                    break;
                }
            }

            matched.Add(rcB);

        }

        if (matched.Count >= 3)
        {
            result.AddRange(matched);
        }

        return result;
    }

    public virtual void Clear(List<Block> blocks)
    {
        blockPool.Release(blocks);
    }

    public virtual void Clear()
    {

    }

    public virtual void Shuffle()
    {

    }

    public virtual void Swap(Block block1, Block block2)
    {
        RCCell block1RCCell = block1.GetRCCell();

        block1.Move(block2.GetRCCell());
        block2.Move(block1RCCell);
    }

    #endregion
}
