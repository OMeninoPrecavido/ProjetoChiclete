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
    [SerializeField] Transform StarHolder;

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

    public void SetSpriteAsSequence()
    {
        MintBallon.gameObject.SetActive(false);
        TuttiBallon.gameObject.SetActive(false);
        BBBallon.gameObject.SetActive(false);

        FirstStar.transform.SetParent(StarHolder.transform);
        SecondStar.transform.SetParent(StarHolder.transform);

        var mintRef = MintGum.GetComponent<RectTransform>();
        var BBRef = BBGum.GetComponent<RectTransform>();
        var tuttiRef = TuttiGum.GetComponent<RectTransform>();

        tuttiRef.localScale *= 1.2f;
        //tuttiRef.eulerAngles = new Vector3(0, 0, Random.Range(-3.5f, 3.5f));

        BBRef.localScale *= 1.2f;
        //BBRef.eulerAngles = new Vector3(0, 0, Random.Range(-3.5f, 3.5f));

        mintRef.localScale *= 1.2f;
        //mintRef.eulerAngles = new Vector3(0, 0, Random.Range(-3.5f, 3.5f));
    }
}
