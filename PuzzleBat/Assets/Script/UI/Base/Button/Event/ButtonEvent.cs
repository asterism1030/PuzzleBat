using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ButtonEvent : MonoBehaviour
{
    [SerializeField]
    private ButtonEvent btnCollider;

    public abstract void OnClick();
}
