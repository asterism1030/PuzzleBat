using DG.Tweening;
using JetBrains.Annotations;
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

    public void Init()
    {
        visual.color = Color.white;
        IsSelected = false;
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

    public IEnumerator Fall()
    {
        // 최하, 최좌하, 최우하
        RCCell downCell = GetRCCell().GetDownCell();
        RCCell leftCell = (GetRCCell().GetDownCell() == null) ? null : GetRCCell().GetDownCell().GetLeftCell();
        RCCell rightCell = (GetRCCell().GetDownCell() == null) ? null : GetRCCell().GetDownCell().GetRightCell();

        while (downCell != null)
        {
            if ((downCell.GetDownCell() == null) || (downCell.GetDownCell().IsFilled() == true))
            {
                break;
            }
            downCell = downCell.GetDownCell();
        }

        while (leftCell != null)
        {
            if ((leftCell.GetDownCell() == null) || (leftCell.GetDownCell().IsFilled() == true))
            {
                break;
            }
            leftCell = leftCell.GetDownCell();
        }

        while (rightCell != null)
        {
            if ((rightCell.GetDownCell() == null) || (rightCell.GetDownCell().IsFilled() == true))
            {
                break;
            }
            rightCell = rightCell.GetDownCell();
        }


        if (downCell == null)
        {
            yield break;
        }

        bool isFilled = downCell.IsFilled();

        if (isFilled == false)
        {
            Move(downCell);
        }

        // TODO) 흘러내리기

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
