using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HourglassSpawner : MonoBehaviour
{
    [SerializeField] private GameManager gameManager = null;
    [SerializeField] private LevelGenerator levelGenerator = null;
    [SerializeField] private Hourglass hourglass = null;
    [SerializeField] private GameObject magicMushroom = null;
    [SerializeField] private float magicMushroomSpawnChance = 0.1f;
    [SerializeField] private float hourglassSpawnChance = 0.1f;
    [SerializeField] private float hourglassSpawnTime = 5f;
    [SerializeField] private int minHourglasses = 1;
    [SerializeField] private int maxHourglasses = 5;

    private float timer = 0f;
    private List<Hourglass> hourglasses = new List<Hourglass>();

    private void Update()
    {
        if (hourglasses.Count < minHourglasses)
            SpawnHourglass();

        timer += Time.deltaTime;
        if (timer >= hourglassSpawnTime)
        {
            timer = 0f;

            if (Random.Range(0f, 1f) <= magicMushroomSpawnChance)
                SpawnMagicMushroom();
            else if (Random.Range(0f, 1f) <= hourglassSpawnChance)
                SpawnHourglass();
        }
    }

    public void SpawnHourglass()
    {
        List<Block> blocks = levelGenerator.GetBaseBlocks();

        if (blocks.Count > 0)
        {
            Block block = blocks[Random.Range(0, blocks.Count)];

            block.SetBlock(BlockType.PowerUp, hourglass.gameObject);

            Hourglass newHourglass = block.BlockObject.GetComponent<Hourglass>();
            newHourglass.SetGameManager(gameManager);
            newHourglass.SetHourglasSpawner(this);
            newHourglass.SetBlock(block);

            hourglasses.Add(newHourglass);
        }
    }

    public void SpawnMagicMushroom()
    {
        List<Block> blocks = levelGenerator.GetBaseBlocks();

        if (blocks.Count > 0)
        {
            Block block = blocks[Random.Range(0, blocks.Count)];

            block.SetBlock(BlockType.PowerUp, magicMushroom);
        }
    }

    public void RemoveHourglass(Hourglass _hourglass)
    {
        hourglasses.Remove(_hourglass);
    }
}
