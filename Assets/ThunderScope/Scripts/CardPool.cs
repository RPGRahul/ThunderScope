using System.Collections.Generic;
using UnityEngine;

public class CardPool : MonoBehaviour
{
    public static CardPool instance;

    [SerializeField]
    private List<GameObject> cardPrefabs;

    private List<GameObject> cardPool;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        cardPool = new List<GameObject>();

        // Populate card pool
        for (int i = 0; i < cardPrefabs.Count; i++)
        {
            GameObject temp = Instantiate(cardPrefabs[i]);
            temp.SetActive(false);
            cardPool.Add(temp);
            // Twice for pairs
            temp = Instantiate(cardPrefabs[i]);
            temp.SetActive(false);
            cardPool.Add(temp);
        }
    }

    public List<GameObject> GetCardsForSession(int count)
    {
        if (count > cardPool.Count)
        {
            Debug.LogError("CardPoolError: Tried to get more cards than available in pool");
            return null;
        }

        List<GameObject> sessionCardPool = new();
        for (int i = 0; i < count; i++)
        {
            sessionCardPool.Add(cardPool[i]);
        }
        return sessionCardPool;
    }
}
