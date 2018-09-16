using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


namespace CMGCO.Unity.CustomGUI.Base
{


    abstract public class GameObjectPickerGUIBase<resultType> : NewCustomGUIBase<resultType>
         where resultType : NewCustomGUIResult<GameObject> // No idea if this will work
    {

        protected ValidationErrors[] validationArray = new ValidationErrors[] { ValidationErrors.NotGUITarget, ValidationErrors.HasRequiredComponent };

        protected override Type[] getArgumentTypes()
        {
            return new Type[] {
               typeof(resultType),
               typeof(GameObject),
               typeof(string)
            };
        }

        public resultType drawGUIControl(resultType currentResult, GameObject GUITargetGameObject, string lableString)
        {
            return base.drawGUIControl(new object[] { currentResult, GUITargetGameObject, lableString });
        }

        protected resultType drawGUIControlBody(resultType currentResult, GameObject GUITargetGameObject, string lableString)
        {
            GameObject currentGameObject = currentResult._resultValue;
            EditorGUI.BeginChangeCheck();
            GameObject newGameObject = EditorGUILayout.ObjectField(lableString, currentResult._resultValue, typeof(GameObject), true) as GameObject;
            if (GUILayout.Button("X", EditorStyles.miniButton, GUILayout.Width(22)))
            {
                newGameObject = null;
            }
            if (EditorGUI.EndChangeCheck())
            {
                if (validateGameObject(newGameObject, currentGameObject, GUITargetGameObject))
                {
                    return packageResult(newGameObject);
                }
            }
            return currentResult;
        }

        private bool validateGameObject(GameObject newGameObject, GameObject currentGameObject, GameObject GUITargetGameObject)
        {
            if (newGameObject == null) { return true; } // GameObject has been cleared 
            if (newGameObject == currentGameObject) { return false; }
            foreach (ValidationErrors validationError in this.validationArray)
            {
                switch (validationError)
                {
                    case ValidationErrors.NotGUITarget:
                        if (!this.validateNotSelf(GUITargetGameObject, newGameObject))
                        {
                            EditorUtility.DisplayDialog("Object Picker Error", this.getErrorMessage(ValidationErrors.NotGUITarget), "OK");
                            return false;
                        }
                        break;
                    case ValidationErrors.HasRequiredComponent:
                        if (!this.validateHasRequiredComponent(newGameObject))
                        {
                            EditorUtility.DisplayDialog("Object Picker Error", this.getErrorMessage(ValidationErrors.HasRequiredComponent), "OK");
                            return false;
                        }
                        break;
                }

            }
            return true;
        }

        // This function allows the extending object to package the result using the correct constructor without the need for more reflection 
        protected abstract resultType packageResult(GameObject result);

        private bool validateNotSelf(GameObject ownGameObject, GameObject newGameObject)
        {
            return ownGameObject != newGameObject;
        }
        protected abstract bool validateHasRequiredComponent(GameObject newGameObject);

        protected abstract string getErrorMessage(ValidationErrors validationError);

    }
}