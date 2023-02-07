using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AchivementsItem",
    menuName = "ScriptableObject/AchivementsItem")]
public class AchivementsItem : ScriptableObject
{
    public int id;
    public string title;
    public string description;
    public bool isUnblocked;
}
