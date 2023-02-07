using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }

    private const float QUEST_COMPLETED_MESSAGE_SECONDS = 1.5f;

    public QuestData data;
    public GameObject mainQuestTrigger;

    private int m_playerLvl = 1;
    private float m_playerXP = 0;

    [Header("Experience")]
    public float maxLvlXP = 750;

    [Range(1.1f, 5.0f)]
    public float XPModificator = 1.1f;

    public AchivementsData achivementsData;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ResetQuestData();
        SpawnStartQuestTrigger();
        HideAllQuestUI();
        GetQuestsAvailable();
        UpdatePlayerData();

        // Reset achivements status
        for (int i = 0; i < achivementsData.items.Length; i++)
        {
            achivementsData.items[i].isUnblocked = false;
        }
    }

    public void HideAllQuestUI()
    {
        QuestInfoUI.Instance.Hide();
        QuestCompletedUI.Instance.Hide();
        QuestConfirmUI.Instance.Hide();
        QuestNavigateUI.Instance.Hide();
    }

    public void Update()
    {
        if (IsQuestConfirm())
        {
            GetHeightObjective();
            QuestNavigateUI.Instance.UpdateDistance(GetDistanceToObjective().ToString());
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
        return data.questItems[GetQuestID()].questName;
    }
    public string GetQuestSubName()
    {
        int currentSubTargetID = data.questItems[GetQuestID()].currentObjectivesID;
        return data.questItems[GetQuestID()].objectives[currentSubTargetID].name;
    }

    public bool IsQuestConfirm()
    {
        return data.questItems[GetQuestID()].isQuestConfirm;
    }

    public void GetQuestInfoData()
    {
        QuestConfirmUI.Instance.UpdateQuestName(GetQuestName());
        QuestInfoUI.Instance.UpdateQuestName(GetQuestName());
        QuestInfoUI.Instance.UpdateQuestObjectives(GetQuestSubName());
    }

    public void SetFirstQuest()
    {
        GetQuestInfoData();
    }

    public void NextQuest()
    {
        GetQuestInfoData();
        data.currentQuestID++;
        GetQuestsAvailable();
        SpawnStartQuestTrigger();
    }

    public void NextQuestSub()
    {
        int currentobjectives = data.questItems[GetQuestID()].objectives.Length;
        if (GetQuestSubID() < currentobjectives)
            data.questItems[GetQuestID()].currentObjectivesID++;

        GetQuestInfoData();
        SpawnObjectiveTrigger();
    }

    public void QuestStart()
    {
        QuestConfirmUI.Instance.Hide();
        QuestInfoUI.Instance.Show();
        QuestNavigateUI.Instance.Show();
    }

    public void GetQuestsAvailable()
    {
        int allQuestsNum = data.questItems.Length;
        int completedQuestsNum = data.currentQuestID;

        QuestAvailableUI.Instance.UpdateQuestsNum($"{allQuestsNum - completedQuestsNum}");
    }

    public void UpdatePlayerData()
    {
        if (XPModificator > 1.0f)
        {
            while (m_playerXP >= maxLvlXP)
            {
                m_playerLvl++;
                m_playerXP -= maxLvlXP;
                maxLvlXP *= XPModificator;
                QuestLvlUI.Instance.ShowLvlUpPanel(true);
                UpdateLvlData();
            }
        }
        else
        {
            Debug.LogError("Setting a value less than 1.0 for the 'XPModificator' will result in an infinite loop during the completion of the quest");
        }
        UpdateLvlData();
    }

    public void UpdateLvlData()
    {
        float _playerXP = Math.Abs((float)Math.Round(m_playerXP, 1));
        float _maxLvlXP = (float)Math.Round(maxLvlXP, 1);
        QuestLvlUI.Instance.UpdateLvl(m_playerLvl.ToString());
        QuestLvlUI.Instance.UpdateXP($"{_playerXP} / {_maxLvlXP}", Math.Abs(_playerXP) / _maxLvlXP);
    }

    public void QuestEnd(bool isDecline = false)
    {
        QuestConfirmUI.Instance.Hide();
        QuestInfoUI.Instance.Hide();
        QuestNavigateUI.Instance.Hide();

        if (!isDecline)
        {
            GetQuestsAvailable();
            m_playerXP += data.questItems[GetQuestID()].xp;
            UpdatePlayerData();
            StartCoroutine(QuestCompleted());
        }
    }

    IEnumerator QuestCompleted()
    {
        QuestCompletedUI.Instance.Show();
        QuestCompletedUI.Instance.UpdateQuestName(GetQuestName());
        QuestCompletedUI.Instance.UpdateQuestXP(data.questItems[GetQuestID()].xp);

        yield return new WaitForSeconds(QUEST_COMPLETED_MESSAGE_SECONDS);

        QuestCompletedUI.Instance.UpdateQuestName("");
        QuestCompletedUI.Instance.Hide();
        QuestLvlUI.Instance.ShowLvlUpPanel(false);
    }

    public void DeclineQuest()
    {
        QuestEnd(true);
    }

    public void SpawnStartQuestTrigger()
    {
        Vector3 position = data.questItems[GetQuestID()].triggerPosition;
        Quaternion rotation = data.questItems[GetQuestID()].triggerRotation;

        Instantiate(mainQuestTrigger, position, rotation);
    }

    public void SpawnObjectiveTrigger()
    {
        Vector3 position = data.questItems[GetQuestID()].objectives[GetQuestSubID()].triggerPosition;
        Quaternion rotation = data.questItems[GetQuestID()].objectives[GetQuestSubID()].triggerRotation;
        Vector3 scale = data.questItems[GetQuestID()].objectives[GetQuestSubID()].triggerScale;
        GameObject spawnObjective = data.questItems[GetQuestID()].objectives[GetQuestSubID()].spawnObjective;

        spawnObjective.transform.localScale = scale;

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
        if (isShow)
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
        Vector3 objectivePosition = data.questItems[GetQuestID()].objectives[GetQuestSubID()].triggerPosition;
        return ((int)(GameSystem.Instance.player.transform.position - objectivePosition).magnitude);
    }

    public void GetHeightObjective()
    {
        Vector3 objectivePosition = data.questItems[GetQuestID()].objectives[GetQuestSubID()].triggerPosition;

        if (objectivePosition.y >= GameSystem.Instance.player.transform.position.y + 2)
        {
            QuestNavigateUI.Instance.UpdateHeightObjective("Up");
        }
        else if (objectivePosition.y + 2 <= GameSystem.Instance.player.transform.position.y)
        {
            QuestNavigateUI.Instance.UpdateHeightObjective("Down");
        }
        else
        {
            QuestNavigateUI.Instance.UpdateHeightObjective("Same level");
        }
    }
}