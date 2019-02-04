using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TCrane;

namespace TCrane
{
    public class LevelStartView : ApplicationView
    {
        //Common===================================================================
        public static LevelStartView self;
        void Awake() { self = this; }
        public override void BeforeView() { }
        public override void AfterView()  { }
        //==========================================================================

        public override void DuringView()
        {
            if (InputLogic.self.pressed) StartGame();
        }

        void StartGame()
        {
            Routes.self.Goto(GameController.self, GameController.self.Play);
        }
    }
}
            
