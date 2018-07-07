
using UnityEngine;


namespace CMGCO.Unity.CustomGUI.NewAnchoredWidthHeight
{
    public enum Anchors { WIDTH, HEIGHT, NONE }

    public class AnchoredWidthHeightResult
    {

        private bool hasChanged;
        public bool _hasChanged
        {
            get;
            private set;
        }



        private bool anchor;
        public Anchors _anchor
        {
            get;
            private set;
        }

        private Rect widthHeightRect;
        public Rect _widthHeightRect
        {
            get;
            private set;
        }

        public AnchoredWidthHeightResult(bool hasChanged, Anchors anchor, Rect widthHeightRect)
        {
            this._hasChanged = hasChanged;
            this._anchor = anchor;
            this._widthHeightRect = widthHeightRect;
        }

        public void ResetHasChanged()
        {
            this._hasChanged = false;
        }
    }
}