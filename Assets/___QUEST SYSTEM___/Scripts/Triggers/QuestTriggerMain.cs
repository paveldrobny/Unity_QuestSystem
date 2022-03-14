using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTriggerMain : MonoBehaviour
{
    QuestManager questManager;

    public void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        questManager.GetQuestInfoData();
        questManager.ui_ConfirmQuest.SetActive(true);
    }

    private void OnTriggerStay(Collider other)
    {
        if (questManager.IsQuestConfirm())
        {
            questManager.SpawnSubTrigger();
            questManager.GetQuestInfoData();
            questManager.QuestStart();
            Destroy(this.gameObject);
        }
    }
}
