using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO) ���丮 ������ ��ũ��Ʈ�� ���� �ڵ����� GO ����� �ڵ� ���̱�
// �̺�Ʈ �ڵ鷯 �ۼ�
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
