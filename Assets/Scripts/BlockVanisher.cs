using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockVanisher : MonoBehaviour
{
    [SerializeField] private LevelGenerator levelGenerator = null;
    [SerializeField] private float vanishTime = 0.5f;
    [SerializeField] private float vanishChance = 0.5f;

    private float vanishTimer = 0f;

    private void Update()
    {
        vanishTimer += Time.deltaTime;

        if (vanishTimer >= vanishTime)
        {
            vanishTimer = 0f;

            if (Random.Range(0f, 1f) <= vanishChance)
            {
                List<Block> blocks = levelGenerator.GetBaseBlocks();

                blocks[Random.Range(0, blocks.Count)].Vanish();
            }
        }
    }
}
