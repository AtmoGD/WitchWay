using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockType
{
    Base,
    Wall,
    Obstacle
}

public class Block : MonoBehaviour
{
    [field: SerializeField] public BlockType blockType { get; private set; } = BlockType.Base;
}
