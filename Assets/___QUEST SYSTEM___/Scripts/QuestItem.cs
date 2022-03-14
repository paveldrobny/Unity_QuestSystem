using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestDataObject",
    menuName = "ScriptableObject/QuestItem")]
public class QuestItem : ScriptableObject
{
    public string questName;
    public bool isQuestConfirm = false;

    // Spawn main trigger location
    public Vector3 triggerPosition;
    public Quaternion triggerRotation;

    public int currentSubTargetID = 0;
    public QuestSubTargets[] subTargets;
}

[System.Serializable]
public class QuestSubTargets
{
    public string targetName;

    // Spawn sub trigger location
    public Vector3 triggerPosition;
    public Quaternion triggerRotation;
}