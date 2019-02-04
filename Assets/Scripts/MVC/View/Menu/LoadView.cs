using UnityEngine.UI;
using TCrane;

namespace TCrane
{
    public class LoadView : ApplicationView
    {
        //Common===================================================================
        public static LoadView self;
        void Awake() { self = this; }
        public override void BeforeView() { Subscribe(true); }
        public override void AfterView() { Subscribe(false); }
        public override void DuringView() { }
        //==========================================================================

        //config
        public Button playBttn;

        void Subscribe(bool trigger)
        {
            if (trigger) playBttn.onClick.AddListener(PlayBttn);
            else playBttn.onClick.RemoveListener(PlayBttn);
        }

        void PlayBttn()
        {
            Routes.self.Goto(GameController.self, GameController.self.LevelStart);
        }
    }
}
