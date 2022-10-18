using System.IO;
using UnityEditor;
using UnityEngine;

public class InfoMenu
{
    [MenuItem("AnimalRescue/game_info/delete")]
    public static void DeleteGameInfo()
    {
        var path = string.Format("{0}/game_info.json", Application.persistentDataPath);
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("game_info.json deleted");
        }
        else
        {
            Debug.Log("game_info.json not found.");
        }

        // https://answers.unity.com/questions/43422/how-to-implement-show-in-explorer.html
        Application.OpenURL(string.Format("file://{0}", Application.persistentDataPath));
    }

    [MenuItem("AnimalRescue/game_info/show in explorer")]
    public static void ShowInExplorer()
    {
        Application.OpenURL(string.Format("file://{0}", Application.persistentDataPath));
    }
}
