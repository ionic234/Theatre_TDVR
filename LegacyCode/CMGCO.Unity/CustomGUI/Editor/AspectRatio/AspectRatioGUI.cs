using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using CMGCO.Unity.CustomGUI.Base;

namespace CMGCO.Unity.CustomGUI.AspectRatio
{

    [ExecuteInEditMode]

    public enum AspectRatioIDs { TV, WideTV, WidePC, UltraWide, CinemaScope, Anamorphic };

    public class AspectRatioGUI : NewCustomGUIBase<AspectRatioResult>
    {

        // Should this belong to the result class???
        public static Dictionary<AspectRatioIDs, DropDownItem<Vector2>> dropDownDictionary = new Dictionary<AspectRatioIDs, DropDownItem<Vector2>>{
            {AspectRatioIDs.TV,  new DropDownItem<Vector2>("TV (4:3)",  new Vector2(1.33f, 1))},
            {AspectRatioIDs.WideTV,  new DropDownItem<Vector2>("Widescreen TV (16:9)",  new Vector2(1.78f, 1))},
            {AspectRatioIDs.WidePC,  new DropDownItem<Vector2>("Widescreen PC (16:10)",  new Vector2(1.61f, 1))},
            {AspectRatioIDs.UltraWide,  new DropDownItem<Vector2>("Ultra Widescreen (21:9)",  new Vector2(2.37f, 1))},
            {AspectRatioIDs.CinemaScope,  new DropDownItem<Vector2>("CinemaScope (2.35:1)",  new Vector2(2.35f, 1))},
            {AspectRatioIDs.Anamorphic,  new DropDownItem<Vector2>("Anamorphic (2.39:1)",  new Vector2(2.39f, 1))},
        };

        private static readonly AspectRatioGUI instance = new AspectRatioGUI();
        public static AspectRatioGUI _instance
        {
            get
            {
                return instance;
            }
        }
        private AspectRatioGUI() { }

        public AspectRatioResult drawGUIControl(AspectRatioResult currentResult, string lableString = "Aspect Ratio")
        {
            return base.drawGUIControl(new object[] { currentResult, lableString });
        }



        protected AspectRatioResult drawGUIControlBody(AspectRatioResult currentResult)
        {


            GUILayout.Label("Aspect Ratio", GUILayout.Width(EditorGUIUtility.labelWidth - 4));
            float controlSpaceWidth = (EditorGUIUtility.currentViewWidth - 28) - EditorGUIUtility.labelWidth; //45



            /* 
            AspectRatioIDs selectedRatioID = (AspectRatioIDs)EditorGUILayout.Popup(
                (int)currentDropDownResult.resultID,
                this.dropDownLabels,
                GUILayout.Width((controlSpaceWidth / 5) * 3)
            );

            Vector2 selectedRatioValue = (Vector2)this.dropDownValues.GetValue((int)selectedRatioID);

            // Dispalay the actual value; 			
            GUILayout.Label("Actual:", CustomEditorStyles._instance._rightAlign, GUILayout.Width(controlSpaceWidth / 5));
            GUI.enabled = false;
            GUILayout.TextField(selectedRatioValue.x.ToString() + ":" + selectedRatioValue.y.ToString(), GUILayout.Width(controlSpaceWidth / 5));
            GUI.enabled = true;

            return (new CustomGUIResult<AspectRatioIDs, Vector2>(selectedRatioID, selectedRatioValue));
			*/

            return null;

        }
    }



    /* 
	public class AspectRatioGUI : DropDownGUIBase<AspectRatioIDs, Vector2> {

		private static readonly AspectRatioGUI instance = new AspectRatioGUI();
		public static AspectRatioGUI _instance{
			get{
				return instance;
			}
		}

		private Dictionary<AspectRatioIDs, DropDownItem<Vector2>> dropDownDictionary = new Dictionary<AspectRatioIDs, DropDownItem<Vector2>>{
			{AspectRatioIDs.TV,  new DropDownItem<Vector2>("TV (4:3)",  new Vector2(1.33f, 1))},
			{AspectRatioIDs.WideTV,  new DropDownItem<Vector2>("Widescreen TV (16:9)",  new Vector2(1.78f, 1))},
			{AspectRatioIDs.WidePC,  new DropDownItem<Vector2>("Widescreen PC (16:10)",  new Vector2(1.61f, 1))},
			{AspectRatioIDs.UltraWide,  new DropDownItem<Vector2>("Ultra Widescreen (21:9)",  new Vector2(2.37f, 1))},
			{AspectRatioIDs.CinemaScope,  new DropDownItem<Vector2>("CinemaScope (2.35:1)",  new Vector2(2.35f, 1))},
			{AspectRatioIDs.Anamorphic,  new DropDownItem<Vector2>("Anamorphic (2.39:1)",  new Vector2(2.39f, 1))},	
		};

		protected override  Dictionary<AspectRatioIDs, DropDownItem<Vector2>> _dropDownDictionary{
			get{
				return this.dropDownDictionary;
			}
		} 
		
		protected override CustomGUIResult<AspectRatioIDs, Vector2>  drawGUIControlBody( CustomGUIResult<AspectRatioIDs, Vector2> currentDropDownResult, string lableString ){
			
			GUILayout.Label("Aspect Ratio", GUILayout.Width(EditorGUIUtility.labelWidth - 4) );
			float controlSpaceWidth = (EditorGUIUtility.currentViewWidth - 28) -  EditorGUIUtility.labelWidth; //45

			AspectRatioIDs selectedRatioID =  (AspectRatioIDs)EditorGUILayout.Popup(
				(int)currentDropDownResult.resultID,
				this.dropDownLabels, 
				GUILayout.Width((controlSpaceWidth /5) * 3 )
			);	

			Vector2 selectedRatioValue =  (Vector2)this.dropDownValues.GetValue((int)selectedRatioID);

			// Dispalay the actual value; 			
			GUILayout.Label("Actual:", CustomEditorStyles._instance._rightAlign ,GUILayout.Width( controlSpaceWidth /5 ));
			GUI.enabled = false;
			GUILayout.TextField(selectedRatioValue.x.ToString() + ":" + selectedRatioValue.y.ToString(), GUILayout.Width(controlSpaceWidth /5));
			GUI.enabled = true;
			
			return (new CustomGUIResult<AspectRatioIDs, Vector2> (selectedRatioID,selectedRatioValue ));
		}
	}
	*/
}
