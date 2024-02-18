using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class BoardManager : Singleton<BoardManager>
{
    [SerializeField]
    private GameObject curMap_Score;

    [SerializeField]
    private GameObject curMap_MoveCnt;

    // 현재 맵의 정보
    private int curMap_moveCnt = 10;
    private int curMap_score = 0;

    // getter setter
    public int CurMapMoveCnt { get { return curMap_moveCnt; } set { curMap_moveCnt = value; } }
    public int CurMapScore { get { return curMap_score; } set { curMap_score = value; } }

    // Action / Func
    public Action<PlayUIHeaderModel> EventHeaderInfoChanged;

    #region Monobehavior
    void Awake()
    {
        Init();
    }

    void Start()
    {
        UpdateUIInfo(new PlayUIHeaderModel(curMap_moveCnt, curMap_score));
    }

    public void Init()
    {
        curMap_score = 0;
        curMap_moveCnt = 15;
    }

    public void UpdateUIInfo(PlayUIHeaderModel playUIHeaderModel)
    {
        curMap_score = playUIHeaderModel.Score;
        curMap_moveCnt = playUIHeaderModel.Move;

        EventHeaderInfoChanged?.Invoke(playUIHeaderModel);
    }

    public PlayUIHeaderModel GetUIInfo()
    {
        return new PlayUIHeaderModel(curMap_moveCnt, curMap_score);
    }
    #endregion
}
