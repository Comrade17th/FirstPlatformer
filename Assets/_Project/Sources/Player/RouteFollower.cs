using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RouteFollower : MonoBehaviour
{
    [SerializeField] private List<Transform> _waypoints;
    [SerializeField] private float _speed = 0.04f;
    
    private int _currentWaypoint = 0;

    private void Update(){
        if(transform.position == _waypoints[_currentWaypoint].position){
            _currentWaypoint = (_currentWaypoint + 1) % _waypoints.Count;
        }

        transform.position = Vector3.MoveTowards(
            transform.position,
            _waypoints[_currentWaypoint].position, 
            _speed * Time.deltaTime);
    }
}
