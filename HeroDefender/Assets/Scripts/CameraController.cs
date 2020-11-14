using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera MainCamera;
    [SerializeField] private BoxCollider2D CameraRestrictor;
    [SerializeField] private float DragSpeed = 1f;

    private Vector2 dragOrigin;
    private Vector2 newPosition;
    private Vector2 moveAmount;

    private void Start()
    {
        Debug.Log(CameraRestrictor.size.x);
    }

    public void OnPointerDown(BaseEventData data)
    {
        dragOrigin = data.currentInputModule.input.mousePosition;
        Debug.Log("On Mouse Down: " + dragOrigin);
    }

    public void OnDrag(BaseEventData data)
    {
        newPosition = MainCamera.ScreenToViewportPoint(data.currentInputModule.input.mousePosition - dragOrigin);
        Debug.Log("On Mouse Drag: " + newPosition);

        moveAmount = new Vector2(-newPosition.x * DragSpeed, -newPosition.y * DragSpeed);

        if (newPosition.x > (CameraRestrictor.size.x / 2))
        {
            moveAmount.x = CameraRestrictor.size.x / 2;
        }

        if (newPosition.x < -(CameraRestrictor.size.x / 2))
        {
            moveAmount.x = -(CameraRestrictor.size.x / 2);
        }

        if (newPosition.y > (CameraRestrictor.size.y / 2))
        {
            moveAmount.y = CameraRestrictor.size.y / 2;
        }

        if (newPosition.y < -(CameraRestrictor.size.y / 2))
        {
            moveAmount.y = -(CameraRestrictor.size.x / 2);
        }

        MainCamera.transform.Translate(moveAmount, Space.World);
    }

    private void OnMouseUp()
    {
    }
}
