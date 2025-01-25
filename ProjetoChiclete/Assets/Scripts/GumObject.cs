using UnityEngine;
using UnityEngine.UI;

//Class representative of the gum icons showing up on screen
public class GumObject : MonoBehaviour
{
    [SerializeField] Image TuttiBallon;
    [SerializeField] Image BBBallon;
    [SerializeField] Image MintBallon;

    [SerializeField] Image TuttiGum;
    [SerializeField] Image BBGum;
    [SerializeField] Image MintGum;

    [SerializeField] Image FirstStar;
    [SerializeField] Image SecondStar;

    public Gum gum { get; private set; } //Gum represented by the GumObject

    void Awake()
    {
        //Initializes Gum field and changes the image's color
        gum = new Gum();
        ChangeSprite();
    }

    //Method that allows other classes to define the Gum field on this class
    public void SetGum(Gum gum)
    {
        this.gum = gum;
        ChangeSprite();
    }

    //Changes color of sprite based on the gum flavour
    private void ChangeSprite()
    {
        MintBallon.gameObject.SetActive(false);
        MintGum.gameObject.SetActive(false);
        BBBallon.gameObject.SetActive(false);
        BBGum.gameObject.SetActive(false);
        TuttiBallon.gameObject.SetActive(false);
        TuttiGum.gameObject.SetActive(false);

        switch (gum.flavour)
        {
            case Flavour.Mint:
                MintBallon.gameObject.SetActive(true);
                MintGum.gameObject.SetActive(true);
                break;
            case Flavour.Tutti:
                TuttiBallon.gameObject.SetActive(true);
                TuttiGum.gameObject.SetActive(true);
                break;
            case Flavour.Blueberry:
                BBBallon.gameObject.SetActive (true);
                BBGum.gameObject.SetActive(true);
                break;
        }

        FirstStar.gameObject.SetActive(false);
        SecondStar.gameObject.SetActive(false);

        switch (gum.difficulty)
        {
            case Difficulty.Normal:
                FirstStar.gameObject.SetActive(true);
                break;
            case Difficulty.Hard:
                FirstStar.gameObject.SetActive(true);
                SecondStar.gameObject.SetActive(true);
                break;

        }
    }
}
