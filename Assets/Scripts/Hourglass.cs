using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hourglass : MonoBehaviour
{
    [SerializeField] private float timeManiputlation = -0.5f;
    [SerializeField] private HourglassSpawner hourglasSpawner = null;
    [SerializeField] private Block block = null;
    private GameManager gameManager = null;

    private void OnTriggerEnter(Collider other)
    {
        Witch witch = other.GetComponent<Witch>();
        if (witch != null)
        {
            gameManager.AddTime(timeManiputlation);
            witch.PowerUp();
            Die();
        }
    }

    public void SetGameManager(GameManager _gameManager)
    {
        gameManager = _gameManager;
    }

    public void SetHourglasSpawner(HourglassSpawner _hourglasSpawner)
    {
        hourglasSpawner = _hourglasSpawner;
    }

    public void SetBlock(Block _block)
    {
        block = _block;
    }

    private void Die()
    {
        hourglasSpawner.RemoveHourglass(this);
        block.SetBlock(BlockType.Base, null);

        Destroy(gameObject);
    }
}
