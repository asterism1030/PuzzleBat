using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayUI : MonoBehaviour
{
    [SerializeField]
    private GameObject completePopup;

    [SerializeField]
    private GameObject pausePopup;

    private void Start()
    {
        BoardManager.Instance.EventHeaderInfoChanged += PopupView;
    }

    public void PopupView(PlayUIHeaderModel playUIHeaderModel)
    {
        if(playUIHeaderModel.Move == 0)
        {
            StartCoroutine(ActiveCompletePopup(true));
        }
    }

    public void ActivePausePopup(bool isActive)
    {
        pausePopup.SetActive(isActive);
        BoardManager.Instance.AnyPopupOpen = isActive;
    }

    public IEnumerator ActiveCompletePopup(bool isActive)
    {
        yield return new WaitForSeconds(1.0f);

        completePopup.SetActive(isActive);
        BoardManager.Instance.AnyPopupOpen = isActive;

        if(isActive == true)
        {
            completePopup.GetComponent<CompletePopup>().UpdateView();
        }
    }
}
