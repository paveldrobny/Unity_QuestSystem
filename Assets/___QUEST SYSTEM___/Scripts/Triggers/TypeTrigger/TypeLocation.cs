using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeLocation : MonoBehaviour
{
    public Canvas canvas;

    public void Update()
    {
        canvas.transform.LookAt(GameSystem.Instance.playerCamera.transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        QuestData data = QuestManager.Instance.data;

        Destroy(gameObject);

        bool isAllQuestsOver = QuestManager.Instance.GetQuestID() == data.questItems.Length - 1;

        if (QuestManager.Instance.GetQuestSubID() == data.questItems[data.currentQuestID].objectives.Length - 1)
        {
            QuestManager.Instance.QuestEnd();

            if (!isAllQuestsOver)
            {
                QuestManager.Instance.NextQuest();
            }
            else
            {
                QuestAvailableUI.Instance.UpdateQuestsNum("all completed");
            }
        }
        else
        {
            QuestManager.Instance.NextQuestSub();
        }
    }
}

