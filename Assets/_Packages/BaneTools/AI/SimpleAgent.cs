using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class SimpleAgent : MonoBehaviour
{
  public Transform target;
  public NavMeshAgent agent;

  void Start()
  {
    agent = GetComponent<NavMeshAgent>();
  }

  public void SetTarget(Transform _target)
  {
    target = _target;
    StartAgent();
  }

  public virtual void StopAgent()
  {
    agent.SetDestination(transform.position);
  }

  public virtual void StartAgent()
  {
    agent.SetDestination(target.position);
  }
}
