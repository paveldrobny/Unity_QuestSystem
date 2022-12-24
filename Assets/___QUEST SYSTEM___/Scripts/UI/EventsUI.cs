using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsUI : MonoBehaviour
{

    public void AcceptQuest()
    {
        QuestManager.Instance.data.questItems[QuestManager.Instance.GetQuestID()].isQuestConfirm = true;
        QuestManager.Instance.data.questItems[QuestManager.Instance.GetQuestID()].currentObjectivesID = 0;
    }
}
