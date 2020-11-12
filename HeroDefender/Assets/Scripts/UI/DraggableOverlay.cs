using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableOverlay : MonoBehaviour
{
    public SpriteRenderer PlacementOverlay;
    public bool IsOverlapping;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        IsOverlapping = true;
        PlacementOverlay.color = Color.red;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        IsOverlapping = false;
        PlacementOverlay.color = Color.green;
    }
}
