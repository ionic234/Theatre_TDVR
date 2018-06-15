using UnityEditor;

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

using CMGCO.Unity.CustomGUI.Base;
using CMGCO.Unity.CustomGUI.AspectRatio;
using CMGCO.Unity.ScreenPortals;
using CMGCO.Unity.CustomGUI.AnchoredWidthHeight;

using System.Reflection;



namespace CMGCO.Unity.Tests
{


    [ExecuteInEditMode]
    [CustomEditor(typeof(Test))]


    public class TestEditor : Editor
    {

        private Test myTest;
        private SerializedProperty testStringGetSetProp;
        private SerializedProperty serializedScreenRatio;
        private CustomGUIResult<AspectRatioIDs, Vector2> currentAspectRatioResult;


        private void OnEnable()
        {


            this.myTest = (Test)target;

            this.testStringGetSetProp = serializedObject.FindProperty("testStringGetSet");

            this.serializedScreenRatio = serializedObject.FindProperty("screenRatio");
            this.currentAspectRatioResult = AspectRatioGUI._instance.getDropDownResultFromValue(this.serializedScreenRatio.vector2Value);
            // need to test and set here. hmm should be a function.

        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            string nString = EditorGUILayout.TextField("Shit", testStringGetSetProp.stringValue);
            if (nString != testStringGetSetProp.stringValue)
            {
                Undo.RegisterCompleteObjectUndo(myTest, "Set Test String");
                myTest._testStringGetSet = nString;
            }


            CustomGUIResult<AspectRatioIDs, Vector2> newAspectRatioResult = AspectRatioGUI._instance.drawGUIControl(this.currentAspectRatioResult);
            if (!newAspectRatioResult.resultValue.Equals(serializedScreenRatio.vector2Value))
            {
                Debug.Log("change ratio!");
                Undo.RegisterCompleteObjectUndo(myTest, "Set Ratio");
                myTest._screenRatio = newAspectRatioResult.resultValue;
            }



            serializedObject.ApplyModifiedProperties();
        }
    }
}