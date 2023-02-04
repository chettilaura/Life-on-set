using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal 
{
    public int countNeeded;
    public int countCurrent;
    public bool completed;
    public Quest Quest;

    public void SetGoal(int needed)
    {
        this.countNeeded = needed;
    }

    public void Increment() => countCurrent++;  

    public bool IsFinished()
    {
        bool finished;
        if(countCurrent == countNeeded)
        {
            finished = true;    
        } else
        {
            finished=false;
        }
        return finished;
    }
}
