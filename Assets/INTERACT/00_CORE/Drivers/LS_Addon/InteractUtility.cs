
public class InteractUtility
{

    public static string SaveFilePanel(string title, string initialDirectory, string defaultname, string extension)
    {
        #if UNITY_EDITOR
        return (UnityEditor.EditorUtility.SaveFilePanel(title, initialDirectory, defaultname, extension));
        #else
        return "";
        #endif
    }

    public static void ClearProgressBar()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorUtility.ClearProgressBar();
        #endif
    }

    public static void DisplayProgressBar(string title, string info, float progress)
    {
        #if UNITY_EDITOR
        UnityEditor.EditorUtility.DisplayProgressBar(title, info, progress);
        #endif
    }

    public static void AddTag(string tag)
    {
        #if UNITY_EDITOR
        UnityEngine.Object[] asset = UnityEditor.AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset");
        if ((asset != null) && (asset.Length > 0))
        {
            UnityEditor.SerializedObject so = new UnityEditor.SerializedObject(asset[0]);
            UnityEditor.SerializedProperty tags = so.FindProperty("tags");

            for (int i = 0; i < tags.arraySize; ++i)
            {
                if (tags.GetArrayElementAtIndex(i).stringValue == tag)
                {
                    return;     // Tag already present, nothing to do.
                }
            }

            tags.InsertArrayElementAtIndex(0);
            tags.GetArrayElementAtIndex(0).stringValue = tag;
            so.ApplyModifiedProperties();
            so.Update();
        }
        #endif
    }

    public static bool CheckTag(string tag)
    {
        #if UNITY_EDITOR
        UnityEngine.Object[] asset = UnityEditor.AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset");
        if ((asset != null) && (asset.Length > 0))
        {
            UnityEditor.SerializedObject so = new UnityEditor.SerializedObject(asset[0]);
            UnityEditor.SerializedProperty tags = so.FindProperty("tags");

            for (int i = 0; i < tags.arraySize; ++i)
            {
                if (tags.GetArrayElementAtIndex(i).stringValue == tag)
                {
                    return true;     // Tag already present, nothing to do.
                }
            }

            return false;
        }
        #endif
        return false;
    }

}