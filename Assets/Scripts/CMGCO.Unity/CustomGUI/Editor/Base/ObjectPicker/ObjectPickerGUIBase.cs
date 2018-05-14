using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using CMGCO.Unity.CustomGUI.Base;

namespace CMGCO.Unity.CustomGUI.Base{


	abstract public class ObjectPickerGUIBase : CustomGUIBase<int,GameObject>   {


		public CustomGUIResult<int, GameObject> drawGUIControl(CustomGUIResult<int, GameObject> currentResult, string lableString, GameObject GUITargetGameObject,   ValidationErrors[] validationArray){
			CustomGUIResult<int, GameObject> newResult = this.drawGUIControl(currentResult, lableString);
			if (this.validateGameObject( newResult.resultValue, currentResult.resultValue, GUITargetGameObject, validationArray)){
				return newResult;
			}else{
				return currentResult;
			}
		} 
		
		protected override CustomGUIResult<int, GameObject> drawGUIControlBody(CustomGUIResult<int, GameObject> currentResult, string lableString){		
			CustomGUIResult <int, GameObject> result = new CustomGUIResult <int, GameObject> (0, EditorGUILayout.ObjectField(lableString, currentResult.resultValue, typeof(GameObject), true) as GameObject);
			if (GUILayout.Button("X",EditorStyles.miniButton, GUILayout.Width(22))){
				result.resultValue = null;
			}						
			return result;
		}

		private bool validateGameObject(GameObject newGameObject, GameObject currentGameObject, GameObject GUITargetGameObject ,ValidationErrors[] validationArray ){
			if (newGameObject == null){return true;}
			if (newGameObject == currentGameObject){return false;}
			foreach (ValidationErrors validationError in validationArray){
				switch (validationError){
					case ValidationErrors.NotGUITarget:
						if (!this.validateNotSelf(GUITargetGameObject, newGameObject )){
							EditorUtility.DisplayDialog("Object Picker Error", this.getErrorMessage(ValidationErrors.NotGUITarget), "OK");
							return false;
						}
					break;
					case ValidationErrors.HasScript:
						if (!this.validateHasScript(newGameObject)){
							EditorUtility.DisplayDialog("Object Picker Error", this.getErrorMessage(ValidationErrors.HasScript), "OK");
							return false;
						}
					break;
				}
			}
			return true;
		}

		private bool validateNotSelf(GameObject ownGameObject, GameObject newGameObject ){
			return ownGameObject != newGameObject;
		}
		protected abstract bool validateHasScript(GameObject newGameObject);
		protected abstract string getErrorMessage(ValidationErrors validationError );


	}
}