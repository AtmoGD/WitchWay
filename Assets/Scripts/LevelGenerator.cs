using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class BlockData
{
    public GameObject prefab;
    public float spawnChance;
}

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private List<BlockData> blocks = new List<BlockData>();
    [SerializeField] private int width = 10;
    [SerializeField] private int height = 10;
    [SerializeField] private float hexagonSize = 1f;
    [SerializeField] private float hexagonSpacing = 0.1f;

    private void Start()
    {
        GenerateLevel();
    }

    public void GenerateLevel()
    {
        // Clear the level
        ClearLevel();

        // Generate a grid of hexagon blocks
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // Get a random block
                BlockData block = GetRandomBlock();

                // Calculate the position of the block
                Vector3 position = new Vector3(
                    x * (hexagonSize + hexagonSpacing) + (y % 2 == 0 ? 0 : hexagonSize / 2f),
                    0,
                    y * (hexagonSize + hexagonSpacing) * Mathf.Sqrt(3) / 2f
                );

                // Instantiate the block
                GameObject blockObject = Instantiate(block.prefab, position, block.prefab.transform.rotation, transform);
            }
        }
    }

    public void ClearLevel()
    {
        // Destroy all blocks
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
#if UNITY_EDITOR
            DestroyImmediate(transform.GetChild(i).gameObject);
#else
            Destroy(transform.GetChild(i).gameObject);
#endif
        }
    }

    private BlockData GetRandomBlock()
    {
        // Get a random block
        float random = UnityEngine.Random.Range(0f, 1f);
        float total = 0f;
        foreach (BlockData block in blocks)
        {
            total += block.spawnChance;
            if (random <= total)
                return block;
        }

        return null;
    }
}
