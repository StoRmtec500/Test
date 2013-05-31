using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System;

public class MkAdministrator : MonoBehaviour {

    public static string dataPath = string.Empty;
    public static string lastSceneName = string.Empty;
    private bool allowQuit;
    public string LANGUAGE = "---------------------";
    public string languageDataPath = "Assets/Data/";
    public string languageFile;

    static MkAdministrator()
    {

    }

    public void Awake()
    {
        MkAdministrator.dataPath = Application.dataPath + "/Data/";
        DontDestroyOnLoad(this);
    }
	// Use this for initialization
	void Start () {
        if (File.Exists(MkAdministrator.dataPath + languageFile))
            new MkTextProvider(MkAdministrator.dataPath + languageFile).parse();
	}

    public static void loadScene(string sceneName)
    {
        MkAdministrator.lastSceneName = Application.loadedLevelName;
        string key = sceneName;
        if (key == "01-MainMenu" || key == "Launcher" )
        {
            MkDialogue.isDialogueEnabled();
        }
        Application.LoadLevel(sceneName);
    }

    public void OnApplicationQuit()
    {
        if (Application.isEditor || this.allowQuit)
            return;
        Application.CancelQuit();
        MkDialogue.showDialogue(MkTextProvider.getTextFromId("exit_game_question"), new MkDialogue.MkApplyCallback(this.exitApplicationCallback), (MkDialogue.MkApplyCallback) null, true);
    }

    public void exitApplicationCallback()
    {
        this.allowQuit = true;
        Application.Quit();
    }
}
