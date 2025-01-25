using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BreathUI : MonoBehaviour
{
    StreakManager streakManager;
    [SerializeField] Image BreathIndicator;
    private const float MAX_BREATH_TIME = 10.0f;
    private const float STREAK_ALTERATION_TIME = 1.0f;
    private float CurrentBreath = MAX_BREATH_TIME;
    private bool HasStreak = false;
    void Start()
    {
        streakManager = StreakManager.instance;
    }

    void Update()
    {
        //Checking if we have an active streak
        if (HasStreak)
        {
            //Check if the player wants to break the streak
            if(Input.GetKeyDown(KeyCode.Space))
                BreakStreak(true);

            //Updating bar UI
            CurrentBreath -= Time.deltaTime;
            BreathIndicator.fillAmount = CurrentBreath / MAX_BREATH_TIME;

            //Check if we are out of breath
            if (CurrentBreath < 0.0f)
                BreakStreak(false);
        }
        else
        {
            BreathIndicator.fillAmount = 0f;
        }
    }

    public void BreakStreak(bool WasSuccess)
    {
        HasStreak = false;
        streakManager.NotifyStreakBreak(WasSuccess);
    }

    public void UpdateStreak(bool WasSuccess)
    {
        if (HasStreak)
        {   
            //We increase out breath if the sequence was successful, otherwise, we decrease it
            if (WasSuccess)
                CurrentBreath = Mathf.Min(CurrentBreath + STREAK_ALTERATION_TIME, MAX_BREATH_TIME);
            else
                CurrentBreath -= STREAK_ALTERATION_TIME;
        }
        else
        {
            //Reseting streak
            HasStreak = true;
            CurrentBreath = MAX_BREATH_TIME;
        }
    }
}
