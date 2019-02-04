using UnityEngine;
using System.Collections;
using TCrane;

namespace TCrane
{
    public class ApplicationController : MonoBehaviour
    {
        public static ApplicationView view;
        public static ApplicationView lastView;

        public virtual void RespondTo(ApplicationView nextView)
        {
            //Debug.Log(view);
            if (view != null)
            {
                view.AfterView();
                UISwitcher.self.Switch(view, false);
            }
            lastView = view;
            view = nextView;

            UISwitcher.self.Switch(view, true);
            view.BeforeView();

        }

        public virtual void BeforeAction()
        {

        }

        public virtual void DuringAction()
        {

        }

        public virtual void AfterAction()
        {
        
        }

    }
}
