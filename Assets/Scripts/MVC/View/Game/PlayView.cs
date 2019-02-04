using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TCrane;
using System.Linq;

namespace TCrane
{
    public class PlayView : ApplicationView
    {
        //Common===================================================================
        public static PlayView self;
        void Awake() { self = this; }
        public override void BeforeView() { Timer.self.Run(); }
        public override void AfterView()  { }
        //==========================================================================

        public override void DuringView() 
        {
            if (GameObject.FindGameObjectWithTag(Mob.mobTag) == null || Input.GetKeyDown(KeyCode.Space))
            {
                Routes.self.Goto(GameController.self, GameController.self.Final);
            }
        }
    }
}
