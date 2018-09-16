﻿using System;
using UnityEditor;
using UnityEngine;

namespace CMGCO.Unity.CustomGUI.Base
{
    // Singleton package based on https://msdn.microsoft.com/en-au/library/ff650316.aspx

    public sealed class CustomEditorStyles
    {
        // Thread safe singleton pattern. 
        private static volatile CustomEditorStyles instance;
        private static object syncRoot = new System.Object();

        private CustomEditorStyles() { }

        public static CustomEditorStyles Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new CustomEditorStyles();
                    }
                }

                return instance;
            }
        }

        private GUIStyle error;
        public GUIStyle Error
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
        public GUIStyle RightAlign
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
        private GUIStyle centerAlign;
        public GUIStyle CenterAlign
        {
            get
            {
                if (this.centerAlign == null)
                {
                    this.centerAlign = new GUIStyle();
                    this.centerAlign.alignment = TextAnchor.UpperCenter;
                    this.centerAlign.padding.top = 1;
                    this.centerAlign.clipping = TextClipping.Clip;
                }
                return this.centerAlign;
            }
        }

        private GUIStyle bold;
        public GUIStyle Bold
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
        public GUIStyle BoldCenterAlign
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
        // TODO: Split these into own editor styles class. 

        private GUIStyle anchoredWidthHeightLabelFix;
        public GUIStyle AnchoredWidthHeightLabelFix
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
        public GUIStyle AnchoredWidthHeightFocus
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
        public GUIStyle AnchoredWidthHeightFocusFix
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
        public GUIStyle AnchordWidthHeightAnchored
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
        public GUIStyle AnchordWidthHeightAnchoredFix
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
        public GUIStyle AnchoredWidthHeightFocusAnchored
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
        public GUIStyle AnchoredWidthHeightFocusAnchoredFix
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
