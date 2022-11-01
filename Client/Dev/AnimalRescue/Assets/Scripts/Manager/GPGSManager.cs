using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using Newtonsoft.Json;
using System;

public class GPGSManager : MonoBehaviour
{
    public static GPGSManager instance;
    public UnityAction<bool> onGPGSConnect;
    private PlayGamesLocalUser localUser;

    public UnityAction onSavedCloud;
    public UnityAction<GameInfo> onLoadedCloud;
    public UnityAction<string> onErrorHandler;
    private bool isAccessed;
    ISavedGameMetadata myMetadata;
    private GameInfo gameInfo;

    private void Awake()
    {
        GPGSManager.instance = this;
    }

    public void Init()
    {
        Application.targetFrameRate = 60;
        Application.runInBackground = true;

        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();

        PlayGamesPlatform.Instance.Authenticate((status) =>
        {
            Debug.Log("=====================> : " + status);
            if (status == SignInStatus.Success)
            {
                this.StartCoroutine(this.WaitForAuthenticate(() => 
                {

                    Debug.Log("Social.localUser.id : " + Social.localUser.id);
                    Debug.Log("PlayGamesPlatform.Instance.localUser.id : " +PlayGamesPlatform.Instance.localUser.id);
                    Debug.Log("localUser : " + this.localUser);
                    Debug.Log("localUser.gameId : " + this.localUser.gameId);
                    Debug.Log("localUser.authenticated : " + this.localUser.authenticated);
                    this.onGPGSConnect(true);
                    //this.textUserId.text = this.localUser.id;
                    //PlayGamesPlatform.Instance.RequestServerSideAccess(true, (token) =>
                    //{
                    //    Debug.Log("****************** token *******************");
                    //    Debug.Log(token);
                    //    Debug.Log("****************** token *******************");

                    //    FirebaseAuth auth = FirebaseAuth.DefaultInstance;
                    //    Credential credential = PlayGamesAuthProvider.GetCredential(token);
                    //    auth.SignInWithCredentialAsync(credential).ContinueWith(task =>
                    //    {
                    //        if (task.IsCanceled)
                    //        {
                    //            Debug.LogFormat("********** task.IsCancled ************");
                    //        }
                    //        if (task.IsFaulted)
                    //        {
                    //            Debug.LogFormat("********** task.IsFaulted ************");
                    //        }
                    //        FirebaseUser newuser = task.Result;
                    //        if (newuser != null)
                    //        {
                    //            Debug.LogFormat("[newuser] DisplayName : {0} UserId : {1}", newuser.DisplayName, newuser.UserId);
                    //            string uid = newuser.UserId;
                    //            this.textUserId.text = uid;
                    //        }
                    //        else
                    //        {
                    //            Debug.LogFormat("currentuser is null");
                    //        }

                    //        FirebaseUser currentUser = auth.CurrentUser;

                    //        if (currentUser != null)
                    //        {
                    //            Debug.LogFormat("[CurrentUser] DisplayName : {0} UserId : {1}", currentUser.DisplayName, currentUser.UserId);
                    //            string uid = currentUser.UserId;
                    //            this.textUserId.text = uid;
                    //            SceneManager.LoadSceneAsync("Game").completed += (oper) => {

                    //                GameObject.FindObjectOfType<GameMain>().Init();

                    //            };
                    //        }
                    //        else
                    //        {
                    //            Debug.LogFormat("currentuser is null");
                    //        }
                    //    });

                    //});


                }));
            }
        });


    }

    private IEnumerator WaitForAuthenticate(UnityAction callback)
    {
        while (true)
        {
            if (this.localUser == null)
            {
                this.localUser = PlayGamesPlatform.Instance.localUser as PlayGamesLocalUser;
                break;
            }
            yield return null;
        }

        callback();
    }



    private void AccessCloud()
    {
        Debug.Log("AccessCloud");
        this.isAccessed = false;
    }

    //클라우드에 저장된 데이터 접근  (비동기)
    private void OpenSavedCloud()
    {
        Debug.Log("OpenSavedCloud");
        var client = PlayGamesPlatform.Instance.SavedGame;
        Debug.LogFormat("client: {0}", client);

        //요청 
        client.OpenWithAutomaticConflictResolution("sc",
            GooglePlayGames.BasicApi.DataSource.ReadCacheOrNetwork,
            ConflictResolutionStrategy.UseLongestPlaytime,
            (status, metaData) =>
            {

                if (status == SavedGameRequestStatus.Success)
                {
                    this.myMetadata = metaData;
                    this.isAccessed = true;
                }
                else
                {
                    this.onErrorHandler(status.ToString());
                    this.isAccessed = false;
                }

            });
    }



    public void SaveToCloud(GameInfo info)
    {
        gameInfo = info;

        Debug.LogFormat("[SaveToCloud] info: {0}", info);

        //인증 -> 데이터 엑세스 -> 저장 시도 
        PlayGamesPlatform.Instance.Authenticate((status) =>
        {
            Debug.LogFormat("[SaveToCloud] Authenticate: {0}", status);

            if (status == GooglePlayGames.BasicApi.SignInStatus.Success)
            {
                this.AccessCloud();
                this.OpenSavedCloud();
                this.StartCoroutine(this.SaveGame());
            }
            else
            {
                this.onErrorHandler(status.ToString());
            }

        });
    }

    private IEnumerator SaveGame()
    {
        yield return new WaitUntil(() => this.isAccessed);

        //while (true) {
        //    if (this.isAccessed) break;
        //    yield return null;
        //}

        var json = JsonConvert.SerializeObject(this.gameInfo);
        var data = System.Text.Encoding.UTF8.GetBytes(json);

        var client = PlayGamesPlatform.Instance.SavedGame;
        SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder();

        builder = builder.WithUpdatedPlayedTime(new System.TimeSpan())
            .WithUpdatedDescription("saved at " + DateTime.Now);

        SavedGameMetadataUpdate update = builder.Build();

        //비동기 
        client.CommitUpdate(this.myMetadata, update, data, (status, metadata) =>
        {

            if (status == SavedGameRequestStatus.Success)
            {
                Debug.Log("*** cloud save complete!!!! ***");
                this.onSavedCloud();
            }
            else
            {
                this.onErrorHandler(status.ToString());
            }
        });
    }


    public void LoadFromCloud()
    {
        //인증 -> 데이터 엑세스 -> 로드 시도 
        PlayGamesPlatform.Instance.Authenticate((status) =>
        {
            if (status == GooglePlayGames.BasicApi.SignInStatus.Success)
            {
                this.AccessCloud();
                this.OpenSavedCloud();

                this.StartCoroutine(this.LoadGame());
            }
            else
            {
                this.onErrorHandler(status.ToString());
            }
        });
    }
    private IEnumerator LoadGame()
    {
        yield return new WaitUntil(() => this.isAccessed);
        this.LoadGameData(this.myMetadata);
    }

    private void LoadGameData(ISavedGameMetadata metaData)
    {
        ISavedGameClient client = PlayGamesPlatform.Instance.SavedGame;
        client.ReadBinaryData(metaData, (status, data) =>
        {
            if (status == SavedGameRequestStatus.Success)
            {
                Debug.Log("Load Complete");
                var json = System.Text.Encoding.UTF8.GetString(data);
                Debug.Log(data);
                var gameInfo = JsonConvert.DeserializeObject<GameInfo>(json);
                InfoManager.instance.SetInfo(gameInfo);
                this.onLoadedCloud(gameInfo);
            }
            else
            {
                Debug.Log("로드 실패");
                this.onErrorHandler(status.ToString());
            }
        });
    }
}
