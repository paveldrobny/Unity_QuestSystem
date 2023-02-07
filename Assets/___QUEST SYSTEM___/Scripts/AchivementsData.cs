using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AchivementsDataObject",
    menuName = "ScriptableObject/AchivementsData")]
public class AchivementsData : ScriptableObject
{
    public AchivementsItem[] items; 
}