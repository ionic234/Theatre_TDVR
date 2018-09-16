
using UnityEngine;

namespace CMGCO.Unity.CustomGUI.Base
{
    public class NewCustomGUIResult<ResultValueType>
    {

        protected ResultValueType resultValue;
        public ResultValueType _resultValue
        {
            get;
            protected set;
        }

        protected bool hasChanged;
        public bool _hasChanged
        {
            get;
            protected set;
        }

        public NewCustomGUIResult(ResultValueType nResultValue, bool nHasChanged)
        {
            this._hasChanged = nHasChanged;
            this._resultValue = nResultValue;
        }

        public void ResetHasChanged()
        {
            this.hasChanged = false;
        }
    }
}