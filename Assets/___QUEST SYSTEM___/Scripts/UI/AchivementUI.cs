using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchivementUI : MonoBehaviour
{
    public static AchivementUI Instance { get; private set; }

    public Text achivementTitle;
    public Text achivementDesc;

    void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        StartCoroutine(ShowUI());
    }

    IEnumerator ShowUI()
    {
        yield return new WaitForSeconds(3);

        Hide();
        UpdateTitle("");
        UpdateDescription("");
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void UpdateTitle(string aTitle)
    {
        achivementTitle.text = aTitle;
    }

    public void UpdateDescription(string aDesc)
    {
        achivementDesc.text = aDesc;
    }
}
