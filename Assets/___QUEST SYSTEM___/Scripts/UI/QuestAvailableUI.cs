using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestAvailableUI : MonoBehaviour
{
    public static QuestAvailableUI Instance { get; private set; }

    public Text QuestsNum;

    void Awake()
    {
        Instance = this;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void UpdateQuestsNum(string questsNum)
    {
        QuestsNum.text = $"({questsNum})";
    }
}
