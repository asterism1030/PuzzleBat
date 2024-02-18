using DG.Tweening;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer visual;

    [SerializeField]
    private AudioSource blockReleased;

    private bool isSelected = false;
    private int row = -1;
    private int col = -1;

    // getter setter
    public bool IsSelected { get { return isSelected; } set { isSelected = value; } }
    public int Row { get { return row; } set {  row = value; } }
    public int Col { get { return col; } set { col = value; } }

    // Action / Func
    //public Action<bool> BlockAction;

    public void Init()
    {
        visual.color = Color.white;
        IsSelected = false;

        row = -1;
        col = -1;
    }

    public void ToggleBlockSelect()
    {
        isSelected = !isSelected;

        visual.color = isSelected ? Color.gray : Color.white;
    }

    // TODO) 애님이펙 효과 추가 예정
    public void RemoveAnimEffect()
    {
        visual.color = Color.red;
        blockReleased.Play();
    }

    public void Drop(bool isDrop, List<int> emptyCol)
    {
        if (isDrop == true && gameObject.activeSelf == true && emptyCol.Contains(col))
        {
            StartCoroutine(Fall());
        }
        else if (isDrop == false)
        {
            StopCoroutine(Fall());
        }
    }

    public void Drop()
    {
        StartCoroutine(Fall());
    }

    public IEnumerator Fall()
    {
        for(; ; )
        {
            // 최하단
            RCCell downCell = GetRCCell().GetDownCell();

            if (downCell == null)
            {
                yield break;
            }

            while (downCell != null)
            {
                if (downCell.GetDownCell() == null)
                {
                    break;
                }

                if (downCell.GetDownCell().IsFilled() == true)
                {
                    break;
                }

                downCell = downCell.GetDownCell();
            }

            //Debug.Log(GetRowCol()[0] + ", " + GetRowCol()[1] + " " + downCell.IsFilled() + ", " + downCell.gameObject.name);

            if (downCell.IsFilled() == false)
            {
                Move(downCell);
            }

            yield return new WaitForSeconds(0.1f);
        }
        

    }

    public void Move(RCCell rcCell)
    {
        transform.SetParent(rcCell.gameObject.transform);
        transform.DOMove(rcCell.gameObject.transform.position, 0.5f);
        
        row = rcCell.Row;
        col = rcCell.Col;
    }

    public void Put(RCCell rc)
    {
        this.transform.SetParent(rc.transform);
        this.transform.position = rc.transform.position;

        row = rc.Row;
        col = rc.Col;
    }

    public string GetBlockType()
    {
        return this.transform.name;
    }

    public RCCell GetRCCell()
    {
        if(transform.parent == null || transform.parent.GetComponent<RCCell>() == null)
        {
            return null;
        }

        return transform.parent.GetComponent<RCCell>();
    }
}
