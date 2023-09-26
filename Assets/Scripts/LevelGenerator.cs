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
    [Header("Level Generation")]
    [SerializeField] private List<BlockData> blocks = new List<BlockData>();
    [SerializeField] private List<BlockData> walls = new List<BlockData>();
    [SerializeField] private List<BlockData> obstacles = new List<BlockData>();
    [field: SerializeField] public int Width { get; private set; } = 10;
    [field: SerializeField] public int Height { get; private set; } = 10;
    [field: SerializeField] public float HexagonSize { get; private set; } = 1.0f;
    [field: SerializeField] public float HexagonSpacing { get; private set; } = 0.1f;
    [field: SerializeField] public float ObstacleSpawnChance { get; private set; } = 0.1f;

    public Block StartBlock { get; private set; } = null;

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
                Vector3 position = new Vector3(
                    x * (HexagonSize + HexagonSpacing) + (y % 2 == 0 ? 0 : HexagonSize / 2f),
                    0,
                    y * (HexagonSize + HexagonSpacing) * Mathf.Sqrt(3) / 2f
                );

                BlockData blockData = GetRandomBlock(blocks);

                Block block = Instantiate(blockData.prefab, position, blockData.prefab.transform.rotation, transform).GetComponent<Block>();

                bool isWall = x == 0 || x == Width - 1 || y == 0 || y == Height - 1;
                bool isObstacle = UnityEngine.Random.Range(0f, 1f) <= ObstacleSpawnChance;

                if (isWall)
                    block.SetBlock(BlockType.Wall, GetRandomBlock(walls).prefab);
                else if (isObstacle)
                    block.SetBlock(BlockType.Obstacle, GetRandomBlock(obstacles).prefab);
            }
        }

        SetStartBlock();
    }

    public void SetStartBlock()
    {
        List<Block> blocks = GetBaseBlocks();

        if (blocks.Count > 0)
            StartBlock = blocks[UnityEngine.Random.Range(0, blocks.Count)];
        else
            print("No base blocks found!");
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

    public List<Block> GetBaseBlocks()
    {
        List<Block> blocks = new List<Block>();
        foreach (Transform child in transform)
        {
            Block block = child.GetComponent<Block>();
            if (block != null && block.BlockType == BlockType.Base)
                blocks.Add(block);
        }
        return blocks;
    }

    private BlockData GetRandomBlock(List<BlockData> _blockToChooseFrom)
    {
        float random = UnityEngine.Random.Range(0f, 1f);
        float total = 0f;
        foreach (BlockData block in _blockToChooseFrom)
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
