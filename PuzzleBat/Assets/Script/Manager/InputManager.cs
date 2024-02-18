using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputManager : Singleton<InputManager>
{
    public Action<RaycastHit2D> MouseBtnUp;

    void Update()
    {
        Key();
    }

    private void Key()
    {
        // 마우스 버튼 업
        if (Input.GetMouseButtonUp(0))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);

            if (hit.collider != null)
            {
                MouseBtnUp?.Invoke(hit);
            }
        }
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else

        Application.Quit();
#endif
    }
}
