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


        private MethodInfo MyDrawGUIControlBodyMethod;
        private MethodInfo MyParameterlessDrawGUIControlBodyMethod;

        public virtual CustomGUIResultType drawGUIControl(object[] args = null)
        {
            if (this.MyDrawGUIControlBodyMethod == null)
            {
                this.getMyDrawGUIControlBodyMethod();
            }

            this.drawGUIControlHead();

            CustomGUIResultType rData;
            if (args == null || args.Length == 0)
            {
                rData = (CustomGUIResultType)this.MyParameterlessDrawGUIControlBodyMethod.Invoke(this, args != null ? args : new object[] { });
            }
            else
            {
                rData = (CustomGUIResultType)this.MyDrawGUIControlBodyMethod.Invoke(this, args);
            }

            this.drawGUIControlFooter();
            return (rData);
        }

        private void getMyDrawGUIControlBodyMethod()
        {
            // Cache the method to call. 
            var myType = this.GetType();
            this.MyDrawGUIControlBodyMethod = myType.GetMethod("drawGUIControlBody", BindingFlags.NonPublic | BindingFlags.Instance, null, this.getArgumentTypes(), null);
            this.MyParameterlessDrawGUIControlBodyMethod = myType.GetMethod("drawGUIControlBody", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { }, null);
        }

        protected virtual void drawGUIControlHead()
        {
            GUILayout.BeginHorizontal();
        }

        protected virtual Type[] getArgumentTypes()
        {
            // The is flaky when stored as a variable. 
            return new Type[] { };
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
