using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CenterPointUIHandler))]
[RequireComponent(typeof(InteractableUIHandler))]
public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Player UI")]
    [SerializeField] private GameObject _playerCanvas;
    [SerializeField] private GameObject _interactOnlyPrompt;
    [SerializeField] private GameObject _inspectOnlyPrompt;
    [SerializeField] private GameObject _inspectAndInteractPrompt;
    [SerializeField] private GameObject _returnItemPrompt;
    [field:SerializeField] public TextMeshProUGUI ReadableItemText { get; private set; }
    public Image _uiCenterPoint;

    [Header("Dialogue UI")]
    public TextMeshProUGUI DialogueText;

    [Header("Menu UI")]
    public GameObject SettingsCanvas;
    public GameObject MainMenuCanvas;
    public GameObject StartGameButton;
    public GameObject ContinueGameButton;

    //Required scripts
    private CenterPointUIHandler _centerPointUIHandler;
    private InteractableUIHandler _interactableUIHandler;


    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;
    }

    private void OnEnable()
    {
        _centerPointUIHandler = GetComponent<CenterPointUIHandler>();
        _interactableUIHandler = GetComponent<InteractableUIHandler>();
        _interactableUIHandler.SetVariables(_interactOnlyPrompt, _inspectOnlyPrompt, _inspectAndInteractPrompt, _returnItemPrompt);

        //Subscribes to player UI events
        PlayerUI.ItemDescriptionActivated += _interactableUIHandler.ActivateInteractableUIElements;
        PlayerUI.ItemDescriptionReset += _interactableUIHandler.DeactivateInteractableUIElements;
        PlayerUI.ItemsBecameVisible += _centerPointUIHandler.AreItemsVisible;
    }

    private void OnDisable()
    {
        //Unsubscribes from player Ui events
        PlayerUI.ItemDescriptionActivated -= _interactableUIHandler.ActivateInteractableUIElements;
        PlayerUI.ItemDescriptionReset -= _interactableUIHandler.DeactivateInteractableUIElements;
        PlayerUI.ItemsBecameVisible -= _centerPointUIHandler.AreItemsVisible;
    }

    private void Update()
    {
        if (GameSettings.Instance.InGame)
        {
            if(GameSettings.Instance.Pause) _playerCanvas.SetActive(false);
            else _playerCanvas.SetActive(true);
        } 

        if(PlayerController.Instance != null && PlayerController.Instance.PlayerInspect.Inspecting())
        {
            _interactableUIHandler.ActivateInteractableUIElements(false, false); //Activates return prompt when player enters inspection of item
        }

        _centerPointUIHandler.UpdateCenterPoint(_uiCenterPoint);
    }
}
