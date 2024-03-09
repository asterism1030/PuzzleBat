using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSwitch : MonoBehaviour
{
    [SerializeField]
    private GameObject onBtn;

    [SerializeField]
    private GameObject offBtn;

    //[SerializeField]
    //private bool 

    private void Start()
    {
        onBtn.SetActive(true);
        offBtn.SetActive(false);
    }

    public void onBtnClicked()
    {
        onBtn.SetActive(true);
        offBtn.SetActive(false);
    }

    public void offBtnClicked()
    {
        onBtn.SetActive(false);
        offBtn.SetActive(true);
    }
}
