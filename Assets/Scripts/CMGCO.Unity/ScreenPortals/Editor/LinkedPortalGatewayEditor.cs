using UnityEditor;
using UnityEngine;

using CMGCO.Unity.CustomGUI.Base;
using CMGCO.Unity.CustomGUI.AspectRatio;
using CMGCO.Unity.CustomGUI.AnchoredWidthHeight;

using CMGCO.Unity.CustomGUI.NewAnchoredWidthHeight;

namespace CMGCO.Unity.ScreenPortals
{

    [ExecuteInEditMode]
    [CustomEditor(typeof(LinkedPortalGateway))]

    public class LinkedPortalGatwayEditor : Editor
    {

        private bool hasChanged = true; // make sure the props are read from the serialized object first run

        // This Editors Target Object
        private LinkedPortalGateway myLinkedPortalGateway;

        // Properties for exit portal; 
        private CustomGUIResult<int, GameObject> exitPortalResult = new CustomGUIResult<int, GameObject>(0, null);
        private LinkedPortalGateway exitPortalScript;

        private CustomGUIResult<AspectRatioIDs, Vector2> aspectRatioResult;
        private AnchoredWidthHeightResult screenSizeResult = new AnchoredWidthHeightResult(false, new Rect(), Anchors.NONE);


        private float cameraFOV;

        private bool isVisible;

        private SerializedProperty serializedTransitionObjectsWhiteList;

        private void OnEnable()
        {
            // Get the editors target 
            this.myLinkedPortalGateway = (LinkedPortalGateway)target;

            // Need to check that we havn't become dissasosiated from our exitPortal or we're connected to a portal we shouldnt be.
            // This can happen when undoing the removal of the linkedPortalGateway compoenent or when cloning a portal that is already linked
            if (this.myLinkedPortalGateway.exitPortalScript)
            {
                // We have an exit portal script. 
                if (this.myLinkedPortalGateway.exitPortalScript.exitPortalScript == null)
                {
                    // It hads forgotten us set it back up 
                    this.myLinkedPortalGateway.setExitPortal(this.myLinkedPortalGateway._exitPortal, true);
                    this.myLinkedPortalGateway.giveLinkedProperties();
                }
                else if (!this.myLinkedPortalGateway.exitPortalScript.exitPortalScript.Equals(this.myLinkedPortalGateway))
                {
                    // We are a clone. Clear our exit portal
                    this.myLinkedPortalGateway.clearExitPortal(false);
                }
            }
        }

        private void getPropertiesFromSerializedObject()
        {
            // Get the serialized Exit Portal
            this.exitPortalResult.resultValue = (GameObject)serializedObject.FindProperty("exitPortal").objectReferenceValue;
            this.exitPortalScript = this.exitPortalResult.resultValue ? this.myLinkedPortalGateway._exitPortal.GetComponent<LinkedPortalGateway>() : null;

            if (this.exitPortalResult.resultValue)
            {
                // Get the values now we know were set up for it 



                Vector2 serializedRatioValue = (Vector2)serializedObject.FindProperty("screenRatio").vector2Value;
                this.aspectRatioResult = AspectRatioGUI._instance.getDropDownResultFromValue(serializedRatioValue);

                Rect serializedScreenSizeValue = (Rect)serializedObject.FindProperty("screenSize").rectValue;
                this.screenSizeResult = new AnchoredWidthHeightResult(false, serializedScreenSizeValue, Anchors.NONE);


                this.cameraFOV = (float)serializedObject.FindProperty("targetFOV").floatValue;
                this.isVisible = (bool)serializedObject.FindProperty("isVisible").boolValue;

                this.serializedTransitionObjectsWhiteList = serializedObject.FindProperty("transitionObjectsWhiltelist");

            }
        }

        public override void OnInspectorGUI()
        {

            if (this.hasChanged || Event.current.commandName == "UndoRedoPerformed")
            {
                // Clean up from last frame. By now the serialized object should have been updated.
                this.hasChanged = false;
                this.getPropertiesFromSerializedObject();
            }

            serializedObject.Update();
            EditorGUILayout.Separator();

            if (!this.exitPortalResult.resultValue)
            {
                EditorGUILayout.LabelField("Own Properties", EditorStyles.boldLabel);
                this.drawExitPortalInput();
                EditorGUILayout.LabelField("An Exit Portal Must be set!!!", CustomEditorStyles._instance._error);
            }
            else
            {
                EditorGUILayout.LabelField("Linked Properties", EditorStyles.boldLabel);
                //this.drawRatioDropDown();
                this.drawWidthHeight();
                this.drawTargetFOV();
                EditorGUILayout.Separator();
                EditorGUILayout.LabelField("Own Properties", EditorStyles.boldLabel);
                this.drawExitPortalInput();
                //this.drawCollisionTargets();
                this.drawIsVisible();
            }
            serializedObject.ApplyModifiedProperties();
            GUILayout.Space(50);
            base.OnInspectorGUI();
        }
        private void drawExitPortalInput()
        {
            this.exitPortalResult = ExitPortalGUI._instance.drawGUIControl(this.exitPortalResult, "Exit Portal", this.myLinkedPortalGateway.gameObject, ExitPortalGUI.validationArray);

            if (this.exitPortalResult.resultValue != this.myLinkedPortalGateway._exitPortal)
            {
                if (this.exitPortalResult.resultValue)
                {
                    this.setExitPortal();
                }
                else
                {
                    this.clearExitPortal();
                }
                this.hasChanged = true;
            }
        }

        private void clearExitPortal()
        {
            int group = Undo.GetCurrentGroup();
            string undoName = "Clear Exit Portal";
            Undo.SetCurrentGroupName(undoName);
            LinkedPortalGateway[] recordObject = this.exitPortalScript ? new LinkedPortalGateway[] { this.myLinkedPortalGateway, this.exitPortalScript } : new LinkedPortalGateway[] { this.myLinkedPortalGateway };
            Undo.RecordObjects(recordObject, undoName);
            this.myLinkedPortalGateway.clearExitPortal(true);
            this.exitPortalScript = null;
            Undo.CollapseUndoOperations(group);
        }

        private void setExitPortal()
        {
            int group = Undo.GetCurrentGroup();
            string undoName = "Set Exit Portal";
            Undo.SetCurrentGroupName(undoName);

            LinkedPortalGateway resultScript = this.exitPortalResult.resultValue.GetComponent<LinkedPortalGateway>();
            LinkedPortalGateway resultExitPortalScript = resultScript.exitPortalScript; // The exit portal of our new exit portal. Also need to capture this being reset

            LinkedPortalGateway[] recordObject;
            if (resultExitPortalScript)
            {
                recordObject = this.exitPortalScript ? new LinkedPortalGateway[] { this.myLinkedPortalGateway, this.exitPortalScript, resultExitPortalScript, resultScript } : new LinkedPortalGateway[] { this.myLinkedPortalGateway, resultExitPortalScript, resultScript };
            }
            else
            {
                recordObject = this.exitPortalScript ? new LinkedPortalGateway[] { this.myLinkedPortalGateway, this.exitPortalScript, resultScript } : new LinkedPortalGateway[] { this.myLinkedPortalGateway, resultScript };
            }




            Undo.RecordObjects(recordObject, undoName);
            bool resultHasExitPortal = resultScript._exitPortal != null;
            this.myLinkedPortalGateway._exitPortal = this.exitPortalResult.resultValue;

            if (!resultHasExitPortal || EditorUtility.DisplayDialog("Inherit Properties from?", "Which Portal would you like to inherit the linked properties from?", "This portal", "Exit portal"))
            {
                if (!this.exitPortalScript)
                {
                    // Set the aspect ratio to the default value 
                    CustomGUIResult<AspectRatioIDs, Vector2> defaultRatioResult = AspectRatioGUI._instance.getDropDownResultFromValue(this.myLinkedPortalGateway._screenRatio);
                    this.myLinkedPortalGateway._screenRatio = defaultRatioResult.resultValue;
                }
                this.myLinkedPortalGateway.giveLinkedProperties();
            }
            else
            {
                this.myLinkedPortalGateway.inheritLinkedProperties();
            }
            this.exitPortalScript = this.exitPortalResult.resultValue ? this.myLinkedPortalGateway._exitPortal.GetComponent<LinkedPortalGateway>() : null;
            Undo.CollapseUndoOperations(group);
        }

        private void drawRatioDropDown()
        {
            this.aspectRatioResult = AspectRatioGUI._instance.drawGUIControl(this.aspectRatioResult);
            if (!this.aspectRatioResult.resultValue.Equals(this.myLinkedPortalGateway._screenRatio))
            {
                int group = Undo.GetCurrentGroup();
                string undoName = "Change Aspect Ratio";
                Undo.SetCurrentGroupName(undoName);
                Undo.RecordObjects(new LinkedPortalGateway[] { this.myLinkedPortalGateway, this.exitPortalScript }, undoName);
                this.myLinkedPortalGateway._screenRatio = this.aspectRatioResult.resultValue;
                this.calcScreenSize();
                Undo.CollapseUndoOperations(group);



            }
        }


        private void drawWidthHeight()
        {






            //this.screenSizeResult = 
            this.screenSizeResult = NewAnchoredWidthHeightGUI._instance.drawGUIControl(this.screenSizeResult);
            if (screenSizeResult._hasChanged)
            {
                int group = Undo.GetCurrentGroup();
                string undoName = "Change Screen Size";
                Undo.SetCurrentGroupName(undoName);
                Undo.RecordObjects(new LinkedPortalGateway[] { this.myLinkedPortalGateway, this.exitPortalScript }, undoName);
                //this.calcScreenSize();
                //this.myLinkedPortalGateway._screenSize = this.screenSizeResult._widthHeightRect;


                Undo.CollapseUndoOperations(group);
                this.screenSizeResult.ResetHasChanged();
            }

            /* 
            if (this.screenSizeResult._hasChanged)
            {
                int group = Undo.GetCurrentGroup();
                string undoName = "Change Screen Size";
                Undo.SetCurrentGroupName(undoName);
                Undo.RecordObjects(new LinkedPortalGateway[] { this.myLinkedPortalGateway, this.exitPortalScript }, undoName);
                this.calcScreenSize();
                Undo.CollapseUndoOperations(group);
                this.screenSizeResult.ResetHasChanged();
            }
            */
        }

        private void calcScreenSize()
        {

            //this.screenSizeResult

            //this.screenSizeResult.resultValue = this.myLinkedPortalGateway.calcScreenSize(this.screenSizeResult.resultValue, this.screenSizeResult.resultID == (WidthHeightAnchors)0);
            // Need to work out how this will change the box collider and how that will get called again on undo / redo
        }

        private void drawTargetFOV()
        {
            GUILayout.BeginHorizontal();
            this.cameraFOV = EditorGUILayout.FloatField("Target FOV", this.cameraFOV);
            if (this.cameraFOV != this.myLinkedPortalGateway._targetFOV)
            {
                int group = Undo.GetCurrentGroup();
                string undoName = "Change FOV";
                Undo.SetCurrentGroupName(undoName);
                Undo.RecordObjects(new LinkedPortalGateway[] { this.myLinkedPortalGateway, this.exitPortalScript }, undoName);
                this.myLinkedPortalGateway._targetFOV = this.cameraFOV;
                Undo.CollapseUndoOperations(group);
            }
            GUILayout.EndHorizontal();
        }

        private void drawIsVisible()
        {
            this.isVisible = EditorGUILayout.Toggle("Is Visible", this.isVisible);
            if (this.isVisible != this.myLinkedPortalGateway._isVisible)
            {
                Undo.RegisterCompleteObjectUndo(this.myLinkedPortalGateway, "Toggle Is Visible");
                this.myLinkedPortalGateway._isVisible = this.isVisible;
            }
        }


        private void drawCollisionTargets()
        {


            // most of this should move to
            //CustomGUIResult<ResultType, List<GameObject>> listResult =

            if (this.serializedTransitionObjectsWhiteList.isArray)
            {
                int objCount = this.serializedTransitionObjectsWhiteList.arraySize;
                string label = "Transition Object Whitelist (" + objCount + ")";
                CustomGUIResult<ResultType, SerializedProperty> whilteListResult = TransitionObjectListGUI._instance.drawGUIControl(
                    new CustomGUIResult<ResultType, SerializedProperty>(ResultType.NO_CHANGE, this.serializedTransitionObjectsWhiteList),
                    label,
                    this.myLinkedPortalGateway.gameObject,
                    ExitPortalGUI.validationArray // This isn't right;
                );
            }
            else
            {
                Debug.LogError("serializedTransitionObjectsWhiteList is not an array");
            }


            // ObjectPickerListGUIBase._instance.drawGUIControl(new CustomGUIResult<ResultType, SerializedProperty>(ResultType.NO_CHANGE, this.serializedTransitionObjectsWhiteList), ");

            if (this.serializedTransitionObjectsWhiteList.isArray)
            {


                // int objCount = this.serializedTransitionObjectsWhiteList.arraySize;
                // if (EditorGUILayout.PropertyField(this.serializedTransitionObjectsWhiteList, new GUIContent("Transition Object Whitelist (" + objCount + ")"), false))
                // {

                //     Debug.Log(objCount + ":" + this.serializedTransitionObjectsWhiteList.arraySize);
                //     if (objCount > this.serializedTransitionObjectsWhiteList.arraySize)
                //     {


                //     }
                //     else
                //     {

                //     }





                //     EditorGUI.indentLevel++;
                //     GameObject obj;
                //     for (var i = 0; i < objCount; i++)
                //     {
                //         obj = (GameObject)this.serializedTransitionObjectsWhiteList.GetArrayElementAtIndex(i).objectReferenceValue;
                //         CustomGUIResult<int, GameObject> result = TransitionObjectGUI._instance.drawGUIControl(new CustomGUIResult<int, GameObject>(i, obj), obj.name, this.myLinkedPortalGateway.gameObject, TransitionObjectGUI.validationArray);
                //         if (result.resultValue != obj)
                //         {
                //             Debug.Log("come get some");
                //             if (this.exitPortalResult.resultValue)
                //             {
                //                 // Set collision Target


                //             }
                //             else
                //             {
                //                 // Remove Collision Target



                //             }
                //         }
                //         Debug.Log(result);

                //     }
                //     EditorGUI.indentLevel--;

                // }
            }
        }



        /*



        private void drawIsVisible(){
            GUILayout.BeginHorizontal();
            this.currentIsVisible = EditorGUILayout.Toggle("Is Visible", this.currentIsVisible);
            if (this.currentIsVisible != this.myLinkedPortalGateway._isVisible){
                this.myLinkedPortalGateway._isVisible = this.currentIsVisible;
            }
            GUILayout.EndHorizontal();	
        }


                private void drawCollisionTargets(){
            SerializedProperty transitionObjectsWhiltelist = serializedObject.FindProperty("transitionObjectsWhiltelist");
            bool hasChanged = false;
            GameObject obj;

            if (EditorGUILayout.PropertyField(transitionObjectsWhiltelist,new GUIContent("Transition Object Whitelist ("  + this.currentTransitionObjectsWhitelist.Count + ")") ,false)){

                if (transitionObjectsWhiltelist.arraySize > this.currentTransitionObjectsWhitelist.Count){
                    SerializedProperty myElement = transitionObjectsWhiltelist.GetArrayElementAtIndex(transitionObjectsWhiltelist.arraySize -1);
                    obj = myElement.objectReferenceValue as GameObject;
                    this.currentTransitionObjectsWhitelist.Add(obj);
                    hasChanged = true;
                }else{
                    EditorGUI.indentLevel++;

                    for (int i =0; i < this.currentTransitionObjectsWhitelist.Count; i++){
                        GUILayout.BeginHorizontal();
                        obj = EditorGUILayout.ObjectField("Element " + i, this.currentTransitionObjectsWhitelist[i], typeof(GameObject), true) as GameObject; 

                        if (obj != this.currentTransitionObjectsWhitelist[i]){
                            this.currentTransitionObjectsWhitelist[i] = obj;
                            hasChanged = true;
                        }


                        if (GUILayout.Button("X",EditorStyles.miniButton, GUILayout.Width(20))){
                            this.currentTransitionObjectsWhitelist[i] = null;
                            hasChanged = true;
                        }						
                        GUILayout.EndHorizontal();
                    }

                    EditorGUI.indentLevel--;

                }; 		

                if(hasChanged){

                    // Ok this is bad. Need to fix this later. 
                    LinkedPortalGateway.transitionWhiltelistValidationResponce responce = this.myLinkedPortalGateway.validateTransitionObjectsWhiltelist(this.currentTransitionObjectsWhitelist);	
                    switch(responce){
                        case LinkedPortalGateway.transitionWhiltelistValidationResponce.NOMESH:
                            EditorUtility.DisplayDialog("Invalid Object",  "Objects on the Whitelist must contrain a MeshRenderer", "OK");
                        break;
                        case LinkedPortalGateway.transitionWhiltelistValidationResponce.REPEATED:
                            EditorUtility.DisplayDialog("Invalid Object",  "Objects can only be on the Whitelist once", "OK");
                        break;
                    }						
                    this.currentTransitionObjectsWhitelist = this.myLinkedPortalGateway._transitionObjectsWhiltelist;

                }
            };
        }





         */





    }
}




/* 
private void drawExitPortalInput()
{

    // Draw control for the exit portal
    this.exitPortalResult = ExitPortalGUI._instance.drawGUIControl(this.exitPortalResult, "Exit Portal", this.myLinkedPortalGateway.gameObject, ExitPortalGUI.validationArray);

    if (this.exitPortalResult.resultValue != (GameObject)this.serializedExitPortal.objectReferenceValue)
    {
        // We have changed 

        // TODO -- Need to get the exit portal to inherit values from this. Will need to make sure they all can be undo'd and save when we exit the program. 
        // Test set up an exit portal. Save. Go to the one you said was the exit. Has its property been saved. 
        // Do above and undo it. Has the property been undone.






        Debug.Log("Change");

        if (this.exitPortalResult.resultValue)
        {
            Undo.RegisterCompleteObjectUndo(this.myLinkedPortalGateway, "Set Exit Portal");

            if ((GameObject)this.serializedExitPortal.objectReferenceValue)
            {




            }
            this.myLinkedPortalGateway._exitPortal = this.exitPortalResult.resultValue;

        }
        else
        {
            Undo.RegisterCompleteObjectUndo(this.myLinkedPortalGateway, "Clear Exit Portal");
            // Need to reset the values that come with this. 


        }

    }

    if (!this.exitPortalResult.resultValue)
    {
        EditorGUILayout.Separator();
        EditorGUILayout.LabelField("An Exit Portal Must be set!!!", CustomEditorStyles._instance._error);
    }


    /* 
    // Test that we have a result value 
    if (this.exitPortalResult.resultValue){
        // Test that result value has changed 
        if (this.exitPortalResult.resultValue != (GameObject)this.serializedExitPortal.objectReferenceValue) {

            Debug.Log("Store undozz!!!!!y");
            Undo.RegisterCompleteObjectUndo( this.myLinkedPortalGateway,"Set Exit Portal");		
            this.myLinkedPortalGateway._exitPortal = this.exitPortalResult.resultValue;
        }
    }else{

    }
    */


/* 
this.currentExitPortalResult = ExitPortalGUI._instance.drawGUIControl(  this.currentExitPortalResult, "Exit Portal", this.myLinkedPortalGateway.gameObject,  ExitPortalGUI.validationArray);
if (this.currentExitPortalResult.resultValue){
    if (this.currentExitPortalResult.resultValue != this.myLinkedPortalGateway._exitPortal){
        // There has been a change

        // Get the script of the new exit portal for processing 
        LinkedPortalGateway currentExitPortalScript = this.currentExitPortalResult.resultValue.GetComponent<LinkedPortalGateway>();

        // Need to work out where we inherit the properties from. Values will only exist is an object already has an exit portal set. So only if one is set is it worth inheriting from. 
        bool inheritFromThis;
        if ( this.myLinkedPortalGateway._exitPortal !=null && currentExitPortalScript._exitPortal != null){ 
            inheritFromThis = EditorUtility.DisplayDialog("Inherit Properties from?", "Which Portal would you like to inherit the linked properties from?", "This portal"  , "Exit portal");
        }else{
            // If the new exit portal doesn't have an exit portal set then inherit from the current one 
            inheritFromThis = currentExitPortalScript._exitPortal ==null;
        }

        // Update the exitPortal of myLinkedPortalGateway; 
        this.myLinkedPortalGateway._exitPortal = this.currentExitPortalResult.resultValue;


        if (inheritFromThis){
            this.myLinkedPortalGateway.giveLinkedProperties();
        }else{
            this.myLinkedPortalGateway.inheritLinkedProperties();
            // Need to get the properties again as they have been updated in the target object. 
            this.currentAspectRatioResult = AspectRatioGUI._instance.getDropDownResultFromValue(this.myLinkedPortalGateway._screenRatio);
            this.currentScreenSizeResult.resultValue = this.myLinkedPortalGateway._screenSize;
        }

        this.myLinkedPortalGateway._exitPortal = this.currentExitPortalResult.resultValue;

    }
}else{
    EditorGUILayout.Separator();
    EditorGUILayout.LabelField("An Exit Portal Must be set!!!", CustomEditorStyles._instance._error);
}
*

}


*/










/* 

private LinkedPortalGateway myLinkedPortalGateway;

private CustomGUIResult<int, GameObject> currentExitPortalResult = new CustomGUIResult<int,GameObject>(0, null);
private CustomGUIResult<AspectRatioIDs, Vector2> currentAspectRatioResult; 
private CustomGUIResult<WidthHeightAnchors, Vector2> currentScreenSizeResult = new CustomGUIResult<WidthHeightAnchors, Vector2>((WidthHeightAnchors)0, new Vector2() );

private void OnEnable(){
    this.myLinkedPortalGateway = (LinkedPortalGateway)target;
    this.currentExitPortalResult.resultValue = this.myLinkedPortalGateway._exitPortal;
    this.currentAspectRatioResult = AspectRatioGUI._instance.getDropDownResultFromValue(this.myLinkedPortalGateway._screenRatio);
    this.currentScreenSizeResult.resultValue = this.myLinkedPortalGateway._screenSize;
}

public override void OnInspectorGUI(){


    EditorGUILayout.Separator();

    if (!this.currentExitPortalResult.resultValue){ 
        EditorGUILayout.LabelField("Own Properties", EditorStyles.boldLabel);
        this.drawExitPortalInput();
    }else{

        EditorGUILayout.LabelField("Linked Properties", EditorStyles.boldLabel);
        this.drawRatioDropDown();
        this.drawWidthHeight();

        EditorGUILayout.Separator();
        EditorGUILayout.LabelField("Own Properties", EditorStyles.boldLabel);
        this.drawExitPortalInput();

    }

    /*
    EditorGUILayout.Separator();
        if (!this.currentExitPortal){
            EditorGUILayout.LabelField("Own Properties", EditorStyles.boldLabel);
            this.drawExitPortalInput();
        }else{
            EditorGUILayout.LabelField("Linked Properties", EditorStyles.boldLabel);

            this.drawAspectRatio();
            //this.drawWidthHeight();
            //this.drawTargetFOV();

            EditorGUILayout.Separator();
            EditorGUILayout.LabelField("Own Properties", EditorStyles.boldLabel);
            this.drawExitPortalInput();
            //this.drawCollisionTargets();
            //this.drawIsVisible();
        }

        EditorGUILayout.Separator();

        GUILayout.Space(50);
        base.OnInspectorGUI();

     */ /*

    GUILayout.Space(50);
    base.OnInspectorGUI();
}


private void drawExitPortalInput(){

    this.currentExitPortalResult = ExitPortalGUI._instance.drawGUIControl(  this.currentExitPortalResult, "Exit Portal", this.myLinkedPortalGateway.gameObject,  ExitPortalGUI.validationArray);
    if (this.currentExitPortalResult.resultValue){
        if (this.currentExitPortalResult.resultValue != this.myLinkedPortalGateway._exitPortal){
            // There has been a change

            // Get the script of the new exit portal for processing 
            LinkedPortalGateway currentExitPortalScript = this.currentExitPortalResult.resultValue.GetComponent<LinkedPortalGateway>();

            // Need to work out where we inherit the properties from. Values will only exist is an object already has an exit portal set. So only if one is set is it worth inheriting from. 
            bool inheritFromThis;
            if ( this.myLinkedPortalGateway._exitPortal !=null && currentExitPortalScript._exitPortal != null){ 
                inheritFromThis = EditorUtility.DisplayDialog("Inherit Properties from?", "Which Portal would you like to inherit the linked properties from?", "This portal"  , "Exit portal");
            }else{
                // If the new exit portal doesn't have an exit portal set then inherit from the current one 
                inheritFromThis = currentExitPortalScript._exitPortal ==null;
            }

            // Update the exitPortal of myLinkedPortalGateway; 
            this.myLinkedPortalGateway._exitPortal = this.currentExitPortalResult.resultValue;


            if (inheritFromThis){
                this.myLinkedPortalGateway.giveLinkedProperties();
            }else{
                this.myLinkedPortalGateway.inheritLinkedProperties();
                // Need to get the properties again as they have been updated in the target object. 
                this.currentAspectRatioResult = AspectRatioGUI._instance.getDropDownResultFromValue(this.myLinkedPortalGateway._screenRatio);
                this.currentScreenSizeResult.resultValue = this.myLinkedPortalGateway._screenSize;
            }

            this.myLinkedPortalGateway._exitPortal = this.currentExitPortalResult.resultValue;

        }
    }else{
        EditorGUILayout.Separator();
        EditorGUILayout.LabelField("An Exit Portal Must be set!!!", CustomEditorStyles._instance._error);
    }
}

private void drawRatioDropDown(){
    // Ratio drop down

    CustomGUIResult<AspectRatioIDs, Vector2> newAspectRatioResult = AspectRatioGUI._instance.drawGUIControl(this.currentAspectRatioResult);
    //this.currentAspectRatioResult = AspectRatioGUI._instance.drawGUIControl(this.currentAspectRatioResult);
    //if (this.myLinkedPortalGateway._screenRatio != this.currentAspectRatioResult.resultValue){
        if (this.myLinkedPortalGateway._screenRatio != newAspectRatioResult.resultValue){


        Debug.Log("SET UNDOzzzr3");
        Undo.RecordObject(this, "Changed Screen Ratio");
        this.currentAspectRatioResult = newAspectRatioResult;

        //Undo.RegisterCompleteObjectUndo (this.myLinkedPortalGateway.gameObject, "Changed Screen Ratio");
        //Undo.RecordObject(this.myLinkedPortalGateway, "Changed Screen Ratio");
        //Undo.RegisterCompleteObjectUndo

        this.myLinkedPortalGateway._screenRatio = this.currentAspectRatioResult.resultValue;



        // update the width height 
        this.calcScreenSize(); 
    }
}

private void drawWidthHeight(){
    this.currentScreenSizeResult =	AnchoredWidthHeightGUI._instance.drawGUIControl(this.currentScreenSizeResult );
    if (!this.currentScreenSizeResult.resultValue.Equals(this.myLinkedPortalGateway._screenSize)){
        // it has changed
        this.calcScreenSize();
    }
}

private void calcScreenSize(){
    this.currentScreenSizeResult.resultValue = this.myLinkedPortalGateway.calcScreenSize(this.currentScreenSizeResult.resultValue ,this.currentScreenSizeResult.resultID == (WidthHeightAnchors)0); 
}

 */


/*
private void drawAspectRatio(){
    this.currentScreenRatioData = AspectRatioGUI.drawGUIControl(this.currentScreenRatioData);
    if (myLinkedPortalGateway._screenRatio != this.currentScreenRatioData.Value){
        myLinkedPortalGateway._screenRatio = this.currentScreenRatioData.Value;
    }
}
*/

/* 
private void drawExitPortalInput(){
    this.currentExitPortal = EditorGUILayout.ObjectField("Exit Portal", this.currentExitPortal, typeof(GameObject), true) as GameObject; 	

    if (this.currentExitPortal){
        if (this.currentExitPortal != this.myLinkedPortalGateway._exitPortal){

            // Check its not set to itself
            if (this.currentExitPortal == myLinkedPortalGateway.gameObject){
                EditorUtility.DisplayDialog("Exit Portal cannot be self", "A portal cannot have its exit portal set to itself", "OK");
                this.currentExitPortal = this.myLinkedPortalGateway._exitPortal; 
                return;
            }

            // Check its not 	

            LinkedPortalGateway currentExitPortalScript = this.currentExitPortal.GetComponent<LinkedPortalGateway>();
            if (!currentExitPortalScript){
                EditorUtility.DisplayDialog("Exit Portal must have the script <LinkedPortalGateway> attached", "Exit Portal must have the script <LinkedPortalGateway> attached", "OK");
                this.currentExitPortal = this.myLinkedPortalGateway._exitPortal; 
                return;
            }

            if (this.myLinkedPortalGateway._exitPortal !=null || currentExitPortalScript._exitPortal != null){

                bool inheritFromThis;
                if ( this.myLinkedPortalGateway._exitPortal !=null && currentExitPortalScript._exitPortal != null){
                    inheritFromThis = EditorUtility.DisplayDialog("Inherit Properties from?", "Which Portal would you like to inherit the linked properties from?", "This portal"  , "Exit portal");
                }else{
                    inheritFromThis = this.myLinkedPortalGateway._exitPortal !=null;
                }

                this.myLinkedPortalGateway._exitPortal = this.currentExitPortal;

                if (inheritFromThis){
                    this.myLinkedPortalGateway.giveLinkedProperties();
                }else{
                    this.myLinkedPortalGateway.inheritLinkedProperties();
                    //this.currentScreenRatio = this.myLinkedPortalGateway._screenRatio; // Doesn't seem enough to work;
                    //this.currentScreenSize = this.myLinkedPortalGateway._screenSize;
                    //this.currentTargetFOV = this.myLinkedPortalGateway._targetFOV;
                }
            }else{
                this.myLinkedPortalGateway._exitPortal = this.currentExitPortal;

            }

        }
    }else{
        EditorGUILayout.Separator();
        EditorGUILayout.LabelField("An Exit Portal Must be set!!!", CustomEditorStyles._instance._error);
    }
}
*/




/* 
    public class LinkedPortalGatway : Editor
    {

        private CMGCO.Unity.TransitionEffects.ScreenPortals.LinkedPortalGateway myLinkedPortalGateway;	
        private enum styleTypes {ERROR, LEFT_ALIGN, BOLD};

        public enum AspectRatioNames {TV, WideTV, WidePC, UltraWide, CinemaScope, Anamorphic};
        private Dictionary<Enum, AspectRatioData> validAspectRatios = new Dictionary<Enum, AspectRatioData>{
            {AspectRatioNames.TV,  new AspectRatioData("TV (4:3)",  new Vector2(1.33f, 1))},
            {AspectRatioNames.WideTV,  new AspectRatioData("Widescreen TV (16:9)",  new Vector2(1.78f, 1))},
            {AspectRatioNames.WidePC,  new AspectRatioData("Widescreen PC (16:10)",  new Vector2(1.61f, 1))},
            {AspectRatioNames.UltraWide,  new AspectRatioData("Ultra Widescreen (21:9)",  new Vector2(2.37f, 1))},
            {AspectRatioNames.CinemaScope,  new AspectRatioData("CinemaScope (2.35:1)",  new Vector2(2.35f, 1))},
            {AspectRatioNames.Anamorphic,  new AspectRatioData("Anamorphic (2.39:1)",  new Vector2(2.39f, 1))},	
        };

        // Properties around the ratio
        private AspectRatioNames selectedRatioName;
        private String[] ratioLables;
        private Vector2[] ratioValues;
        private bool constrianToX = true;


        // Local stores of editable values;
        private GameObject currentExitPortal;
        private Vector2 currentScreenRatio; 
        private Vector2 currentScreenSize;
        private float currentTargetFOV;
        private bool currentIsVisible;

        private List<GameObject> currentTransitionObjectsWhitelist;



        private GUIStyle errorStyle;
        private GUIStyle leftAlignStyle;
        private GUIStyle boldStyle;
        private bool isTrackCollisionsFoldout;



        private GUIStyle getGUIStyle(styleTypes styleType){			

            GUIStyle style = new GUIStyle();

            switch (styleType){
                case styleTypes.ERROR:
                    // redoubling up of new is silly here but it keeps the compiler happy.					
                    style = new GUIStyle();
                    style.normal.textColor = Color.red;
                    style.alignment = TextAnchor.MiddleCenter;;
                    break;
                case styleTypes.LEFT_ALIGN:
                    style = new GUIStyle();
                    style.alignment = TextAnchor.MiddleRight;
                    style.padding.top = 3;
                    style.clipping = TextClipping.Clip;
                    break;
                case styleTypes.BOLD:
                    style = new GUIStyle();
                    style.fontStyle = FontStyle.Bold;
                    style.alignment = TextAnchor.MiddleLeft;
                    style.padding.top = 3;
                    style.clipping = TextClipping.Clip;
                    break;
            }
            return style; 
        }

        private void OnEnable(){

            this.errorStyle = getGUIStyle(styleTypes.ERROR);
            this.leftAlignStyle = getGUIStyle(styleTypes.LEFT_ALIGN);
            this.boldStyle = getGUIStyle(styleTypes.BOLD);

            this.myLinkedPortalGateway = (CMGCO.Unity.TransitionEffects.ScreenPortals.LinkedPortalGateway)target;
            this.currentExitPortal = this.myLinkedPortalGateway._exitPortal;
            this.currentTargetFOV = this.myLinkedPortalGateway._targetFOV;

            #region Init Ratio Values   

                this.currentScreenRatio = this.myLinkedPortalGateway._screenRatio; 

                this.ratioLables = this.validAspectRatios.Select(x => x.Value.ratioDescription).ToArray();
                this.ratioValues = this.validAspectRatios.Select(x => x.Value.ratioValue).ToArray();

                int selectedRatioIndex = Array.IndexOf(this.ratioValues, this.currentScreenRatio);
                if (selectedRatioIndex == -1){
                    // We need to set the ratio to an approved value. This must feed back into the origional object 
                    selectedRatioIndex = 0;
                    this.currentScreenRatio = 	this.ratioValues[selectedRatioIndex];		
                    this.myLinkedPortalGateway._screenRatio = this.currentScreenRatio;
                    this.myLinkedPortalGateway.calcScreenSize(); 

                }
                this.selectedRatioName = (AspectRatioNames)selectedRatioIndex;
            #endregion

            this.currentScreenSize = this.myLinkedPortalGateway._screenSize;
            this.currentTransitionObjectsWhitelist = new List<GameObject>(this.myLinkedPortalGateway._transitionObjectsWhiltelist);

            this.currentIsVisible = this.myLinkedPortalGateway._isVisible;
        }


        public override void OnInspectorGUI(){

            EditorGUILayout.Separator();
            if (!this.currentExitPortal){
                EditorGUILayout.LabelField("Own Properties", EditorStyles.boldLabel);
                this.drawExitPortalInput();
            }else{
                EditorGUILayout.LabelField("Linked Properties", EditorStyles.boldLabel);

                this.drawAspectRatio();
                this.drawWidthHeight();
                this.drawTargetFOV();

                EditorGUILayout.Separator();
                EditorGUILayout.LabelField("Own Properties", EditorStyles.boldLabel);
                this.drawExitPortalInput();
                this.drawCollisionTargets();
                this.drawIsVisible();
            }

            EditorGUILayout.Separator();

            GUILayout.Space(50);
            base.OnInspectorGUI();

        }


        private void drawExitPortalInput(){
            this.currentExitPortal = EditorGUILayout.ObjectField("Exit Portal", this.currentExitPortal, typeof(GameObject), true) as GameObject; 	

            if (this.currentExitPortal){
                if (this.currentExitPortal != this.myLinkedPortalGateway._exitPortal){

                    // Check its not set to itself
                    if (this.currentExitPortal == myLinkedPortalGateway.gameObject){
                        EditorUtility.DisplayDialog("Exit Portal cannot be self", "A portal cannot have its exit portal set to itself", "OK");
                        this.currentExitPortal = this.myLinkedPortalGateway._exitPortal; 
                        return;
                    }

                    // Check its not 	

                    LinkedPortalGateway currentExitPortalScript = this.currentExitPortal.GetComponent<LinkedPortalGateway>();
                    if (!currentExitPortalScript){
                        EditorUtility.DisplayDialog("Exit Portal must have the script <LinkedPortalGateway> attached", "Exit Portal must have the script <LinkedPortalGateway> attached", "OK");
                        this.currentExitPortal = this.myLinkedPortalGateway._exitPortal; 
                        return;
                    }



                    if (this.myLinkedPortalGateway._exitPortal !=null || currentExitPortalScript._exitPortal != null){

                        bool inheritFromThis;
                        if ( this.myLinkedPortalGateway._exitPortal !=null && currentExitPortalScript._exitPortal != null){
                            inheritFromThis = EditorUtility.DisplayDialog("Inherit Properties from?", "Which Portal would you like to inherit the linked properties from?", "This portal"  , "Exit portal");
                        }else{
                            inheritFromThis = this.myLinkedPortalGateway._exitPortal !=null;
                        }

                        this.myLinkedPortalGateway._exitPortal = this.currentExitPortal;

                        if (inheritFromThis){
                            Debug.Log("Inherit from this");
                            this.myLinkedPortalGateway.giveLinkedProperties();
                        }else{
                            Debug.Log("Inheret from Exit");
                            this.myLinkedPortalGateway.inheritLinkedProperties();
                            this.currentScreenRatio = this.myLinkedPortalGateway._screenRatio; // Doesn't seem enough to work;
                            this.currentScreenSize = this.myLinkedPortalGateway._screenSize;
                            this.currentTargetFOV = this.myLinkedPortalGateway._targetFOV;
                        }
                    }else{
                        this.myLinkedPortalGateway._exitPortal = this.currentExitPortal;

                    }

                }
            }else{
                EditorGUILayout.Separator();
                EditorGUILayout.LabelField("An Exit Portal Must be set!!!", this.errorStyle);
            }
        }

        private void drawAspectRatio(){
            GUILayout.BeginHorizontal();
            GUILayout.Label("Aspect Ratio", GUILayout.Width(EditorGUIUtility.labelWidth - 4) );
            float controlSpaceWidth = (EditorGUIUtility.currentViewWidth - 28) -  EditorGUIUtility.labelWidth; //45
            this.selectedRatioName = (AspectRatioNames)EditorGUILayout.Popup((int)this.selectedRatioName ,this.ratioLables, GUILayout.Width((controlSpaceWidth /5) * 3 ));
            this.currentScreenRatio = (Vector2)this.ratioValues.GetValue((int)this.selectedRatioName);

            GUILayout.Label("Actual:", this.leftAlignStyle ,GUILayout.Width( controlSpaceWidth /5 ));

            GUI.enabled = false;
            GUILayout.TextField(this.currentScreenRatio.x.ToString() + ":" + this.currentScreenRatio.y.ToString(), GUILayout.Width(controlSpaceWidth /5));
            GUI.enabled = true;
            GUILayout.EndHorizontal();
            GUILayout.Space(5);

            // Now do some processing of what we just grabbed 
            if (this.currentScreenRatio != this.myLinkedPortalGateway._screenRatio){
                this.myLinkedPortalGateway._screenRatio = this.currentScreenRatio;
                this.currentScreenSize = this.myLinkedPortalGateway.calcScreenSize(); 
            }
        }
        private void drawWidthHeight(){
            GUILayout.BeginHorizontal();

            GUILayout.Label("Size", GUILayout.Width(EditorGUIUtility.labelWidth - 4) );
            float charWidth = 12;
            float controlSpaceWidth = (EditorGUIUtility.currentViewWidth - 31) -  EditorGUIUtility.labelWidth; 

            if (this.constrianToX){
                GUILayout.Label("W", 	this.boldStyle, GUILayout.Width(charWidth));
            }else{
                GUILayout.Label("W", GUILayout.Width(charWidth));
            }
            this.currentScreenSize.x = EditorGUILayout.FloatField(this.currentScreenSize.x,  GUILayout.Width((controlSpaceWidth /2) - charWidth));

            if (!this.constrianToX){
                GUILayout.Label("H", 	this.boldStyle,  GUILayout.Width(charWidth));
            }else{
                GUILayout.Label("H", GUILayout.Width(charWidth));
            }

            this.currentScreenSize.y = EditorGUILayout.FloatField(this.currentScreenSize.y, GUILayout.Width((controlSpaceWidth /2) - charWidth));
            GUILayout.EndHorizontal();
            GUILayout.Space(5);

            if (!this.currentScreenSize.Equals( this.myLinkedPortalGateway._screenSize)){
                // it has changed. 
                this.constrianToX = this.currentScreenSize.x != this.myLinkedPortalGateway._screenSize.x;
                this.currentScreenSize = this.myLinkedPortalGateway.calcScreenSize(this.currentScreenSize,this.constrianToX); 
            }
        }
        private void drawCollisionTargets(){
            SerializedProperty transitionObjectsWhiltelist = serializedObject.FindProperty("transitionObjectsWhiltelist");
            bool hasChanged = false;
            GameObject obj;

            if (EditorGUILayout.PropertyField(transitionObjectsWhiltelist,new GUIContent("Transition Object Whitelist ("  + this.currentTransitionObjectsWhitelist.Count + ")") ,false)){

                if (transitionObjectsWhiltelist.arraySize > this.currentTransitionObjectsWhitelist.Count){
                    SerializedProperty myElement = transitionObjectsWhiltelist.GetArrayElementAtIndex(transitionObjectsWhiltelist.arraySize -1);
                    obj = myElement.objectReferenceValue as GameObject;
                    this.currentTransitionObjectsWhitelist.Add(obj);
                    hasChanged = true;
                }else{
                    EditorGUI.indentLevel++;

                    for (int i =0; i < this.currentTransitionObjectsWhitelist.Count; i++){
                        GUILayout.BeginHorizontal();
                        obj = EditorGUILayout.ObjectField("Element " + i, this.currentTransitionObjectsWhitelist[i], typeof(GameObject), true) as GameObject; 

                        if (obj != this.currentTransitionObjectsWhitelist[i]){
                            this.currentTransitionObjectsWhitelist[i] = obj;
                            hasChanged = true;
                        }


                        if (GUILayout.Button("X",EditorStyles.miniButton, GUILayout.Width(20))){
                            this.currentTransitionObjectsWhitelist[i] = null;
                            hasChanged = true;
                        }						
                        GUILayout.EndHorizontal();
                    }

                    EditorGUI.indentLevel--;

                }; 		

                if(hasChanged){

                    // Ok this is bad. Need to fix this later. 
                    LinkedPortalGateway.transitionWhiltelistValidationResponce responce = this.myLinkedPortalGateway.validateTransitionObjectsWhiltelist(this.currentTransitionObjectsWhitelist);	
                    switch(responce){
                        case LinkedPortalGateway.transitionWhiltelistValidationResponce.NOMESH:
                            EditorUtility.DisplayDialog("Invalid Object",  "Objects on the Whitelist must contrain a MeshRenderer", "OK");
                        break;
                        case LinkedPortalGateway.transitionWhiltelistValidationResponce.REPEATED:
                            EditorUtility.DisplayDialog("Invalid Object",  "Objects can only be on the Whitelist once", "OK");
                        break;
                    }						
                    this.currentTransitionObjectsWhitelist = this.myLinkedPortalGateway._transitionObjectsWhiltelist;

                }
            };
        }

        private void drawTargetFOV(){
            GUILayout.BeginHorizontal();
            this.currentTargetFOV = EditorGUILayout.FloatField("Target FOV", this.currentTargetFOV);
            if (this.currentTargetFOV != this.myLinkedPortalGateway._targetFOV){
                this.myLinkedPortalGateway._targetFOV = this.currentTargetFOV;
            }
            GUILayout.EndHorizontal();	
        }

        private void drawIsVisible(){
            GUILayout.BeginHorizontal();
            this.currentIsVisible = EditorGUILayout.Toggle("Is Visible", this.currentIsVisible);
            if (this.currentIsVisible != this.myLinkedPortalGateway._isVisible){
                this.myLinkedPortalGateway._isVisible = this.currentIsVisible;
            }
            GUILayout.EndHorizontal();	
        }




    }
*/

//}






/* 
		private enum styleTypes {ERROR};
		private CMGCO.Unity.TransitionEffects.ScreenPortals.LinkedPortalGateway myLinkedPortalGateway;

		// We will hold a lot of emus that describe aspect ratios. Holding them here and not in the LinkedPortalGateway class will 
		// save memory as they are not required at runtime 

		public enum AspectRatioNames {TV, WideTV, WidePC, UltraWide, CinemaScope, Anamorphic};
		private Dictionary<Enum, AspectRatioData> validAspectRatios = new Dictionary<Enum, AspectRatioData>{
			{AspectRatioNames.TV,  new AspectRatioData("TV (4:3)",  new Vector2(1.33f, 1))},
			{AspectRatioNames.WideTV,  new AspectRatioData("Widescreen TV (16:9)",  new Vector2(1.78f, 1))},
			{AspectRatioNames.WidePC,  new AspectRatioData("Widescreen PC (16:10)",  new Vector2(1.61f, 1))},
			{AspectRatioNames.UltraWide,  new AspectRatioData("Ultra Widescreen (21:9)",  new Vector2(2.37f, 1))},
			{AspectRatioNames.CinemaScope,  new AspectRatioData("CinemaScope (2.35:1)",  new Vector2(2.35f, 1))},
			{AspectRatioNames.Anamorphic,  new AspectRatioData("Anamorphic (2.39:1)",  new Vector2(2.39f, 1))},	
		};

		private AspectRatioNames selectedRatioName;
		private String[] ratioLables;
		private Vector2[] ratioValues;


		private bool constrianToX = true;



		// To detect changes we need to get a set up our own copies of all the variables in linkedPortalGateway 
		// These will be known as current 

		private GameObject currentExitPortal;
		private Vector2 currentScreenRatio; 
		private Vector2 currentScreenSize;

		
		private GUIStyle errorStyle;
		private GUIStyle getGUIStyle(styleTypes styleType){			
			switch (styleType){
				case styleTypes.ERROR:
					// redoubling up of new is silly here but it keeps the compiler happy.					
					GUIStyle style = new GUIStyle();
					style.normal.textColor = Color.red;
					
					style.alignment = TextAnchor.MiddleCenter;
					return (style);
			}
			return new GUIStyle(); 
		}


        private void OnEnable()
        {
			this.errorStyle = getGUIStyle(styleTypes.ERROR);
            this.myLinkedPortalGateway = (CMGCO.Unity.TransitionEffects.ScreenPortals.LinkedPortalGateway)target;
			
			this.currentExitPortal = this.myLinkedPortalGateway._exitPortal;
			
			this.currentScreenSize = this.myLinkedPortalGateway._screenSize;
			this.currentScreenRatio = this.myLinkedPortalGateway.screenRatio; 

			// Grab the ratio Arrays
			this.ratioLables = this.validAspectRatios.Select(x => x.Value.ratioDescription).ToArray();
			this.ratioValues = this.validAspectRatios.Select(x => x.Value.ratioValue).ToArray();

			
			// Get the name of the currently selected ratio
			int selectedRatioIndex = Array.IndexOf(this.ratioValues, this.myLinkedPortalGateway.screenRatio);
			if (selectedRatioIndex == -1){
				// We need to set the ratio to an approved value. This must feed back into the origional object 
				selectedRatioIndex = 0;
				this.currentScreenRatio = 	this.ratioValues[selectedRatioIndex];		
				this.myLinkedPortalGateway.screenRatio = this.currentScreenRatio;
				this.currentScreenSize = calcScreenSize( this.currentScreenSize, this.currentScreenRatio);
				this.myLinkedPortalGateway._screenSize = this.currentScreenSize;
			}
			this.selectedRatioName = (AspectRatioNames)selectedRatioIndex;		
        }

		private Vector2 calcScreenSize (Vector2 screenSize, Vector2 ratio){
			Vector2 nScreenSize;
			if (this.constrianToX){
				nScreenSize = new Vector2(screenSize.x, (screenSize.x / ratio.x) * ratio.y );
			}else{
				nScreenSize = new Vector2((screenSize.y /ratio.y) * ratio.x, screenSize.y); 
			}
			
			return nScreenSize;
		}

        public override void OnInspectorGUI()
        {

			EditorGUILayout.Separator();
			EditorGUILayout.LabelField("Linked Properties", EditorStyles.boldLabel);
			this.drawExitPortalInput();
			if (this.currentExitPortal){
				this.drawAspectRatio();
				this.drawWidthHeight();
				this.drawFov();

				EditorGUILayout.Separator();
				EditorGUILayout.LabelField("Own Properties", EditorStyles.boldLabel);

			}

			EditorGUILayout.Separator();
        }

		private void drawExitPortalInput(){
			this.currentExitPortal = EditorGUILayout.ObjectField("Exit Portal", this.currentExitPortal, typeof(GameObject), true) as GameObject; 	

			if (this.currentExitPortal){
				if (this.currentExitPortal != this.myLinkedPortalGateway._exitPortal){
					
					// Check its not set to itself
					if (this.currentExitPortal == myLinkedPortalGateway.gameObject){
						EditorUtility.DisplayDialog("Exit Portal cannot be self", "A portal cannot have its exit portal set to itself", "OK");
						this.currentExitPortal = this.myLinkedPortalGateway._exitPortal; 
						return;
					}

					// Check its not 	
					if (!this.currentExitPortal.GetComponent<LinkedPortalGateway>() ){
						EditorUtility.DisplayDialog("Exit Portal must have the script <LinkedPortalGateway> attached", "Exit Portal must have the script <LinkedPortalGateway> attached", "OK");
						this.currentExitPortal = this.myLinkedPortalGateway._exitPortal; 
						return;
					}

					if (this.myLinkedPortalGateway._exitPortal != null){
						this.myLinkedPortalGateway.exitPortalScript.clearExitPortal();
					}
				
					//this.myLinkedPortalGateway.setExitPortal(this.currentExitPortal, true);

					//if (EditorUtility.DisplayDialog("Inherit Properties from?", "Which Portal would you like to inherit the linked properties from?", "This portal"  , "Exit portal")){
			//			Debug.Log("A");  
		//			}else{
	//					Debug.Log("B");
//					}
				}
			}else{
				EditorGUILayout.Separator();
				EditorGUILayout.LabelField("An Exit Portal Must be set!!!", this.errorStyle);
			}
		}

		private void drawAspectRatio(){
			this.selectedRatioName = (AspectRatioNames)EditorGUILayout.Popup("Aspect Ratio", (int)this.selectedRatioName , this.ratioLables);
			//this.currentScreenRatio = (Vector2)this.ratioValues.GetValue((int)this.selectedRatioName);

		}

		private void drawWidthHeight(){

		}

		private void drawFov(){
			
		}

	*/
