using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobBehavior : MonoBehaviour
{
    [SerializeField] private Wanderer _wanderer;
    [SerializeField] private Follower _follower;

    public void StartFollowing()
    {
        _wanderer.enabled = false;
    }

    public void StopFollowing()
    {
        _wanderer.enabled = true;
    }
}
