using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCrane;

namespace TCrane
{
    public class FollowTo : MonoBehaviour
    {
        //configuration
        public Transform target;
        public bool trackingX, trackingY, trackingZ;

        //bookkeeping
        Vector3 offset;

        private void OnEnable()
        {
            offset = transform.position - target.position;
        }

        void Update()
        {
            if (trackingX) transform.position = new Vector3(target.position.x + offset.x,   transform.position.y,           transform.position.z);
            if (trackingY) transform.position = new Vector3(transform.position.x,           target.position.y + offset.y,   transform.position.z);
            if (trackingZ) transform.position = new Vector3(transform.position.x,           transform.position.y,           target.position.z + offset.z);
        }
    }
}