using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class TopperData
{
    public GameObject prefab;
    public float spawnChance;
}

public class BlockTopper : MonoBehaviour
{
    [SerializeField] private Block block = null;
    [SerializeField] private List<TopperData> toppers = new List<TopperData>();

    private GameObject topperObject = null;

    private void Start()
    {
        if (block.BlockType != BlockType.Obstacle)
            SpawnTopper();
    }

    private void SpawnTopper()
    {
        TopperData topperData = GetRandomTopper(toppers);

        if (topperData == null) return;

        topperObject = Instantiate(topperData.prefab, transform.position, topperData.prefab.transform.rotation, block.ObjectParent);
    }

    private TopperData GetRandomTopper(List<TopperData> toppers)
    {
        TopperData topperData = null;

        float random = UnityEngine.Random.Range(0f, 1f);

        foreach (TopperData topper in toppers)
        {
            if (random <= topper.spawnChance)
            {
                topperData = topper;
                break;
            }
            else
                random -= topper.spawnChance;
        }

        return topperData;
    }

    public void UpdateTopper(BlockType _blockType)
    {
        if (_blockType == BlockType.Obstacle)
        {
            if (topperObject != null)
                Destroy(topperObject);
        }
    }
}
