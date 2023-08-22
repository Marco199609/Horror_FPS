using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop_Tetris : MonoBehaviour, IInteractable
{
    [SerializeField] private int _id;

    [Header("Mini games")]
    [SerializeField] private Tetris_Snake _snakeGame;

    [Header("Use only if inspectable")]
    [SerializeField] private bool _rotateX = true, _rotateY = true, _rotateZ = true;
    [SerializeField] private Vector3 _positionInInventory, _rotationInInventory, _scaleInInventory;

    [SerializeField] private Trigger_DialogueSystem[] _dialogueTriggers;

    private Collider _collider;
    private float _reactivateColliderTimer = 1f; //Prevents dialogue trigger overlap
    private bool _inInventory;
    public void AssignInStateLoader()
    {
        SceneStateLoader.Instance.objects.Add(_id, gameObject);
    }

    public void Interact(PlayerController playerController)
    {
        StartCoroutine(DisableColliderTemporarily(_reactivateColliderTimer));

        playerController.Inventory.Add(gameObject, _positionInInventory, _rotationInInventory, _scaleInInventory);
        _inInventory = true;
        _snakeGame.StartGame();
    }

    private void OnEnable()
    {
        if (_inInventory)
        {
            _snakeGame.StartGame();
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

    public bool[] InteractableType()
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
            _dialogueTriggers[1].TriggerAction(0);
        }
        else
            _dialogueTriggers[0].TriggerAction(0);
    }
}
