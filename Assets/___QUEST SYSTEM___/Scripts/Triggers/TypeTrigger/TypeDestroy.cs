using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeDestroy : MonoBehaviour
{
    public float health = 100.0f;
    public Canvas canvas;
    public Slider slider = null;

    public void Start()
    {
        GetSliderValue();
    }

    public void Update()
    {
        canvas.transform.LookAt(GameSystem.Instance.playerCamera.transform);
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
        QuestData data = QuestManager.Instance.data;

        Damage(55.0f);

        if(health <= 0)
        {
            Destroy(gameObject);

            bool isAllQuestsOver = QuestManager.Instance.GetQuestID() == data.questItems.Length - 1;

            if (QuestManager.Instance.GetQuestSubID() == data.questItems[data.currentQuestID].objectives.Length - 1)
            {
                QuestManager.Instance.QuestEnd();

                if (!isAllQuestsOver)
                {
                    QuestManager.Instance.NextQuest();
                }

                return;
            }

            QuestManager.Instance.NextQuestSub();
        }
    }
}
