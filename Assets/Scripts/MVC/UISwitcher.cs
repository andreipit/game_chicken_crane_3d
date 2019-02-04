using UnityEngine;
using TCrane;

namespace TCrane
{
    public class UISwitcher : MonoBehaviour
    {
        //config
        public static UISwitcher self;
        void Awake() { self = this; }
        [Header("Load")]
        public Canvas loadCanvas;
        [Header("LevelStart")]
        public GameObject tapArrow;
        [Header("Play")]
        public Canvas playCanvas;
        public GameObject objects, environment;
        [Header("Final")]
        public Canvas finalCanvas;

        public void Switch(ApplicationView v, bool trigger)
        {
            if (v == LoadView.self)
            {
                loadCanvas.enabled = trigger;
            }
            else if (v == LevelStartView.self)
            {
                tapArrow.SetActive(trigger);
                playCanvas.enabled = trigger;
                environment.SetActive(trigger);
            }
            else if (v == PlayView.self)
            {
                playCanvas.enabled = trigger;
                objects.SetActive(trigger);
                environment.SetActive(trigger);
            }
            else if (v == FinalView.self)
            {
                finalCanvas.enabled = trigger;
            }
        }
    }
}
