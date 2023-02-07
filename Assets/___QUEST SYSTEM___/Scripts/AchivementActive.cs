using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchivementActive : MonoBehaviour
{
    public int id;
 
    private void OnTriggerEnter(Collider other)
    {
        int aSize = QuestManager.Instance.achivementsData.items.Length;

        for (int i = 0; i < aSize; i++)
        {
            int aId = QuestManager.Instance.achivementsData.items[i].id;
            bool isUnlocked = QuestManager.Instance.achivementsData.items[i].isUnblocked;
            if (id == aId && isUnlocked == false)
            {
                string title = QuestManager.Instance.achivementsData.items[i].title;
                string desc = QuestManager.Instance.achivementsData.items[i].description;

                AchivementUI.Instance.Show();
                AchivementUI.Instance.UpdateTitle(title);
                AchivementUI.Instance.UpdateDescription(desc);
                QuestManager.Instance.achivementsData.items[i].isUnblocked = true;

                Destroy(gameObject);
            }
        }
    }
}
