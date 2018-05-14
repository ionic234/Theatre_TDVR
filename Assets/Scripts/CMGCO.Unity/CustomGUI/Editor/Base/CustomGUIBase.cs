using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


namespace CMGCO.Unity.CustomGUI.Base{

	[ExecuteInEditMode]

	abstract public class CustomGUIBase<DataIDType, DataValueType>  {		
		
		public CustomGUIResult<DataIDType, DataValueType> drawGUIControl(CustomGUIResult<DataIDType, DataValueType> currentResult, string lableString = "" ){
			this.drawGUIControlhead();
			CustomGUIResult<DataIDType, DataValueType> rData = this.drawGUIControlBody(currentResult, lableString );
			this.drawGUIControlFooter();
			return (rData);
		}

		protected virtual void drawGUIControlhead(){
			GUILayout.BeginHorizontal();
		}
		
		protected abstract CustomGUIResult<DataIDType, DataValueType> drawGUIControlBody(CustomGUIResult<DataIDType, DataValueType> currentDropDownResult, string lableString);
		
		protected virtual void drawGUIControlFooter(){
			GUILayout.EndHorizontal();
			GUILayout.Space(5);
		}


		
	}

}