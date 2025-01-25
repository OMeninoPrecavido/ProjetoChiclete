using System.Collections.Generic;
using UnityEngine;

//This script deals with the Gum Choosing UI and its subsequent functionalities
public class GumChooserUI : MonoBehaviour
{
    GameManager gameManager; //Game manager instance

    [SerializeField] GameObject gumChooserContainer; //Reference to Gum Choosing UI
    [SerializeField] private ArrowsUI arrowsUi; //Reference to Arrows UI
    [SerializeField] GameObject gumObject; //Reference to gum object prefab

    private const int GUM_CHOICE_AMOUNT = 4; //Amount of gum options

    private List<GameObject> currentGumChoices = new List<GameObject>(); //List of current gum objects onscreen

    void Start()
    {
        gameManager = GameManager.instance;
        currentGumChoices = CreateGumChoices(); //Creates new random selection of gums
    }

    void Update()
    {

        if (gameManager.gameState == GameState.GumSelection) //Only works in the correct game state
        {
            Gum chosenGum = null;

            //Allows player to choose next gum with arrow keys
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

            if (chosenGum != null)
            {
                Input.ResetInputAxes(); //Prevents pressed key from influencing the next game state

                arrowsUi.SetSequence(chosenGum); //Passes the chosen gum to the Arrows UI
                gameManager.SetGameState(GameState.ArrowSequence); //Changes game state

                currentGumChoices = CreateGumChoices(); //Resets gum choices
            }
        }
    }

    //Clears the currentGumChoices list
    private void ClearChoices()
    {
        foreach (GameObject gum in currentGumChoices)
        {
            Destroy(gum);
        }
    }

    //Creates a new, random list of gum choices and places them on screen
    private List<GameObject> CreateGumChoices()
    {
        ClearChoices();

        List<GameObject> gumChoices = new List<GameObject>();
        for (int i = 0; i < GUM_CHOICE_AMOUNT; i++)
        {
            GameObject gum = Instantiate(gumObject);
            gum.transform.SetParent(gumChooserContainer.transform, false);
            gumChoices.Add(gum);
        }

        return gumChoices;
    }
}
