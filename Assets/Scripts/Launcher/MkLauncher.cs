using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public class MkLauncher : MonoBehaviour
{
    private bool runGameInBackground = true;
    public int windowWidth = 1000;
    public int windowHeight = 400;
    public UILabel _playLabel;
    public GameObject background;
    private const int DefaultWidth = 1024;
    private const int DefaultHight = 768;
    public const string screenHeightKey = "height";
    public const string screenWidthKey = "width";
    public string resolutionListName = "UIList";
    public const string screenFullscreenKey = "fullscreen";
    public string[] availableResolution;
    public UICheckbox _isFullscreenCheckbox;
    public bool _isFullscreen;
    
    private int _windowHeight;
    private int _windowWidth;
    private bool _fullscreen;

    private void Awake()
    {
        Time.timeScale = 1f;
        this.AdjustLauncherSize();
        this.setupGUI();
        this.readSettings();
        Application.runInBackground = this.runGameInBackground;
    }

    private void AdjustLauncherSize()
    {
        Screen.SetResolution(this.windowWidth, windowHeight, false);
    }

    private void readSettings()
    {
        this._windowHeight = !PlayerPrefs.HasKey("height") ? 768 : PlayerPrefs.GetInt("height");
        this._windowWidth = !PlayerPrefs.HasKey("width") ? 1024 : PlayerPrefs.GetInt("width");
        this._fullscreen = PlayerPrefs.HasKey("fullscreen");
    }

    // Use this for initialization
    void Start()
    {
        //this._playLabel = ExtensionMethods.FindComponent<UILabel>(this.transform, "TestLabel", true);
        //this._isFullscreenCheckbox = GameObject.Find("chkFullsreen").GetComponent<UICheckbox>();
        this._playLabel = GameObject.Find("TestLabel").GetComponent<UILabel>();
        _playLabel.text = MkTextProvider.getTextFromId("game_start");
        if ((UnityEngine.Object)this._playLabel != (UnityEngine.Object)null)
            UIEventListener.Get(this._playLabel.gameObject).onClick += new UIEventListener.VoidDelegate(this.StartGame);
    }

    private void setupGUI()
    {
        NGUITools.SetActive(this.background, true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void StartGame(GameObject go)
    {
        this.loadNextScene();
        this.applySettings();
        this.StoreSettings();
    }

    private void loadNextScene()
    {
        Application.LoadLevel("00-Intro");
    }

    private void applySettings()
    {
        this._windowWidth = 1024;
        this._windowHeight = 768;
        this._isFullscreen = this._isFullscreenCheckbox.isChecked;
        Screen.SetResolution(this._windowWidth, this._windowHeight, this._isFullscreen);
    }

    public static string FormResolutionString(int width, int height)
    {
        return width.ToString() + "x" + height.ToString();
    }

    private void StoreSettings()
    {
        int num1;
        int num2;

        num1 = this._windowWidth;
        num2 = this._windowHeight;
        PlayerPrefs.SetInt("height", num2);
        PlayerPrefs.SetInt("width", num1);
    }

}
