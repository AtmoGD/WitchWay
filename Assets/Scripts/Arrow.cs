using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Witch witch;

    private void Start()
    {
        witch = GameObject.FindObjectOfType<Witch>();
    }

    private void Update()
    {
        if (!witch)
            return;

        transform.rotation = Quaternion.LookRotation(transform.position - witch.transform.position);
    }
}