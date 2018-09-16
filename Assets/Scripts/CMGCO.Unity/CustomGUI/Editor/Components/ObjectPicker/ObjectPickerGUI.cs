using System;
using UnityEngine;
using UnityEditor;
using CMGCO.Unity.CustomGUI.Base;

namespace CMGCO.Unity.CustomGUI.Components
{
    abstract public class ObjectPickerGUI<T> : CustomGUIBase<CustomGUIResult<T>>
        where T : UnityEngine.Object
    {
        protected override Type[] getArgumentTypes()
        {
            return new Type[] {
               typeof(CustomGUIResult<T>),
               typeof(string)
            };
        }

        public CustomGUIResult<T> drawGUIControl(CustomGUIResult<T> currentResult, string lableString)
        {
            return base.drawGUIControl(new object[] { currentResult, lableString });
        }

        protected CustomGUIResult<T> drawGUIControlBody(CustomGUIResult<T> currentResult, string lableString)
        {
            T resultValue = currentResult.ResultValue;
            EditorGUI.BeginChangeCheck();
            T newResultValue = EditorGUILayout.ObjectField(lableString, currentResult.ResultValue, typeof(T), true) as T;
            if (GUILayout.Button("X", EditorStyles.miniButton, GUILayout.Width(22)))
            {
                newResultValue = null;
            }
            if (EditorGUI.EndChangeCheck())
            {
                return new CustomGUIResult<T>(newResultValue, true);
            }
            else
            {
                return currentResult;
            }
        }
    }
}
