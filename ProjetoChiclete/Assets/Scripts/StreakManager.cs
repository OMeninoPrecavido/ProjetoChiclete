using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StreakManager : MonoBehaviour
{
    [SerializeField] BreathUI breathUI;
    [SerializeField] SequenceUI sequenceUI;
    
    public static StreakManager instance; //SINGLETON instance

    private void Awake()
    {
        //SINGLETON pattern initialization
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    
    public void NotifyEndOfSequence(bool WasSuccess)
    {
        breathUI.UpdateStreak(WasSuccess);
    }

    public void NotifyStreakBreak(bool WasSuccess) {
        List<Gum> finalSequence = sequenceUI.BreakSequence();
        if(WasSuccess) 
        {
            Debug.Log("SCORE!!!");
        }
    }
}
