using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingUIButton : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private Camera MainCamera;
    [SerializeField] private DraggableOverlay Building;
    [SerializeField] private GameObject BuildingPrefab;
    [SerializeField] private int BuildingCost = 10;
    private Vector3 NewDraggableBuildingPosition = new Vector3();

    public void OnPointerDown(PointerEventData eventData)
    {
        if (LevelManager.m_LevelManager.CurrentGold > BuildingCost)
        {
            Debug.Log("On Mouse Down");
            Building.gameObject.SetActive(true);
            NewDraggableBuildingPosition = MainCamera.ScreenToWorldPoint(Input.mousePosition);
            NewDraggableBuildingPosition.z = 0;
            Building.transform.position = NewDraggableBuildingPosition;
        }
        else
        {
            Debug.Log("Not Enough Currency");
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("On Mouse Drag");

        if (Building != null)
        {
            NewDraggableBuildingPosition = MainCamera.ScreenToWorldPoint(Input.mousePosition);
            NewDraggableBuildingPosition.z = 0;
            Building.transform.position = NewDraggableBuildingPosition;
        }
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("On Mouse Up");

        if (Building.IsOverlapping == true || LevelManager.m_LevelManager.CurrentGold < BuildingCost)
        {
            Building.gameObject.SetActive(false);
        }
        else
        {
           Building.gameObject.SetActive(false);
            LevelManager.m_LevelManager.BuildingPurchased(0, BuildingCost);
            // Instantiate(BuildingPrefab, NewDraggableBuildingPosition, Quaternion.identity);
        }
    }
}
