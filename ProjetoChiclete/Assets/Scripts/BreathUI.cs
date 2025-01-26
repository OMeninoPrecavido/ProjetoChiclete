using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BreathUI : MonoBehaviour
{
    StreakManager streakManager; //Streak Manager reference
    GameManager gameManager; //Game Manager reference

    [SerializeField] Image BreathIndicator; //Visual indicator of breath. The inside of the bar

    [SerializeField] GameObject roundBubblePrefab;
    [SerializeField] GameObject squareBubblePrefab;
    [SerializeField] GameObject pyramidBubblePrefab;
    [SerializeField] GameObject poodleBubblePrefab;

    [SerializeField] GameObject popPrefab;

    [SerializeField] Transform catMouth;
    [SerializeField] SequenceUI sequenceUI;

    [SerializeField] Animator anim;
    [SerializeField] AudioClip popClip;
    [SerializeField] AudioClip chewingClip;

    private AudioSource audioSource;

    private const float MAX_BREATH_TIME = 10.0f; //Max amount of breath
    private const float STREAK_ALTERATION_TIME = 1.0f; //Amount of breath incremented or decremented. Used continuously or in mistakes/successes

    private float CurrentBreath = MAX_BREATH_TIME; //Current amount of breath
    private bool HasStreak = false; //Indicates if the player is currently chewing gum

    void Start()
    {
        streakManager = StreakManager.instance;
        gameManager = GameManager.instance;
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = chewingClip;
        audioSource.loop = true;
        audioSource.Play();
    }

    void Update()
    {
        //Checking if we have an active streak
        if (HasStreak)
        {
            //Check if the player wants to break the streak
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (sequenceUI.GetCurrentSequence().Count > 0)
                    StartCoroutine(BlowBubble());
                else
                    BreakStreak(true);
            }

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
            switch (state)
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

    public float getAvailableBreath()
    {
        return CurrentBreath / MAX_BREATH_TIME;
    }

    private IEnumerator BlowBubble()
    {
        audioSource.Stop();
        anim.SetBool("isBlow", true);

        float points = Score.Calculate(sequenceUI.GetCurrentSequence(), CurrentBreath);

        GameObject bubbleInstance = null;
        
        if (points < 270)
        {
            bubbleInstance = Instantiate(roundBubblePrefab);
        }
        else
        {
            int r = UnityEngine.Random.Range(0, 4);

            switch (r)
            {
                case 0:
                    bubbleInstance = Instantiate(roundBubblePrefab);
                    break;
                case 1:
                    bubbleInstance = Instantiate(squareBubblePrefab);
                    break;
                case 2:
                    bubbleInstance = Instantiate(pyramidBubblePrefab);
                    break;
                case 3:
                    bubbleInstance = Instantiate(poodleBubblePrefab);
                    break;
            }
        }
        List<Gum> currentSequence = sequenceUI.GetCurrentSequence();
        bubbleInstance.GetComponent<ColorChanger>().ChangeBubbleColor(currentSequence);
        Transform bubble = bubbleInstance.transform;
        bubble.position = catMouth.position;

        float scaleFactor = CurrentBreath * 0.03f + currentSequence.Count * 0.2f;
        float scaleAdd = 0.005f;
        float breathStepAmount = CurrentBreath / ((scaleFactor - bubble.localScale.x) / scaleAdd);
        float localCurrentBreath = CurrentBreath;
        // This can't be moved anywhere because it breaks the timing
        BreakStreak(true);

        gameManager.SetGameState(GameState.BubblePop);
        while (bubble.localScale.x < scaleFactor)
        {
            bubble.localScale += new Vector3(1f, 1f, 1f) * scaleAdd;
            localCurrentBreath -= breathStepAmount;
            BreathIndicator.fillAmount = localCurrentBreath / MAX_BREATH_TIME;

            yield return null;
        }

        yield return new WaitForSeconds(0.3f);
        anim.SetBool("isBlow", false);

        Destroy(bubbleInstance);

        GameObject pop = Instantiate(popPrefab);
        pop.transform.position = catMouth.position;
        pop.transform.localScale += new Vector3(1f, 1f, 1f) * scaleFactor;
        pop.GetComponent<ColorChanger>().ChangeBubbleColor(currentSequence);

        audioSource.clip = popClip;
        audioSource.loop = false;
        audioSource.time = 0f;
        audioSource.Play(); 
        Destroy(pop, 0.1f);
        yield return new WaitForSeconds(audioSource.clip.length);

        audioSource.clip = chewingClip;
        audioSource.loop = true;
        audioSource.Play();

        gameManager.SetGameState(GameState.GumSelection);
    }
}

public enum StreakState
{
    Choice,
    Success,
    Fail
}
