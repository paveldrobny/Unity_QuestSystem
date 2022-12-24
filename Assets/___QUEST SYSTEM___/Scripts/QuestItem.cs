using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestDataObject",
    menuName = "ScriptableObject/QuestItem")]
public class QuestItem : ScriptableObject
{
    public string questName;
    public bool isQuestConfirm = false;

    // Spawn start quest trigger
    public Vector3 triggerPosition;
    public Quaternion triggerRotation;

    public int currentObjectivesID = 0;
    public int xp = 0;
    public QuestObjective[] objectives;
}

//[System.Serializable]
//public enum TypeObjective { Location, Destroy };

[System.Serializable]
public class QuestObjective
{
    public string name;
    //public TypeObjective typeObjective;
    public GameObject spawnObjective;

    // Spawn objective location
    public Vector3 triggerPosition;
    public Quaternion triggerRotation;
    public Vector3 triggerScale = new Vector3(1, 1, 1);
}