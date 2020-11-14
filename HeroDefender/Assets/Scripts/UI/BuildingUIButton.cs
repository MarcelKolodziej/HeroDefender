using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingUIButton : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private Camera MainCamera;
    [SerializeField] private DraggableOverlay Building;
    [SerializeField] private GameObject BuildingPrefab;
    [SerializeField] private int BuildingIronCost = 10;
    [SerializeField] private int BuildingGoldCost = 10;
    private Vector3 NewDraggableBuildingPosition = new Vector3();

    public void OnPointerDown(PointerEventData eventData)
    {
        if (LevelManager.m_LevelManager.CurrentIron > BuildingIronCost && LevelManager.m_LevelManager.CurrentGold > BuildingGoldCost)
        {
                Debug.Log("On Left Click");
                Building.gameObject.SetActive(true);
                NewDraggableBuildingPosition = MainCamera.ScreenToWorldPoint(Input.mousePosition);
                NewDraggableBuildingPosition.z = 0;
                Building.transform.position = NewDraggableBuildingPosition;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("On Mouse Drag");

        if (Building != null)
        {
            NewDraggableBuildingPosition = MainCamera.ScreenToWorldPoint(Input.mousePosition);
            NewDraggableBuildingPosition.z = 0;
            Building.transform.position = NewDraggableBuildingPosition;
        }
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        //Debug.Log("On Mouse Up");

        if (Building.IsOverlapping == true || LevelManager.m_LevelManager.CurrentIron < BuildingIronCost || LevelManager.m_LevelManager.CurrentGold < BuildingGoldCost)
        {
            //Debug.Log("Building Overlapping or Not Enough Funds");
            Building.gameObject.SetActive(false);
        }
        else
        { 
            //Debug.Log("Building Created");
            Building.gameObject.SetActive(false);
            LevelManager.m_LevelManager.BuildingPurchased(BuildingIronCost, BuildingGoldCost);
            Instantiate(BuildingPrefab, NewDraggableBuildingPosition, Quaternion.identity);
        }
    }
}
