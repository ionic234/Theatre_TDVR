using UnityEditor;

namespace CMGCO.Unity.CustomGUI.Base
{
    public class CustomEditorGUIUtility
    {

        public static int flowBreakWidth = 332;
        public static float singleCharWidth = 15f;

        public static float fieldLineVerticalPadding = 2;

        public static float getControlRectHeight(int controlLines)
        {
            return EditorGUIUtility.singleLineHeight * controlLines + (fieldLineVerticalPadding * controlLines - 1);
        }
    }
}