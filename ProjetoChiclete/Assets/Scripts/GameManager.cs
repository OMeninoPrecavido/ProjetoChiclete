using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

//Game manager class
public class GameManager : MonoBehaviour
{
    //References to other UI elements
    [SerializeField] GameObject arrowsUi; //Center arrows UI
    [SerializeField] GameObject gumChooserUi; //Side gum options UI
    [SerializeField] TextMeshProUGUI timerUi; //Game timer UI
    [SerializeField] TextMeshProUGUI pointsUi; //Game points UI

    public static GameManager instance; //SINGLETON instance

    public GameState gameState; //Variable that defines the current state of the game

    public int Timer { get; private set; }
    private const int GAME_TIME_SECS = 90;

    public int Points { get; private set; }

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
            SetGameState(GameState.GumSelection);
        }

        timerUi.text = GAME_TIME_SECS.ToString();
        Timer = GAME_TIME_SECS;

        Points = 0;
        pointsUi.text = Points.ToString();
    }

    private void Start()
    {
        StartCoroutine(DecreaseTimer());
    }

    //Configures UI for the GumSelection game state
    private void GumSelection()
    {
        arrowsUi.SetActive(false);
        gumChooserUi.SetActive(true);
    }

    //Configures UI for the ArrowSequence game state
    private void ArrowSequence()
    {
        arrowsUi.SetActive(true);
        gumChooserUi.SetActive(false);
    }

    private void EndGame()
    {
        arrowsUi.SetActive(false);
        gumChooserUi.SetActive(false);
    }

    //Method used by other scripts to change the game state
    public void SetGameState(GameState state)
    {
        gameState = state;
        switch (state)
        {
            case GameState.GumSelection:
                GumSelection();
                break;
            case GameState.ArrowSequence:
                ArrowSequence();
                break;
            case GameState.EndGame:
                EndGame();
                break;
        }   
    }

    public void IncreasePoints(int value)
    {
        Points += value;
        pointsUi.text = Points.ToString();
    }

    IEnumerator DecreaseTimer()
    {
        while (Timer > 0)
        {
            Timer--;
            timerUi.text = Timer.ToString();
            yield return new WaitForSeconds(1);
        }
        SetGameState(GameState.EndGame);
    }
}

public enum GameState //Enumerator for game states
{
    GumSelection,
    ArrowSequence,
    EndGame
}
