using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

//This script deals with the Arrows UI and its subsequent functionalities
public class ArrowsUI : MonoBehaviour
{
    GameManager gameManager; //Game manager reference
    StreakManager streakManager; //Streak manager ref
    [SerializeField] Animator anim; //Gumcat animator ref

    [SerializeField] GameObject arrowsContainer; //Reference to Arrows UI

    [SerializeField] SequenceUI sequenceUI; //Reference to the Combo Sequence UI

    //References to the arrow image prefabs
    [SerializeField] GameObject arrowUpPrefab;
    [SerializeField] GameObject arrowDownPrefab;
    [SerializeField] GameObject arrowLeftPrefab;
    [SerializeField] GameObject arrowRightPrefab;

    private List<KeyCode> currentSequence = new List<KeyCode>(); //List of Arrow KeyCodes. Represents the sequence of arrows displayed on screen
    private List<GameObject> arrowObjects = new List<GameObject>(); //List of the actual Arrow images on screen

    private Gum currentGum; //Holds the gum whose series of arrows is being displayed on screen

    private void Awake()
    {
        gameManager = GameManager.instance; //Game manager instance
        streakManager = StreakManager.instance; // Streak manager instance
    }

    private void Update()
    {
        ClearAnimParams();
        if (gameManager.gameState == GameState.ArrowSequence) //Only works in the correct game state
        {
            KeyCode? pressedKey = GetInput(); //Returns key pressed by player
            if (pressedKey != null)
            {
                if (pressedKey == currentSequence[0]) //If key was right...
                {
                    if (pressedKey == KeyCode.LeftArrow)
                    {
                        ClearAnimParams();
                        anim.SetBool("isLeft", true);
                    }
                    else if (pressedKey == KeyCode.UpArrow)
                    {
                        ClearAnimParams();
                        anim.SetBool("isUp", true);
                    }
                    else if (pressedKey == KeyCode.RightArrow)
                    {
                        ClearAnimParams();
                        anim.SetBool("isRight", true);
                    }
                    else if (pressedKey == KeyCode.DownArrow)
                    {
                        ClearAnimParams();
                        anim.SetBool("isDown", true);
                    }

                    //Removes first arrow both from KeyCode list and actual arrow image list
                    currentSequence.RemoveAt(0);
                    Destroy(arrowObjects[0]); //Destroys first arrow image
                    arrowObjects.RemoveAt(0);

                    if (currentSequence.Count <= 0) //If it was the last key...
                    {

                        Input.ResetInputAxes(); //Prevents pressed key from influencing the next game state

                        CleanUp(); //Destroys every arrow image and clears both lists

                        sequenceUI.AddToSequence(currentGum); //Adds gum to combo sequence

                        streakManager.NotifyUpdateStreak(StreakState.Success); //Notify that the right sequence was typed
                        gameManager.SetGameState(GameState.GumSelection); //Sets game state to GumSelection
                    }
                }
                else
                {
                    Input.ResetInputAxes(); //Prevents pressed key from influencing the next game state

                    CleanUp(); //Destroys every arrow image and clears both lists

                    streakManager.NotifyUpdateStreak(StreakState.Fail); //Notify that the wrong sequence was typed
                    gameManager.SetGameState(GameState.GumSelection);//Sets game state to GumSelection
                }
            }
            else
            {
                ClearAnimParams();
            }
        }
    }

    void CleanUp() //Destroys every arrow image and clears both lists
    {
        foreach (GameObject arrow in arrowObjects)
        {
            Destroy(arrow);
        }
        arrowObjects.Clear();
        currentSequence.Clear();
    }

    //Returns arrow pressed by user. Simplifies code
    private KeyCode? GetInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            return KeyCode.LeftArrow;
        if (Input.GetKeyDown(KeyCode.RightArrow))
            return KeyCode.RightArrow;
        if (Input.GetKeyDown(KeyCode.UpArrow))
            return KeyCode.UpArrow;
        if (Input.GetKeyDown(KeyCode.DownArrow))
            return KeyCode.DownArrow;

        return null;
    }

    //Used by other classes to set this system's gum and arrow sequence
    public void SetSequence(Gum gum)
    {
        currentSequence = gum.arrowSequence;
        currentGum = gum;
        DisplaySequence();
    }

    //Displays the arrow sequence on screen and populates the arrow object list (with those same arrows being displayed on screen)
    private void DisplaySequence()
    {
        foreach (KeyCode key in currentSequence)
        {
            GameObject instPrefab = arrowDownPrefab; //Default

            switch (key)
            {
                case KeyCode.UpArrow:
                    instPrefab = arrowUpPrefab;
                    break;
                case KeyCode.DownArrow:
                    instPrefab = arrowDownPrefab;
                    break;
                case KeyCode.LeftArrow:
                    instPrefab = arrowLeftPrefab;
                    break;
                case KeyCode.RightArrow:
                    instPrefab = arrowRightPrefab;
                    break;
            }

            GameObject arrow = Instantiate(instPrefab);
            arrow.transform.SetParent(arrowsContainer.transform);
            arrowObjects.Add(arrow);
        }
    }

    private void ClearAnimParams()
    {
        anim.SetBool("isUp", false);
        anim.SetBool("isDown", false);
        anim.SetBool("isLeft", false);
        anim.SetBool("isRight", false);
    }
}
