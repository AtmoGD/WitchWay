using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Witch witch = null;
    [SerializeField] private LevelGenerator levelGenerator = null;
    [SerializeField] private PlacementController placementController = null;

    private void Start()
    {
        StartLevel();
    }

    public void StartLevel()
    {
        levelGenerator.GenerateLevel();

        witch.SetTargetBlock(levelGenerator.StartBlock);
        witch.transform.position = levelGenerator.StartBlock.transform.position;
        witch.CalculateNextBlock();
    }
}
