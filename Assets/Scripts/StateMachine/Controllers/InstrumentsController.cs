using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.Rendering;

public class InstrumentsController : MonoBehaviour
{
    [Header("Instruments")]
    [SerializeField] private Instrument _crowBarSO;
    [SerializeField] private Instrument _sledgeHammerSO;
    [SerializeField] private Instrument _pipeWrenchSO;
    [SerializeField] private Instrument _crossSO;

    [Header("Instruments SpawnPoints")] 
    [SerializeField] private List<Transform> _crowBarSpawnPoints;
    [SerializeField] private List<Transform> _sledgeHammerSpawnPoints;
    [SerializeField] private List<Transform> _pipeWrenchSpawnPoints;

    [Header("Animating Rigging")]
    [SerializeField] private Transform _crossTransform;
    [SerializeField] private Transform _crowBarTransform;
    [SerializeField] private Transform _pipeWrenchTransform;
    [SerializeField] private Transform _sledgeHammerTransform;
    [SerializeField] private Transform _noneTransform;

    private StateMachine _stateMachine;
    private bool _isUsed;
    private Transform _previousChild;
    
    public State ConvertingTo { get; set; }
    public CrossState CrossState { get; set; }
    public CrowBarState CrowBarState { get; set; }
    public PipeWrenchState PipeWrenchState { get; set; }
    public SledgeHammerState SledgeHammerState { get; set; }
    public NoneState NoneState { get; set; }
    public Transform Parent 
    {
        set
        {
            if (_previousChild is not null)
            {
                _previousChild.gameObject.SetActive(false);
            }
            
            value.gameObject.SetActive(true);
            _previousChild = value;
        }
    }
    
    public Transform CrossTransform { get => _crossTransform; }
    public Transform CrowBarTransform { get => _crowBarTransform; }
    public Transform PipeWenchTransform { get => _pipeWrenchTransform; }
    public Transform SledgeHammerTransform { get => _sledgeHammerTransform; }
    public Transform NoneTransform { get => _noneTransform; }
    public Instrument CrossSO { get => _crossSO; }
    public Instrument CrowBarSO { get => _crowBarSO; }
    public Instrument PipeWrenchSO { get => _pipeWrenchSO; }
    public Instrument SledgeHammerSO { get => _sledgeHammerSO; }
    public bool IsUsed { get => _isUsed; set => _isUsed = value; }
    
    public UnityAction<GameObject> CrowBarAction { set => _crowBarSO.BehaviourAction = value; }
    public UnityAction<GameObject> SledgeHammerAction { set => _sledgeHammerSO.BehaviourAction = value; }
    public UnityAction<GameObject> PipeWrenchAction { set => _pipeWrenchSO.BehaviourAction = value; }
    
    void Start()
    {
        _crowBarSO.TakeAction += () => ConvertingTo = CrowBarState; 
        _crowBarSO.AfterTakeAction += () => ResetTransform(_crowBarSO.GameObject, _crowBarSO.StartTransform.position, _crowBarSO.StartTransform.rotation);
        _crowBarSO.SpawnPoints = _crowBarSpawnPoints;

        _sledgeHammerSO.TakeAction += () => ConvertingTo = SledgeHammerState; 
        _sledgeHammerSO.AfterTakeAction += () => ResetTransform(_sledgeHammerSO.GameObject, _sledgeHammerSO.StartTransform.position, _sledgeHammerSO.StartTransform.rotation);
        _sledgeHammerSO.SpawnPoints = _sledgeHammerSpawnPoints;
        
        _pipeWrenchSO.TakeAction += () => ConvertingTo = PipeWrenchState; 
        _pipeWrenchSO.AfterTakeAction = () => ResetTransform(_pipeWrenchSO.GameObject, _pipeWrenchSO.StartTransform.position, _pipeWrenchSO.StartTransform.rotation);
        _pipeWrenchSO.SpawnPoints = _pipeWrenchSpawnPoints;
        
        _crossSO.TakeAction += () => ConvertingTo = CrossState; 
        _crossSO.AfterTakeAction += () => ResetTransform(_crossSO.GameObject, _crossSO.StartTransform.position, _crossSO.StartTransform.rotation);

        _stateMachine = new StateMachine();

        CrossState = new CrossState(this, _stateMachine);
        CrowBarState = new CrowBarState(this, _stateMachine);
        PipeWrenchState = new PipeWrenchState(this, _stateMachine);
        SledgeHammerState = new SledgeHammerState(this, _stateMachine);
        NoneState = new NoneState(this, _stateMachine);
        
        _stateMachine.Initialize(NoneState);
    }
    
    void Update()
    {
       _stateMachine.OnUpdate();
    }

    public void BreakWall(Instrument usedInstrument, GameObject gameObject)
    {
        Debug.Log("Breaking the wall!");
        gameObject.GetComponent<PlayableDirector>().Play();
        gameObject.GetComponent<PlayableDirector>().stopped += pd => Played(usedInstrument, pd);
        _isUsed = true;
        
    }
    
    public void Played(Instrument usedInstrument, PlayableDirector pd)
    {
        Debug.Log("Animation played!");
        DisableAllBoxColliders(pd.gameObject.GetComponents<BoxCollider>());
        
        GameObject crossSpawner = pd.gameObject.transform.GetChild(0).gameObject;
        
        if (CrossSO.GameObject != null)
        {
            ResetTransform(CrossSO.GameObject, crossSpawner.transform.position, crossSpawner.transform.rotation);
        }
        
        if (usedInstrument.GameObject != null)
        {
            Transform newTransform = usedInstrument.SpawnPoints[usedInstrument.CurrentSpawnPoint];
            usedInstrument.CurrentSpawnPoint++;
            
            ResetTransform(usedInstrument.GameObject, newTransform.position, newTransform.rotation);
        }
    }
    
    public void BreakLock(Instrument usedInstrument, GameObject gameObject)
    {
        Debug.Log("Breaking the lock!");
        gameObject.GetComponent<PlayableDirector>().Play();
        gameObject.GetComponent<PlayableDirector>().stopped += pd => Played(usedInstrument, pd);
        _isUsed = true;
    }

    public void RemoveBarricade(Instrument usedInstrument,GameObject gameObject)
    {
        Debug.Log("Removing the barricade");
        gameObject.GetComponent<PlayableDirector>().Play();
        gameObject.GetComponent<PlayableDirector>().stopped += pd => Played(usedInstrument, pd);
        _isUsed = true;
    }

    public void NeedCross(GameObject gameObject)
    {
        Debug.Log("I need my cross");
    }

    public void CantDoAnything(GameObject gameObject) 
    {
        Debug.Log("I cant do anything with cross. I need items...");
    }
    
    private void DisableAllBoxColliders(BoxCollider[] colliders) 
    {
        foreach (BoxCollider boxCollider in colliders) boxCollider.enabled = false;
    }
    
    private void ResetTransform(GameObject movableObject, Vector3 newPosition, Quaternion newRotation)
    {
        movableObject.transform.position = newPosition;
        movableObject.transform.rotation = newRotation;
        movableObject.SetActive(true);
    }


    public void CanTakeInstrument<T>(T instrumentSO, bool value) where T : Instrument => instrumentSO.CanTake = value;

    public void ResetInstrument<T>(T instrumentSO) where T: Instrument
    {
        ResetTransform(instrumentSO.GameObject, instrumentSO.StartTransform.position, instrumentSO.StartTransform.rotation);
    } 
}
