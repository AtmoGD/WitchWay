using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn : MonoBehaviour
{
    [SerializeField] private int dir = 1;
    private Block block;

    private void OnTriggerEnter(Collider other)
    {
        Witch witch = other.GetComponent<Witch>();
        if (witch != null)
        {
            witch.Turn(dir);
            witch.PowerUp();
            Die();
        }

    }

    public void SetBlock(Block block)
    {
        this.block = block;
    }

    private void Die()
    {
        if (block != null)
            block.SetBlock(BlockType.Base, null);

        Destroy(gameObject);
    }
}
