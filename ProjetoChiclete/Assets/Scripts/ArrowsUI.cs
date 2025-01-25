using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class ArrowsUI : MonoBehaviour
{
    [SerializeField] GameObject arrowsContainer;

    [SerializeField] GameObject arrowUpPrefab;
    [SerializeField] GameObject arrowDownPrefab;
    [SerializeField] GameObject arrowLeftPrefab;
    [SerializeField] GameObject arrowRightPrefab;

    private List<KeyCode> currentSequence = new List<KeyCode>();

    public void SetSequence(List<KeyCode> sequence)
    {
        currentSequence = sequence;
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
        }
    }
}
