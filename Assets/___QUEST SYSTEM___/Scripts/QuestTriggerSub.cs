using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTriggerSub : MonoBehaviour
{
    QuestManager questManager;

    public void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        QuestData data = questManager.data;

        Destroy(this.gameObject);

        bool isQuestsOver = questManager.GetQuestID() == data.quests.Length - 1;

        if (questManager.GetQuestSubID() == data.quests[data.currentQuestID].subTargets.Length - 1)
        {
            questManager.QuestEnd();

            if (!isQuestsOver)
            {
                questManager.NextQuest();
            }

            return;
        }

        if (!isQuestsOver)
        {
            questManager.NextQuestSub();
        }
    }
}
