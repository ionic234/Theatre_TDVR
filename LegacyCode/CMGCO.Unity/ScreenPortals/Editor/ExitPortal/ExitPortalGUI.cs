using UnityEngine;
using CMGCO.Unity.CustomGUI.Base;

namespace CMGCO.Unity.ScreenPortals
{

    [ExecuteInEditMode]
    public class ExitPortalGUI : ObjectPickerGUIBase
    {

        public static ValidationErrors[] validationArray = new ValidationErrors[] { ValidationErrors.NotGUITarget, ValidationErrors.HasRequiredComponent };

        private static readonly ExitPortalGUI instance = new ExitPortalGUI();

        public static ExitPortalGUI _instance
        {
            get
            {
                return instance;
            }

        }


        override protected bool validateHasRequiredComponent(GameObject newGameObject)
        {
            // Must have the script LinkedPortalGateway
            LinkedPortalGateway currentExitPortalScript = newGameObject.GetComponent<LinkedPortalGateway>();
            if (currentExitPortalScript)
            {
                return true;
            }
            return false;
        }

        override protected string getErrorMessage(ValidationErrors validationError)
        {
            switch (validationError)
            {
                case ValidationErrors.NotGUITarget:
                    return "A portal cannot have its exit portal set to itself";
                case ValidationErrors.HasRequiredComponent:
                    return "Exit Portal must have the script <LinkedPortalGateway> attached";
            }
            return ("Error Not Found");
        }
    }
}
