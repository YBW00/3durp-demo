using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.IO;

public class RIGHTCONTROLLERLOG : MonoBehaviour
{
    [SerializeField] string LogFileNameSuffix = "RightController";
    [SerializeField] GameObject Target;
    private DateTime StartTime;
    private DateTime PreviousTime;
    private DateTime LogStartTime;
    private DateTime LogPreviousTime;
    private string LogFilePath;
    private StreamWriter writer;

    // Start is called before the first frame update
    void Start()
    {
        StartTime = DateTime.Now;
        LogStartTime = StartTime;
        LogFilePath = Application.dataPath + "/Log/" + StartTime.ToString("yyyyMMddHHmmss") + LogFileNameSuffix + ".csv";

        // Ensure Log directory exists
        Directory.CreateDirectory(Path.GetDirectoryName(LogFilePath));

        // Create or overwrite the file
        writer = new StreamWriter(LogFilePath, false);
        // Write the header line
        writer.WriteLine("Time,ElapsedTime,x,y,z");

        PreviousTime = StartTime;
        LogPreviousTime = LogStartTime;
    }

    private void OnApplicationQuit()
    {
        // Close the StreamWriter when the application quits
        writer.Close();
    }

    // Update is called once per frame
    void Update()
    {
        var time = DateTime.Now;
        TimeSpan Logspan = time - LogPreviousTime;

        // Check if 600 seconds (10 minutes) have passed to create a new log file
        if (Logspan.TotalSeconds > 600)
        {
            writer.Close();

            LogStartTime = time;
            LogFilePath = Application.dataPath + "/Log/" + LogStartTime.ToString("yyyyMMddHHmmss") + LogFileNameSuffix + ".csv";

            // Create or overwrite the file
            writer = new StreamWriter(LogFilePath, false);
            // Write the header line
            writer.WriteLine("Time,ElapsedTime,x,y,z");

            LogPreviousTime = time;
        }

        TimeSpan span = time - PreviousTime;
        // Log data every 100 milliseconds
        if (span.TotalMilliseconds > 100)
        {
            var x = Target.transform.position.x;
            var y = Target.transform.position.y;
            var z = Target.transform.position.z;

            // Write data to the CSV file
            writer.WriteLine($"{time.ToString("HH:mm:ss.fff")},{(time - StartTime).TotalMilliseconds},{x},{y},{z}");
            writer.Flush();
            PreviousTime = time;
        }
    }
}
