using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayUIBtnEvent : ButtonEvent
{
    [SerializeField]
    private PlayUI playUI;

    [SerializeField]
    private GameObject pauseBtnCollider;

    // TODO) ∏Æ∆—≈‰∏µ

    public void OnPauseBtnClicked()
    {
        base.OnClicked();

        BoardManager.Instance.AnyPopupOpen = true;
        playUI.ActivePausePopup(true);
        pauseBtnCollider.SetActive(false);
    }

    public void OnPausePopupQuitBtnClicked()
    {
        base.OnClicked();

        InputManager.Instance.Exit();
    }

    public void OnPausePopupCloseBtnClicked()
    {
        base.OnClicked();

        BoardManager.Instance.AnyPopupOpen = false;
        playUI.ActivePausePopup(false);
        pauseBtnCollider.SetActive(true);
    }

    public void OnPausePopupBgmTgBtnClicked()
    {
        //base.OnClicked();

    }

    public void OnPausePopupSoundTgBtnClicked()
    {
        //base.OnClicked();

    }
}
