﻿using Assets.Scripts.Utils;
using UnityEngine;
using static Assets.Scripts.Utils.Enums;

public class Wandering : MovingObject
{
    private Vector3 direction;
    private float idleTime = 2f;
    private float nextMove = 0f;

    public float restDuration = 2f;
    public float moveProbability = 0.25f;
    public float moveDistance = 60f;

    protected override void Update()
    {
        switch (state)
        {
            case WalkingState.Standing:
            default:
                if (ShouldMove())
                    MoveToRandomPosition();
                break;
            case WalkingState.Walking:
                break;
        }

        base.Update();
    }

    private bool ShouldMove()
    {
        if (Time.time > nextMove)
        {
            nextMove = Time.time + idleTime;
            return Random.Range(0f, 1f) < moveProbability;
        }

        return false;
    }

    private void MoveToRandomPosition()
    {
        direction = DetermineDirection();
        var target = transform.position + direction * moveDistance;
        Move(target);
    }

    private Vector3 DetermineDirection()
    {
        var facing = (Facing)Random.Range(1, 5);
        return facing.ConvertToVector3();
    }
}
