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

        // If you recieve overload method missmatch remember you need to implemenmt an overloaded Method in your extending class that converts the given parmeters into an object array. 
        public virtual CustomGUIResultType drawGUIControl(object[] args = null)
        {
            if (this.MyDrawGUIControlBodyMethod == null)
            {
                this.getMyDrawGUIControlBodyMethod();
            }
            this.drawGUIControlHead();
            CustomGUIResultType rData;

            try
            {
                rData = (CustomGUIResultType)this.MyDrawGUIControlBodyMethod.Invoke(this, args != null ? args : new object[] { });
            }
            // Default messages are not very helpful for finding the actualy problem. 
            catch (NullReferenceException)
            {
                Debug.LogError(this.GetType().Name + " does not implement the required function [drawGUIControlBody] or arguments are wrong");
                rData = default(CustomGUIResultType);
            }
            catch (TargetParameterCountException)
            {
                Debug.LogError(this.GetType().Name + " function [drawGUIControlBody] recieved the wrong number of arguments");
                rData = default(CustomGUIResultType);
            }
            catch (ArgumentException)
            {
                Debug.LogError(this.GetType().Name + " function [drawGUIControlBody] recieved the wrong type of arguments");
                rData = default(CustomGUIResultType);
            }

            this.drawGUIControlFooter();
            return (rData);
        }

        private void getMyDrawGUIControlBodyMethod()
        {
            // Cache the method to call. 
            var myType = this.GetType();
            this.MyDrawGUIControlBodyMethod = myType.GetMethod("drawGUIControlBody", BindingFlags.NonPublic | BindingFlags.Instance, null, this.getArgumentTypes(), null);
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

        protected virtual void drawGUIControlFooter()
        {
            GUILayout.EndHorizontal();
            GUILayout.Space(5);
        }
    }
}
