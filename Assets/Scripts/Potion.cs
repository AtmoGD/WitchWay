using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    [SerializeField] private GameObject diePrefab = null;
    private void OnTriggerEnter(Collider other)
    {
        Witch witch = other.GetComponent<Witch>();
        if (witch != null)
        {
            if (!witch.IsImmune)
            {
                witch.SetImmune();
                Die();
            }
        }
    }

    private void Die()
    {
        if (diePrefab != null)
            Instantiate(diePrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
