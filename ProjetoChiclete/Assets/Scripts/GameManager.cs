using Unity.VisualScripting;
using UnityEngine;

//Game manager class
public class GameManager : MonoBehaviour
{
    //References to other UI panels
    [SerializeField] GameObject arrowsUi; //Center arrows UI
    [SerializeField] GameObject gumChooserUi; //Side gum options UI

    public static GameManager instance; //SINGLETON instance

    public GameState gameState; //Variable that defines the current state of the game

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
        }
    }
}

public enum GameState //Enumerator for game states
{
    GumSelection,
    ArrowSequence
}
