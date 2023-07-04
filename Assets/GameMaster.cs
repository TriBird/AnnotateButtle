using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GameMaster : MonoBehaviour
{

    [System.Serializable]
    private class DataFrame
    {
        public Dataset[] datasets;
    }

    [System.Serializable]
    public class Dataset
    {
        public string text = "";
        public int target = 0;
        public float[] embed;
    }

    public GameObject SentenceCard_Prefab;
    public Transform SentenseBox_Trans, DragObj_Trans;
    public AnnoFieldCtrl annofield_ctrl;
    public Text AnnotationRemain;
    
    public int CurrentPageIndex = 0;

    private List<int> annotater = new List<int>();
    private List<Dataset> Datasets = new List<Dataset>();

    // Start is called before the first frame update
    void Start()
    {
        AnnotationRemain.text = annotater.Count + "/10";
        Datasets = OnLoad();
        SentenceUpdate();
    }

    public void Annotate(int n){
        // limited annotate
        if(annotater.Count >= 10) return;

        // add annotate
        int number = CurrentPageIndex * 10 + n;
        if(!annotater.Contains(number)){
            annotater.Add(number);
            AnnotationRemain.text = annotater.Count + "/10";
        }
    }

    public void ChangePage(int n){
        CurrentPageIndex = n;
        SentenceUpdate();
    }

    private void SentenceUpdate(){
        foreach(Transform tmp in SentenseBox_Trans){ Destroy(tmp.gameObject); }

        int counter = 0;
        foreach(Dataset data in Datasets){
            if(data.target == CurrentPageIndex){
                GameObject tmp = Instantiate(SentenceCard_Prefab, SentenseBox_Trans);
                tmp.GetComponentInChildren<Text>().text = data.text;
                tmp.GetComponent<CardCtrl>().gm = this;
                tmp.GetComponent<CardCtrl>().CardNumber = counter;

                counter++;
            }
        }
    }

    private List<Dataset> OnLoad()
    {
        string _dataPath = Path.Combine(Application.dataPath, "Resources\\datasets.json");
        if (!File.Exists(_dataPath)) return null;

        var json = File.ReadAllText(_dataPath);
        DataFrame df = JsonUtility.FromJson<DataFrame>(json);

        return new List<Dataset>(df.datasets);
    }


}
