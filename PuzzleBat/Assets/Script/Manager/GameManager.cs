using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region MonoBehaviour
    void Start()
    {
        
    }

    void Update()
    {
        // TODO) 테스트 코드, 수정 예정
        if(Input.GetMouseButton(0))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);
            if(hit.collider != null)
            {
                Debug.Log(hit.transform.gameObject.name);
            }
        }
    }
    #endregion
}
