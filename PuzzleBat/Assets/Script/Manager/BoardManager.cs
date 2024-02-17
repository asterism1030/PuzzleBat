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
    private int curMap_score = 0;
    private int cumrMap_moveCnt = 0;

    // getter setter
    public int CurMapScore { get { return curMap_score; } set { curMap_score = value; } }
    public int CurMapMoveCnt { get { return cumrMap_moveCnt; } set { cumrMap_moveCnt = value; } }



    #region Monobehavior
    void Awake()
    {

    }

    #endregion
}
