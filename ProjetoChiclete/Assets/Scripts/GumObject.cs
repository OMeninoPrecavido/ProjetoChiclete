using UnityEngine;
using UnityEngine.UI;

public class GumObject : MonoBehaviour
{
    public Gum gum { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gum = new Gum();
        ChangeSpriteColor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetGum(Gum gum)
    {
        this.gum = gum;
        ChangeSpriteColor();
    }

    private void ChangeSpriteColor()
    {
        Image image = this.GetComponent<Image>();
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
