using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEvent : MonoBehaviour
{
    public void OnClicked()
    {
        SoundManager.Instance.Play(ESoundType.BtnClicked);
    }
}
