using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlacementController : MonoBehaviour
{
    [SerializeField] private LayerMask placementLayer = 0;
    private Vector2 pointerPosition = Vector2.zero;
    private bool isPlacing = false;
    private Card card = null;
    private GameObject placementObject = null;
    private Block block = null;

    public void StartPlacingCard(Card card)
    {
        if (isPlacing) return;

        this.card = card;

        isPlacing = true;
    }

    private void StopPlacing()
    {
        if (!isPlacing) return;

        if (block && card.CanBePlacedOn.Contains(block.BlockType) && placementObject && placementObject.activeSelf)
        {
            block.SetBlock(card.BlockType, card.Prefab);
            card.Die();
        }
        else
        {
            card.CancelPlacement();
        }

        ResetThis();

        isPlacing = false;
    }

    private void ResetThis()
    {
        card = null;
        Destroy(placementObject);
        placementObject = null;
        block = null;
    }

    public void OnPlaceObject(InputAction.CallbackContext _context)
    {
        if (_context.canceled)
            StopPlacing();
    }

    public void OnPointerPosition(InputAction.CallbackContext _context)
    {
        pointerPosition = _context.ReadValue<Vector2>();

        if (!isPlacing || !card) return;

        UpdatePlacementObject();
    }

    private void UpdatePlacementObject()
    {
        if (!placementObject)
            placementObject = Instantiate(card.Prefab, pointerPosition, Quaternion.identity);

        Ray ray = Camera.main.ScreenPointToRay(pointerPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, placementLayer))
        {
            Block newBlock = hit.collider.GetComponent<Block>();

            if (newBlock != null && card.CanBePlacedOn.Contains(newBlock.BlockType))
            {
                block = newBlock;
                placementObject.transform.position = block.transform.position;
                placementObject.SetActive(true);
            }
            else
                placementObject.SetActive(false);
        }
    }
}
