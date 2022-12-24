using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestInfoUI : MonoBehaviour
{
    public static QuestInfoUI Instance { get; private set; }

    public Text QuestName;
    public Text QuestObjectives;

    void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void UpdateQuestName(string questName)
    {
        QuestName.text = questName;
    }

    public void UpdateQuestObjectives(string questObjectives)
    {
        QuestObjectives.text = questObjectives;
    }
}
