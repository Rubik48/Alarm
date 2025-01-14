using UnityEngine;

public class Thief : MonoBehaviour
{
    [SerializeField] private Transform _pointA;
    [SerializeField] private Transform _pointB;
    [SerializeField] private float _speed = 3f;
    
    private Transform _currentTarget;

    private float _minDistance = 0.1f;
    
    private void Start()
    {
        _currentTarget = _pointB;
    }
    
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _currentTarget.position, _speed * Time.deltaTime);
        
        if (Vector3.Distance(transform.position, _currentTarget.position) < _minDistance)
        {
            _currentTarget = _currentTarget == _pointA ? _pointB : _pointA;
        }
    }
}
