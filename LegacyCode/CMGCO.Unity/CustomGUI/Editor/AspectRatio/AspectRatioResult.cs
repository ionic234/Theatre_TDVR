
using UnityEngine;
using CMGCO.Unity.CustomGUI.Base;

namespace CMGCO.Unity.CustomGUI.AspectRatio
{
    public class AspectRatioResult : NewCustomGUIResult<Vector2>
    {
        private AspectRatioIDs aspectRatioID;
        public AspectRatioIDs _aspectRatioID
        {
            get;
            private set;
        }
        public AspectRatioResult(Vector2 nResultValue = new Vector2(), AspectRatioIDs nAspectRatioID = AspectRatioIDs.TV, bool nHasChanged = false) : base(nResultValue, nHasChanged)
        {
            this._aspectRatioID = nAspectRatioID;
        }
    }
}
