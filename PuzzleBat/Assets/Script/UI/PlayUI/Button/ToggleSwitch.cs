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
        onBtn.SetActive(false);
        offBtn.SetActive(true);
    }

    public void onBtnClicked()
    {
        onBtn.SetActive(false);
        offBtn.SetActive(true);
    }

    public void offBtnClicked()
    {
        onBtn.SetActive(true);
        offBtn.SetActive(false);
    }
}
