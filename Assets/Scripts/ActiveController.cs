using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveController : MonoBehaviour
{
    [SerializeField] private bool isSetActive = false;
    [SerializeField] private List<Collider> colliders = new List<Collider>();

    public bool IsSetActive
    {
        get
        {
            return isSetActive;
        }
        set
        {
            isSetActive = value;
            foreach (Collider collider in colliders)
                collider.enabled = isSetActive;
        }
    }

    private void Start()
    {
        IsSetActive = isSetActive;
    }
}
