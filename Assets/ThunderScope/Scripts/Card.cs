using System.Collections;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int id;
    public bool isFlippedUp;

    [SerializeField]
    private Transform visualTransform;

    private bool isInteractable, isSelected, isFlippingUp, isFlippingDown, isRunningUnselectionSequence;
    private Coroutine flipUp, flipDown, unselectSequence;

    private void OnEnable()
    {
        visualTransform.rotation = Quaternion.identity;

        isInteractable = false;
        isSelected = false;
        isFlippedUp = true;
        isFlippingUp = false;
        isFlippingDown = false;
        isRunningUnselectionSequence = false;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator FlipUp()
    {
        isFlippedUp = true;

        isFlippingUp = true;

        Quaternion targetRot = Quaternion.Euler(0f, 0f, 0f);
        while (visualTransform.rotation != targetRot)
        {
            visualTransform.rotation = Quaternion.RotateTowards(visualTransform.rotation, targetRot, 1800 * Time.deltaTime);
            yield return null;
        }
        visualTransform.rotation = targetRot;

        isFlippingUp = false;
    }

    private IEnumerator FlipDown()
    {
        isFlippedUp = false;

        isFlippingDown = true;

        Quaternion targetRot = Quaternion.Euler(0f, 180f, 0f);
        while (visualTransform.rotation != targetRot)
        {
            visualTransform.rotation = Quaternion.RotateTowards(visualTransform.rotation, targetRot, 1800 * Time.deltaTime);
            yield return null;
        }
        visualTransform.rotation = targetRot;

        isFlippingDown = false;
    }

    private IEnumerator RevealSequence()
    {
        isInteractable = false;

        yield return new WaitForSeconds(3f);

        flipDown = StartCoroutine(FlipDown());
        isInteractable = true;
    }

    private IEnumerator UnselectSequence()
    {
        isRunningUnselectionSequence = true;

        isSelected = false;

        yield return new WaitForSeconds(1f);

        flipDown = StartCoroutine(FlipDown());

        isRunningUnselectionSequence = false;
    }

    private IEnumerator DeactivationSequence()
    {
        isInteractable = false;

        yield return new WaitForSeconds(1f);

        gameObject.SetActive(false);
    }

    public Card Tapped()
    {
        if (!isInteractable || isSelected)
            return null;

        isSelected = true;

        // Handle reselection of already revealed card from a mismatch
        if (isRunningUnselectionSequence)
        {
            StopCoroutine(unselectSequence);
            return this;
        }

        if (isFlippingUp)
        {
            StopCoroutine(flipUp);
            isFlippingUp = false;

            flipDown = StartCoroutine(FlipDown());
        }
        else if (isFlippingDown)
        {
            StopCoroutine(flipDown);
            isFlippingDown = false;

            flipUp = StartCoroutine(FlipUp());
        }
        else
        {
            if (isFlippedUp)
            {
                flipDown = StartCoroutine(FlipDown());
            }
            else
            {
                flipUp = StartCoroutine(FlipUp());
            }
        }

        return this;
    }

    public void StartRevealSequence()
    {
        StartCoroutine(RevealSequence());
    }

    public void StartUnselectSequence()
    {
        unselectSequence = StartCoroutine(UnselectSequence());
    }

    public void StartDeactivationSequence()
    {
        StartCoroutine(DeactivationSequence());
    }
}
