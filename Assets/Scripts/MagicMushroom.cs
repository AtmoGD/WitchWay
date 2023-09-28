using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicMushroom : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Witch witch = other.GetComponent<Witch>();
        if (witch != null)
        {
            witch.GetHigh();
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
