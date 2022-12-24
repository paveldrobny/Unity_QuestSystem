using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestConfirmUI : MonoBehaviour
{
    public static QuestConfirmUI Instance { get; private set; }

    public Text QuestName;

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
}
