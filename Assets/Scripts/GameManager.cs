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

        PositionWitch(10);
        // witch.SetTargetBlock(levelGenerator.StartBlock);
        // witch.transform.position = levelGenerator.StartBlock.transform.position;
        // witch.CalculateNextBlock();
    }

    public void PositionWitch(int _maxCount)
    {
        if (_maxCount <= 0)
        {
            Debug.LogError("Could not position witch");
            return;
        }

        levelGenerator.SetStartBlock();
        witch.SetTargetBlock(levelGenerator.StartBlock);
        witch.transform.position = levelGenerator.StartBlock.transform.position;

        if (!witch.SetFreeStartRotation())
            PositionWitch(_maxCount--);
        // witch.CalculateNextBlock();
    }
}
