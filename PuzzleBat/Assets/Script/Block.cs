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

    // TODO) �ִ����� ȿ�� �߰� ����
    public void RemoveAnimEffect()
    {
        visual.color = Color.red;
    }

    public void Move(GameObject rcCell)
    {
        this.gameObject.transform.SetParent(rcCell.gameObject.transform);
        this.gameObject.transform.DOMove(rcCell.gameObject.transform.position, 0.5f);
    }

    public void Put(GameObject targetCellObj)
    {
        this.transform.SetParent(targetCellObj.transform);
        this.transform.position = targetCellObj.transform.position;
    }

    public List<int> GetRowCol()
    {
        List<int> rc = new List<int> { -1, -1 };

        if (this.transform.parent == null || 
            this.transform.parent.parent == null ||
            this.transform.parent.parent.GetComponent<Cell>() == null)
        {
            return rc;
        }

        string cellName = this.transform.parent.name;
        string[] rcInfo = cellName.Split('_')[1].Split(',');

        rc[0] = int.Parse(rcInfo[0]);
        rc[1] = int.Parse(rcInfo[1]);

        return rc;
    }

    public string GetBlockType()
    {
        return this.transform.name;
    }

    public GameObject GetRCCell()
    {
        if(transform.parent == null || transform.parent.parent.GetComponent<Cell>() == null)
        {
            return null;
        }

        return transform.parent.gameObject;
    }
}
