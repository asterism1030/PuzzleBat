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


    public void ToggleBlockSelect()
    {
        isSelected = !isSelected;

        visual.color = isSelected ? Color.gray : Color.white;
    }

    // TODO) 수정 예정
    public void Release(BaseObjectPool blockPool)
    {
        visual.color = Color.red;

        // TODO) 애니메이션 효과, 0.3f 정도 딜레이 후 Release
        blockPool.Pool.Release(this.gameObject);
    }

    public void Move(GameObject targetCellObj)
    {
        this.gameObject.transform.SetParent(targetCellObj.transform);
        this.gameObject.transform.DOMove(targetCellObj.transform.position, 0.5f);
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
}
