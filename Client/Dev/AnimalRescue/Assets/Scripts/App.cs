using GooglePlayGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class App : MonoBehaviour
{
    public enum eSceneType
    {
        App, Logo, Loading, Title, Game, Loby, Shop
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
        GPGSManager.instance.Init();

        GPGSManager.instance.onGPGSConnect = (status) =>
        {
            this.LoadScene<LogoMain>(eSceneType.Logo);
        };

        this.LoadScene<LogoMain>(eSceneType.Logo);

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
                case eSceneType.Logo:
                    {
                        this.uiApp.FadeOutImmediately();

                        var logoMain = main as LogoMain;
                        logoMain.AddListener("onShowLogoComplete", (param) =>
                        {
 
                            this.uiApp.FadeOut(0.5f, () =>
                            {
                                this.LoadScene<LoadingMain>(eSceneType.Loading);
                            });

                        });

                        this.uiApp.FadeIn(2f, () =>
                        {
                            logoMain.Init();
                        });
                        break;
                    }
                case eSceneType.Loading:
                    {
                        this.uiApp.FadeIn(0.5f, () =>
                        {
                            main.AddListener("onLoadComplete", (data) =>
                            {
                                this.uiApp.FadeOut(0.5f, () =>
                                {
                                    this.LoadScene<TitleMain>(eSceneType.Title);
                                });
                            });
                            main.Init();
                        });

                        break;
                    }
                case eSceneType.Title:
                    {
                        this.uiApp.FadeIn();

                        main.AddListener("onClickGameReady", (data) =>
                        {
                            this.uiApp.FadeOut(0.5f, () =>
                            {
                                this.LoadScene<GameMain>(eSceneType.Game);
                            });
                        });
                        main.AddListener("onClickShop", (data) =>
                        {
                            this.uiApp.FadeOut(0.5f, () =>
                            {
                                this.LoadScene<GameMain>(eSceneType.Shop);
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
                case eSceneType.Shop:
                    {
                        this.uiApp.FadeIn();

                        main.AddListener("onClickTitle", (data) =>
                        {
                            this.uiApp.FadeOut(0.5f, () =>
                            {
                                this.LoadScene<TitleMain>(eSceneType.Title);
                            });
                        });
                        main.Init();
                        break;
                    }
            }
        };
        
    }

}
