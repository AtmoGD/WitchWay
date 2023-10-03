using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    [SerializeField] private int dir = 30;

    private void OnTriggerEnter(Collider other)
    {
        Witch witch = other.GetComponent<Witch>();
        if (witch != null)
        {
            if (witch.Dir == dir || witch.Dir == (dir + 180) % 360)
            {
                witch.SetImmune();
            }
        }

    }
}
