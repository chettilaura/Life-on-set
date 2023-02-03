using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    [SerializeField] private Quest Quest;
    [SerializeField] private Player _player;

    public void AcceptQuest()
    {
        Quest.isActive = true;
        //give quest to player
        _player.addQuest(Quest);
    }

}
