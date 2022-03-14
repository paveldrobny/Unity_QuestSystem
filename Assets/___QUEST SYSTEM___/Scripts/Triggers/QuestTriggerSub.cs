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

        bool isAllQuestsOver = questManager.GetQuestID() == data.questItems.Length - 1;

        if (questManager.GetQuestSubID() == data.questItems[data.currentQuestID].subTargets.Length - 1)
        {
            questManager.QuestEnd();

            if (!isAllQuestsOver)
            {
                questManager.NextQuest();
            }

            return;
        }

       // if (!isAllQuestsOver)
       // {
            questManager.NextQuestSub();
       // }
    }
}
