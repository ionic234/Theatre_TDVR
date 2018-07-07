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

        public virtual CustomGUIResultType drawGUIControl(object[] args = null)
        {
            if (this.MyDrawGUIControlBodyMethod == null)
            {
                this.MyDrawGUIControlBodyMethod = getMyDrawGUIControlBodyMethod();
            }
            this.drawGUIControlHead();
            CustomGUIResultType rData = (CustomGUIResultType)this.MyDrawGUIControlBodyMethod.Invoke(this, args != null ? args : new object[] { });
            this.drawGUIControlFooter();
            return (rData);
        }

        private MethodInfo getMyDrawGUIControlBodyMethod()
        {
            // Cache the method to call. 
            var myType = this.GetType();
            return myType.GetMethod("drawGUIControlBody", BindingFlags.NonPublic | BindingFlags.Instance, null, this.getArgumentTypes(), null);
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
