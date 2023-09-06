using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Item_Pillbottle : MonoBehaviour, IInteractable
{
    [SerializeField] private int _id;
    [SerializeField] private List<GameObject> _pills;
    [SerializeField] private GameObject _pillbottleCap;
    [SerializeField] private GameObject[] _noPillsDialogueTriggers;
    [SerializeField] private bool _rotateX = true, _rotateY = true, _rotateZ = true;

    
    [SerializeField] private bool _alreadyTriggered;
    [SerializeField] private float _triggerDelay; //Use in case of having more than one trigger
    private ITrigger _trigger;

    public void AssignInStateLoader()
    {
        SceneStateLoader.Instance.objects.Add(_id, gameObject);
    }

    public void Interact(PlayerController playerController, bool isInteracting, bool isInspecting)
    {
        if(_pills.Count > 0)
        {
            SoundManager.Instance.Play2DSoundEffect(SoundManager.Instance.SoundData.PillbottleClip, SoundManager.Instance.SoundData.PillbottleClipVolume);
            Destroy(_pills[_pills.Count - 1]);
            _pills.RemoveAt(_pills.Count - 1);
        }

        if(_pills.Count <= 0)
        {
            _pillbottleCap.transform.localPosition = new Vector3(0.00449999981f, 0.0137999998f, 0.177100003f);
            _pillbottleCap.transform.localRotation = Quaternion.Euler(90, 40, 0);
            gameObject.transform.localEulerAngles = Vector3.zero;
        }

        StartCoroutine(DeactivateCollider(GetComponent<Collider>(), 1));
    }

    private IEnumerator DeactivateCollider(Collider collider, float delay)
    {
        collider.enabled = false;
        yield return new WaitForSeconds(delay);
        collider.enabled = true;
    }

    public bool[] InteractableNonInspectableOrInspectableOnly()
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
        if(_pills.Count > 0 )
        {
            _trigger = gameObject.GetComponent<ITrigger>();

            if (_trigger != null && !_alreadyTriggered)
            {
                _trigger.TriggerBehaviour(_triggerDelay);
                _alreadyTriggered = true;
            }
        }
        else if(!PlayerController.Instance.PlayerInspect.Inspecting())
        {
            int dialogueIndex = Random.Range(0, _noPillsDialogueTriggers.Length);
            _trigger = _noPillsDialogueTriggers[dialogueIndex].GetComponent<ITrigger>();
            _trigger.TriggerBehaviour(0);
        }
    }
}
