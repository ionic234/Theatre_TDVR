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

        private GUIStyle LeftAlign;
        public GUIStyle _leftAlign
        {
            get
            {
                if (this.LeftAlign == null)
                {
                    this.LeftAlign = new GUIStyle();
                    this.LeftAlign.alignment = TextAnchor.MiddleLeft;
                    this.LeftAlign.padding.top = 3;
                    this.LeftAlign.clipping = TextClipping.Clip;
                }
                return this.LeftAlign;
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
    }
}
