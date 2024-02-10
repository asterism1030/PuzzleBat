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

    private bool isSelected = false;

    // getter setter
    public bool IsSelected { get { return isSelected; } set { isSelected = value; } }

    // Action / Func
    //public Action<bool> BoardDelegate;

    public void Init()
    {
        visual.color = Color.white;
        IsSelected = false;

        //BoardDelegate += Drop;
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
    }


    // TODO) 명명 변경
    public void Drop(bool isAnyCellEmpty)
    {
        if(isAnyCellEmpty == true)
        {
            StartCoroutine(Fall());
        }
        else
        {
            StopCoroutine(Fall());
        }
    }

    public IEnumerator Fall()
    {
        // 최하, 최좌하, 최우하
        RCCell downCell = GetRCCell().GetDownCell();

        if (downCell == null)
        {
            yield break;
        }

        if(downCell.IsFilled() == true)
        {
            yield return null;
        }

        while (downCell != null)
        {
            if ((downCell.GetDownCell() == null) || (downCell.GetDownCell().IsFilled() == true))
            {
                break;
            }

            downCell = downCell.GetDownCell();
        }

        Move(downCell);

        yield return null;
    }

    public void Move(RCCell rcCell)
    {
        transform.SetParent(rcCell.gameObject.transform);
        transform.DOMove(rcCell.gameObject.transform.position, 0.5f);
    }

    public void Put(RCCell rc)
    {
        this.transform.SetParent(rc.transform);
        this.transform.position = rc.transform.position;
    }

    public List<int> GetRowCol()
    {
        List<int> rc = new List<int> { -1, -1 };

        if (transform.parent == null || transform.parent.GetComponent<RCCell>() == null)
        {
            return rc;
        }

        rc[0] = transform.parent.GetComponent<RCCell>().Row;
        rc[1] = transform.parent.GetComponent<RCCell>().Col;

        return rc;
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
