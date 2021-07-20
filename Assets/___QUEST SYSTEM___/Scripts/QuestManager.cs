using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public QuestData data;

    public GameObject mainQuestTrigger;
    public GameObject subQuestTrigger;

    public GameObject ui_QuestInfo;
    public GameObject ui_ConfirmQuest;
    public GameObject ui_CompletedQuest;

    public Text[] questMainText;
    public Text questSubText;


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

    public string GetQuestName()
    {
        return data.quests[data.currentQuestID].questName;
    }
    public string GetQuestSubName()
    {
        int currentSubTargetID = data.quests[data.currentQuestID].currentSubTargetID;
        return data.quests[data.currentQuestID].subTargets[currentSubTargetID].questTargetName;
    }

    public bool IsQuestConfirm()
    {
        return data.quests[data.currentQuestID].isQuestConfirm;
    }

    public void GetQuestInfoData()
    {
        for(int i = 0; i < questMainText.Length; i++)
        {
            questMainText[i].text = GetQuestName();
        }

        questSubText.text = GetQuestSubName();
    }

    public void SetFirstQuest()
    {
        GetQuestInfoData();
    }

    public void NextQuest()
    {
        if (data.currentQuestID < data.quests.Length - 1)
        {
            data.currentQuestID++;
        }

        GetQuestInfoData();
        SpawnMainTrigger();
    }

    public void NextQuestSub()
    {
        int currentSubID = data.quests[data.currentQuestID].currentSubTargetID,
            currentSubTargets = data.quests[data.currentQuestID].subTargets.Length - 1;

        if (currentSubID < currentSubTargets)
            data.quests[data.currentQuestID].currentSubTargetID++;

        GetQuestInfoData();
        SpawnSubTrigger();
    }

    public void QuestStart()
    {
        ui_ConfirmQuest.SetActive(false);
        ui_QuestInfo.SetActive(true);
    }

    public void QuestEnd()
    {
        ui_ConfirmQuest.SetActive(false);
        ui_QuestInfo.SetActive(false);
        StartCoroutine(QuestCompleted());
    }

    IEnumerator QuestCompleted()
    {
        ui_CompletedQuest.SetActive(true);
        yield return new WaitForSeconds(3);
        ui_CompletedQuest.SetActive(false);
    }

    public void AcceptQuest()
    {
        data.quests[data.currentQuestID].isQuestConfirm = true;
        QuestStart();
    }

    public void DeclineQuest()
    {
        QuestEnd();
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


    // Reset currentQuestID and currentSubTargetID on ScriptableObject in play mode start
    public void ResetQuestData()
    {
        data.currentQuestID = 0;
        for (int i = 0; i < data.quests[GetQuestID()].subTargets.Length - 1; i++)
        {
            data.quests[GetQuestID()].currentSubTargetID = 0;
        }

        for (int i = 0; i < data.quests.Length - 1; i++)
        {
            data.quests[GetQuestID()].isQuestConfirm = false;
        }
    }
}
