
using UnityEngine;
using CMGCO.Unity.CustomGUI.Base;
using System;

namespace CMGCO.Unity.CustomGUI.Base
{
    public class DropDownResult<resultType, idType> : NewCustomGUIResult<resultType> where idType : struct, IConvertible
    {
        private idType id;
        public idType _id
        {
            get;
            private set;
        }
        public DropDownResult(resultType nResultValue, idType nId, bool nHasChanged = false) : base(nResultValue, nHasChanged)
        {
            this._id = nId;
        }
    }
}
