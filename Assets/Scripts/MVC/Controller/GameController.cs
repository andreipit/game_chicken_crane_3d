using TCrane;

namespace TCrane
{
    public class GameController : ApplicationController
    {
        //Common===================================================================
        public static GameController self;
        void Awake() { self = this; }
        public override void BeforeAction()    { }
        public override void DuringAction()    { view.DuringView(); }
        public override void AfterAction()     { }
        //==========================================================================
    
        public void LevelStart()
        {
            RespondTo(LevelStartView.self);
        }
    
        public void Play()
        {
            RespondTo(PlayView.self);
        }

        public void Final()
        {
            FinalView.self.@record = UserModel.self.GetRecord();
            RespondTo(FinalView.self);
        }

        //db queries

        public void UpdateRecord(int value)
        {
            UserModel.self.SetRecord(value);
            FinalView.self.@record = value;
        }
    }
}
