using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Item_Pillbottle : MonoBehaviour, IInteractable
{
    [SerializeField] private int _id;
    [SerializeField] private List<GameObject> _pills;
    [SerializeField] private GameObject _pillbottleCap;
    [SerializeField] private bool _rotateX = true, _rotateY = true, _rotateZ = true;

    [SerializeField] private ITriggerAction _trigger;
    [SerializeField] private bool _alreadyTriggered;
    [SerializeField] private float _triggerDelay;

    public string ActionDescription()
    {
        return "";
    }

    public void AssignInStateLoader()
    {
        SceneStateLoader.Instance.objects.Add(_id, gameObject);
    }

    public void Interact(PlayerController playerController)
    {
        if(_pills.Count > 0)
        {
            SoundManager.Instance.Play2DSoundEffect(SoundManager.Instance.PillbottleClip, SoundManager.Instance.PillbottleClipVolume);
            Destroy(_pills[_pills.Count - 1]);
            _pills.RemoveAt(_pills.Count - 1);
        }

        if(_pills.Count <= 0)
        {
            _pillbottleCap.SetActive(false);
            gameObject.transform.localEulerAngles = Vector3.zero;
        }
    }

    public string InteractableDescription()
    {
        return "";
    }

    public bool[] InteractableType()
    {
        bool nonInspectable = false;
        bool inspectableOnly = false;

        bool[] interactableType = new bool[] { nonInspectable, inspectableOnly };

        return interactableType;
    }

    public bool[] RotateXYZ()
    {
        bool[] rotateXYZ = new bool[] { _rotateX, _rotateY, _rotateZ };
        return rotateXYZ;
    }

    public void TriggerActions()
    {
        _trigger = gameObject.GetComponent<ITriggerAction>();

        if (_trigger != null && !_alreadyTriggered)
        {
            _trigger.TriggerAction(_triggerDelay);
            _alreadyTriggered = true;
        }
    }
}
