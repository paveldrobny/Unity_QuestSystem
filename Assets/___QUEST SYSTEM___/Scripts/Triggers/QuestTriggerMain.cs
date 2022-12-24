using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTriggerMain : MonoBehaviour
{
    public Canvas canvas;

    public void Update()
    {
        canvas.transform.LookAt(GameSystem.Instance.playerCamera.transform);
    }

    private void OnTriggerStay(Collider other)
    {
        QuestConfirmUI.Instance.Show();
        QuestManager.Instance.SetCursor(true);
        QuestManager.Instance.GetQuestInfoData();

        if (QuestManager.Instance.IsQuestConfirm())
        {
            QuestManager.Instance.SetCursor();
            QuestManager.Instance.GetQuestInfoData();
            QuestManager.Instance.QuestStart();
            QuestManager.Instance.SpawnObjectiveTrigger();
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        QuestConfirmUI.Instance.Hide();
        QuestManager.Instance.SetCursor(false);
    }
}
