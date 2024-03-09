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
        base.OnClicked();

        if(SoundManager.Instance.IsPlayBGM() == true)
        {
            SoundManager.Instance.BGM_ON = false;
            SoundManager.Instance.Stop(ESoundType.BGM);
        }
        else
        {
            SoundManager.Instance.BGM_ON = true;
            SoundManager.Instance.Play(ESoundType.BGM);
        }
    }

    public void OnPausePopupSoundTgBtnClicked()
    {
        base.OnClicked();

        SoundManager.Instance.Effect_ON = !SoundManager.Instance.Effect_ON;
    }
}
