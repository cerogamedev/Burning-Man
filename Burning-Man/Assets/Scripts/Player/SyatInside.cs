using UnityEngine;

public class SyatInside : MonoBehaviour
{
    private Transform target; 
    [SerializeField] private float padding = -1f; 

    private Camera _camera;
    private float _minX, _maxX, _minY, _maxY;

    void Start()
    {
        _camera = Camera.main;
        CalculateCameraBounds();
        target = this.transform;
    }

    void Update()
    {
        if (target != null)
        {
            Vector3 clampedPosition = target.position;
            clampedPosition.x = Mathf.Clamp(clampedPosition.x, _minX, _maxX);
            clampedPosition.y = Mathf.Clamp(clampedPosition.y, _minY, _maxY);
            transform.position = new Vector3(clampedPosition.x, clampedPosition.y, transform.position.z);
        }
        CalculateCameraBounds();
    }

    void CalculateCameraBounds()
    {
        float camVertExtent = _camera.orthographicSize;
        float camHorizExtent = camVertExtent * Screen.width / Screen.height;

        _minX = _camera.transform.position.x - camHorizExtent + padding;
        _maxX = _camera.transform.position.x + camHorizExtent - padding;
        _minY = _camera.transform.position.y - camVertExtent + padding;
        _maxY = _camera.transform.position.y + camVertExtent - padding;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(new Vector3(_minX, _minY, 0), new Vector3(_minX, _maxY, 0));
        Gizmos.DrawLine(new Vector3(_minX, _maxY, 0), new Vector3(_maxX, _maxY, 0));
        Gizmos.DrawLine(new Vector3(_maxX, _maxY, 0), new Vector3(_maxX, _minY, 0));
        Gizmos.DrawLine(new Vector3(_maxX, _minY, 0), new Vector3(_minX, _minY, 0));
    }
}
