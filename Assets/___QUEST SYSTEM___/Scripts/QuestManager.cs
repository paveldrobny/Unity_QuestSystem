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
    public GameObject ui_QuestsJournal;
    public GameObject ui_JournalItem;

    public Text[] questMainText;
    public Text questSubText;

    private void Start()
    {
        ResetQuestData();
        SpawnMainTrigger();
        AddToJournal();
    }

    private void Update()
    {
    }

    public int GetQuestID()
    {
        return data.currentQuestID;
    }

    public int GetQuestSubID()
    {
        return data.questItems[GetQuestID()].currentSubTargetID;
    }

    public string GetQuestName()
    {
        return data.questItems[data.currentQuestID].questName;
    }
    public string GetQuestSubName()
    {
        int currentSubTargetID = data.questItems[data.currentQuestID].currentSubTargetID;
        Debug.Log(data.questItems[data.currentQuestID].subTargets[currentSubTargetID].targetName);
        return data.questItems[data.currentQuestID].subTargets[currentSubTargetID].targetName;
    }

    public bool IsQuestConfirm()
    {
        return data.questItems[data.currentQuestID].isQuestConfirm;
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
        GetQuestInfoData();
        SpawnMainTrigger();

        if (data.currentQuestID < data.questItems.Length - 1)
        {
            data.currentQuestID++;

            AddToJournal();
        }
    }

    public void NextQuestSub()
    {
        int currentSubID = data.questItems[data.currentQuestID].currentSubTargetID;
        int currentSubTargets = data.questItems[data.currentQuestID].subTargets.Length ;

        if (currentSubID < currentSubTargets)
            data.questItems[data.currentQuestID].currentSubTargetID++;

        GetQuestInfoData();
        SpawnSubTrigger();
    }

    public void QuestStart()
    {
        ui_ConfirmQuest.SetActive(false);
        ui_QuestInfo.SetActive(true);
    }

    public void QuestEnd(bool isDecline = false)
    {
        ui_ConfirmQuest.SetActive(false);
        ui_QuestInfo.SetActive(false);

        if (!isDecline)
        {
            StartCoroutine(QuestCompleted());
        }

        // Set complete previous quests
        GameObject journalContent = GameObject.Find("JournalContent");

        for (int i = 0; i < journalContent.transform.childCount; i++)
        {
            journalContent.transform.GetChild(i).GetComponent<JournalItem>().SetCompleted();
        }
    }

    IEnumerator QuestCompleted()
    {
        ui_CompletedQuest.SetActive(true);

        yield return new WaitForSeconds(2);

        ui_CompletedQuest.SetActive(false);
    }

    public void AcceptQuest()
    {
        data.questItems[data.currentQuestID].isQuestConfirm = true;
        data.questItems[data.currentQuestID].currentSubTargetID = 0;
        QuestStart();
    }

    public void DeclineQuest()
    {
        QuestEnd(true);
    }

    public void SpawnMainTrigger()
    {
        Vector3 position = data.questItems[data.currentQuestID].triggerPosition;
        Quaternion rotation = data.questItems[data.currentQuestID].triggerRotation;

        Instantiate(mainQuestTrigger, position, rotation);
    }

    public void SpawnSubTrigger()
    {
        int currentSubID = data.questItems[data.currentQuestID].currentSubTargetID;
        Vector3 position = data.questItems[data.currentQuestID].subTargets[currentSubID].triggerPosition;
        Quaternion rotation = data.questItems[data.currentQuestID].subTargets[currentSubID].triggerRotation;

        Instantiate(subQuestTrigger, position, rotation);
    }


    // Reset currentQuestID and currentSubTargetID on ScriptableObject in play mode start
    public void ResetQuestData()
    {
        data.currentQuestID = 0;
        for (int i = 0; i < data.questItems[GetQuestID()].subTargets.Length - 1; i++)
        {
            data.questItems[GetQuestID()].currentSubTargetID = 0;
        }

        for (int i = 0; i < data.questItems.Length; i++)
        {
            data.questItems[i].isQuestConfirm = false;
        }
    }

    void AddToJournal()
    {
        GameObject obj = Instantiate(ui_JournalItem, new Vector3(0, 0, 0), Quaternion.identity);

        obj.transform.localScale = new Vector3(1, 1, 1);
        obj.transform.SetParent(GameObject.Find("JournalContent").transform, false);
        obj.GetComponent<JournalItem>().title.text = GetQuestName();
    }
}
