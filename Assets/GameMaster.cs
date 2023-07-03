using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameMaster : MonoBehaviour
{

    [System.Serializable]
    private class DataFrame{
        public Dataset[] datasets;
    }

    [System.Serializable]
    public class Dataset{
        public string text = "";
        public int target = 0;
        public float[] embed;
    }

    private List<Dataset> OnLoad()
    {
        string _dataPath = Path.Combine(Application.dataPath, "Resources\\datasets.json");
        
        if (!File.Exists(_dataPath)) return null;

        var json = File.ReadAllText(_dataPath);
        print(json);
        
        DataFrame df = JsonUtility.FromJson<DataFrame>(json);
        print(df.datasets[0].text);
        print(df.datasets[0].target);
        print(df.datasets[0].embed);

        return null;
    }

    // Start is called before the first frame update
    void Start()
    {
        OnLoad();
    }

}
