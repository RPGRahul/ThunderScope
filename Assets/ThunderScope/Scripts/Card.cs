using System.Collections;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int id;
    public bool isFlippedUp;

    [SerializeField]
    private Transform visualTransform;

    private bool isInteractable, isSelected, isBeingRevealed, isFlippingUp, isFlippingDown;
    private Coroutine flippingUp, flippingDown;

    private void OnEnable()
    {
        visualTransform.rotation = Quaternion.identity;

        isFlippedUp = true;
        isInteractable = false;
        isBeingRevealed = false;
        isFlippingUp = false;
        isFlippingDown = false;
    }

    public void Tapped()
    {
        if (!isInteractable || isBeingRevealed)
            return;
    }

    public void BeginReveal()
    {
        isBeingRevealed = true;
    }
}
