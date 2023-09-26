using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Witch witch = null;
    [SerializeField] private LevelGenerator levelGenerator = null;
    [SerializeField] private PlacementController placementController = null;
    [SerializeField] private AnimationCurve speedCurve = null;

    public bool IsSetActive { get; private set; } = false;
    public float GameTime { get; private set; } = 0f;
    public float SpeedMultiplier
    {
        get
        {
            return speedCurve.Evaluate(GameTime * 0.1f);
        }
    }

    private void Start()
    {
        SetUpLevel();
    }

    public void SetUpLevel()
    {
        levelGenerator.GenerateLevel();

        PositionWitch(10);
    }

    public void StartLevel()
    {
        witch.SetIsActive();

        IsSetActive = true;
    }

    public void EndLevel()
    {
        IsSetActive = false;
    }

    public void Update()
    {
        if (!IsSetActive) return;

        GameTime += Time.deltaTime;
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
    }
}
