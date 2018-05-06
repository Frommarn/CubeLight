using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBasicMovement : MonoBehaviour {

    public float _MoveSpeed = 1.0f;
    public float _GoalRadius = 0.01f;
    public float _RotationSpeed = 1.0f;

    public float _AccumulatedRotation;
    public float RotationTime = 1.0f;

    private Vector3 _Goal;
    private Quaternion _GoalDirection;
    private Quaternion _RotationStart;
    private bool _IsRotating;
    private bool _IsMoving;

    private void Start()
    {
        _Goal = transform.position;
    }

    public void MoveOrder(Vector3 newGoal)
    {
        _IsRotating = true;
        _IsMoving = false;
        _AccumulatedRotation = 0.0f;
        _Goal = newGoal;
        _Goal.y = transform.position.y;
        Vector3 goalDirectionVector = (_Goal - transform.position).normalized;
        _GoalDirection = Quaternion.LookRotation(goalDirectionVector);
        _RotationStart = transform.rotation;
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, (_Goal - transform.position), Color.red);
        Debug.DrawRay(transform.position, transform.forward, Color.blue);
        if (_IsRotating)
        {
            RotateTowardsGoal();
        }
        if (_IsMoving)
        {
            MoveTowardsGoal();
        }
    }

    private void RotateTowardsGoal()
    {
        _AccumulatedRotation += Time.deltaTime;
        transform.rotation = Quaternion.Slerp(_RotationStart, _GoalDirection, _AccumulatedRotation / RotationTime);
        if (_AccumulatedRotation >= RotationTime)
        {
            Debug.Log("Rotation finished!");
            transform.rotation = _GoalDirection;
            _IsRotating = false;
            _IsMoving = true;
        }
    }

    private void MoveTowardsGoal()
    {
        Vector3 moveVector = (_Goal - transform.position).normalized * _MoveSpeed * Time.deltaTime;
        float distanceToGoal = (_Goal - transform.position).magnitude;
        float distanceCoveredInStep = moveVector.magnitude;
        if (distanceToGoal <= distanceCoveredInStep)
        {
            transform.position = _Goal;
            _IsMoving = false;
        }
        else
        {
            transform.position += (_Goal - transform.position).normalized * _MoveSpeed * Time.deltaTime;
        }
    }
}
