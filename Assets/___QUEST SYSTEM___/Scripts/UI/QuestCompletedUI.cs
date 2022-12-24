using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestCompletedUI : MonoBehaviour
{
    public static QuestCompletedUI Instance { get; private set; }

    public Text QuestName;
    public Text QuestXP;

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
    public void UpdateQuestXP(int questXP)
    {
        QuestXP.text = $"+{questXP} XP";
    }
}
