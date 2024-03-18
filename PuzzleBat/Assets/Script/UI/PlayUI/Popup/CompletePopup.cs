using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class CompletePopup : MonoBehaviour
{
    // TODO) ���� ��Ÿ ����, ���÷��� �ؽ�Ʈ ��ư �̺�Ʈ ó��

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
