using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class GPGSManager : MonoBehaviour
{
    public static GPGSManager instance;
    public UnityAction<bool> onGPGSConnect;
    private PlayGamesLocalUser localUser;

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

}
