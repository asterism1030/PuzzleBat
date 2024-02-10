using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPool : BaseObjectPool
{
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
                Debug.LogError(typeof(BlockPool) + " 의 프리팹이 올바르지 않습니다.");
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
        foreach (Block block in blocks)
        {
            block.RemoveAnimEffect();
            yield return new WaitForSeconds(0.3f);

            Pool.Release(block.gameObject);
        }

        // TODO) 분리 예정


        yield break;
    }
    #endregion
}
