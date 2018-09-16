using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



using CMGCO.Unity.CustomGUI.Base;


namespace CMGCO.Unity.CustomGUI.AnchoredWidthHeight{

	public enum  WidthHeightAnchors {WIDTH, HEIGHT}

	public class AnchoredWidthHeightGUI : CustomGUIBase<WidthHeightAnchors, Vector2> {

		private static readonly AnchoredWidthHeightGUI instance = new AnchoredWidthHeightGUI();
		public static AnchoredWidthHeightGUI _instance{
			get{
				return instance;
			}
		}
		protected override CustomGUIResult<WidthHeightAnchors, Vector2> drawGUIControlBody(CustomGUIResult<WidthHeightAnchors, Vector2> currentResult,  string lableString = ""   ){

			Vector2 currentVector = (Vector2)currentResult.resultValue; 
			Vector2 newVector = new Vector2();

			GUILayout.Label("Size", GUILayout.Width(EditorGUIUtility.labelWidth - 4) );
			float charWidth = 14;
			float controlSpaceWidth = (EditorGUIUtility.currentViewWidth - 31) -  EditorGUIUtility.labelWidth; 

			if (currentResult.resultID == WidthHeightAnchors.WIDTH){
				GUILayout.Label("W", CustomEditorStyles._instance._bold, GUILayout.Width(charWidth));
			}else{
				GUILayout.Label("W", GUILayout.Width(charWidth));
			}

			newVector.x = EditorGUILayout.FloatField(currentVector.x,  GUILayout.Width((controlSpaceWidth /2) - charWidth));

			if (currentResult.resultID == WidthHeightAnchors.HEIGHT){
				GUILayout.Label("H", CustomEditorStyles._instance._bold, GUILayout.Width(charWidth));
			}else{
				GUILayout.Label("H", GUILayout.Width(charWidth));
			}

			newVector.y = EditorGUILayout.FloatField(currentVector.y, GUILayout.Width((controlSpaceWidth /2) - charWidth));

			if (!newVector.Equals(currentVector)){

				WidthHeightAnchors currentAnchor; 
				if (newVector.x != currentVector.x){
					currentAnchor = WidthHeightAnchors.WIDTH;
				}else{
					currentAnchor = WidthHeightAnchors.HEIGHT;
				}
				return new CustomGUIResult<WidthHeightAnchors, Vector2> (currentAnchor, newVector);

			}else{
				return currentResult;
			}

		}

	}
}
