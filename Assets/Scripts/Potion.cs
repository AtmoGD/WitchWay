using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer = null;
    [SerializeField] private GameObject diePrefab = null;
    private void OnTriggerEnter(Collider other)
    {
        Witch witch = other.GetComponent<Witch>();
        if (witch != null)
        {
            witch.PowerUp();
            meshRenderer.enabled = false;
            // if (!witch.IsImmune)
            // {
            //     witch.SetImmune();
            //     witch.PowerUp();
            //     Die();
            // }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Witch witch = other.GetComponent<Witch>();
        if (witch != null)
        {
            witch.SetImmune();
            Die();
        }
    }

    private void Die()
    {
        if (diePrefab != null)
            Instantiate(diePrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
