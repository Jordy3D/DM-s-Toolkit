using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using BT;

public class ProvideAgentsATarget : MonoBehaviour
{
  public Transform target;
  public GameObject spawnObject;
  public Transform objectContainer;

  public bool spawnOverCap;
  public int objectCap;

  int hardCap = 20;

  void Start()
  {
    if (!objectContainer)
      objectContainer = transform;
  }

  public void SpawnAgent()
  {
    print("Starting spawn attempt...");
    if (spawnOverCap || (objectContainer.childCount < objectCap))
    {
      print("Passed spawn check 1...");
      if (objectContainer.childCount < hardCap)
      {
        print("Passed spawn check 2...");
        GameObject agent = Instantiate(spawnObject, objectContainer.transform, false);
        agent.GetComponent<SimpleAgent>().target = target;
      }
      else
        print("Hard cap reached");
    }
  }
}
