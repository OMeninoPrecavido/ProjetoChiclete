using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StreakManager : MonoBehaviour
{
    [SerializeField] BreathUI breathUI;
    [SerializeField] SequenceUI sequenceUI;
    [SerializeField] GumChooserUI gumChooserUI;

    GameManager gameManager;
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
            gameManager = GameManager.instance;
        }
    }

    public void NotifyUpdateStreak(StreakState state)
    {
        
        breathUI.UpdateStreak(state);

    }
    
    public void NotifyStreakBreak(bool WasSuccess)
    {
        gumChooserUI.ReloadGumChoices();
        List<Gum> finalSequence = sequenceUI.BreakSequence();
        if (WasSuccess)
        {
            int streakScore = Score.Calculate(finalSequence);
            gameManager.IncreasePoints(streakScore);
        }
    }
}



