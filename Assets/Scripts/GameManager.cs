using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Witch witch = null;
    [SerializeField] private LevelGenerator levelGenerator = null;

    private Vector2 pointerPosition = Vector2.zero;
    private bool isPlacing = false;

    private void StartPlacing()
    {
        if (isPlacing) return;

        isPlacing = true;

        print("Start Placing");
    }

    private void StopPlacing()
    {
        if (!isPlacing) return;

        isPlacing = false;

        print("Stop Placing");
    }

    public void OnPlaceObject(InputAction.CallbackContext context)
    {
        if (context.started)
            StartPlacing();
        else if (context.canceled)
            StopPlacing();
    }

    public void OnPointerPosition(InputAction.CallbackContext context)
    {
        pointerPosition = context.ReadValue<Vector2>();
    }
}
