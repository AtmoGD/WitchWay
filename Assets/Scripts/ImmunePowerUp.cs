using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmunePowerUp : MonoBehaviour
{
    [SerializeField] private bool dieOnTrigger = true;
    private void OnTriggerEnter(Collider other)
    {
        Witch witch = other.GetComponent<Witch>();
        if (witch != null)
            witch.SetImmune();

        if (dieOnTrigger)
            Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
