using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using TCrane;

namespace TCrane
{
    public class NavMoveTo : MonoBehaviour
    {
        //config
        public Transform goal;
        NavMeshAgent agent;

        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        void Update()
        {
            if (goal != null) agent.destination = goal.position;
        }
    }
}