using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private float timeToLive = 30f;
    [SerializeField] private float speed = 1f;
    [SerializeField] private Slider timeSlider = null;
    [field: SerializeField] public BlockType BlockType { get; private set; } = BlockType.Obstacle;
    [field: SerializeField] public GameObject Prefab { get; private set; } = null;

    private Transform target = null;
    private CardController cardController = null;
    private bool isPlacing = false;
    private float timeLeft = 0f;

    private void Start()
    {
        timeLeft = timeToLive;
    }

    private void Update()
    {
        Move();

        if (isPlacing) return;

        timeLeft -= Time.deltaTime;

        timeSlider.value = timeLeft / timeToLive;

        if (timeLeft <= 0f)
            Die();
    }

    public void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    public void OnPointerDown(PointerEventData _eventData)
    {
        cardController.StartPlacingCard(this);
        isPlacing = true;
    }

    public void CancelPlacement()
    {
        isPlacing = false;
    }

    public void SetTarget(Transform _target)
    {
        this.target = _target;
    }

    public void SetCardController(CardController _cardController)
    {
        this.cardController = _cardController;
    }

    public void Die()
    {
        cardController.RemoveCard(this);
        Destroy(gameObject);
    }
}