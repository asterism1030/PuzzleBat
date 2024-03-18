using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class CompletePopup : MonoBehaviour
{
    // TODO) 쓰리 스타 적용, 리플레이 넥스트 버튼 이벤트 처리

    [SerializeField]
    private TextMeshProUGUI score;

    private void Start()
    {
        //BoardManager.Instance.EventHeaderInfoChanged += UpdateView;
    }

    public void UpdateView(PlayUIHeaderModel playUIHeaderModel)
    {
        score.text = playUIHeaderModel.Score.ToString();
    }

    public void UpdateView()
    {
        score.text = BoardManager.Instance.CurMapScore.ToString();
    }
}
