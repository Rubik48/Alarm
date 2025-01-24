using System.Collections;
using UnityEngine;

public class Thief : MonoBehaviour
{
    [SerializeField] private Transform _pointA;
    [SerializeField] private Transform _pointB;
    [SerializeField] private float _speed = 3f;
    
    private Transform _currentTarget;

    private float _minDistanceSqr = 0.1f;
    
    private void Start()
    {
        _currentTarget = _pointB;
        
        StartCoroutine(Moving());
    }

    private IEnumerator Moving()
    {
        while (enabled)
        {
            transform.position = Vector3.MoveTowards(transform.position, _currentTarget.position, _speed * Time.deltaTime);
       
            if ((transform.position - _currentTarget.position).sqrMagnitude < _minDistanceSqr)
            {
                _currentTarget = _currentTarget == _pointA ? _pointB : _pointA;
            }

            yield return null;
        }
    }
}