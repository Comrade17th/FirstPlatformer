using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Wanderer : MonoBehaviour
{
    [SerializeField] private Transform _waypointsParent;
    [SerializeField] private float _waypointPositionSpread = 2f;
    [SerializeField] private float _speed = 3f;
    [SerializeField, Min(0)] private float _stayTimeBase = 1f;
    [SerializeField, Min(0)] private float _stayTimeSpread = 0.3f;
    
    private int _currentWaypoint = 0;
    private Vector3 _targetPosition;
    private WaitForSeconds _stayTime;
    private bool _isStaying = false;
    private bool _isFacingRight = true;
    private Transform[] _waypoints;
    
    private void Update()
    {
        if(_waypoints != null)
            FollowWaypoints();
    }

    private void FollowWaypoints()
    {
        Flip();
        
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

    private void InitWaypoints()
    {
        _waypoints = new Transform[_waypointsParent.childCount];
        for (int i = 0; i < _waypoints.Length; i++)
            _waypoints[i] = _waypointsParent.GetChild(i);
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
            newWaypoint = Random.Range(0, _waypoints.Length);
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

    private void Flip()
    {
        float direction = _targetPosition.x - transform.position.x;
        
        if (_isFacingRight && direction < 0f || !_isFacingRight && direction > 0f)
        {
            Vector3 localScale = transform.localScale;

            _isFacingRight = !_isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private IEnumerator Stay()
    {
        _isStaying = true;
        yield return _stayTime;
        _isStaying = false;
    }

    public void SetWaypointsParent(Transform waypointParent)
    {
        _waypointsParent = waypointParent;
        InitWaypoints();
        ChangeWaypoint();
        _targetPosition = transform.position;
        InitStayTime();
    }
}
