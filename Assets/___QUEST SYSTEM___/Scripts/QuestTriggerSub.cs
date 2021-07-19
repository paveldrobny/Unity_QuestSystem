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

        if (questManager.GetQuestSubID() == data.quests[data.currentQuestID].subTargets.Length - 1)
        {
            questManager.NextQuest();
            return;
        }
        questManager.NextQuestSub();
    }

}
