using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer visual;

    private bool isSelected = false;

    public bool IsSelected { get { return isSelected; } set { isSelected = value; } }

    public void ToggleBlockSelect()
    {
        isSelected = !isSelected;

        visual.color = isSelected ? Color.gray : Color.white;
    }
}
