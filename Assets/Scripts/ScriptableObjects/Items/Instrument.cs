using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "New Instrument", menuName = "Scriptable Objects/Items/New Instrument")]
public class Instrument: ScriptableObject, ITakable, IInteractable
{
    private bool _canTake = true;
    private GameObject _gameObject;
    private Transform _startTransform;
    private AudioSource _takeSound;
    private List<Transform> _spawnPoints;
    private int _currentSpawnPoint;

    public bool CanTake { get => _canTake; set => _canTake = value; }

    public List<Transform> SpawnPoints { get => _spawnPoints; set => _spawnPoints = value; }
    public int CurrentSpawnPoint { get => _currentSpawnPoint; set => _currentSpawnPoint = value % _spawnPoints.Count; }
    public AudioSource TakeSound { set => _takeSound = value; }
    public GameObject GameObject { get => _gameObject; set => _gameObject = value; }
    public Transform StartTransform { get => _startTransform; set => _startTransform = value; }
    
    public UnityAction TakeAction = delegate {  };
    public UnityAction<GameObject> BehaviourAction = delegate(GameObject arg0) {  };
    public UnityAction AfterTakeAction = delegate {  };


    public void OnTake(GameObject gameObject)
    {
        _startTransform = gameObject.transform;
        _gameObject = gameObject;
        
        TakeAction.Invoke();
    }

    public void Interact(GameObject gameObject) { BehaviourAction.Invoke(gameObject); }
    
    public IEnumerator Take(Rig takingRig, float takingSpeed, GameObject gameObject, Transform handTransform)
    {
        if (_canTake)
        {
            _takeSound.Play();

            while (takingRig.weight < 1f)
            {
                takingRig.weight = Mathf.MoveTowards(takingRig.weight, 1, takingSpeed * Time.deltaTime);
                yield return null;
            }

            while (takingRig.weight > 0f)
            {
                takingRig.weight = Mathf.MoveTowards(takingRig.weight, 0, takingSpeed * Time.deltaTime);

                float distance = Vector3.Distance(gameObject.transform.position, handTransform.position);
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, handTransform.position,
                    distance / (1 / (takingSpeed * Time.deltaTime)));
                yield return null;
            }
            
            gameObject.SetActive(false);
        }
        else Debug.Log("Can't take");
    }
}
