using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Player & UI References")]
    public PlayerController playerController;

    public GameObject Logo;
    public GameObject Background;
    public GameObject Mainmenu;

    [Header("Control Toggles")]
    public Toggle joystickToggle;
    public Toggle arrowToggle;

    [Header("Control Icons (Image Components)")]
    public Image joystickImage;
    public Image arrowImage;

    [Header("Joystick Sprites")]
    public Sprite joystickEnableSprite;
    public Sprite joystickDisableSprite;

    [Header("Arrow Sprites")]
    public Sprite arrowEnableSprite;
    public Sprite arrowDisableSprite;


    [Header("InGameincons")]
    public GameObject inGameIcons;

    [Header("ShowControls")]
    public GameObject GameplayUI;

    private void Start()
    {
        // Listen for toggle changes
        joystickToggle.onValueChanged.AddListener(delegate { OnToggleChanged(); });
        arrowToggle.onValueChanged.AddListener(delegate { OnToggleChanged(); });

        OnToggleChanged();
    }

    public void OnToggleChanged()
    {
        if (joystickToggle.isOn)
        {

            joystickImage.sprite = joystickEnableSprite;
            arrowImage.sprite = arrowDisableSprite;

            playerController.UseJoystick(true);
            playerController.UseArrowButtons(false);
        }
        else if (arrowToggle.isOn)
        {

            joystickImage.sprite = joystickDisableSprite;
            arrowImage.sprite = arrowEnableSprite;

            playerController.UseArrowButtons(true);
            playerController.UseJoystick(false);
        }
    }

    public void Play()
    {
        if (playerController != null)
            playerController.EnableMovement();

        Logo?.SetActive(false);
        Background?.SetActive(false);
        Mainmenu?.SetActive(false);
        inGameIcons?.SetActive(true);
        GameplayUI?.SetActive(true);
    }
}