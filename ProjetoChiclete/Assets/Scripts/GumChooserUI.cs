using System.Collections.Generic;
using UnityEngine;

public class GumChooserUI : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] GameObject gumChooserContainer;
    [SerializeField] GameObject gumObject;
    private const int GUM_CHOICE_AMOUNT = 4;
    private List<GameObject> currentGumChoices = new List<GameObject>();

    [SerializeField] private ArrowsUI arrowsUi;

    void Start()
    {
        gameManager = GameManager.instance;
        currentGumChoices = CreateGumChoices();
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

            if (chosenGum != null)
            {
                Input.ResetInputAxes();

                arrowsUi.SetSequence(chosenGum);
                gameManager.SetGameState(GameState.ArrowSequence);

                currentGumChoices = CreateGumChoices();
            }
        }
    }

    private void ClearChoices()
    {
        foreach (GameObject gum in currentGumChoices)
        {
            Destroy(gum);
        }
    }

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
