using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

//This script deals with the Combo Sequence UI and its subsequent functionalities
public class SequenceUI : MonoBehaviour
{
    [SerializeField] GameObject gumObjectPrefab; //Reference to gum object prefab
    [SerializeField] GameObject sequenceContainer; //Reference to Combo Sequence UI

    private List<Gum> currSequence = new List<Gum>(); //List of current combo sequence's gums
    private List<GameObject> gumObjects = new List<GameObject>(); //List of current combo sequence's gum object's (actual images on screen)

    //Called by other classes. Adds a new gum to the combo sequence and displays it on screen
    public void AddToSequence(Gum gum)
    {
        currSequence.Add(gum);
        DisplaySequence();
    }

    //Displays the current gum sequence on screen
    private void DisplaySequence()
    {
        foreach (GameObject gumObj in gumObjects)
        {
            Destroy(gumObj);
        }
        gumObjects.Clear();

        foreach (Gum gum in currSequence) //Creates gum objects on screen and adds them to gum objects' list 
        {
            GameObject gumObj = Instantiate(gumObjectPrefab);
            gumObj.GetComponent<GumObject>().SetGum(gum);
            gumObj.transform.SetParent(sequenceContainer.transform);
            gumObjects.Add(gumObj);
        }
    }
}
