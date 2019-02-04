using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCrane;

namespace TCrane
{
    public class PlayerMove : MonoBehaviour
    {
        //config
        public Transform bridge, carriage;
        public Rigidbody rb;
        const float speed = 100;

        void Update()
        {
            if (InputLogic.self.pressed)
            {
                Vector3 norm = InputLogic.self.dir.normalized * speed;
                rb.AddForce(norm.x, 0, norm.y, ForceMode.Acceleration);
            }
        }
    }
}
