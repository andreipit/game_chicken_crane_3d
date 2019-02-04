using TCrane;

namespace TCrane
{
    public class MenuController : ApplicationController
    {
        //Common====================================================================
        public static MenuController self;
        void Awake() { self = this; }
        public override void BeforeAction()    { }
        public override void DuringAction()    { view.DuringView(); }
        public override void AfterAction()     { }
        //==========================================================================

        public void Load()
        {
            RespondTo(LoadView.self);
        }
    }
}