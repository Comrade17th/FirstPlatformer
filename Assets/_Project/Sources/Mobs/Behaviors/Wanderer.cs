using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(MobMovement))]
public class Wanderer : MonoBehaviour
{
    [SerializeField] private Transform _waypointsParent;
    [SerializeField] private float _waypointPositionSpread = 2f;
    [SerializeField, Min(0)] private float _stayTimeBase = 1f;
    [SerializeField, Min(0)] private float _stayTimeSpread = 0.3f;
    
    private float _speed;
    
    private int _currentWaypoint = 0;
    private Vector3 _targetPosition;
    private WaitForSeconds _stayTime;
    private bool _isStaying = false;
    private Transform[] _waypoints;

    private MobMovement _mobMovement;

    private void Start()
    {
        _mobMovement = GetComponent<MobMovement>();
        _speed = _mobMovement.Speed;
    }

    private void Update()
    {
        if(_waypoints != null)
            FollowWaypoints();
    }

    private void FollowWaypoints()
    {
        if(transform.position == _targetPosition)
        {
            _currentWaypoint = ChangeWaypoint();
            _targetPosition = SpreadWaypointPosition(_waypoints[_currentWaypoint].position);
            StartCoroutine(Stay());
        }
        
        if(_isStaying == false)
            _mobMovement.Follow(_targetPosition);
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
