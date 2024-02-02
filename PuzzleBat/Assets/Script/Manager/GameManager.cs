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
        // TODO) �׽�Ʈ �ڵ�, ���� ����
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
