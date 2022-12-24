using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestLvlUI : MonoBehaviour
{
    public static QuestLvlUI Instance { get; private set; }

    public Text Lvl;
    public Text CurrentXP;
    public Slider slider;
    public GameObject lvlUpPanel;

    void Awake()
    {
        Instance = this;
        lvlUpPanel.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void ShowLvlUpPanel(bool isShow = false)
    {
        lvlUpPanel.SetActive(isShow);
    }

    public void UpdateLvl(string lvl)
    {
        Lvl.text = lvl;
    }

    public void UpdateXP(string currentXP, float xp)
    {
        CurrentXP.text = currentXP;
        slider.value = xp;
    }
}
