using UnityEditor;
using UnityEngine;



namespace CMGCO.Unity.CustomGUI.Base
{
    // Singleton package based on https://msdn.microsoft.com/en-au/library/ff650316.aspx

    public sealed class CustomEditorStyles
    {
        private static readonly CustomEditorStyles instance = new CustomEditorStyles();
        public static CustomEditorStyles _instance
        {
            get
            {
                return instance;
            }

        }



        private GUIStyle error;
        public GUIStyle _error
        {
            get
            {
                if (this.error == null)
                {
                    this.error = new GUIStyle();
                    this.error.normal.textColor = Color.red;
                    this.error.alignment = TextAnchor.MiddleCenter;
                }
                return this.error;
            }
        }

        private GUIStyle rightAlign;
        public GUIStyle _rightAlign
        {
            get
            {
                if (this.rightAlign == null)
                {
                    this.rightAlign = new GUIStyle();
                    this.rightAlign.alignment = TextAnchor.MiddleRight;
                    this.rightAlign.padding.top = 3;
                    this.rightAlign.clipping = TextClipping.Clip;
                }
                return this.rightAlign;
            }
        }




        private GUIStyle CenterAlign;
        public GUIStyle _centerAlign
        {
            get
            {
                if (this.CenterAlign == null)
                {
                    this.CenterAlign = new GUIStyle();
                    this.CenterAlign.alignment = TextAnchor.UpperCenter;
                    this.CenterAlign.padding.top = 1;
                    this.CenterAlign.clipping = TextClipping.Clip;
                }
                return this.CenterAlign;
            }
        }


        private GUIStyle bold;
        public GUIStyle _bold
        {
            get
            {
                if (this.bold == null)
                {
                    this.bold = new GUIStyle();
                    this.bold.fontStyle = FontStyle.Bold;
                    this.bold.alignment = TextAnchor.MiddleLeft;
                    this.bold.padding.top = 3;
                    this.bold.clipping = TextClipping.Clip;
                }
                return this.bold;
            }
        }

        private GUIStyle boldCenterAlign;
        public GUIStyle _boldCenterAlign
        {
            get
            {
                if (this.boldCenterAlign == null)
                {
                    this.boldCenterAlign = new GUIStyle();
                    this.boldCenterAlign.fontStyle = FontStyle.Bold;
                    this.boldCenterAlign.alignment = TextAnchor.UpperCenter;
                    this.boldCenterAlign.padding.top = 3;
                    this.boldCenterAlign.clipping = TextClipping.Clip;
                }
                return this.boldCenterAlign;
            }
        }







        // Styles for use with AnchoredWidthHeight
        private GUIStyle anchoredWidthHeightLabelFix;
        public GUIStyle _anchoredWidthHeightLabelFix
        {
            get
            {
                if (this.anchoredWidthHeightLabelFix == null)
                {
                    this.anchoredWidthHeightLabelFix = new GUIStyle();
                    this.anchoredWidthHeightLabelFix.padding.left = 3;
                    this.anchoredWidthHeightLabelFix.padding.top = 1;
                }
                return this.anchoredWidthHeightLabelFix;
            }
        }

        private GUIStyle anchoredWidthHeightFocus;
        public GUIStyle _anchoredWidthHeightFocus
        {
            get
            {
                if (this.anchoredWidthHeightFocus == null)
                {
                    this.anchoredWidthHeightFocus = new GUIStyle();
                    this.anchoredWidthHeightFocus.normal.textColor = Color.blue;
                    this.anchoredWidthHeightFocus.padding.top = 1;
                    this.anchoredWidthHeightFocus.padding.left = 2;

                }
                return this.anchoredWidthHeightFocus;
            }
        }

        private GUIStyle anchoredWidthHeightFocusFix;
        public GUIStyle _anchoredWidthHeightFocusFix
        {
            get
            {
                if (this.anchoredWidthHeightFocusFix == null)
                {
                    this.anchoredWidthHeightFocusFix = new GUIStyle();
                    this.anchoredWidthHeightFocusFix.normal.textColor = Color.blue;
                    this.anchoredWidthHeightFocusFix.padding.top = 1;
                    this.anchoredWidthHeightFocusFix.padding.left = 3;
                }
                return this.anchoredWidthHeightFocusFix;
            }
        }

        private GUIStyle anchordWidthHeightAnchored;
        public GUIStyle _anchordWidthHeightAnchored
        {
            get
            {
                if (this.anchordWidthHeightAnchored == null)
                {
                    this.anchordWidthHeightAnchored = new GUIStyle();
                    this.anchordWidthHeightAnchored.fontStyle = FontStyle.Bold;
                    this.anchordWidthHeightAnchored.padding.top = 1;
                    this.anchordWidthHeightAnchored.padding.left = 1;
                }
                return this.anchordWidthHeightAnchored;
            }
        }

        private GUIStyle anchoredWidhtHeightAnchoredFix;
        public GUIStyle _anchordWidthHeightAnchoredFix
        {
            get
            {
                if (this.anchoredWidhtHeightAnchoredFix == null)
                {
                    this.anchoredWidhtHeightAnchoredFix = new GUIStyle();
                    this.anchoredWidhtHeightAnchoredFix.fontStyle = FontStyle.Bold;
                    this.anchoredWidhtHeightAnchoredFix.padding.top = 1;
                    this.anchoredWidhtHeightAnchoredFix.padding.left = 3;
                }
                return this.anchoredWidhtHeightAnchoredFix;
            }
        }

        private GUIStyle anchoredWidthHeightFocusAnchored;
        public GUIStyle _anchoredWidthHeightFocusAnchored
        {
            get
            {
                if (this.anchoredWidthHeightFocusAnchored == null)
                {
                    this.anchoredWidthHeightFocusAnchored = new GUIStyle();
                    this.anchoredWidthHeightFocusAnchored.normal.textColor = Color.blue;
                    this.anchoredWidthHeightFocusAnchored.padding.top = 1;
                    this.anchoredWidthHeightFocusAnchored.padding.left = 1;
                    this.anchoredWidthHeightFocusAnchored.fontStyle = FontStyle.Bold;
                }
                return this.anchoredWidthHeightFocusAnchored;
            }
        }

        private GUIStyle anchoredWidthHeightFocusAnchoredFix;
        public GUIStyle _anchoredWidthHeightFocusAnchoredFix
        {
            get
            {
                if (this.anchoredWidthHeightFocusAnchoredFix == null)
                {
                    this.anchoredWidthHeightFocusAnchoredFix = new GUIStyle();
                    this.anchoredWidthHeightFocusAnchoredFix.normal.textColor = Color.blue;
                    this.anchoredWidthHeightFocusAnchoredFix.fontStyle = FontStyle.Bold;
                    this.anchoredWidthHeightFocusAnchoredFix.padding.top = 1;
                    this.anchoredWidthHeightFocusAnchoredFix.padding.left = 3;
                }
                return this.anchoredWidthHeightFocusAnchoredFix;
            }


        }
    }
}
