using System.Collections.Generic;
using UnityEngine;

public class CardGrid : MonoBehaviour
{
    private Transform thisTransform;

    [SerializeField]
    private Camera mainCamera;

    private List<GameObject> sessionCards;

    private void Start()
    {
        thisTransform = transform;

        // TEST
        BuildCards(4, 4);
    }

    public void BuildCards(int rows, int columns)
    {
        int cardCount = rows * columns;

        sessionCards = CardPool.instance.GetCardsForSession(cardCount);
        Shuffle(sessionCards);

        // Insert grid logic here

        foreach (GameObject card in sessionCards)
        {
            card.SetActive(true);
            card.GetComponentInParent<Card>().BeginReveal();
        }
    }

    private void Shuffle(List<GameObject> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int swapIndex = Random.Range(i, list.Count);
            (list[i], list[swapIndex]) = (list[swapIndex], list[i]);
        }
    }
}
