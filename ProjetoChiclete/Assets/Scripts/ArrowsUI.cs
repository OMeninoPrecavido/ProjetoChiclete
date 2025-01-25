using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class ArrowsUI : MonoBehaviour
{
    GameManager gameManager;

    [SerializeField] GameObject arrowsContainer;

    [SerializeField] GameObject arrowUpPrefab;
    [SerializeField] GameObject arrowDownPrefab;
    [SerializeField] GameObject arrowLeftPrefab;
    [SerializeField] GameObject arrowRightPrefab;
    [SerializeField] SequenceUI sequenceUI;

    private List<KeyCode> currentSequence = new List<KeyCode>();
    private List<GameObject> arrowObjects = new List<GameObject>();

    private void Awake()
    {
        gameManager = GameManager.instance;
    }

    private void Update()
    {
        if (gameManager.gameState == GameState.ArrowSequence)
        {
            KeyCode? pressedKey = getInput();
            if (pressedKey != null)
            {
                if (pressedKey == currentSequence[0])
                {
                    currentSequence.RemoveAt(0);
                    Destroy(arrowObjects[0]);
                    arrowObjects.RemoveAt(0);

                    if (currentSequence.Count <= 0)
                    {
                        gameManager.SetGameState(GameState.GumSelection);
                        //sequenceUI.AddToSequence(currentGum);
                        CleanUp();
                    }
                }
                else
                {
                    gameManager.SetGameState(GameState.GumSelection);
                    CleanUp();
                }
            }
        }
    }

    void CleanUp()
    {
        arrowObjects.Clear();
        currentSequence.Clear();
    }

    private KeyCode? getInput()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
            return KeyCode.LeftArrow;
        if (Input.GetKeyDown(KeyCode.RightArrow))
            return KeyCode.RightArrow;
        if (Input.GetKeyDown(KeyCode.UpArrow))
            return KeyCode.UpArrow;
        if (Input.GetKeyDown(KeyCode.DownArrow))
            return KeyCode.DownArrow;

        return null;
    }

    public void SetSequence(Gum gum)
    {
        currentSequence = gum.arrowSequence;
        DisplaySequence();
    }

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
}
