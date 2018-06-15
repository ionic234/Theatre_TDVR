using UnityEngine;
using CMGCO.Unity.CustomGUI.Base;

namespace CMGCO.Unity.ScreenPortals
{


    [ExecuteInEditMode]
    public class TransitionObjectGUI : ObjectPickerGUIBase
    {


        public static ValidationErrors[] validationArray = new ValidationErrors[] { ValidationErrors.NotGUITarget };

        private static readonly TransitionObjectGUI instance = new TransitionObjectGUI();

        public static TransitionObjectGUI _instance
        {
            get
            {
                return instance;
            }

        }

        override protected string getErrorMessage(ValidationErrors validationError)
        {
            // Need to change this

            switch (validationError)
            {
                case ValidationErrors.NotGUITarget:
                    return "A portal cannot have its exit portal set to itself";

            }
            return ("Error Not Found");
        }

    }
}
