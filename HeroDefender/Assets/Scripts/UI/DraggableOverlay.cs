using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableOverlay : MonoBehaviour
{
    public SpriteRenderer PlacementOverlay;
    public LayerMask PlacementLayermask;
    public bool IsOverlapping;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != PlacementLayermask)
        {
            IsOverlapping = true;
            PlacementOverlay.color = Color.red;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer != PlacementLayermask)
        {
            IsOverlapping = false;
            PlacementOverlay.color = Color.green;
        }
    }
}
