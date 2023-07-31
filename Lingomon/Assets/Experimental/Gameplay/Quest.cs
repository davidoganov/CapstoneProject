using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public string task;
    bool unlocked = false;
    bool complete = false;
    int counter = 0;
    public int goal = 1;

    public bool updateTask()
    {
        if (unlocked && !complete) {
            if (++counter == goal) complete = true;
            return complete;
        }
        return false;
    }

    public void unlockTask()
    { 
        unlocked = true;
    }

    override
    public string ToString() {
        if (!unlocked || complete) return "";
        string rtn = "- " + task;
        if (goal > 1) {
            rtn += " (" + counter + "/" + goal + ")";
        }
        return rtn + "\n";
    }
}
