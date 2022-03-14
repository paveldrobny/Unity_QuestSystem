using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestDataObject",
    menuName = "ScriptableObject/QuestData")]
public class QuestData : ScriptableObject
{
    public int currentQuestID = 0;

    public QuestItem[] questItems;
}
