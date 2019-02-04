using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCrane;
using UnityEngine.AI;

namespace TCrane
{
    public class Mob : MonoBehaviour
    {
        //config
        public const string mobTag = "Mob";

        //state
        public enum States { Running, Freezed, Dying };
        States state;
        public bool isFreezed;
        public static int aliveCount;

        //bookkeeping
        float speed;

        void Subscribe(bool trigger)
        {
            if (trigger) Enemy.AttackEvent += FreezeSelf;   else Enemy.AttackEvent -= FreezeSelf;
            if (trigger) Enemy.EatEvent += Die;             else Enemy.EatEvent -= Die;
            if (trigger) Enemy.DieEvent += UnFreezeSelf;    else Enemy.DieEvent -= UnFreezeSelf;
        }

        void OnEnable()
        {
            speed = GetComponent<NavMeshAgent>().speed;
            aliveCount = GameObject.FindGameObjectsWithTag(mobTag).Length;
            Subscribe(true);
        }

        void OnDisable()
        {
            Subscribe(false);
        }

        void Update()
        {
            switch (state)
            {
                case States.Running:
                    if (isFreezed)
                    {
                        GetComponent<NavMeshAgent>().speed = 0;
                        state = States.Freezed;
                    }
                    break;

                case States.Freezed:
                    if (!isFreezed)
                    {
                        GetComponent<NavMeshAgent>().speed = speed;
                        state = States.Running;
                    }
                    break;

                case States.Dying:
                    Destroy(gameObject);
                    break;
            }
        }

        void FreezeSelf(ref Mob obj)
        {
            if (obj == this) isFreezed = true;
        }

        void UnFreezeSelf(ref Mob obj)
        {
            if (obj == this) isFreezed = false;
        }

        void Die(ref Mob obj)
        {
            if (obj == this) state = States.Dying;
        }
    }
}
