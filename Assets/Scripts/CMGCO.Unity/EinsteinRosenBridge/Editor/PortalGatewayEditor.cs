using UnityEditor;
using UnityEngine;
using CMGCO.Unity.CustomGUI.Base;

namespace CMGCO.Unity.EinsteinRosenBridge
{
    [ExecuteInEditMode]
    [CustomEditor(typeof(PortalGateway))]

    public class PortalGatewayEditor : Editor
    {
        private bool hasChanged = true; // make sure the props are read from the serialized object first run
        private PortalGateway editorTarget;
        private CustomGUIResult<PortalGateway> destinationPortalGatewayResult = new CustomGUIResult<PortalGateway>();
        private void OnEnable()
        {
            this.editorTarget = (PortalGateway)target;
            if (this.editorTarget.DestinationPortalGateway)
            {
                this.CheckPortalAssosiations();
            }
        }

        private void CheckPortalAssosiations()
        {
            // Need to check that we havn't become dissasiasiated from our DesitinationPortalGateway or we're connected to a portal we shouldn't be.
            // This can happen when undoing the removal of the PortalGateway compoenent or when cloning a portal that is already linked

            // Grab the PortalGateway that is our destination
            PortalGateway destinationPortalGateway = this.editorTarget.DestinationPortalGateway;

            // Test that our destination has us set as its destination.  
            if (destinationPortalGateway.DestinationPortalGateway == null)
            {
                // Our destination has forgotten that we are it's destination. Set that assosiation back up.
                this.editorTarget.SetDestinationPortal(this.editorTarget.DestinationPortalGateway, true);
                this.editorTarget.PropagateSharedProperties();
            }
            else if (!destinationPortalGateway.DestinationPortalGateway.Equals(this.editorTarget))
            {
                // We have are a copied object and need to clear our desitination portal. 
                this.editorTarget.ClearDestinationPortal();
            }
        }

        public override void OnInspectorGUI()
        {
            if (this.hasChanged || Event.current.commandName == "UndoRedoPerformed")
            {
                // Clean up from last frame. By now the serialized object should have been updated.
                this.hasChanged = false;
                this.ReadPropertiesFromSerializedObject();
            }
            serializedObject.Update();
            EditorGUILayout.Separator();

            if (!this.destinationPortalGatewayResult.ResultValue)
            {
                EditorGUILayout.LabelField("Own Properties", EditorStyles.boldLabel);
                this.drawPortalGatewayInput();
                EditorGUILayout.LabelField("An Exit Portal Must be set!!!", CustomEditorStyles.Instance.Error);
            }
            else
            {
                EditorGUILayout.LabelField("Linked Properties", EditorStyles.boldLabel);
                EditorGUILayout.LabelField("Own Properties", EditorStyles.boldLabel);
                this.drawPortalGatewayInput();
            }
        }

        private void ReadPropertiesFromSerializedObject()
        {
            PortalGateway serializedDestinationPortalGateway = (PortalGateway)serializedObject.FindProperty("destinationPortalGateway").objectReferenceValue;
            if (serializedDestinationPortalGateway)
            {
                this.destinationPortalGatewayResult = new CustomGUIResult<PortalGateway>(serializedDestinationPortalGateway, this.destinationPortalGatewayResult.HasChanged);
            }
        }
        private void drawPortalGatewayInput()
        {
            CustomGUIResult<PortalGateway> newDestinationPortalGatewayResult = PortalGatewayGUI.Instance.drawGUIControl(this.destinationPortalGatewayResult, "Destination Portal Gateway");
            if (newDestinationPortalGatewayResult.HasChanged)
            {
                newDestinationPortalGatewayResult.ResetHasChanged();
                if (PortalGatewayValidator.Validate(newDestinationPortalGatewayResult.ResultValue, editorTarget))
                {
                    this.destinationPortalGatewayResult = newDestinationPortalGatewayResult;
                    if (this.destinationPortalGatewayResult != null)
                    {
                        this.setDestinationPortalGateway(newDestinationPortalGatewayResult.ResultValue);
                    }
                    else
                    {
                        this.clearDestinationPortalGateway();
                    }
                    this.hasChanged = true;
                }
            }
        }

        private void setDestinationPortalGateway(PortalGateway newDestinationPortalGateway)
        {

            int group = Undo.GetCurrentGroup();
            string undoName = "Set Exit Portal";
            Undo.SetCurrentGroupName(undoName);
            PortalGateway[] recordObject;
            // Need to do some tests to work out what goes in the array 
            if (this.editorTarget.DestinationPortalGateway == null)
            {
                recordObject = newDestinationPortalGateway.DestinationPortalGateway == null ?
                    new PortalGateway[] { this.editorTarget, newDestinationPortalGateway } :
                    new PortalGateway[] { this.editorTarget, newDestinationPortalGateway, newDestinationPortalGateway.DestinationPortalGateway };
            }
            else
            {
                recordObject = newDestinationPortalGateway.DestinationPortalGateway == null ?
                    new PortalGateway[] { this.editorTarget, this.editorTarget.DestinationPortalGateway, newDestinationPortalGateway } :
                    new PortalGateway[] { this.editorTarget, this.editorTarget.DestinationPortalGateway, newDestinationPortalGateway, newDestinationPortalGateway.DestinationPortalGateway };
            }
            Undo.RecordObjects(recordObject, undoName);
            this.editorTarget.DestinationPortalGateway = newDestinationPortalGateway;


            // If are new destination is linked then it will already have valid properties set.  
            if (newDestinationPortalGateway.DestinationPortalGateway == null
                || EditorUtility.DisplayDialog("Inherit Properties from?", "Which Portal Gateway would you like to inherit the linked properties from?", "This Portal Gateway", "Destination Portal Gateway"))
            {
                this.editorTarget.giveLinkedProperties();
            }
            else
            {
                this.editorTarget.inheritLinkedProperties();
            }
            Undo.CollapseUndoOperations(group);
        }

        private void clearDestinationPortalGateway()
        {
            if (this.editorTarget.DestinationPortalGateway == null)
            {
                Debug.Log("Nothing changed here so do nothing");
                return;
            }

            int group = Undo.GetCurrentGroup();
            string undoName = "Clear Exit Portal";
            Undo.SetCurrentGroupName(undoName);
            PortalGateway[] recordObject = new PortalGateway[] { this.editorTarget, this.editorTarget.DestinationPortalGateway };
            Undo.RecordObjects(recordObject, undoName);
            this.editorTarget.clearExitPortal(true);
            Undo.CollapseUndoOperations(group);
        }
    }
}