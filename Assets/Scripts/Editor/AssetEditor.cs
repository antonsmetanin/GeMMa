using UnityEditor;
using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;



public class AssetEditor : MonoBehaviour
{
	static char[] separators = new char[] { '\\', '/' };
	
	
	static void CreateFolder(string path)
	{
		string[] directoryNames = path.Split(separators);
		
		CreateFolderRecursive(directoryNames, 0);
	}
	
	
	static void CreateFolderRecursive(string[] directoryNames, int index)
	{
		if (directoryNames.Length == index) {
			return;
		}
		
		StringBuilder stringBuilder = new StringBuilder();
		
		if (index > 0) {
			for (int i = 0; i < index; ++i) {
				stringBuilder.Append(directoryNames[i]);
				
				if (i < index - 1) {
					stringBuilder.Append("/");
				}
			}
		}
		
		string parentDirectory = stringBuilder.ToString();
		string currentDirectory = directoryNames[index];
		
		if (!Directory.Exists(parentDirectory + "/" + currentDirectory)) {
			Debug.Log("Creating folder " + currentDirectory + " at " + parentDirectory);
			AssetDatabase.CreateFolder(parentDirectory, currentDirectory);
		} else {
			Debug.Log("" + currentDirectory + " already exists");
		}
		
		CreateFolderRecursive(directoryNames, index + 1);
	}
	
	
	
	static void CreateAssetAtFolder<T>(string folder, string assetName) where T : ScriptableObject, new()
	{
		CreateFolder(folder);
		
		T asset = ScriptableObject.CreateInstance(typeof(T)) as T;
        AssetDatabase.CreateAsset(asset, folder + "/" + assetName);
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
	}
	
	
	[MenuItem("Assets/Create/Action Card")]
    public static void CreateActionCard()
    {
		CreateAssetAtFolder<ActionCardData>("Assets/Data/Action Cards", "Action Card.asset");
    }
	
	
	[MenuItem("Assets/Create/Character")]
    public static void CreateCharacterCard()
    {
		CreateAssetAtFolder<CharacterData>("Assets/Data/Characters", "Character.asset");
    }
}
