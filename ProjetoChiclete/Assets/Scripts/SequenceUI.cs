using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class SequenceUI : MonoBehaviour
{
    [SerializeField] GameObject gumObjectPrefab;
    [SerializeField] GameObject sequenceContainer;

    private List<Gum> currSequence = new List<Gum>();
    private List<GameObject> gumObjects = new List<GameObject>();

    public void AddToSequence(Gum gum)
    {
        currSequence.Add(gum);
        DisplaySequence();
    }

    private void DisplaySequence()
    {
        foreach (GameObject gumObj in gumObjects)
        {
            Destroy(gumObj);
        }
        gumObjects.Clear();

        foreach (Gum gum in currSequence)
        {
            GameObject gumObj = Instantiate(gumObjectPrefab);
            gumObj.GetComponent<GumObject>().SetGum(gum);
            gumObj.transform.SetParent(sequenceContainer.transform);
            gumObjects.Add(gumObj);
        }
    }
}
