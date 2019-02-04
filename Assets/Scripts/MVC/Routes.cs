using UnityEngine;
using TCrane;

namespace TCrane
{
    public class Routes : MonoBehaviour
    {
        //config
        public static Routes self;

        //state
        public ApplicationController controller;
        public string actionName;

        //bookkeeping
        public delegate void Action();
        public ApplicationController lastController;

        void Awake() {
            self = this;
        }

        void Start () {
            if (!FinalView.was) Goto(MenuController.self, MenuController.self.Load);
            else Goto(GameController.self, GameController.self.LevelStart);
        }

        public void Goto(ApplicationController nextController, Action nextAction)
        {
            if (controller != null)controller.AfterAction();
            lastController = controller;
            controller = nextController;
            controller.BeforeAction();

            nextAction();
            actionName = nextAction.Method.Name;
        }

        void Update() {
            controller.DuringAction();
        }
    }
}