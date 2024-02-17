using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayUIBtnEvent : MonoBehaviour
{
    [SerializeField]
    private PlayUI playUI;

    [SerializeField]
    private GameObject pauseBtnCollider;


    public void OnPauseBtnClicked()
    {
        playUI.ActivePausePopup(true);
        pauseBtnCollider.SetActive(false);
    }

    public void OnPausePopupQuitBtnClicked()
    {
        InputManager.Instance.Exit();
    }
}
