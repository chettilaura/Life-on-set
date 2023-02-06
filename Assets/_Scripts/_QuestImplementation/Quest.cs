using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public enum QuestProgress { AVAILABLE, ACCEPTED, COMPLETE, DONE};

    public string title;
    public int id;
    public QuestProgress progress;
    public string description;

    public string questObjective;   //name of the quest objective
    public int questObjectiveCount; //current number of quest objects
    public int questObjectiveRequirement; //required amount of quest objects
}
