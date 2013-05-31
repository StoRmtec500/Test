using UnityEngine;
using System.Collections;

public class MkIntroGUI : MonoBehaviour {

    //public MovieTexture introMovie;
    //public GUIStyle introMovieStyle;
    //------------------------------//
    //------- SPLASHSCREEN ---------//
    public string SPLASHSCREENBEGIN = "---------------------";
    public int guiDepth = 0;
    public Texture splashScreen1;
    public Texture2D[] splashScreen2;
    public float fadeSpeed = 0.3f;
    public float waitTime = 0.05f;
    public float startedTime = 1f;
    public bool startAutomatic = true;
    public int bildNummer;
    public string SPLASHSCREENEND = "---------------------";
    public string levelToLoad = "";
    private bool loadingNextLevel = false;
    private float alpha = 0.0f;
    private float timeFadingToFinish = 0.0f;
    private bool waitForInput = false;

    // LOGO POSITION ANFANG --------- //
    public enum splashType
    {
        loadNextLevelThenFadeOut,
        fadeOutThenLoadNextLevel,
        loadNextSplashScreen
    }
    public splashType SplashType;
    // LOGO POSITION ENDE -------------- //

    // LOGO POSITION ANFANG --------- //
    public enum fadeStatus
    {
        paused,
        started,
        fadeIn,
        fadeOut,
        fadeWaiting
    }
    public fadeStatus status = fadeStatus.started;
    // LOGO POSITION ENDE -------------- //

    // LOGO POSITION ANFANG --------- //
    private Rect splashLogoPos = new Rect();
    public enum logoPositioning
    {
        center,
        streched
    }
    public logoPositioning LogoPositioning;
    // LOGO POSITION ENDE -------------- //


    // ---------------------------------------------------------------------------
    // ---------------------------------------------------------------------------

	// Use this for initialization
	void Start () {
       // this.introMovie.Play();
        if (startAutomatic)
        {
            status = fadeStatus.started;
        }
        else
        {
            status = fadeStatus.paused;
        }

        if (LogoPositioning == logoPositioning.center)
        {
            splashLogoPos.x = (Screen.width * 0.5f) - (splashScreen1.width * 0.5f);
            splashLogoPos.y = (Screen.height * 0.5f) - (splashScreen1.height * 0.5f);

            splashLogoPos.width = splashScreen1.width;
            splashLogoPos.height = splashScreen1.height;
        }
        else
        {
            splashLogoPos.x = 0;
            splashLogoPos.y = 0;

            splashLogoPos.width = Screen.width;
            splashLogoPos.height = Screen.height;
        }

        if (SplashType == splashType.loadNextLevelThenFadeOut)
        {
            DontDestroyOnLoad(this);
        }

        if((Application.levelCount <= 1) || (levelToLoad == ""))
        {
            Debug.LogWarning("Invalid levelToLoad value.");
        }

        bildNummer = 0;
	}
	
	// Update is called once per frame
	public void Update () {
      /*  if (this.introMovie.isPlaying && !Input.anyKeyDown)
            return;
        this.introMovie.Stop();*/
        startedTime = startedTime - Time.deltaTime;
        switch (status)
        {
            case fadeStatus.started:
                if (startedTime <= 0)
                {
                    status = fadeStatus.fadeIn;
                }
                break;
            case fadeStatus.fadeIn:
                alpha += fadeSpeed * Time.deltaTime;
                break;
            case fadeStatus.fadeWaiting:
                if ((!waitForInput && Time.time >= timeFadingToFinish + waitTime) || (waitForInput && Input.anyKey))
                {
                    status = fadeStatus.fadeOut;
                }
                break;
            case fadeStatus.fadeOut:
                alpha += -fadeSpeed * Time.deltaTime;
                break;
        }

       // MkAdministrator.loadScene("01-MainMenu");
	}

    public void startSplash()
    {
        status = fadeStatus.fadeIn;
    }

    public void OnGUI()
    {
        /*float height = (float)((double)Screen.width / 4.0 * 3.0);
        float top = (float)((double)Screen.height * 0.5 - (double)height * 0.5);
        GUI.Label(new Rect(0.0f, 0.0f, (float)Screen.width, (float)Screen.height), string.Empty, this.introMovieStyle);
        GUI.Label(new Rect(0.0f, top, (float)Screen.width, height), (Texture)this.introMovie, this.introMovieStyle);*/
 
        GUI.depth = guiDepth;
            if (splashScreen1 != null)
            {
                GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, Mathf.Clamp01(alpha));
                GUI.DrawTexture(splashLogoPos, splashScreen2[bildNummer]);

                if (alpha > 1.0f)
                {
                    status = fadeStatus.fadeWaiting;
                    timeFadingToFinish = Time.time;
                    alpha = 1.0f;
                    if (SplashType == splashType.loadNextLevelThenFadeOut)
                    {
                        loadingNextLevel = true;
                        {
                            if ((Application.levelCount >= 1) && (levelToLoad != ""))
                            {
                                Application.LoadLevel(levelToLoad);
                            }
                        }
                    }
                }
                if (alpha < 0.0f)
                {
                    if (SplashType == splashType.fadeOutThenLoadNextLevel)
                    {
                        loadingNextLevel = true;
                        {
                            if ((Application.levelCount >= 1) && (levelToLoad != ""))
                            {
                                Application.LoadLevel(levelToLoad);
                            }
                        }
                    }
                    else
                    {
                        Destroy(this);
                    }
                }
            }
    }
}
