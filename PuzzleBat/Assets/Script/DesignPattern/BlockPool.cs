using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPool : BaseObjectPool
{
    private bool isReleasing = true;

    // getter setter
    public bool IsReleasing { get { return isReleasing; } }

    // Action / Func
    public Action<bool, List<int>> BlockPoolDelegate;

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
        BlockPoolDelegate += block.Drop;

        return block;
    }

    public void Release(List<Block> blocks)
    {
        isReleasing = true;
        StartCoroutine(RemoveBlock(blocks));
    }

    public void Clear()
    {
        Pool.Clear();
    }

    #endregion

    #region Etc Function
    // TODO) �����丵
    private IEnumerator RemoveBlock(List<Block> blocks)
    {
        List<int> emptyCol = new List<int>();

        foreach (Block block in blocks)
        {
            block.RemoveAnimEffect();
            emptyCol.Add(block.Col);

            yield return new WaitForSeconds(0.3f);

            block.Init();
            Pool.Release(block.gameObject);
        }

        isReleasing = false;

        BlockPoolDelegate(true, emptyCol);


    }
    #endregion
}
