using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using DG.Tweening;
using System.Diagnostics;

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
    public Transform SentenseBox_Trans, DragObj_Trans, Train_Trans, RightBG_Trans;
    public Transform Label_Trans, Category_Trans, AnnoRemain_Trans;
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
        TrainView(false);
    }

    public void TrainRun(){
        // ui masking
        RightBG_Trans.DOLocalMoveX(260f, 0.3f);
        Train_Trans.gameObject.SetActive(false);

        Sequence seq = DOTween.Sequence();
        seq.Insert(0, Label_Trans.DOLocalMoveX(-1200f, 0.3f));
        seq.Insert(0.1f, Category_Trans.DOLocalMoveX(-1200f, 0.3f));
        seq.Insert(0.2f, AnnoRemain_Trans.DOLocalMoveX(-1200f, 0.3f));

        // string make
        string csvmake = "";
        foreach(int a in annotater){
            csvmake += a + ",";
        }
        csvmake = csvmake.Remove(csvmake.Length - 1);

        // save label to csv, then run deep.py
        string _dataPath = Path.Combine(Application.dataPath, "Resources\\labeled.csv");
        using (StreamWriter sw = new StreamWriter(_dataPath, false, System.Text.Encoding.GetEncoding("utf-8"))){
            sw.WriteLine(csvmake);
        }

        // run deep
        ConnectPython();
    }

    private void ConnectPython(){
        ProcessStartInfo psInfo = new ProcessStartInfo();
        psInfo.FileName = @"C:\Users\shige\anaconda3\envs\M1GP\python.exe";
        psInfo.Arguments = string.Format("\"{0}\" {1}", @"C:\Users\shige\HARPhone\Assets\Python_scr\deep.py", "");
        psInfo.CreateNoWindow = true;
        psInfo.UseShellExecute = false;
        psInfo.RedirectStandardOutput = true;
        Process p = Process.Start(psInfo); 
        DOVirtual.DelayedCall(3, ()=>{
            print(p.StandardOutput.ReadLine());
        });
    }

    public void TrainView(bool isView){
        if(isView){
            Train_Trans.Find("Mask").GetComponent<Image>().fillAmount = 1;
            Train_Trans.gameObject.SetActive(true);

            DOVirtual.DelayedCall(0.5f, ()=>{
                //DOVirtual.Float(from, to, duration, onUpdate)
                DOVirtual.Float(1f, 0f, 0.5f, value => {
                    Train_Trans.Find("Mask").GetComponent<Image>().fillAmount = value;
                });
            });
        } else {
            Train_Trans.gameObject.SetActive(false);
        }
    }

    public bool Annotate(int n){
        // limited annotate
        if(annotater.Count >= 10) return false;

        // add annotate
        int number = CurrentPageIndex * 10 + n;
        if(!annotater.Contains(number)){
            annotater.Add(number);
            AnnotationRemain.text = annotater.Count + "/10";
        }

        if(annotater.Count >= 10){
            TrainView(true);
        }

        return true;
    }

    public void Annotate_del(int n){
        int number = CurrentPageIndex * 10 + n;
        if(annotater.Contains(number)){
            annotater.Remove(number);
            AnnotationRemain.text = annotater.Count + "/10";
        }
        TrainView(false);
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

                // isSelected...?
                int number = CurrentPageIndex * 10 + counter;
                if(annotater.Contains(number)){
                    tmp.GetComponent<Image>().color = new Color32(0xac, 0x53, 0x53, 0xff);
                    tmp.GetComponent<CardCtrl>().isSelected = true;
                }

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
