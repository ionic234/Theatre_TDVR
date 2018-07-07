
using UnityEngine;
using CMGCO.Unity.CustomGUI.Base;

namespace CMGCO.Unity.CustomGUI.NewAnchoredWidthHeight
{
    public enum Anchors { WIDTH, HEIGHT, NONE }
    public class AnchoredWidthHeightResult : NewCustomGUIResult<Rect>
    {
        private bool anchor;
        public Anchors _anchor
        {
            get;
            private set;
        }

        public AnchoredWidthHeightResult(bool nHasChanged, Rect nResultValue, Anchors nAnchor) : base(nHasChanged, nResultValue)
        {
            this._anchor = nAnchor;
        }
    }
}
