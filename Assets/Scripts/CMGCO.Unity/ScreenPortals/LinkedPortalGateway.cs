
#if UNITY_EDITOR
using UnityEditor;
#endif


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


namespace CMGCO.Unity.ScreenPortals
{

    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(BoxCollider))]


    public class LinkedPortalGateway : MonoBehaviour
    {

        public enum transitionWhiltelistValidationResponce { VALID, NOMESH, REPEATED, NULL }


        [SerializeField]
        private GameObject exitPortal;
        public GameObject _exitPortal
        {
            get
            {
                return this.exitPortal;
            }
            set
            {
                this.setExitPortal(value, true);
            }
        }
        public LinkedPortalGateway exitPortalScript;

        [SerializeField]
        private Vector2 screenRatio = new Vector2(0, 0);
        public Vector2 _screenRatio
        {
            get
            {
                return this.screenRatio;
            }
            set
            {
                this.setScreenRatio(value, true);
            }

        }

        [SerializeField]
        private Vector2 screenSize = new Vector2(1, 1);
        public Vector2 _screenSize
        {
            get
            {
                return this.screenSize;
            }
            set
            {
                this.setScreenSize(value, true);
            }

        }

        [SerializeField]
        private float targetFOV = 60;
        public float _targetFOV
        {
            get
            {
                return this.targetFOV;
            }
            set
            {
                this.setTargetFOV(value, true);
            }

        }
        [SerializeField]
        private List<GameObject> transitionObjectsWhiltelist = new List<GameObject>();
        public List<GameObject> _transitionObjectsWhiltelist
        {
            get
            {
                return this.transitionObjectsWhiltelist;
            }
            set
            {
                this.transitionObjectsWhiltelist = value;
            }

        }
        private List<MeshRenderer> transitionMeshesWhiltelist;

        private bool isVisible = false;
        public bool _isVisible
        {
            get
            {
                return this.isVisible;
            }
            set
            {
                this.isVisible = value;
            }
        }

        public void setExitPortal(GameObject newExitPortal, bool isPropigate = false)
        {
            // want to tell our current exit portal to clear itself 
            if (this.exitPortal != null)
            {
                this.exitPortalScript.clearExitPortal();
            }

            this.exitPortal = newExitPortal;
            if (this.exitPortal != null)
            {
                this.exitPortalScript = this.exitPortal.GetComponent<LinkedPortalGateway>();
                if (isPropigate)
                {
                    this.exitPortalScript.setExitPortal(this.gameObject);
                }
            }
        }

        public void giveLinkedProperties()
        {
            if (this.exitPortal != null)
            {
                this.exitPortalScript.setScreenRatio(this.screenRatio);
                this.exitPortalScript.setScreenSize(this.screenSize);
                this.exitPortalScript.setTargetFOV(this.targetFOV);
            }
        }

        public void inheritLinkedProperties()
        {
            if (this.exitPortal != null)
            {

                this.setScreenRatio(this.exitPortalScript._screenRatio);
                this.setScreenSize(this.exitPortalScript._screenSize);
                this.setTargetFOV(this.exitPortalScript._targetFOV);
            }
        }

        public void clearExitPortal()
        {
            this.exitPortal = null;
            this.exitPortalScript = null;
        }

        public void setScreenRatio(Vector2 newScreenRatio, bool isPropigate = false)
        {
            this.screenRatio = newScreenRatio;
            if (isPropigate && this.exitPortal != null)
            {
                this.exitPortalScript.setScreenRatio(newScreenRatio);
            }
        }

        public Vector2 calcScreenSize(bool constrianToX = true)
        {

            Vector2 newScreenSize;
            if (constrianToX)
            {
                newScreenSize = new Vector2(screenSize.x, (screenSize.x / screenRatio.x) * screenRatio.y);
            }
            else
            {
                newScreenSize = new Vector2((screenSize.y / screenRatio.y) * screenRatio.x, screenSize.y);
            }
            this.setScreenSize(newScreenSize, true);
            return (newScreenSize);
        }
        public Vector2 calcScreenSize(Vector2 currentScreenSize, bool constrianToX = true)
        {
            Vector2 newScreenSize;
            if (constrianToX)
            {
                newScreenSize = new Vector2(currentScreenSize.x, (currentScreenSize.x / screenRatio.x) * screenRatio.y);
            }
            else
            {
                newScreenSize = new Vector2((currentScreenSize.y / screenRatio.y) * screenRatio.x, currentScreenSize.y);
            }
            this.setScreenSize(newScreenSize, true);
            return (newScreenSize);
        }

        public void setScreenSize(Vector2 newScreenSize, bool isPropigate = false)
        {
            this.screenSize = newScreenSize;
            if (isPropigate && this.exitPortal != null)
            {
                this.exitPortalScript.setScreenSize(newScreenSize);
            }
        }

        public transitionWhiltelistValidationResponce validateTransitionObjectsWhiltelist(List<GameObject> newTransitionObjectsWhiltelist)
        {

            // While it is possible for the list to be rejected for multiple reasons we will only return one because its only in the editor (where only one is possible) that errors are reported

            transitionWhiltelistValidationResponce responce = transitionWhiltelistValidationResponce.VALID;
            List<GameObject> duplicatesRemoved = newTransitionObjectsWhiltelist.Distinct().ToList();

            if (duplicatesRemoved.Count != newTransitionObjectsWhiltelist.Count)
            {
                responce = transitionWhiltelistValidationResponce.REPEATED;
            }



            this.transitionObjectsWhiltelist = new List<GameObject>();
            this.transitionMeshesWhiltelist = new List<MeshRenderer>();
            MeshRenderer targetMesh;

            foreach (GameObject obj in duplicatesRemoved)
            {
                if (obj == null)
                {
                    responce = transitionWhiltelistValidationResponce.NULL;
                }
                else
                {
                    // Test we have a mesh 
                    targetMesh = obj.GetComponent<MeshRenderer>();
                    if (targetMesh)
                    {
                        transitionObjectsWhiltelist.Add(obj);
                        transitionMeshesWhiltelist.Add(targetMesh);
                    }
                    else
                    {
                        responce = transitionWhiltelistValidationResponce.NOMESH;
                    }

                }
            }
            return responce;
        }
        public void setTargetFOV(float newFOV, bool isPropigate = false)
        {
            this.targetFOV = newFOV;
            if (isPropigate && this.exitPortal != null)
            {
                this.exitPortalScript.setTargetFOV(targetFOV);
            }
        }
        private void OnDestroy()
        {
            if (!Application.isPlaying && !EditorApplication.isPlayingOrWillChangePlaymode && this.gameObject.activeInHierarchy && this.exitPortal != null)
            {
                EditorUtility.DisplayDialog("Reference Removed", "The reference to this portal has been removed from " + this.exitPortal.name, "OK");
                this.exitPortalScript.clearExitPortal();
            }

        }
    }
}
