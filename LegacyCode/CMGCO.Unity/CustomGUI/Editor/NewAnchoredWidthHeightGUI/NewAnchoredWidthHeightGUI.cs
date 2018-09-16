using UnityEngine;
using UnityEditor;

using CMGCO.Unity.CustomGUI.Base;
using System;
using System.Reflection;
using System.ComponentModel;

namespace CMGCO.Unity.CustomGUI.NewAnchoredWidthHeight
{

    public class NewAnchoredWidthHeightGUI : NewCustomGUIBase<AnchoredWidthHeightResult>
    {

        protected override Type[] getArgumentTypes()
        {
            return new Type[] {
                typeof(AnchoredWidthHeightResult), typeof(String)
            };
        }

        private static readonly NewAnchoredWidthHeightGUI instance = new NewAnchoredWidthHeightGUI();
        public static NewAnchoredWidthHeightGUI _instance
        {
            get
            {
                return instance;
            }
        }

        private NewAnchoredWidthHeightGUI() { }

        public AnchoredWidthHeightResult drawGUIControl(AnchoredWidthHeightResult currentResult, string lableString = "Dimensions")
        {
            return base.drawGUIControl(new object[] { currentResult, lableString });
        }

        protected AnchoredWidthHeightResult drawGUIControlBody(AnchoredWidthHeightResult currentResult, string lableString = "Dimensions")
        {
            Rect currentRect = currentResult._resultValue;

            int controlLines = EditorGUIUtility.currentViewWidth > CustomEditorGUIUtility.flowBreakWidth ? 1 : 2; // Determine if our content will wrap over several lines or not 
            Rect controlContainerRect = EditorGUILayout.GetControlRect(true, EditorGUIUtility.singleLineHeight);
            Rect floatControlsContainerRect = this.drawPrefixLabel(controlContainerRect, controlLines, lableString);

            int divisionFieldCount = controlLines > 1 ? 2 : 3;
            float singleFieldWidth = floatControlsContainerRect.width / divisionFieldCount;

            EditorGUI.BeginChangeCheck();
            float newWidth = drawFloatControl("W", currentRect.width, floatControlsContainerRect, singleFieldWidth, 0, currentResult._anchor.Equals(Anchors.WIDTH), lableString);
            if (EditorGUI.EndChangeCheck())
            {
                return new AnchoredWidthHeightResult(new Rect(currentRect.x, currentRect.y, newWidth, currentRect.height), Anchors.WIDTH, true);
            }

            EditorGUI.BeginChangeCheck();
            float newHeight = drawFloatControl("H", currentRect.height, floatControlsContainerRect, singleFieldWidth, 1, currentResult._anchor.Equals(Anchors.HEIGHT), lableString);
            if (EditorGUI.EndChangeCheck())
            {
                return new AnchoredWidthHeightResult(new Rect(currentRect.x, currentRect.y, currentRect.width, newHeight), Anchors.WIDTH, true);
            }

            return currentResult;
        }

        protected override void drawGUIControlHead()
        {
            // Nothing needed for this one.
        }

        private Rect drawPrefixLabel(Rect containerRect, int controlLines, string lableString)
        {
            Rect rRect = EditorGUI.PrefixLabel(containerRect, new GUIContent(lableString));
            if (controlLines > 1)
            {
                EditorGUI.indentLevel++;
                rRect = EditorGUI.IndentedRect(EditorGUILayout.GetControlRect(true, EditorGUIUtility.singleLineHeight));
                EditorGUI.indentLevel--;
            }
            return rRect;
        }

        private float drawFloatControl(string label, float value, Rect containerRect, float singleFieldWidth, int controlCount, bool isAnchored, string parentLabel)
        {

            // We need to use reflection to get the method we want to use. Silly really 
            Type editorGUIType = typeof(EditorGUI);
            Type RecycledTextEditorType = Assembly.GetAssembly(editorGUIType).GetType("UnityEditor.EditorGUI+RecycledTextEditor");
            Type[] argumentTypes = new Type[] { RecycledTextEditorType, typeof(Rect), typeof(Rect), typeof(int), typeof(float), typeof(string), typeof(GUIStyle), typeof(bool), typeof(float) };
            MethodInfo doFloatFieldMethod = editorGUIType.GetMethod("DoFloatField", BindingFlags.NonPublic | BindingFlags.Static, null, argumentTypes, null);

            Rect controlRect = new Rect(
                containerRect.x + (singleFieldWidth * controlCount),
                containerRect.y,
                singleFieldWidth,
                containerRect.height
            );

            Rect labelRect = new Rect(
                controlRect.x,
                controlRect.y,
                CustomEditorGUIUtility.singleCharWidth,
                controlRect.height
            );

            Rect fieldRect = new Rect(
                controlRect.x + CustomEditorGUIUtility.singleCharWidth,
                controlRect.y,
                controlRect.width - CustomEditorGUIUtility.singleCharWidth,
                controlRect.height
            );

            string controlName = parentLabel + "_" + label; // This has to be called before we  get the controller ID so cant 100% be sure it is unique. 
            GUI.SetNextControlName(controlName);
            int controlID = GUIUtility.GetControlID("EditorTextField".GetHashCode(), FocusType.Keyboard, controlRect);
            EditorGUI.PrefixLabel(controlRect, controlID, new GUIContent(label), getLableStyle(controlName, controlCount, isAnchored)); // The rect this gives back is soo very wrong;
            FieldInfo fieldInfo = editorGUIType.GetField("s_RecycledEditor", BindingFlags.NonPublic | BindingFlags.Static);
            object recycledEditor = fieldInfo.GetValue(null);
            object[] parameters = new object[] { recycledEditor, fieldRect, labelRect, controlID, value, "g7", EditorStyles.numberField, true, .2f };
            return (float)doFloatFieldMethod.Invoke(null, parameters);
        }
        private GUIStyle getLableStyle(string controlName, int controlCount, bool isAnchored)
        {
            if (GUI.GetNameOfFocusedControl().Equals(controlName))
            {
                if (isAnchored)
                {
                    return controlCount.Equals(0) ? CustomEditorStyles._instance._anchoredWidthHeightFocusAnchored : CustomEditorStyles._instance._anchoredWidthHeightFocusAnchoredFix;
                }
                else
                {
                    return controlCount.Equals(0) ? CustomEditorStyles._instance._anchoredWidthHeightFocus : CustomEditorStyles._instance._anchoredWidthHeightFocusFix;
                }
            }
            else
            {
                if (isAnchored)
                {
                    return controlCount.Equals(0) ? CustomEditorStyles._instance._anchordWidthHeightAnchored : CustomEditorStyles._instance._anchordWidthHeightAnchoredFix;
                }
                else
                {
                    // not focused + not Anchored 
                    return controlCount.Equals(0) ? EditorStyles.label : CustomEditorStyles._instance._anchoredWidthHeightLabelFix;
                }
            }
        }

        protected override void drawGUIControlFooter()
        {
            GUILayout.Space(5);
        }
    }
}
