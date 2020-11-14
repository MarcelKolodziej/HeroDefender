using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera MainCamera;
    [SerializeField] private float MaxOthographicView = 10;
    [SerializeField] private float minOthographicView = 4;

    [SerializeField] private BoxCollider2D CameraRestrictor;
    [SerializeField] private float DragSpeed = 1f;

    private Vector2 dragOrigin;
    private Vector2 distanceTravelled;
    private Vector2 amountToMove;
    private Vector3 newPosition;
    private Vector4 CameraRestrictorBounds;

    private void Start()
    {
        CameraRestrictorBounds = new Vector4
            (CameraRestrictor.size.x / 2,
            -(CameraRestrictor.size.x / 2),
            (CameraRestrictor.size.y + (CameraRestrictor.offset.y * 2)) / 2,
            -((CameraRestrictor.size.y - (CameraRestrictor.offset.y * 2)) / 2));
    }

    public void OnViewSliderChanged(Slider slider)
    {
        MainCamera.orthographicSize = Mathf.Lerp(minOthographicView, MaxOthographicView, slider.value);
    }

    public void OnPointerDown(BaseEventData data)
    {
        dragOrigin = data.currentInputModule.input.mousePosition;
        //Debug.Log("On Mouse Down: " + dragOrigin);
    }

    public void OnDrag(BaseEventData data)
    {
        distanceTravelled = MainCamera.ScreenToViewportPoint(data.currentInputModule.input.mousePosition - dragOrigin);
        amountToMove = new Vector2(-distanceTravelled.x * DragSpeed, -distanceTravelled.y * DragSpeed);
        newPosition = new Vector3(amountToMove.x + MainCamera.transform.position.x, amountToMove.y + MainCamera.transform.position.y, MainCamera.transform.position.z);

        if (newPosition.x > CameraRestrictorBounds.x)
        {
            newPosition.x = CameraRestrictorBounds.x;
        }

        if (newPosition.x < CameraRestrictorBounds.y)
        {
            newPosition.x = CameraRestrictorBounds.y;
        }

        if (newPosition.y > CameraRestrictorBounds.z)
        {
            newPosition.y = CameraRestrictorBounds.z;
        }

        if (newPosition.y < CameraRestrictorBounds.w)
        {
            newPosition.y = CameraRestrictorBounds.w;
        }

        MainCamera.transform.position = newPosition;
    }
}
