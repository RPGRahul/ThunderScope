using System.Collections.Generic;
using UnityEngine;

public class CardGrid : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;

    private List<GameObject> sessionCards;

    private float camHeight;
    private float camWidth;
    private float camTop;
    private float usableHeight;

    private const float spacing = 0.5f;
    private const float verticalPadding = 1.5f;

    private void Start()
    {
        camHeight = mainCamera.orthographicSize * 2f;
        camWidth = camHeight * mainCamera.aspect;
        camTop = camHeight / 2f;
        usableHeight = camHeight - (verticalPadding * 2f);

        // TEST
        BuildCards(3, 6);
    }

    public void BuildCards(int rows, int columns)
    {
        int count = rows * columns;

        sessionCards = CardPool.instance.GetCardsForSession(count);
        Shuffle(sessionCards);

        // Calculate grid spacing
        float totalSpacingW = (columns - 1) * spacing;
        float totalSpacingH = (rows - 1) * spacing;

        // Per-card cell area (assuming card size is always 1x1)
        float cellWidth = (camWidth - totalSpacingW) / columns;
        float cellHeight = (usableHeight - totalSpacingH) / rows;
        float scale = Mathf.Min(cellWidth, cellHeight);
        Vector3 cardScale = new(scale, scale, 1f);

        // Total grid dimensions after scaling
        float gridWidth = (columns * scale) + totalSpacingW;
        float gridHeight = (rows * scale) + totalSpacingH;

        float gridTop = camTop - verticalPadding;

        Vector3 startPoint = new(-gridWidth / 2f + (scale * 0.5f), gridTop - (scale * 0.5f), 0f);

        // Populate grid
        int index = 0;
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < columns; c++)
            {
                GameObject card = sessionCards[index];

                Vector3 worldPos = new(startPoint.x + c * (scale + spacing), startPoint.y - r * (scale + spacing), 0f);
                card.transform.position = worldPos;
                card.transform.localScale = cardScale;

                index++;
            }
        }

        // Start initial reveal behavior
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
