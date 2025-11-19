using System.Collections;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int id;
    public bool isFlippedUp;

    [SerializeField]
    private Transform visualTransform;

    private bool isFlippingUp, isFlippingDown;
    private Coroutine flippingUp, flippingDown;
}
