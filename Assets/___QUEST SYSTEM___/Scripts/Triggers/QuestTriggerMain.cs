using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTriggerMain : MonoBehaviour
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

    private void OnTriggerStay(Collider other)
    {
        questManager.ui_QuestConfirm.SetActive(true);
        questManager.SetCursor(true);
        questManager.GetQuestInfoData();

        if (questManager.IsQuestConfirm())
        {
            questManager.SetCursor();
            questManager.GetQuestInfoData();
            questManager.QuestStart();
            questManager.SpawnObjectiveTrigger();
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        questManager.ui_QuestConfirm.SetActive(false);
        questManager.SetCursor(false);
    }
}
