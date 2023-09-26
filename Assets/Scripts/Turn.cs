using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn : MonoBehaviour
{
    [SerializeField] private int dir = 1;

    private void OnTriggerEnter(Collider other)
    {
        Witch witch = other.GetComponent<Witch>();
        if (witch != null)
        {
            witch.Turn(dir);
        }

        Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
