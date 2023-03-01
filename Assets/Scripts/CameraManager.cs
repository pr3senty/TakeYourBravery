using UnityEngine;
using UnityEngine.Serialization;
using Debug = UnityEngine.Debug;

public class CameraManager : MonoBehaviour
{

    [SerializeField] private InputHandler _inputHandler;

    [SerializeField] private float _cameraSlowDown = 3;
    
    [Header("Camera Boundaries")]
    [SerializeField] private int _cameraUpperBoundary = 290;
    [SerializeField] private int _cameraDownBoundary = 70;
    [SerializeField] private int _cameraRightBoundary = 30;
    [SerializeField] private int _cameraLeftBoundary = 330;

    private GameObject _prevTouchableObject;
    private GameObject _prevDestroyableObject;


    private Camera _camera;

    // Start is called before the first frame update
    private void Start()
    {
        _camera = Camera.main;

        _inputHandler.OnCameraMoveEvent += OnCameraMove;
    }

    private void OnCameraMove(Vector2 movement)
    {
        Vector3 oldRotation = _camera.transform.localEulerAngles;

        float newYRotation = oldRotation.y + (movement.x / _cameraSlowDown);
        float newXRotation = oldRotation.x + (movement.y / _cameraSlowDown);

        if (newXRotation < _cameraDownBoundary || newXRotation > _cameraUpperBoundary)
        {
            _camera.transform.localRotation = Quaternion.Euler(newXRotation, oldRotation.y, 0);
        }

        oldRotation = _camera.transform.localEulerAngles;

        if (newYRotation < _cameraRightBoundary || newYRotation > _cameraLeftBoundary)
        {
            _camera.transform.localRotation = Quaternion.Euler(oldRotation.x, newYRotation, 0);
        }
    }

    private void FixedUpdate()
    {
        Vector3 origin = _camera.transform.position;
        Vector3 direction = _camera.transform.forward;

        RaycastHit hit;
        
        ResetPrevObjects();
        
        if (Physics.Raycast(origin, direction, out hit, Mathf.Infinity))
        {
            CheckTouchable(hit);
            CheckInteractable(hit);
        }
        Debug.DrawRay(origin, direction * 100f, Color.blue);
    }

    private void CheckTouchable(RaycastHit hit)
    {
        if (hit.transform.TryGetComponent<Takable>(out Takable touchableObject))
        {
            _prevTouchableObject = touchableObject.gameObject;
            
            _prevTouchableObject.GetComponent<Takable>().enabled = true;
        }
    }

    private void CheckInteractable(RaycastHit hit)
    {
        if (hit.transform.TryGetComponent<Interactable>(out Interactable destroyableObject))
        {
            _prevDestroyableObject = destroyableObject.gameObject;

            _prevDestroyableObject.GetComponent<Interactable>().enabled = true;
        }
    }

    private void ResetPrevObjects()
    {
        if (_prevTouchableObject is not null)
        {
            _prevTouchableObject.GetComponent<Takable>().enabled = false;
        }

        if (_prevDestroyableObject is not null)
        {
            _prevDestroyableObject.GetComponent<Interactable>().enabled = false;
        }
    }
}
