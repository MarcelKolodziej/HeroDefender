using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera MainCamera;
    [SerializeField] private Vector3 CameraDefaultPosition = new Vector3(0f, 0f, 10f);
    [SerializeField] private float MaxOthographicView = 10;
    [SerializeField] private float minOthographicView = 4;

    [SerializeField] private BoxCollider2D CameraRestrictor;
    [SerializeField] private float DragSpeed = 1f;

    private Vector2 dragOrigin;
    private Vector2 distanceTravelled;
    private Vector2 amountToMove;
    private Vector3 newPosition;
    private Vector4 cameraRestrictorBounds;

    private void Start()
    {
        UpdateCameraRestrictorBounds();
    }

    public void UpdateCameraRestrictorBounds()
    {
        float screenAspect = (float)(Screen.width / (float)Screen.height);
        float cameraHeight = MainCamera.orthographicSize;

        cameraRestrictorBounds = new Vector4
        ((CameraRestrictor.size.x / 2) - ((screenAspect * cameraHeight)),
        -((CameraRestrictor.size.x / 2) - ((screenAspect * cameraHeight))),
        ((CameraRestrictor.size.y + (CameraRestrictor.offset.y * 2)) / 2) - cameraHeight,
        -((CameraRestrictor.size.y - (CameraRestrictor.offset.y * 2)) / 2) + cameraHeight);
    }

    public void OnViewSliderChanged(Slider slider)
    {
        MainCamera.orthographicSize = Mathf.Lerp(minOthographicView, MaxOthographicView, slider.value);
        UpdateCameraRestrictorBounds();
        MainCamera.transform.position = ValidateNewPosition(newPosition);
    }

    public void ResetCameraPosition()
    {
        MainCamera.transform.position = CameraDefaultPosition;
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

        MainCamera.transform.position = ValidateNewPosition(newPosition);
    }

    private Vector3 ValidateNewPosition(Vector3 position)
    {
        if (position.x > cameraRestrictorBounds.x)
        {
            position.x = cameraRestrictorBounds.x;
        }

        if (position.x < cameraRestrictorBounds.y)
        {
            position.x = cameraRestrictorBounds.y;
        }

        if (position.y > cameraRestrictorBounds.z)
        {
            position.y = cameraRestrictorBounds.z;
        }

        if (position.y < cameraRestrictorBounds.w)
        {
            position.y = cameraRestrictorBounds.w;
        }

        return position;
    }
}
