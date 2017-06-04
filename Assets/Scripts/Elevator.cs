using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public float StartingHeight = 0.0f;
    public float StoppingHeight = 3.0f;

    public float Interval = 1.0f;

    public AnimationCurve Curve;

    private Rigidbody _rigidbody;

    // Use this for initialization
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var position = _rigidbody.position;
        position.y = StartingHeight + Curve.Evaluate(Mathf.PingPong(Time.time, Interval) / Interval) *
                     (StoppingHeight - StartingHeight);

        _rigidbody.MovePosition(position);
    }
}