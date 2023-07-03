using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameMaster : MonoBehaviour
{


    private class DataFrame{
        public List<string> sentense;
        public List<int> label;
        public List<List<float>> embedding;
    }

    private void OnLoad()
    {
        string _dataPath = Path.Combine(Application.dataPath, "Resources\\datasets.json");
        
        if (!File.Exists(_dataPath)) return;

        var json = File.ReadAllText(_dataPath);
        print(json);
        
        var obj = JsonUtility.FromJson<DataFrame>(json);
        print(obj);
    }

    // Start is called before the first frame update
    void Start()
    {
        OnLoad();
    }

}
