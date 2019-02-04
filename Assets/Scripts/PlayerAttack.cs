using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TCrane;

namespace TCrane
{
    public class PlayerAttack : MonoBehaviour
    {
        //config
        public Rigidbody rb;
        float speedDown = 150f, speedUp = 0.5f;

        //bookkeeping
        float startPosY;
        float lastAttackTime;

        public delegate void Messenger(Enemy enemy);
        public static event Messenger AttackEvent = (Enemy enemy) => { };

        void Subscribe(bool trigger)
        {
            if (trigger) InputLogic.OnPointerUpEvent += Attack; else InputLogic.OnPointerUpEvent -= Attack;
        }

        void OnEnable()
        {
            Subscribe(true);
            startPosY = rb.transform.position.y;
        }

        void OnDisable()
        {
            Subscribe(false);
        }

        void Attack()
        {
            rb.AddForce(0, -speedDown, 0, ForceMode.Impulse);
        }

        void OnTriggerEnter(Collider other)
        {
            switch (other.tag)
            {
                case Enemy.enemyTag:
                    AttackEvent(other.GetComponent<Enemy>());
                    break;
            }
            rb.AddForce(0, speedDown * 0.3f, 0, ForceMode.Impulse);
            StartCoroutine(AttackBack());
        }

        IEnumerator AttackBack()
        {
            while (true)
            {
                rb.isKinematic = true;
                Vector3 pos = rb.transform.position;
                rb.transform.position = new Vector3(pos.x, pos.y + speedUp, pos.z);
                yield return new WaitForEndOfFrame();
                if (pos.y >= startPosY)
                {
                    rb.isKinematic = false;
                    yield break;
                }
            }
        }
    }
}
