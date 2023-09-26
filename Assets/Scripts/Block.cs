using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockType
{
    Base,
    Wall,
    Obstacle,
    PowerUp
}

public class Block : MonoBehaviour
{
    [field: SerializeField] public Transform ObjectParent { get; set; } = null;
    [field: SerializeField] public BlockType BlockType { get; set; } = BlockType.Base;
    [field: SerializeField] public GameObject BlockObject { get; set; } = null;

    public void SetBlock(BlockType _blockType, GameObject _prefab)
    {
        BlockType = _blockType;

        if (BlockType != BlockType.Base)
        {
            BlockObject = Instantiate(_prefab, transform.position, _prefab.transform.rotation, ObjectParent);
            BlockObject.transform.localScale = Vector3.one;
        }
    }
}