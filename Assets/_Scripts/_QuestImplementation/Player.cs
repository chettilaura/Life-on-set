using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private List<Quest> _quests;

    public void addQuest(Quest quest)
    {
        _quests.Add(quest);
    }
}
