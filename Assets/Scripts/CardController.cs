using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class CardData
{
    public Card card;
    public float spawnChance;
}

public class CardController : MonoBehaviour
{
    [SerializeField] private PlacementController placementController = null;
    [SerializeField] private List<Transform> cardPlaces = new List<Transform>();
    [SerializeField] private Transform startPosition = null;
    [SerializeField] private List<CardData> cardDatas = new List<CardData>();

    private Card[] cards;

    private void Start()
    {
        cards = new Card[cardPlaces.Count];
        SpawnStartCards();
    }

    private void SpawnStartCards()
    {
        for (int i = 0; i < cardPlaces.Count; i++)
            cards[i] = SpawnCard(cardPlaces[i]);
    }

    private Card SpawnCard(Transform _target)
    {
        CardData cardData = GetRandomCard(cardDatas);

        Card card = Instantiate(cardData.card, startPosition.position, startPosition.rotation, transform).GetComponent<Card>();
        card.SetTarget(_target);
        card.SetCardController(this);

        return card;
    }

    private CardData GetRandomCard(List<CardData> _cardDatas)
    {
        float totalSpawnChance = 0f;

        foreach (CardData cardData in _cardDatas)
        {
            totalSpawnChance += cardData.spawnChance;
        }

        float randomValue = UnityEngine.Random.Range(0f, totalSpawnChance);

        foreach (CardData cardData in _cardDatas)
        {
            if (randomValue <= cardData.spawnChance)
                return cardData;

            randomValue -= cardData.spawnChance;
        }

        return null;
    }

    public void StartPlacingCard(Card _card)
    {
        placementController.StartPlacingCard(_card);
    }

    public void RemoveCard(Card _card)
    {
        int index = Array.IndexOf(cards, _card);

        for (int i = index - 1; i >= 0; i--)
        {
            cards[i + 1] = cards[i];
        }

        cards[0] = SpawnCard(cardPlaces[0]);

        foreach (Card card in cards)
        {
            card.SetTarget(cardPlaces[Array.IndexOf(cards, card)]);
        }
    }
}
