using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BreathUI : MonoBehaviour
{
    StreakManager streakManager; //Streak Manager reference
    GameManager gameManager; //Game Manager reference

    [SerializeField] Image BreathIndicator; //Visual indicator of breath. The inside of the bar

    [SerializeField] GameObject bubblePrefab;
    [SerializeField] Transform catMouth; 

    private const float MAX_BREATH_TIME = 10.0f; //Max amount of breath
    private const float STREAK_ALTERATION_TIME = 1.0f; //Amount of breath incremented or decremented. Used continuously or in mistakes/successes

    private float CurrentBreath = MAX_BREATH_TIME; //Current amount of breath
    private bool HasStreak = false; //Indicates if the player is currently chewing gum

    void Start()
    {
        streakManager = StreakManager.instance;
        gameManager = GameManager.instance;
    }

    void Update()
    {
        //Checking if we have an active streak
        if (HasStreak)
        {
            //Check if the player wants to break the streak
            if (Input.GetKeyDown(KeyCode.Space))
                StartCoroutine(BlowBubble());
                //BreakStreak(true);

            //Updating bar UI
            CurrentBreath -= Time.deltaTime;
            BreathIndicator.fillAmount = CurrentBreath / MAX_BREATH_TIME;

            //Check if we are out of breath
            if (CurrentBreath < 0.0f)
                BreakStreak(false);
        }
        else
        {
            BreathIndicator.fillAmount = 1.0f;
        }
    }

    public void BreakStreak(bool WasSuccess)
    {
        HasStreak = false;
        streakManager.NotifyStreakBreak(WasSuccess);
        gameManager.SetGameState(GameState.GumSelection);
    }

    public void UpdateStreak(StreakState state)
    {
        if (HasStreak)
        {
            //We increase out breath if the sequence was successful, otherwise, we decrease it
            switch(state)
            {
                case StreakState.Choice:
                    break;
                case StreakState.Success:
                    CurrentBreath = Mathf.Min(CurrentBreath + STREAK_ALTERATION_TIME, MAX_BREATH_TIME);
                    break;
                case StreakState.Fail:           
                    CurrentBreath -= STREAK_ALTERATION_TIME;
                    break;
            }
        }
        else
        {
            //Reseting streak
            HasStreak = true;
            CurrentBreath = MAX_BREATH_TIME;
        }
    }

    public float getAvailableBreath() {
        return CurrentBreath/MAX_BREATH_TIME;
    }

    private IEnumerator BlowBubble()
    {
        Transform bubble = Instantiate(bubblePrefab).transform;
        bubble.position = catMouth.position;

        float scaleFactor = CurrentBreath * 0.1f + 0.5f
            ;
        Vector3 scaleAdd = new Vector3(0.1f, 0.1f, 0.1f);

        Debug.Log("CurrBreath: " + CurrentBreath);
        Debug.Log("scaleFactor: " + scaleFactor);

        while (bubble.localScale.x < scaleFactor)
        {
            Debug.Log(bubble.localScale);
            bubble.localScale += scaleAdd;

            yield return null;
        }

        BreakStreak(true);
    }
}

public enum StreakState
{
    Choice,
    Success,
    Fail
}
