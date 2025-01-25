using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class GumChooser : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] GameObject gumObject;
    private const int GUM_CHOICE_AMOUNT = 4;
    private List<GameObject> currentGumChoices = new List<GameObject>();

    [SerializeField] private ArrowsUI arrowsUi;

    void Start()
    {
        gameManager = GameManager.instance;
        currentGumChoices = createGumChoices();
    }

    void Update()
    {
        
        if (gameManager.gameState == GameState.GumSelection)
        {
            Gum chosenGum = null;

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                chosenGum = currentGumChoices[0].GetComponent<GumObject>().gum;
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                chosenGum = currentGumChoices[1].GetComponent<GumObject>().gum;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                chosenGum = currentGumChoices[2].GetComponent<GumObject>().gum;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                chosenGum = currentGumChoices[3].GetComponent<GumObject>().gum;
            }

            if (chosenGum != null) {
                arrowsUi.SetSequence(chosenGum.arrowSequence);
                gameManager.SetGameState(GameState.ArrowSequence);
            }
        }
    }

    private void clearChoices()
    {
        foreach(GameObject gum in currentGumChoices)
        {
            Destroy(gum);
        }
    }

    private List<GameObject> createGumChoices()
    {
        clearChoices();

        List<GameObject> gumChoices = new List<GameObject>();
        for(int i = 0;i < GUM_CHOICE_AMOUNT;i++)
        {
            GameObject gum = Instantiate(gumObject);
            gum.transform.SetParent(this.transform, false);
            gumChoices.Add(gum);
        }

        return gumChoices;
    }
}
