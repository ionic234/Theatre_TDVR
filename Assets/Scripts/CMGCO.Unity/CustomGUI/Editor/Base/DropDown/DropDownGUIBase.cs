using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


namespace CMGCO.Unity.CustomGUI.Base
{

    [ExecuteInEditMode]

    abstract public class DropDownGUIBase<DataIDType, DataValueType> : CustomGUIBase<DataIDType, DataValueType>
    //where DataIDType : struct,  IComparable, IFormattable, IConvertible
    {
        protected abstract Dictionary<DataIDType, DropDownItem<DataValueType>> _dropDownDictionary { get; }
        protected string[] dropDownLabels;
        protected DataValueType[] dropDownValues;

        protected DropDownGUIBase()
        {

            if (!typeof(DataIDType).IsEnum)
            {
                throw new Exception("DropDownGUIBase: DataIDType must be an Enum");
            }

            if (this._dropDownDictionary.Count() == 0)
            {
                throw new Exception("DropDownGUIBase: dropDownDictionary cannot be empty");
            }

            this.dropDownLabels = this._dropDownDictionary.Select(x => x.Value.itemLabel).ToArray();
            this.dropDownValues = this._dropDownDictionary.Select(x => x.Value.itemValue).ToArray();
        }
        public CustomGUIResult<DataIDType, DataValueType> getDropDownResultFromValue(DataValueType value)
        {
            KeyValuePair<DataIDType, DropDownItem<DataValueType>> dictionaryKeyValue = this._dropDownDictionary.FirstOrDefault(x => x.Value.itemValue.Equals(value));
            if (dictionaryKeyValue.Value != null)
            {
                return new CustomGUIResult<DataIDType, DataValueType>(dictionaryKeyValue.Key, dictionaryKeyValue.Value.itemValue);
            }
            else
            {
                // If value is not found it will give us back the first id, so also get the first value. 

                // CHANGE THIS TO ASK CALL GET DEFALT FUNCTION

                return new CustomGUIResult<DataIDType, DataValueType>(dictionaryKeyValue.Key, this.dropDownValues[0]);
            }
        }

        protected override CustomGUIResult<DataIDType, DataValueType> drawGUIControlBody(CustomGUIResult<DataIDType, DataValueType> currentDropDownResult, string lableString)
        {

            // Very messy to get round c# non support of generic Enums	

            GUILayout.Label(lableString);
            int selectedInt = EditorGUILayout.Popup(
                (int)Convert.ChangeType(currentDropDownResult.resultID, Enum.GetUnderlyingType(currentDropDownResult.resultID.GetType())),
                this.dropDownLabels
            );

            return new CustomGUIResult<DataIDType, DataValueType>((DataIDType)Enum.Parse(typeof(DataIDType), selectedInt.ToString(), true), this.dropDownValues[selectedInt]);
        }

    }




























    /*	
    abstract public DropDownGUIBase<DataIDType, DataValueType>

    public string testData = "Inside Drop Down GUI Base";
    public string testData2 = "Base is smelly";

    public virtual void printVars(){

        Debug.Log(_instance);
        Debug.Log(testData);
        Debug.Log(testData2);
    }
    */


    /* 
	public abstract class DropDownGUIBase<DataIDType, DataValueType> : Editor {

		
		static private Dictionary<DataIDType, DropDownItem<DataValueType>> dropDownDictionary = new Dictionary<DataIDType, DropDownItem<DataValueType>>{};
		

		
		
		
		static private string[] dropDownLabels=  dropDownDictionary.Select(x => x.Value.itemLabel).ToArray();
		static private DataValueType[] drowDownValues = dropDownDictionary.Select(x => x.Value.itemValue).ToArray();
		static public DropDownResult<DataIDType, DataValueType> getDropDownResultFromValue(DataValueType value){
			
			Debug.Log(dropDownDictionary.Count);
			
			KeyValuePair<DataIDType, DropDownItem<DataValueType>> dictionaryKeyValue = dropDownDictionary.FirstOrDefault( x => x.Value.itemValue.Equals(value)); 	
			Debug.Log("What the hell chuck");
			DropDownResult<DataIDType, DataValueType> rData = new DropDownResult<DataIDType, DataValueType>(dictionaryKeyValue.Key, dictionaryKeyValue.Value.itemValue);
			Debug.Log(rData);
			return rData;
		}
	}
	*/
}
















/*

using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace CMGCO.Unity.CustomGUI{
	
	[ExecuteInEditMode]
	
	public class AspectRatioGUI : Editor {
		
		public enum AspectRatioName {TV, WideTV, WidePC, UltraWide, CinemaScope, Anamorphic};
		static private Dictionary<AspectRatioName, AspectRatioData> validAspectRatios = new Dictionary<AspectRatioName, AspectRatioData>{
			{AspectRatioName.TV,  new AspectRatioData("TV (4:3)",  new Vector2(1.33f, 1))},
			{AspectRatioName.WideTV,  new AspectRatioData("Widescreen TV (16:9)",  new Vector2(1.78f, 1))},
			{AspectRatioName.WidePC,  new AspectRatioData("Widescreen PC (16:10)",  new Vector2(1.61f, 1))},
			{AspectRatioName.UltraWide,  new AspectRatioData("Ultra Widescreen (21:9)",  new Vector2(2.37f, 1))},
			{AspectRatioName.CinemaScope,  new AspectRatioData("CinemaScope (2.35:1)",  new Vector2(2.35f, 1))},
			{AspectRatioName.Anamorphic,  new AspectRatioData("Anamorphic (2.39:1)",  new Vector2(2.39f, 1))},	
		};
		
		static private string[] ratioLabels =  validAspectRatios.Select(x => x.Value.ratioDescription).ToArray();
		static private Vector2[] ratioValues =  validAspectRatios.Select(x => x.Value.ratioValue).ToArray();


		static public KeyValuePair<AspectRatioName, Vector2> getScreenRatioData(Vector2 value){
			KeyValuePair<AspectRatioName,AspectRatioData> dictionaryItem = validAspectRatios.FirstOrDefault(x => x.Value.ratioValue.Equals(value)); 
			return (new KeyValuePair<AspectRatioName, Vector2>(dictionaryItem.Key, dictionaryItem.Value.ratioValue));
		}
		static public KeyValuePair<AspectRatioName, Vector2> drawGUIControl( KeyValuePair<AspectRatioName, Vector2> currentScreenRatioData){
			
			GUILayout.BeginHorizontal();
			GUILayout.Label("Aspect Ratio", GUILayout.Width(EditorGUIUtility.labelWidth - 4) );
			float controlSpaceWidth = (EditorGUIUtility.currentViewWidth - 28) -  EditorGUIUtility.labelWidth; //45

			AspectRatioName selectedRatioName = (AspectRatioName)EditorGUILayout.Popup(
				(int)currentScreenRatioData.Key,
				AspectRatioGUI.ratioLabels, 
				GUILayout.Width((controlSpaceWidth /5) * 3 )
			);

			Vector2 selectedRatioValue =  (Vector2)AspectRatioGUI.ratioValues.GetValue((int)selectedRatioName);

			// Dispalay the actual value; 			
			GUILayout.Label("Actual:", CustomEditorStyles._instance._rightAlign ,GUILayout.Width( controlSpaceWidth /5 ));
			GUI.enabled = false;
			GUILayout.TextField(selectedRatioValue.x.ToString() + ":" + selectedRatioValue.y.ToString(), GUILayout.Width(controlSpaceWidth /5));
			GUI.enabled = true;
			GUILayout.EndHorizontal();
			GUILayout.Space(5);

			Debug.Log("boom");


			DropDownResult<AspectRatioName, Vector2> resultTest =  new DropDownResult<AspectRatioName, Vector2>(selectedRatioName,selectedRatioValue);

			return (new KeyValuePair<AspectRatioName, Vector2> (selectedRatioName,selectedRatioValue ));

		}	
	}
}



 */
