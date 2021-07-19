using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public QuestData data;

    public GameObject mainQuestTrigger;
    public GameObject subQuestTrigger;

    public Text questName;
    public Text questSubName;

    private void Start()
    {
        ResetQuestData();
        SpawnMainTrigger();
    }

    public int GetQuestID()
    {
        return data.currentQuestID;
    }

    public int GetQuestSubID()
    {
        return data.quests[GetQuestID()].currentSubTargetID;
    }

    public void GetQuestName()
    {
        questName.text = data.quests[data.currentQuestID].questName;
    }
    public void GetQuestSubName()
    {
        int currentSubTargetID = data.quests[data.currentQuestID].currentSubTargetID;
        questSubName.text = data.quests[data.currentQuestID].subTargets[currentSubTargetID].questTargetName;
    }

    public void SetFirstQuest()
    {
        GetQuestName();
        GetQuestSubName();
    }

    public void NextQuest()
    {
        if (data.currentQuestID < data.quests.Length - 1)
        {
            data.currentQuestID++;
        }

        GetQuestName();
        GetQuestSubName();
        SpawnMainTrigger();
    }

    public void NextQuestSub()
    {
        int currentSubID = data.quests[data.currentQuestID].currentSubTargetID,
            currentSubTargets = data.quests[data.currentQuestID].subTargets.Length - 1;

        if (currentSubID < currentSubTargets)
            data.quests[data.currentQuestID].currentSubTargetID++;

        GetQuestSubName();
        SpawnSubTrigger();
    }

    public void SpawnMainTrigger()
    {
        Vector3 position = data.quests[data.currentQuestID].triggerPosition;
        Quaternion rotation = data.quests[data.currentQuestID].triggerRotation;

        Instantiate(mainQuestTrigger, position, rotation);
    }

    public void SpawnSubTrigger()
    {
        int currentSubID = data.quests[data.currentQuestID].currentSubTargetID;
        Vector3 position = data.quests[data.currentQuestID].subTargets[currentSubID].triggerPosition;
        Quaternion rotation = data.quests[data.currentQuestID].subTargets[currentSubID].triggerRotation;

        Instantiate(subQuestTrigger, position, rotation);
    }

    public void ResetQuestData()
    {
        data.currentQuestID = 0;
        for (int i = 0; i < data.quests[GetQuestID()].subTargets.Length -1; i++)
        {
            data.quests[GetQuestID()].currentSubTargetID = 0;
        }
    }
}
