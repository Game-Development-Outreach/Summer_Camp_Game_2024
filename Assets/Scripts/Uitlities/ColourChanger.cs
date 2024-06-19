using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourChanger : MonoBehaviour
{
    [SerializeField] private Color endColour;
    [SerializeField] private float changeSpeed;
    [SerializeField] private SpriteRenderer spriteRenderer;

    public void ChangeColour() => StartCoroutine(ChangeColourRoutine());

    IEnumerator ChangeColourRoutine()
    {
        while (spriteRenderer.color != endColour)
        {
            spriteRenderer.color = Color.Lerp(spriteRenderer.color, endColour, Time.deltaTime * changeSpeed);
            yield return new WaitForEndOfFrame();
        }
    }
}
