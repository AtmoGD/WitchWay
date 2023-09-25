using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Witch : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] private LayerMask blockLayer = 0;

    private int dir = 30;
    private bool isActive = false;
    private Block targetBlock = null;

    // Just a helper property to get the LevelGenerator instanceat runtime and in the editor
    private LevelGenerator LevelGen
    {
        get
        {
            if (LevelGenerator.Instance == null)
                return FindObjectOfType<LevelGenerator>();

            return LevelGenerator.Instance;
        }
    }

    private void Start()
    {
        isActive = true;
    }

    private void Update()
    {
        if (!isActive) return;

        if (targetBlock == null)
        {
            isActive = false;
            return;
        }

        Move();
        Rotate();
    }

    public void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetBlock.transform.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetBlock.transform.position) < 0.1f)
        {
            CheckCurrentBlock();
            CalculateNextBlock();
        }
    }

    public void Rotate()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(targetBlock.transform.position - transform.position), rotationSpeed * Time.deltaTime);
    }

    public void Turn(int direction)
    {
        dir += direction * 60;
        CalculateNextBlock();
    }

    public void TurnRight()
    {
        Turn(1);
    }

    public void TurnLeft()
    {
        Turn(-1);
    }

    public void SetTargetBlock(Block block)
    {
        targetBlock = block;
    }

    public void CheckCurrentBlock()
    {
        // Check if the current block is a obstacle and if so die
    }

    public bool SetFreeStartRotation()
    {
        CalculateNextBlock();

        if (targetBlock == null) return false;

        List<int> directions = new List<int>() { 30, 90, 150, 210, 270, 330 };

        while (directions.Count > 0)
        {
            dir = directions[UnityEngine.Random.Range(0, directions.Count)];
            directions.Remove(dir);

            CalculateNextBlock();

            transform.rotation = Quaternion.LookRotation(targetBlock.transform.position - transform.position);

            if (targetBlock.BlockType == BlockType.Base) return true;
        }

        return false;
    }

    public void CalculateNextBlock()
    {
        Vector3 nextBlockPosition = transform.position + Quaternion.Euler(0, dir, 0) * Vector3.forward * LevelGen.HexagonSize;

        Collider[] blocks = Physics.OverlapSphere(nextBlockPosition, 0.1f, blockLayer);
        if (blocks.Length > 0)
            SetTargetBlock(blocks[0].GetComponent<Block>());
    }

    private void OnDrawGizmos()
    {
        if (targetBlock != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(targetBlock.transform.position, 0.5f);
        }
    }
}
