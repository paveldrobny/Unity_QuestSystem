using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public QuestData data;

    public GameObject mainQuestTrigger;

    [Header("UI Prefab")]
    public GameObject ui_QuestInfo;
    public GameObject ui_QuestCompleted;
    public GameObject ui_QuestConfirm;
    public GameObject ui_QuestNavigate;

    [Header("UI Text")]
    public Text[] questTitleText;
    public Text questObjectiveText;
    public Text metersToObjectiveText;
    public Text HeightObjectiveText;

    [Header("Player")]
    public GameObject player;
    public GameObject playerCamera;

    private void Start()
    {
        ResetQuestData();
        SpawnStartQuestTrigger();

        ui_QuestInfo.SetActive(false);
        ui_QuestCompleted.SetActive(false);
        ui_QuestConfirm.SetActive(false);
        ui_QuestNavigate.SetActive(false);
    }

    public void Update()
    {
        if(IsQuestConfirm())
        {
            metersToObjectiveText.text = GetDistanceToObjective().ToString() + " (m)";
            GetHeightObjective();
        }
    }

    public int GetQuestID()
    {
        return data.currentQuestID;
    }

    public int GetQuestSubID()
    {
        return data.questItems[GetQuestID()].currentObjectivesID;
    }

    public string GetQuestName()
    {
        return data.questItems[data.currentQuestID].questName;
    }
    public string GetQuestSubName()
    {
        int currentSubTargetID = data.questItems[data.currentQuestID].currentObjectivesID;
        return data.questItems[data.currentQuestID].objectives[currentSubTargetID].name;
    }

    public bool IsQuestConfirm()
    {
        return data.questItems[data.currentQuestID].isQuestConfirm;
    }

    public void GetQuestInfoData()
    {
        for (int i = 0; i < questTitleText.Length; i++)
        {
            questTitleText[i].text = GetQuestName();
        }

        questObjectiveText.text = GetQuestSubName();
    }

    public void SetFirstQuest()
    {
        GetQuestInfoData();
    }

    public void NextQuest()
    {
        GetQuestInfoData();

        if (data.currentQuestID < data.questItems.Length - 1)
        {
            data.currentQuestID++;
        }

        SpawnStartQuestTrigger();
    }

    public void NextQuestSub()
    {
        int currentSubID = data.questItems[data.currentQuestID].currentObjectivesID;
        int currentobjectives = data.questItems[data.currentQuestID].objectives.Length ;

        if (currentSubID < currentobjectives)
            data.questItems[data.currentQuestID].currentObjectivesID++;

        GetQuestInfoData();
        SpawnObjectiveTrigger();
    }

    public void QuestStart()
    {
        ui_QuestConfirm.SetActive(false);
        ui_QuestInfo.SetActive(true);
        ui_QuestNavigate.SetActive(true);
    }

    public void QuestEnd(bool isDecline = false)
    {
        ui_QuestConfirm.SetActive(false);
        ui_QuestInfo.SetActive(false);
        ui_QuestNavigate.SetActive(false);

        if (!isDecline)
        {
            StartCoroutine(QuestCompleted());
        }
    }

    IEnumerator QuestCompleted()
    {
         ui_QuestCompleted.SetActive(true);

        yield return new WaitForSeconds(2);

        ui_QuestCompleted.SetActive(false);
    }

    public void AcceptQuest()
    {
        data.questItems[data.currentQuestID].isQuestConfirm = true;
        data.questItems[data.currentQuestID].currentObjectivesID = 0;
    }

    public void DeclineQuest()
    {
        QuestEnd(true);
    }

    public void SpawnStartQuestTrigger()
    {
        Vector3 position = data.questItems[data.currentQuestID].triggerPosition;
        Quaternion rotation = data.questItems[data.currentQuestID].triggerRotation;

        Instantiate(mainQuestTrigger, position, rotation);
    }

    public void SpawnObjectiveTrigger()
    {
        int objectiveID = data.questItems[data.currentQuestID].currentObjectivesID;
        Vector3 position = data.questItems[data.currentQuestID].objectives[objectiveID].triggerPosition;
        Quaternion rotation = data.questItems[data.currentQuestID].objectives[objectiveID].triggerRotation;

        GameObject spawnObjective = data.questItems[data.currentQuestID].objectives[objectiveID].spawnObjective;

        Instantiate(spawnObjective, position, rotation);
    }


    // Reset currentQuestID, currentObjectivesID and isQuestConfirm on ScriptableObject in play mode start
    public void ResetQuestData()
    {
        data.currentQuestID = 0;
        for (int i = 0; i < data.questItems[GetQuestID()].objectives.Length - 1; i++)
        {
            data.questItems[GetQuestID()].currentObjectivesID = 0;
        }

        for (int i = 0; i < data.questItems.Length; i++)
        {
            data.questItems[i].isQuestConfirm = false;
        }
    }

    public void SetCursor(bool isShow = false)
    {
        if(isShow)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            return;
        }

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public int GetDistanceToObjective()
    {
        int objectiveID = data.questItems[data.currentQuestID].currentObjectivesID;
        Vector3 objectivePosition = data.questItems[data.currentQuestID].objectives[objectiveID].triggerPosition;

        return ((int)(player.transform.position - objectivePosition).magnitude);
    }

    public void GetHeightObjective()
    {
        int objectiveID = data.questItems[data.currentQuestID].currentObjectivesID;
        Vector3 objectivePosition = data.questItems[data.currentQuestID].objectives[objectiveID].triggerPosition;

        if(objectivePosition.y >= player.transform.position.y + 2)
        {
            HeightObjectiveText.text = "Objective is higher";
        }
        else if (objectivePosition.y + 2 <= player.transform.position.y)
        {
            HeightObjectiveText.text = "Objective is below";
        }
        else
        {
            HeightObjectiveText.text = "Objective is at the same height";
        }
    }
}