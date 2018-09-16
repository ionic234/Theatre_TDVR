using UnityEngine;
using CMGCO.Unity.CustomGUI.Base;
using System;
using UnityEditor;

namespace CMGCO.Unity.ScreenPortals
{

    [ExecuteInEditMode]
    public class NewExitPortalGUI : GameObjectPickerGUIBase<ExitPortalResult>
    {

        private static readonly NewExitPortalGUI instance = new NewExitPortalGUI();
        public static NewExitPortalGUI _instance
        {
            get
            {
                return instance;
            }
        }
        private NewExitPortalGUI() { }

        protected override bool validateHasRequiredComponent(GameObject newGameObject)
        {
            // Must have the script LinkedPortalGateway
            LinkedPortalGateway currentExitPortalScript = newGameObject.GetComponent<LinkedPortalGateway>();
            if (currentExitPortalScript)
            {
                return true;
            }
            return false;
        }

        protected override string getErrorMessage(ValidationErrors validationError)
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
        protected override ExitPortalResult packageResult(GameObject result)
        {
            if (result != null)
            {
                return new ExitPortalResult(result, result.GetComponent<LinkedPortalGateway>(), true);
            }
            else
            {
                return new ExitPortalResult(null, null, true);
            }
        }
    }
}