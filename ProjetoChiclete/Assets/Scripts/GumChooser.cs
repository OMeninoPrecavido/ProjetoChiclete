using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class GumChooser : MonoBehaviour
{
    [SerializeField] GameObject gumObject;
    private const int GUM_CHOICE_AMOUNT = 4;
    private List<GameObject> currentGumChoices = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentGumChoices = createGumChoices();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            currentGumChoices = createGumChoices();
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
