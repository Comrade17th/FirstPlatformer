using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Wanderer : MonoBehaviour
{
    [SerializeField] private List<Transform> _waypoints;
    [SerializeField] private float _waypointPositionSpread = 2f;
    [SerializeField] private float _speed = 3f;
    [SerializeField, Min(0)] private float _stayTimeBase = 1f;
    [SerializeField, Min(0)] private float _stayTimeSpread = 0.3f;
    
    private int _currentWaypoint = 0;
    private Vector3 _targetPosition;
    private WaitForSeconds _stayTime;
    private bool _isStaying = false;

    private void Start()
    {
        ChangeWaypoint();
        _targetPosition = transform.position;
        InitStayTime();
    }
    
    private void Update()
    {
        if(transform.position == _targetPosition)
        {
            _currentWaypoint = ChangeWaypoint();
            _targetPosition = SpreadWaypointPosition(_waypoints[_currentWaypoint].position);
            StartCoroutine(Stay());
        }
        
        if(_isStaying == false)
            transform.position = Vector3.MoveTowards(
                 transform.position,
                  _targetPosition, 
                _speed * Time.deltaTime);
    }

    private Vector3 SpreadWaypointPosition(Vector3 position)
    {
        return new Vector3(
            Utilities.GetSpreadValue(position.x, _waypointPositionSpread),
            position.y);
    }

    private int ChangeWaypoint()
    {
        int newWaypoint;
        
        do
        {
            newWaypoint = Random.Range(0, _waypoints.Count);
        } while (newWaypoint == _currentWaypoint);
        
        return newWaypoint;
    }

    private void InitStayTime()
    {
        float time = Utilities.GetSpreadValue(_stayTimeBase, _stayTimeSpread);
        
        if (time < 0)
            time = 0;

        _stayTime = new WaitForSeconds(time);
    }
    
    private IEnumerator Stay()
    {
        _isStaying = true;
        yield return _stayTime;
        _isStaying = false;
    }
}
