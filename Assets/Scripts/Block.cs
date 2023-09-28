using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum BlockType
{
    Base,
    Wall,
    Obstacle,
    PowerUp,
    Vanished
}

public class Block : MonoBehaviour
{
    [field: SerializeField] public Animator Animator { get; set; } = null;
    [field: SerializeField] public Transform ObjectParent { get; set; } = null;
    [field: SerializeField] public BlockTopper BlockTopper { get; set; } = null;
    [field: SerializeField] public BlockType BlockType { get; set; } = BlockType.Base;

    public GameObject BlockObject { get; set; } = null;

    public void SetBlock(BlockType _blockType, GameObject _prefab)
    {
        BlockType = _blockType;

        if (BlockType != BlockType.Base)
        {
            if (BlockObject != null)
                Destroy(BlockObject);

            BlockObject = Instantiate(_prefab, transform.position, _prefab.transform.rotation, ObjectParent);
            BlockObject.transform.localScale = Vector3.one;

            ActiveController activeController = BlockObject.GetComponent<ActiveController>();
            if (activeController != null)
                activeController.IsSetActive = true;

            Turn turn = BlockObject.GetComponent<Turn>();
            if (turn != null)
                turn.SetBlock(this);
        }

        if (BlockTopper != null)
            BlockTopper.UpdateTopper(BlockType);
    }

    public void Vanish()
    {
        BlockType = BlockType.Vanished;
        Animator.SetTrigger("Vanish");
    }
}
