using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayUIBtnEvent : ButtonEvent
{
    [SerializeField]
    private PlayUI playUI;

    [SerializeField]
    private GameObject pauseBtnCollider;

    // TODO) 리팩토링

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

    // TODO) 구현, 팝업창 띄울 시 뒤의 블록들 클릭 블락 구현
    public void OnPausePopupBgmTgBtnClicked()
    {

    }

    public void OnPausePopupSoundTgBtnClicked()
    {

    }
}
