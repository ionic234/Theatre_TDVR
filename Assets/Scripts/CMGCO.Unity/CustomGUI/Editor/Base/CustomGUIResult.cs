
using UnityEngine;

namespace CMGCO.Unity.CustomGUI.Base
{
    public class CustomGUIResult<T> where T : UnityEngine.Object
    {

        //protected T resultValue;
        public T ResultValue
        {
            get;
            protected set;
        }

        //protected bool hasChanged;
        public bool HasChanged
        {
            get;
            protected set;
        }

        public CustomGUIResult(T newResultValue = null, bool newHasChanged = false)
        {
            this.HasChanged = newHasChanged;
            this.ResultValue = newResultValue;
        }

        public void ResetHasChanged()
        {
            this.HasChanged = false;
        }
    }
}