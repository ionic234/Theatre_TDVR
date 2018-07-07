using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Reflection;

namespace CMGCO.Unity.CustomGUI.Base
{

    [ExecuteInEditMode]

    abstract public class NewCustomGUIBase<CustomGUIResultType>
    {
        public virtual CustomGUIResultType drawGUIControl(Type[] argumentTypes = null, object[] args = null)
        {
            if (argumentTypes == null)
            {
                argumentTypes = new Type[] { };
            }
            if (args == null)
            {
                args = new object[] { };
            }

            this.drawGUIControlHead();
            var myType = this.GetType();
            var myDrawGUIControlBodyMethod = myType.GetMethod("drawGUIControlBody", BindingFlags.NonPublic | BindingFlags.Instance, null, argumentTypes, null);
            CustomGUIResultType rData = (CustomGUIResultType)myDrawGUIControlBodyMethod.Invoke(this, args);
            this.drawGUIControlFooter();
            return (rData);
        }

        protected virtual void drawGUIControlHead()
        {
            GUILayout.BeginHorizontal();
        }

        protected abstract CustomGUIResultType drawGUIControlBody();

        protected virtual void drawGUIControlFooter()
        {
            GUILayout.EndHorizontal();
            GUILayout.Space(5);
        }
    }
}




/*
      public CustomGUIResult<DataIDType, DataValueType> drawGUIControl(CustomGUIResult<DataIDType, DataValueType> currentResult, string lableString = "")
      {
          this.drawGUIControlhead();
          CustomGUIResult<DataIDType, DataValueType> rData = this.drawGUIControlBody(currentResult, lableString);
          this.drawGUIControlFooter();
          return (rData);
      }

      protected virtual void drawGUIControlhead()
      {
          GUILayout.BeginHorizontal();
      }

      protected abstract CustomGUIResult<DataIDType, DataValueType> drawGUIControlBody(CustomGUIResult<DataIDType, DataValueType> currentResult, string lableString);

      protected virtual void drawGUIControlFooter()
      {
          GUILayout.EndHorizontal();
          GUILayout.Space(5);
      }
       */
