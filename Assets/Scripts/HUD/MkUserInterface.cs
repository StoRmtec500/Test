using UnityEngine;
using System.Collections;

public class MkUserInterface : MonoBehaviour {

    public static MkUserInterface instance;
    public static bool screenActive;
    public MkScreen [] screens;
    public MkScreen currentScreen;

    static MkUserInterface()
    {
    }

	// Use this for initialization
	public void Start () 
    {
        MkUserInterface.instance = this;
        this.showScreen(MkUserInterface.MkScreenType.ProfileChooser);
	}
	
	// Update is called once per frame
	public void Update () {

    //    if ((Object)this.currentScreen == (Object)null)
      //  {
            //this.showScreen(MkUserInterface.MkScreenType.Logbook);
      //  }
      //  else
      //      this.hideCurrentScreen(false);
	}

    public void showScreen(MkUserInterface.MkScreenType screenType)
    {
        this.hideCurrentScreen(false);
        this.currentScreen = this.screens[(int)screenType];
        this.currentScreen.showScreen();
        MkUserInterface.screenActive = true;
        if (screenType != MkUserInterface.MkScreenType.Logbook && screenType != MkUserInterface.MkScreenType.ProfileChooser)
            return;
    }

    public void hideCurrentScreen(bool keepPaused)
    {
        if (!((Object)this.currentScreen != (Object)null))
            return;
        this.currentScreen.hideScreen();
        MkUserInterface.screenActive = false;
    }

    public enum MkScreenType
    {
        ProfileChooser,
        Logbook,
    }
}
