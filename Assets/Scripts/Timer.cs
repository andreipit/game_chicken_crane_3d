using UnityEngine.UI;
using UnityEngine;
using System;
using TCrane;

namespace TCrane
{
    public class Timer : MonoBehaviour
    {
        //config
        Text text;
        public static Timer self;

        //state
        public static int timePassed;

        //bookkeeping
        float startTime;

        void OnEnable()
        {
            self = this;
        }

        public void Run()
        {
            text = GetComponent<Text>();
            startTime = Time.time;
        }

        void Update()
        {
            if (startTime != 0)
            {
                timePassed = Convert.ToInt32(Time.time - startTime);
                text.text = timePassed.ToString();
            }
        }
    }
}
