using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TCrane;
using System.Linq;

namespace TCrane
{
    public class Enemy : MonoBehaviour
    {
        //config
        NavMoveTo navMoveTo;
        Animator anim;
        public const string enemyTag = "Enemy";
        public const string freezeParameter = "freeze";

        //state
        public enum States { Run, Attack, Eating, Death };
        public States state;
        bool isNearMob;

        //bookkeeping
        Mob currentMob;
        float lastAttackTime;
        Transform lastParent;
        static int lastSpeed = 3;

        public delegate void Messenger(ref Mob victimMob);
        public static event Messenger AttackEvent = (ref Mob victim) => { };
        public static event Messenger EatEvent = (ref Mob victim) => { };
        public static event Messenger DieEvent = (ref Mob victim) => { };

        void Subscribe(bool trigger)
        {
            if (trigger) PlayerAttack.AttackEvent += Die; else PlayerAttack.AttackEvent -= Die;
        }

        void OnEnable()
        {
            navMoveTo = GetComponent<NavMoveTo>();
            anim = GetComponent<Animator>();
            IncreaseSpeed();
            Subscribe(true);
            lastParent = transform.parent.transform;
            state = States.Run;
        }

        void Update()
        {
            switch (state)
            {
                case States.Run:
                    if (navMoveTo.goal == null) FindGoal();
                    if (isNearMob)
                    {
                        isNearMob = false;
                        state = States.Attack;
                    }
                    break;
                case States.Attack:
                    AttackEvent(ref currentMob);
                    PlaceOnMob(currentMob, true);
                    lastAttackTime = Time.time;
                    state = States.Eating;
                    break;
                case States.Eating:
                    if (Time.time - lastAttackTime > 3)
                    {
                        PlaceOnMob(currentMob, false);
                        EatEvent(ref currentMob);
                        state = States.Run;
                    }
                    break;
                case States.Death:
                    Subscribe(false);
                    DieEvent(ref currentMob);
                    Destroy(gameObject);
                    break;
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (state == States.Run && other.tag == Mob.mobTag && !other.GetComponent<Mob>().isFreezed)
            {
                isNearMob = true;
                currentMob = other.GetComponent<Mob>();
            }
        }

        void PlaceOnMob(Mob mob, bool trigger)
        {
            if (trigger)
            {
                navMoveTo.goal = currentMob.transform;
                transform.parent = mob.transform;
                transform.localPosition = Vector3.up*-0.1f;
            }
            else
            {
                transform.parent = lastParent;
            }
            FreezeSelf(trigger);
        }

        void FreezeSelf(bool trigger)
        {
            navMoveTo.enabled = !trigger;
            GetComponent<Rigidbody>().isKinematic = trigger;
            GetComponent<NavMeshAgent>().enabled = !trigger;
            anim.SetBool(freezeParameter, trigger);
        }

        void FindGoal()
        {
            GameObject goal = GameObject.FindGameObjectWithTag(Mob.mobTag);
            if (goal != null) navMoveTo.goal = goal.transform;
        }

        void Die(Enemy obj)
        {
            if (obj == this) state = States.Death;
        }

        void IncreaseSpeed()
        {
            lastSpeed += 3;
            GetComponent<NavMeshAgent>().speed = lastSpeed;
        }
    }
}
