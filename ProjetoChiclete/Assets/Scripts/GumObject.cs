using UnityEngine;
using UnityEngine.UI;

//Class representative of the gum icons showing up on screen
public class GumObject : MonoBehaviour
{
    public Gum gum { get; private set; } //Gum represented by the GumObject
    void Awake()
    {
        //Initializes Gum field and changes the image's color
        gum = new Gum();
        ChangeSpriteColor();
    }

    //Method that allows other classes to define the Gum field on this class
    public void SetGum(Gum gum)
    {
        this.gum = gum;
        ChangeSpriteColor();
    }

    //Changes color of sprite based on the gum flavour
    private void ChangeSpriteColor()
    {
        Image image = this.GetComponent<Image>(); //Image component attached to the same object this script is attached to
        switch (gum.flavour)
        {
            case Flavour.Mint:
                image.color = Color.green;
                break;
            case Flavour.Tutti:
                image.color = Color.magenta;
                break;
            case Flavour.Blueberry:
                image.color = Color.blue;
                break;
        }
    }
}
