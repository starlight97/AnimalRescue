using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class App : MonoBehaviour
{
    public enum eSceneType
    {
        App, LogoScene, LoadingScene, TitleScene, Game
    }

    public static App instance;

    private UIApp uiApp;

    private void Awake()
    {
        App.instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        this.uiApp = GameObject.FindObjectOfType<UIApp>();
        this.uiApp.Init();

        this.LoadScene<LogoMain>(eSceneType.LogoScene);
    }

    public void LoadScene<T>(eSceneType sceneType) where T : SceneMain
    {
        var idx = (int)sceneType;
        
        SceneManager.LoadSceneAsync(idx).completed += (obj) =>
        {
            var main = GameObject.FindObjectOfType<T>();

            main.onDestroy.AddListener(() =>
            {
                uiApp.FadeOut();
            });

            switch (sceneType)
            {
                case eSceneType.LogoScene:
                    {
                        this.uiApp.FadeOutImmediately();

                        var logoMain = main as LogoMain;
                        logoMain.AddListener("onShowLogoComplete", (param) =>
                        {
 
                            this.uiApp.FadeOut(0.5f, () =>
                            {
                                this.LoadScene<LoadingMain>(eSceneType.LoadingScene);
                            });

                        });

                        this.uiApp.FadeIn(2f, () =>
                        {
                            logoMain.Init();
                        });
                        break;
                    }
                case eSceneType.LoadingScene:
                    {
                        this.uiApp.FadeIn(0.5f, () =>
                        {
                            main.AddListener("onLoadComplete", (data) =>
                            {
                                this.uiApp.FadeOut(0.5f, () =>
                                {
                                    this.LoadScene<TitleMain>(eSceneType.TitleScene);
                                });
                            });
                            main.Init();
                        });

                        break;
                    }
                case eSceneType.TitleScene:
                    {
                        this.uiApp.FadeIn();

                        main.AddListener("onClick", (data) =>
                        {
                            this.uiApp.FadeOut(0.5f, () =>
                            {
                                this.LoadScene<GameMain>(eSceneType.Game);
                            });
                        });
                        main.Init();
                        break;
                    }
                case eSceneType.Game:
                    {
                        this.uiApp.FadeIn();

                        main.AddListener("onGameOver", (data) =>
                        {
                            this.uiApp.FadeOut(0.5f, () =>
                            {
                                this.LoadScene<GameMain>(eSceneType.Game);
                            });
                        });
                        main.Init();
                        break;
                    }
            }
        };
        
    }

}
