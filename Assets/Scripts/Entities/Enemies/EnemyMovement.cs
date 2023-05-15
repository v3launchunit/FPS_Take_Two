using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : Movement
{
    [SerializeField] private Transform _target;
    [SerializeField] private float     _targetMinDistance     = 0f;
    [SerializeField] private float     _targetDesiredDistance = 1f;
    [SerializeField] private float     _targetMaxDistance     = 1f;
    [SerializeField] private bool      _busy = false;
    
    private Vector3      _destination;
    private NavMeshAgent _agent;

    // Start is called before the first frame update
    void Start()
    {
        if (_target == null)
            _target             = Camera.main.transform;
        _agent                  = gameObject.GetComponent<NavMeshAgent>();
        _destination            = _agent.destination;
        _agent.stoppingDistance = _targetDesiredDistance;        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(_destination, _target.position) > _targetMaxDistance)
        {
            _destination       = _target.position;
        }
        else if (Vector3.Distance(_destination, _target.position) < _targetMinDistance)
        {
            _destination       = transform.position - (2 * _targetDesiredDistance * (transform.rotation * Vector3.forward));
        }
        if (_busy)
            _agent.destination = transform.position;
        else
            _agent.destination = _destination;


        // if (!_busy)
        //     _controller.Move(Time.deltaTime * _agent.desiredVelocity);
    }
    
    public Transform Target { get => _target; }
    public bool      Busy   { get => _busy; set => _busy = value; }
}
