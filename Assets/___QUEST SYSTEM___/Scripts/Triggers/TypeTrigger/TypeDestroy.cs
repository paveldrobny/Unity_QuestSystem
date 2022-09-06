using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeDestroy : MonoBehaviour
{
    QuestManager questManager;
    public float health = 100.0f;
    public Canvas canvas;
    public Slider slider = null;

    public void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
        GetSliderValue();
    }

    public void Update()
    {
        canvas.transform.LookAt(questManager.playerCamera.transform);
    }

    public void GetSliderValue()
    {
        slider.value = health / 100.0f;
    }

    public void Damage(float value)
    {
        health -= value;
        GetSliderValue();
    }

    private void OnTriggerEnter(Collider other)
    {
        QuestData data = questManager.data;

        Damage(55.0f);

        if(health <= 0)
        {
            Destroy(gameObject);

            bool isAllQuestsOver = questManager.GetQuestID() == data.questItems.Length - 1;

            if (questManager.GetQuestSubID() == data.questItems[data.currentQuestID].objectives.Length - 1)
            {
                questManager.QuestEnd();

                if (!isAllQuestsOver)
                {
                    questManager.NextQuest();
                }

                return;
            }

            questManager.NextQuestSub();
        }
    }
}
