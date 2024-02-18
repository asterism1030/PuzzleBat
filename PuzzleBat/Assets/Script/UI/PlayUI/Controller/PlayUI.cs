using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayUI : MonoBehaviour
{
    [SerializeField]
    private GameObject completePopup;

    [SerializeField]
    private GameObject pausePopup;

    public void ActivePausePopup(bool isActive)
    {
        pausePopup.SetActive(isActive);
    }
}
