using UnityEditor;
using UnityEngine;

using CMGCO.Unity.ScreenPortals;
using System.Collections.Generic;

namespace CMGCO.Unity.CustomGUI.Base
{

    public enum ResultType { NO_CHANGE, ITEM_REMOVED, ITEM_INSERTED, ITEM_CHANGED };

    public class ObjectPickerListGUIBase : CustomGUIBase<ResultType, SerializedProperty>
    {

        private static readonly ObjectPickerListGUIBase instance = new ObjectPickerListGUIBase();
        public static ObjectPickerListGUIBase _instance
        {
            get
            {
                return instance;
            }
        }


        public CustomGUIResult<ResultType, SerializedProperty> drawGUIControl(CustomGUIResult<ResultType, SerializedProperty> currentResult, string lableString, GameObject GUITargetGameObject, ValidationErrors[] validationArray)
        {
            this.drawGUIControlhead();
            CustomGUIResult<ResultType, SerializedProperty> newResult = this.drawGUIControlBody(currentResult, lableString, GUITargetGameObject, validationArray);
            this.drawGUIControlFooter();
            // Some kind of validation;


            return newResult; //<-- wrong

            // Do array wide validation 
            // if (this.validateList(newResult.resultValue, currentResult.resultValue, GUITargetGameObject, validationArray))
            // {
            //     return newResult;
            // }
            // else
            // {
            //     return currentResult;
            // }
        }



        protected override CustomGUIResult<ResultType, SerializedProperty> drawGUIControlBody(CustomGUIResult<ResultType, SerializedProperty> currentResult, string lableString)
        {


            // No validation. using the base drawGUIControl not the one above. 
            SerializedProperty currentList = currentResult.resultValue;
            int objCount = currentList.arraySize;
            if (EditorGUILayout.PropertyField(currentList, new GUIContent(lableString), false))
            {

            }


            return null;
        }

        protected CustomGUIResult<ResultType, SerializedProperty> drawGUIControlBody(CustomGUIResult<ResultType, SerializedProperty> currentResult, string lableString, GameObject GUITargetGameObject, ValidationErrors[] validationArray)
        {
            Debug.Log("this one has validation");

            // with validation 
            SerializedProperty currentList = currentResult.resultValue;
            int objCount = currentList.arraySize;
            if (EditorGUILayout.PropertyField(currentList, new GUIContent(lableString), false))
            {
                // is expanded so draw children.
                GameObject obj;
                for (var i = 0; i < currentList.arraySize; i++)
                {
                    obj = (GameObject)currentList.GetArrayElementAtIndex(i).objectReferenceValue;


                    //CustomGUIResult<int, GameObject> result = TransitionObjectGUI._instance.drawGUIControl(new CustomGUIResult<int, GameObject>(i, obj), obj.name, this.myLinkedPortalGateway.gameObject, TransitionObjectGUI.validationArray);

                }


            }




            //this.drawGUIControlBody(currentResult, lableString);
            return null;
        }


        // private bool validateList(List<GameObject> currentList, GameObject currentGameObject, GameObject GUITargetGameObject, ValidationErrors[] validationArray)
        // {
        //     return null;
        // 

    }


    /* 
      public CustomGUIResult<int, GameObject> drawGUIControl(CustomGUIResult<int, GameObject> currentResult, string lableString, GameObject GUITargetGameObject, ValidationErrors[] validationArray)        {
         CustomGUIResult<int, GameObject> newResult = base.drawGUIControl(currentResult, lableString);

         if (this.validateGameObject(newResult.resultValue, currentResult.resultValue, GUITargetGameObject, validationArray))
         {
             return newResult;
         }
         else
         {
             return currentResult;
         }
     }


     public CustomGUIResult<ResultType, SerializedProperty> drawGUIControl(CustomGUIResult<ResultType, SerializedProperty> currentResult, bool test, string lableString = "")
     {
         Debug.Log("respct me");
         return null;
     }


     protected override CustomGUIResult<ResultType, SerializedProperty> drawGUIControlBody(CustomGUIResult<ResultType, SerializedProperty> currentResult, string lableString)
     {

         SerializedProperty currentList = currentResult.resultValue;
         int objCount = currentList.arraySize;
         if (EditorGUILayout.PropertyField(currentList, new GUIContent(lableString), false))
         {

             if (objCount != currentList.arraySize)
             {
                 Debug.Log("it has changed");

             }

             // Draw  the children 
             GameObject obj;
             for (var i = 0; i < currentList.arraySize; i++)
             {
                 obj = (GameObject)currentList.GetArrayElementAtIndex(i).objectReferenceValue;
                 //CustomGUIResult<int, GameObject> result = TransitionObjectGUI._instance.drawGUIControl(new CustomGUIResult<int, GameObject>(i, obj), obj.name, this.myLinkedPortalGateway.gameObject, TransitionObjectGUI.validationArray);




             }

             //obj = (GameObject)this.serializedTransitionObjectsWhiteList.GetArrayElementAtIndex(i).objectReferenceValue;



             // int objCount = this.serializedTransitionObjectsWhiteList.arraySize;
             // if (EditorGUILayout.PropertyField(this.serializedTransitionObjectsWhiteList, new GUIContent("Transition Object Whitelist (" + objCount + ")"), false))
             // {

             //     Debug.Log(objCount + ":" + this.serializedTransitionObjectsWhiteList.arraySize);
             //     if (objCount > this.serializedTransitionObjectsWhiteList.arraySize)
             //     {


             //     }
             //     else
             //     {

             //     }
             //     EditorGUI.indentLevel++;
             //     GameObject obj;
             //     for (var i = 0; i < objCount; i++)
             //     {
             //         obj = (GameObject)this.serializedTransitionObjectsWhiteList.GetArrayElementAtIndex(i).objectReferenceValue;
             //         CustomGUIResult<int, GameObject> result = TransitionObjectGUI._instance.drawGUIControl(new CustomGUIResult<int, GameObject>(i, obj), obj.name, this.myLinkedPortalGateway.gameObject, TransitionObjectGUI.validationArray);
             //         if (result.resultValue != obj)
             //         {
             //             Debug.Log("come get some");
             //             if (this.exitPortalResult.resultValue)
             //             {
             //                 // Set collision Target
             //             }
             //             else
             //             {
             //                 // Remove Collision Target
             //             }
             //         }
             //         Debug.Log(result);

             //     }
             //     EditorGUI.indentLevel--;

             // }



         }

         return null;
     }

     */





    /* 
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
	*/

}