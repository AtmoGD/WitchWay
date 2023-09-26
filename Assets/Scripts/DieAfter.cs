using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieAfter : MonoBehaviour
{
    [SerializeField] private float time = 2f;

    private void Start()
    {
        Destroy(gameObject, time);
    }
}
