using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMenager : MonoBehaviour
{
    [SerializeField] private Transform WoodHarvester;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Instantiate(WoodHarvester, GetMouseWorldPosition(), Quaternion.identity);
        }
    }
    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseWorldPosition = (mainCamera.ScreenToWorldPoint(Input.mousePosition));
        mouseWorldPosition.z = 0f;
        return mouseWorldPosition;
    }
}