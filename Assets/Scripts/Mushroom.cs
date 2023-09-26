using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Witch witch = other.GetComponent<Witch>();
        if (witch != null)
            witch.Jump();
    }

    private void OnTriggerExit(Collider other)
    {
        Witch witch = other.GetComponent<Witch>();
        if (witch != null)
            witch.SetImmune();
    }
}
