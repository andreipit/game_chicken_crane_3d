using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TCrane;

namespace TCrane
{
    public class FinalView : ApplicationView
    {
        //Common===================================================================
        public static FinalView self;
        void Awake() { self = this; }
        public override void BeforeView() { Subscribe(true); ShowScores(); }
        public override void AfterView() {  }
        public override void DuringView() { }
        //==========================================================================

        //config
        public Button replayBttn;
        public Text bestScores;
        public Text currentScores;

        //state
        public static bool was;

        //bookkeeping
        public int @record;

        void Subscribe(bool trigger)
        {
            if (trigger) replayBttn.onClick.AddListener(StartGame);
            else replayBttn.onClick.RemoveListener(StartGame);
        }

        void ShowScores()
        {
            if (Timer.timePassed > @record) GameController.self.UpdateRecord(Timer.timePassed);
            bestScores.text = @record.ToString();
            currentScores.text = Timer.timePassed.ToString();
        }

        void StartGame()
        {
            was = true;
            Subscribe(false);
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }
    }
}
            
