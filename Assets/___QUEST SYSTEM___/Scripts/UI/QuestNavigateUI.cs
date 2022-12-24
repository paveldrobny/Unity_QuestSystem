using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestNavigateUI : MonoBehaviour
{
    public static QuestNavigateUI Instance { get; private set; }

    public Text Distance;
    public Text HeightObjective;

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

    public void UpdateDistance(string distance)
    {
        Distance.text = $"{distance} (m)";
    }

    public void UpdateHeightObjective(string heightObjective)
    {
        HeightObjective.text = heightObjective;
    }
}
