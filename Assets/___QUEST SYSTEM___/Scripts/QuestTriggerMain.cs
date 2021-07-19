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
        questManager.SpawnSubTrigger();
        questManager.GetQuestName();
        questManager.GetQuestSubName();
        Destroy(this.gameObject);
    }
}
