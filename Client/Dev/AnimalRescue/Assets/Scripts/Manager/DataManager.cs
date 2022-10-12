using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using System;

public class DataManager
{
    public static readonly DataManager instance = new DataManager();

    public UnityEvent<string, float> onDataLoadComplete = new UnityEvent<string, float>();
    public UnityEvent onDataLoadFinished = new UnityEvent();

    private Dictionary<int, RawData> dicDatas = new Dictionary<int, RawData>();
    private List<DatapathData> dataPathList;

    private DataManager()
    {
    }

    public void Init()
    {
        var datapath = "Datas/datapath_data";
        var asset = Resources.Load<TextAsset>(datapath);
        var json = asset.text;
        var datas = JsonConvert.DeserializeObject<DatapathData[]>(json);

        this.dataPathList = datas.ToList();
    }
    public void LoadData<T>(string json) where T : RawData
    {
        var datas = JsonConvert.DeserializeObject<T[]>(json);
        datas.ToDictionary(x => x.id).ToList().ForEach(x => dicDatas.Add(x.Key, x.Value));
    }

    public void LoadAllData()
    {
        App.instance.StartCoroutine(LoadAllDataRoutine());
    }
    public void LoadAllData(MonoBehaviour mono)
    {
        mono.StartCoroutine(LoadAllDataRoutine());
    }
    private IEnumerator LoadAllDataRoutine()
    {
        int idx = 0;
        // 데이터 없을때 테스트 용도
        if (dataPathList.Count == 0)
        {
            for (int i = 0; i < 10; i++)
            {
                this.onDataLoadComplete.Invoke("호출할 데이터 없음", (float)(i + 1) / 10);
                yield return new WaitForSeconds(0.1f);
            }
        }

        foreach (var data in this.dataPathList)
        {
            var path = string.Format("Datas/{0}", data.res_name);
            ResourceRequest req = Resources.LoadAsync<TextAsset>(path);
            yield return req;
            float progress = (float)(idx + 1) / this.dataPathList.Count;
            this.onDataLoadComplete.Invoke(data.res_name, progress);
            TextAsset asset = (TextAsset)req.asset;
            var typeDef = Type.GetType(data.type);
            this.GetType().GetMethod(nameof(LoadData))
                .MakeGenericMethod(typeDef).Invoke(this, new string[] { asset.text });

            yield return new WaitForSeconds(0.3f);
            idx++;
        }
        yield return null;

        this.onDataLoadFinished.Invoke();
    }


    // id 값으로 딕셔너리에서 검색하는 메소드
    public T GetData<T>(int id) where T : RawData
    {
        var collection = this.dicDatas.Values.Where(x => x.GetType().Equals(typeof(T)));
        return (T)collection.ToList().Find(x => x.id == id);
    }

    // 특정 데이터 그룹을 검색하고싶을대 해당 데이터 타입을 호출하면 해당 데이터타입 객체만 반환한다.
    // ex(GetDatas<Student>()) = 딕셔너리에 저장된 데이터들중 Student타입을 가진 객체들만 반환
    public IEnumerable<T> GetDataList<T>() where T : RawData
    {
        IEnumerable<RawData> col = this.dicDatas.Values.Where(x => x.GetType().Equals(typeof(T)));
        return col.Select(x => (T)Convert.ChangeType(x, typeof(T)));
    }

}