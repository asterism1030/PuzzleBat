using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
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

    // sound
    // TODO) 분리
    public Sound sound;

    // Action / Func
    public Action<bool, List<int>> EventBlockDrop;

    // 기타 변수
    private int totalCellCnt = 0;
    private int totalBlockCnt = 0;

    private List<Block> selectedBlocks = new List<Block>(2);

    #region MonoBehaviour
    public void Start()
    {
        Init();
    }

    void Update()
    {
        // TODO) 리팩토링 예정 목표) Update 제거
        if(totalCellCnt == totalBlockCnt)
        {
            Drop(null);
            return;
        }

        List<RCCell> rcCells = cell.GetEmptyRefillPointRCCell();

        foreach (RCCell rc in rcCells)
        {
            Refill(rc).Drop();
        }
    }
    #endregion

    #region EtcFunc
    public virtual void Init()
    {
        //// Subscribe
        // Input Event
        InputManager.Instance.EventMouseBtnUp += OnMouseBtnUP;
        // BlockPool Event
        blockPool.EventReleaseEnd += Drop;

        // Board set
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

                Refill(rc);
            }
        }
    }

    public virtual void OnMouseBtnUP(RaycastHit2D hit)
    {
        // TODO) 리팩토링 예정
        Block block = hit.transform.GetComponent<Block>();

        if(block == null)
        {
            return;
        }

        Select(block);
        sound.ButtonClicked();

        if(selectedBlocks.Count != 2)
        {
            return;
        }

        PlayUIHeaderModel playUIHeaderModel = BoardManager.Instance.GetUIInfo();
        playUIHeaderModel.Move -= 1;

        Swap(selectedBlocks[0], selectedBlocks[1]);

        if (CanSwap(selectedBlocks[0], selectedBlocks[1]) == false)
        {
            Swap(selectedBlocks[0], selectedBlocks[1]);
            foreach (Block sb in selectedBlocks)
            {
                sb.ToggleBlockSelect();
            }
            selectedBlocks.Clear();
            return;
        }

        List<Block> matched = Match(selectedBlocks[0].Row, selectedBlocks[0].Col);
        matched.AddRange(Match(selectedBlocks[1].Row, selectedBlocks[1].Col));

        if (matched.Count != 0)
        {
            Release(matched);

            // TODO) 변경
            playUIHeaderModel.Score += 3;
        }
        else
        {
            Swap(selectedBlocks[0], selectedBlocks[1]);
        }

        foreach (Block sb in selectedBlocks)
        {
            sb.ToggleBlockSelect();
        }
        selectedBlocks.Clear();

        BoardManager.Instance.UpdateUIInfo(playUIHeaderModel);
    }

    public Block Refill(RCCell rcCell)
    {
        Block block = blockPool.Get();
        block.Put(rcCell);
        EventBlockDrop += block.Drop;

        totalBlockCnt++;

        return block;
    }

    public void Select(Block block)
    {
        if (block.IsSelected == true || selectedBlocks.Contains(block))
        {
            block.ToggleBlockSelect();
            selectedBlocks.Remove(block);
        }

        selectedBlocks.Add(block);
        block.ToggleBlockSelect();
    }

    public List<Block> Match(int row, int col)
    {
        List<Block> result = new List<Block>();
        List<Block> matched = new List<Block>();

        // 상 -> 하
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

        // 좌 -> 우
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

    public virtual void Release(List<Block> blocks)
    {
        blockPool.Release(blocks);
        totalBlockCnt -= blocks.Count;
    }

    public void Shuffle()
    {

    }

    public bool CanSwap(Block block1, Block block2)
    {
        bool result = false;

        // Swap (인접한 두 블럭인지, 같은 Row 이거나 Col)
        List<int> block1RC = new List<int> { block1.Row, block1.Col };
        List<int> block2RC = new List<int> { block2.Row, block2.Col };

        if (Mathf.Abs(block1RC[0] - block2RC[0]) + Mathf.Abs(block1RC[1] - block2RC[1]) == 1)
        {
            result = true;
        }

        return result;
    }

    public void Swap(Block block1, Block block2)
    {
        RCCell block1RCCell = block1.GetRCCell();

        block1.Move(block2.GetRCCell());
        block2.Move(block1RCCell);
    }

    public void Drop(List<int> emptyCol)
    {
        bool isDrop = (totalCellCnt == totalBlockCnt) ? false : true;
        EventBlockDrop?.Invoke(isDrop, emptyCol);
    }

    #endregion
}
