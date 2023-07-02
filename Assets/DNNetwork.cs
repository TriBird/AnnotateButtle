using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;

public class DNNetwork : MonoBehaviour {
    private string pyExePath = @"C:\Users\shige\AppData\Local\Programs\Python\Python38\python.exe";
    private string pyCodePath = @"C:\Users\shige\HARPhone\Assets\deep.py";

    private void Start () {
        ProcessStartInfo processStartInfo = new ProcessStartInfo() {
            FileName = pyExePath, 
            UseShellExecute = false,
            CreateNoWindow = true, 
            RedirectStandardOutput = true, 
            Arguments = pyCodePath + " " + "Hello,python.", 
        };

        Process process = Process.Start(processStartInfo);

        StreamReader streamReader = process.StandardOutput;
        string str = streamReader.ReadLine();

        process.WaitForExit();
        process.Close();

        print(str);
    }
}