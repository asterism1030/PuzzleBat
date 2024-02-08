using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO) 팩토리 등으로 스크립트를 통해 자동으로 GO 만들고 코드 붙이기
// 이벤트 핸들러 작성
public class InputManager : MonoBehaviour
{
    public Action KeyAction = null;

    void Update()
    {
        Key();
    }

    private void Key()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);

            if (hit.collider == null)
            {
                return;
            }

            Block block = hit.transform.GetComponent<Block>();
            if (block != null)
            {
                //KeyAction?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
