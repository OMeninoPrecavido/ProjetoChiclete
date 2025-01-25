using System;
using UnityEngine;
using UnityEngine.UI;

public class BreathUI : MonoBehaviour
{
    [SerializeField] Image BreathIndicator;
    private const float MAX_BREATH_TIME = 10.0f;
    private const float STREAK_ALTERATION_TIME = 1.0f;
    private float CurrentBreath = MAX_BREATH_TIME;
    private bool HasStreak = false;
    void Start()
    {

    }

    void Update()
    {
        if (HasStreak)
        {
            CurrentBreath -= Time.deltaTime;
            BreathIndicator.fillAmount = CurrentBreath / MAX_BREATH_TIME;

            if (CurrentBreath < 0.0f)
                BreakStreak();
        }
        else
        {
            BreathIndicator.fillAmount = 0f;
        }
    }

    public void BreakStreak()
    {
        HasStreak = false;
    }

    public void UpdateStreak(bool WasSuccess)
    {
        if (HasStreak)
        {
            if (WasSuccess)
                CurrentBreath = Mathf.Min(CurrentBreath + STREAK_ALTERATION_TIME, MAX_BREATH_TIME);
            else
                CurrentBreath -= STREAK_ALTERATION_TIME;
        }
        else
        {
            HasStreak = true;
            CurrentBreath = MAX_BREATH_TIME;
        }
    }
}
