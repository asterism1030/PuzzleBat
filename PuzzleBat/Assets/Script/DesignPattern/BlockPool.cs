using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPool : BaseObjectPool
{
    //private bool isReleasing = true;

    // getter setter
    //public bool IsReleasing { get { return isReleasing; } }

    // Action / Func
    public Action<List<int>> ActionReleaseEnd; // Release �Ϸ��� ��� �� ����Ʈ

    public void Start()
    {
        CheckPref();
    }

    private void CheckPref()
    {
        foreach (GameObject go in goPref)
        {
            if (go.GetComponent<Block>() == null)
            {
                Debug.LogError(typeof(BlockPool) + " �� �������� �ùٸ��� �ʽ��ϴ�.");
            }
        }
    }

    #region Base Function
    public Block Get()
    {
        Block block = Pool.Get().GetComponent<Block>();
        block.Init();

        return block;
    }

    public void Release(List<Block> blocks)
    {
        StartCoroutine(RemoveBlock(blocks));
    }

    public void Clear()
    {
        Pool.Clear();
    }

    #endregion

    #region Etc Function
    private IEnumerator RemoveBlock(List<Block> blocks)
    {
        //isReleasing = true;
        List<int> emptyCol = new List<int>();

        foreach (Block block in blocks)
        {
            block.RemoveAnimEffect();
            emptyCol.Add(block.Col);

            yield return new WaitForSeconds(0.3f);

            block.Init();
            Pool.Release(block.gameObject);
        }

        ActionReleaseEnd?.Invoke(emptyCol);
    }
    #endregion
}
