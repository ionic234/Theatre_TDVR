using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using CMGCO.Unity.CustomGUI.Base;
using CMGCO.Unity.ScreenPortals;

namespace CMGCO.Unity.CustomGUI.ExitPortal{

	[ExecuteInEditMode]
	public class ExitPortalGUI : ObjectPickerGUIBase {

		public static ValidationErrors[] validationArray = new ValidationErrors[]{ValidationErrors.NotGUITarget, ValidationErrors.HasScript}; 

		private static readonly ExitPortalGUI instance = new ExitPortalGUI();
		
		public static ExitPortalGUI _instance{
			get{
				return instance;
			}

		}


		override protected bool validateHasScript(GameObject newGameObject){		
			LinkedPortalGateway currentExitPortalScript = newGameObject.GetComponent<LinkedPortalGateway>();
			if (currentExitPortalScript){
				return true;
			}
			return false;
		}

		override protected string getErrorMessage(ValidationErrors validationError){
			switch (validationError){
				case ValidationErrors.NotGUITarget:
					return "A portal cannot have its exit portal set to itself";
				case ValidationErrors.HasScript:
					return "Exit Portal must have the script <LinkedPortalGateway> attached";
			}
			return ("Error Not Found");
		}		
	}
}
