using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialog
{
    public List<string> lines;
    public bool isTrainer;
    
    public List<string> Lines {
        get { return lines; }
    }

    public bool IsTrainer {
        get { return isTrainer;  }
    }
}