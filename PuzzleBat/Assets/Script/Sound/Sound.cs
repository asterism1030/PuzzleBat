using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    [SerializeField]
    private AudioSource btnClicked;

    [SerializeField]
    private AudioSource blockReleased;

    public void ButtonClicked()
    {
        btnClicked.Play();
    }

    public void BlockReleased()
    {
        blockReleased.Play();
    }
}
