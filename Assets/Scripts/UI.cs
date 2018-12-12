using UnityEngine.UI;
using UnityEngine;

public class UI : MonoBehaviour {

    public Text poopNum;
    public Text itemNum;
    public Text lives;
    private CharacterController2D controller;
    private Health health;
    public Sprite poopNullSprite;
    public Sprite keyOnSprite;
    public Image poopImage;
    public Image keyImage;
    //public Sprite itemSprite;
    //private ItemSlot itemUI;

    void Awake()
    {
        controller = FindObjectOfType<CharacterController2D>();
        health = FindObjectOfType<Health>();
    }

    void Update()
    {
        poopNum.text = controller.currentPoops.ToString();
        lives.text = health.lives.ToString();
        PoopSpriteOverride();
        KeySpriteOverride();
    }

    void PoopSpriteOverride()
    {
        if (controller.currentPoops <= 0)
        {
            poopImage.overrideSprite = poopNullSprite;
        }
    }

    void KeySpriteOverride()
    {
        if (Inventory.hasKey == true)
        {
            keyImage.overrideSprite = keyOnSprite;
        }
    }
}
