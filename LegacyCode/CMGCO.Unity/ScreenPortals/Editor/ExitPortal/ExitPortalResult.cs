
using UnityEngine;
using CMGCO.Unity.CustomGUI.Base;

namespace CMGCO.Unity.ScreenPortals
{
    public class ExitPortalResult : NewCustomGUIResult<GameObject>
    {

        private LinkedPortalGateway resultScript;
        public LinkedPortalGateway _resultScript
        {
            get;
            private set;
        }

        public ExitPortalResult(GameObject nResultValue = null, LinkedPortalGateway nResultScript = null, bool nHasChanged = false) : base(nResultValue, nHasChanged)
        {
            this._resultScript = nResultScript;
        }


    }
}
