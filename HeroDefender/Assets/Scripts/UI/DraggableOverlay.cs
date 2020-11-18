using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableOverlay : MonoBehaviour
{
    public SpriteRenderer PlacementOverlay;
    public LayerMask PlacementLayermask;
    public bool IsOverlapping;
    private int EnemiesInsideCollisionRange = 0;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != PlacementLayermask)
        {
            IsOverlapping = true;
            EnemiesInsideCollisionRange++;
            PlacementOverlay.color = Color.red;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer != PlacementLayermask)
        {
            EnemiesInsideCollisionRange--;

            if (EnemiesInsideCollisionRange < 1)
            {
                IsOverlapping = false;
                PlacementOverlay.color = Color.green;
            }
        }
    }
}
