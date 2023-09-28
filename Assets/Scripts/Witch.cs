using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Witch : MonoBehaviour
{
    [SerializeField] private Animator animator = null;
    [SerializeField] private LevelGenerator LevelGen = null;
    [SerializeField] private GameManager gameManager = null;
    [SerializeField] private Animator magicMushroomController = null;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] private LayerMask blockLayer = 0;
    [SerializeField] private GameObject dieObject = null;
    [SerializeField] private AudioSource powerUpSound = null;

    public int Dir { get; private set; } = 30;
    public bool IsImmune { get; private set; } = false;
    private bool isActive = false;
    private Block targetBlock = null;

    private float Speed
    {
        get
        {
            return speed * gameManager.SpeedMultiplier;
        }
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

        animator.SetFloat("SpeedMultiplier", gameManager.SpeedMultiplier);
    }

    public void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetBlock.transform.position, Speed * Time.deltaTime);

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
        Dir += direction * 60;
    }

    public void TurnRight()
    {
        Turn(1);
    }

    public void TurnLeft()
    {
        Turn(-1);
    }

    public void SetTargetBlock(Block _block)
    {
        targetBlock = _block;
    }

    public void SetIsActive()
    {
        isActive = true;
    }

    public void PowerUp()
    {
        powerUpSound.Play();
    }

    public void GetHigh()
    {
        magicMushroomController.SetTrigger("GetHigh");
    }

    public void Die()
    {
        isActive = false;

        if (dieObject != null)
            Instantiate(dieObject, transform.position, Quaternion.identity);

        gameManager.EndLevel();

        Destroy(gameObject);
    }

    public void SetImmune()
    {
        IsImmune = true;
    }

    public void Jump()
    {
        animator.SetTrigger("Jump");
    }

    public void CheckCurrentBlock()
    {
        if (targetBlock == null) return;

        switch (targetBlock.BlockType)
        {
            case BlockType.Obstacle:
                if (!IsImmune)
                    Die();
                break;
            case BlockType.Wall:
                Die();
                break;
            case BlockType.Vanished:
                Die();
                break;
            case BlockType.PowerUp:
                break;
            case BlockType.Base:
                break;
        }

        IsImmune = false;
    }

    public bool SetFreeStartRotation()
    {
        if (targetBlock == null) return false;

        List<int> directions = new List<int>() { 30, 90, 150, 210, 270, 330 };

        while (directions.Count > 0)
        {
            Dir = directions[UnityEngine.Random.Range(0, directions.Count)];
            directions.Remove(Dir);

            CalculateNextBlock();

            transform.rotation = Quaternion.LookRotation(targetBlock.transform.position - transform.position);

            if (targetBlock.BlockType == BlockType.Base) return true;
        }

        return false;
    }

    public void CalculateNextBlock(float _distance = 1f)
    {
        Vector3 nextBlockPosition = transform.position + Quaternion.Euler(0, Dir, 0) * Vector3.forward * _distance * LevelGen.HexagonSize;

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
