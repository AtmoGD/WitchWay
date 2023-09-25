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
    public static LevelGenerator Instance { get; private set; } = null;

    [Header("Level Generation")]
    [SerializeField] private List<BlockData> blocks = new List<BlockData>();
    [field: SerializeField] public int Width { get; private set; } = 10;
    [field: SerializeField] public int Height { get; private set; } = 10;
    [field: SerializeField] public float HexagonSize { get; private set; } = 1.0f;
    [field: SerializeField] public float HexagonSpacing { get; private set; } = 0.1f;

    public GameObject StartBlock { get; private set; } = null;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        GenerateLevel();
    }

    public void GenerateLevel()
    {
        ClearLevel();

        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                BlockData block = GetRandomBlock();

                Vector3 position = new Vector3(
                    x * (HexagonSize + HexagonSpacing) + (y % 2 == 0 ? 0 : HexagonSize / 2f),
                    0,
                    y * (HexagonSize + HexagonSpacing) * Mathf.Sqrt(3) / 2f
                );

                GameObject blockObject = Instantiate(block.prefab, position, block.prefab.transform.rotation, transform);

                if (StartBlock == null && UnityEngine.Random.Range(0f, 1f) <= 0.1f)
                    StartBlock = blockObject;
            }
        }
    }

    public void ClearLevel()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
#if UNITY_EDITOR
            DestroyImmediate(transform.GetChild(i).gameObject);
#else
            Destroy(transform.GetChild(i).gameObject);
#endif
        }

        StartBlock = null;
    }

    private BlockData GetRandomBlock()
    {
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

    private void OnDrawGizmos()
    {
        if (StartBlock != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(StartBlock.transform.position, HexagonSize / 2f);
        }
    }
}
