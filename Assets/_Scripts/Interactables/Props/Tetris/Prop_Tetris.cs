using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Prop_Tetris : MonoBehaviour, IInteractable
{
    [SerializeField] private int _id;

    [SerializeField] private GameObject _completeModel, _grayModel;
    [Header("Mini games")]
    [SerializeField] private GameObject _miniGameHolder;
    [SerializeField] private GameObject _prompts;

    [SerializeField] private bool _rotateX = true, _rotateY = true, _rotateZ = true;
    [SerializeField] private Vector3 _positionInInventory, _rotationInInventory, _scaleInInventory;

    [SerializeField] private Trigger_DialogueSystem[] _dialogueTriggers;

    private Collider _collider;
    private float _reactivateColliderTimer = 1f; //Prevents dialogue trigger overlap
    private bool _inInventory;
    private IMiniGame _snakeGame;

    public void AssignInStateLoader()
    {
        SceneStateLoader.Instance.objects.Add(_id, gameObject);
    }

    public void Interact(PlayerController playerController, bool isInteracting, bool isInspecting)
    {
        StartCoroutine(DisableColliderTemporarily(_reactivateColliderTimer));

        playerController.Inventory.Add(gameObject, _positionInInventory, _rotationInInventory, _scaleInInventory);
        _inInventory = true;
        _prompts.SetActive(true);
        _snakeGame.StartGame();
    }

    private void OnEnable()
    {
        if(_snakeGame == null) _snakeGame = _miniGameHolder.GetComponent<IMiniGame>();

        if(SceneManager.GetActiveScene().name == "Level_Dream")
        {
            _grayModel.SetActive(true);
            _completeModel.SetActive(false);
        }
        else
        {
            _grayModel.SetActive(false);
            _completeModel.SetActive(true);

            if (_inInventory)
            {
                _dialogueTriggers[1].TriggerBehaviour(0, false, false);
                _prompts.SetActive(true);
                _snakeGame.StartGame();
            }
        }
    }

    private void OnDisable()
    {
        _snakeGame.StopGame();
    }

    IEnumerator DisableColliderTemporarily(float timer)
    {
        if (_collider == null) _collider = GetComponent<Collider>();
        _collider.enabled = false;

        yield return new WaitForSeconds(timer);

        _collider.enabled = true;
    }

    public bool[] InteractableNonInspectableOrInspectableOnly()
    {
        bool[] interactableType = new bool[] { false, false };  //NonInspectable, InspectableOnly
        return interactableType;
    }

    public bool[] RotateXYZ()
    {
        bool[] rotateXYZ = new bool[] { _rotateX, _rotateY, _rotateZ };
        return rotateXYZ;
    }

    public void TriggerActions()
    {
        if(_inInventory)
        {
            _dialogueTriggers[1].TriggerBehaviour(0, false, false);
        }
        else
            _dialogueTriggers[0].TriggerBehaviour(0, false, false);
    }
}
