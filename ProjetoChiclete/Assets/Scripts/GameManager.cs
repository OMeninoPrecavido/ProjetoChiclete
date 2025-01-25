using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //UI REFS
    [SerializeField] GameObject arrowsUi;
    [SerializeField] GameObject gumChooserUi;

    public static GameManager instance;

    public GameState gameState;

    private void Awake()
    {
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

    private void GumSelection()
    {
        arrowsUi.SetActive(false);
        gumChooserUi.SetActive(true);
    }

    private void ArrowSequence()
    {
        arrowsUi.SetActive(true);
        gumChooserUi.SetActive(false);
    }

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

public enum GameState
{
    GumSelection,
    ArrowSequence
}
