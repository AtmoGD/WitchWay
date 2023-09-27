using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotation : MonoBehaviour
{
    [SerializeField] private bool randomX = false;
    [SerializeField] private bool randomY = false;
    [SerializeField] private bool randomZ = false;

    private void Start()
    {
        Vector3 rotation = transform.rotation.eulerAngles;

        if (randomX)
            rotation.x = Random.Range(0f, 360f);

        if (randomY)
            rotation.y = Random.Range(0f, 360f);

        if (randomZ)
            rotation.z = Random.Range(0f, 360f);

        transform.rotation = Quaternion.Euler(rotation);
    }
}
