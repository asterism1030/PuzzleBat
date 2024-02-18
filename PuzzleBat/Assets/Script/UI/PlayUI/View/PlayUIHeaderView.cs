using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayUIHeaderView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI move;

    [SerializeField]
    private TextMeshProUGUI score;

    [SerializeField]
    private GameObject pauseBtn;

    private void Start()
    {
        BoardManager.Instance.EventHeaderInfoChanged += UpdateView;
    }

    public void UpdateView(PlayUIHeaderModel playUIHeaderModel)
    {
        move.text = playUIHeaderModel.Move.ToString();
        score.text = playUIHeaderModel.Score.ToString();
    }
}
