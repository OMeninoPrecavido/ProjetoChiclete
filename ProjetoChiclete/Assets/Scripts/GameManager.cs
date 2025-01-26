using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

//Game manager class
public class GameManager : MonoBehaviour
{
    //References to other UI elements
    [SerializeField] GameObject arrowsUi; //Center arrows UI
    [SerializeField] GameObject gumChooserUi; //Side gum options UI
    [SerializeField] TextMeshProUGUI timerUi; //Game timer UI
    [SerializeField] TextMeshProUGUI totalPointsUi; //Game points UI
    [SerializeField] TextMeshProUGUI realTimePointsUi; //Game points UI
    [SerializeField] GameObject pointerArrowsUi; //UI for indication arrows

    public static GameManager instance; //SINGLETON instance

    public GameState gameState; //Variable that defines the current state of the game

    public int Timer { get; private set; }
    private const int GAME_TIME_SECS = 70;

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
        totalPointsUi.text = Points.ToString() + " pts";
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
        pointerArrowsUi.SetActive(true);
    }

    //Configures UI for the ArrowSequence game state
    private void ArrowSequence()
    {
        arrowsUi.SetActive(true);
        gumChooserUi.SetActive(false);
        pointerArrowsUi.SetActive(false);
    }

    private void EndGame()
    {
        SceneManager.LoadScene("EndScene");
    }

    private void BubblePop()
    {
        arrowsUi.SetActive(false);
        gumChooserUi.SetActive(false);
        pointerArrowsUi.SetActive(false);
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
            case GameState.BubblePop:
                BubblePop();
                break;
        }
    }

    public void IncreasePoints(int value)
    {
        Points += value;
        totalPointsUi.text = Points.ToString() + " pts";
    }

    public void UpdateRealTimePoints(int value)
    {
        realTimePointsUi.text = value.ToString() + " pts";
    }

    IEnumerator DecreaseTimer()
    {
        while (Timer > 0)
        {
            Timer--;
            timerUi.text = Timer.ToString() + "s";
            yield return new WaitForSeconds(1);
        }
        SetGameState(GameState.EndGame);
    }
}

public enum GameState //Enumerator for game states
{
    GumSelection,
    ArrowSequence,
    EndGame,
    BubblePop
}
