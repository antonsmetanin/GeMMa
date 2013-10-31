using UnityEditor;
using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;



public class AssetEditor : MonoBehaviour
{
	static void CreateFolder(string path)
	{
		string[] directoryNames = path.Split(new char[] { '/' });
		
		for (int i = 0, count = directoryNames.Length - 1; i < count; ++i) {
			StringBuilder stringBuilder = new StringBuilder();
			
			for (int j = 0; j < i + 1; ++j) {
				stringBuilder.Append(directoryNames[j]);
				
				if (j < i) {
					stringBuilder.Append("/");
				}
			}
					
			string currentFolderName = stringBuilder.ToString();
			string nextFolderName = directoryNames[i + 1];
			
			if (!Directory.Exists(currentFolderName)) {
				Debug.Log("Creating folder " + nextFolderName + " at " + currentFolderName);
				AssetDatabase.CreateFolder(currentFolderName, nextFolderName);
			} else {
				Debug.Log("" + currentFolderName + " already exists");
			}
		}
	}
	
	static void CreateAsset<T>(string path) where T : ScriptableObject, new()
	{
		T asset = ScriptableObject.CreateInstance(typeof(T)) as T;
        AssetDatabase.CreateAsset(asset, path);
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
	}
	
	
	[MenuItem("Assets/Create/Action Card")]
    public static void CreateActionCard()
    {
		CreateFolder("Assets/1/2/3/4/5/6");
		
//		CreateAsset<ActionCardData>("Assets/Data/Action Cards/Action Card.asset");
    }
	
	
	[MenuItem("Assets/Create/Character")]
    public static void CreateCharacterCard()
    {
//		CreateAsset<CharacterData>("Assets/Data/Characters/Character.asset");
    }
}
