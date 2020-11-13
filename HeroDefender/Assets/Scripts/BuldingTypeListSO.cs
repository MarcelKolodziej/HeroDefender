using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BuildingTypeList")]
public class BuldingTypeListSO : ScriptableObject
{
    public List<BuildingTypeSO> list;
}
