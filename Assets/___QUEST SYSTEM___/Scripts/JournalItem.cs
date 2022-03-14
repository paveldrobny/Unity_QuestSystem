using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalItem : MonoBehaviour
{
    public Text title;
    public GameObject background;

    public void SetCompleted()
    {
        background.GetComponent<Image>().color = new Color(0.03f, 0.7f, 0.0f, 1.0f);
    }
}
