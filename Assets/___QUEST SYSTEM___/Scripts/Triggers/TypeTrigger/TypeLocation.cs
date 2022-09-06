using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeLocation : MonoBehaviour
{
    QuestManager questManager;
    public Canvas canvas;

    public void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
    }

    public void Update()
    {
        canvas.transform.LookAt(questManager.playerCamera.transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        QuestData data = questManager.data;

        Destroy(gameObject);

        bool isAllQuestsOver = questManager.GetQuestID() == data.questItems.Length - 1;

        if (questManager.GetQuestSubID() == data.questItems[data.currentQuestID].objectives.Length - 1)
        {
            questManager.QuestEnd();

            if (!isAllQuestsOver)
            {
                questManager.NextQuest();
            }

        }
        else
        {
            questManager.NextQuestSub();
        }
    }
}

