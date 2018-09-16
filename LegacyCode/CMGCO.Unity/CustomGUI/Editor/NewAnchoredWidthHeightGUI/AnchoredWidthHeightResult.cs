
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

        public AnchoredWidthHeightResult(Rect nResultValue = new Rect(), Anchors nAnchor = Anchors.NONE, bool nHasChanged = false) : base(nResultValue, nHasChanged)
        {
            this._anchor = nAnchor;
        }

    }
}
