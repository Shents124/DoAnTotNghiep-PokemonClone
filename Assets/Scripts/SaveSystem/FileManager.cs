
using System;
using System.IO;
using UnityEngine;

namespace SaveSystem
{
    public static class FileManager 
    {
        public static bool WriteToFile(string fileName, string fileContents)
        {
            var fullPatch = Path.Combine(Application.persistentDataPath, fileName);

            try
            {
                File.WriteAllText(fullPatch, fileContents);
                return true;
            }
            catch (Exception e)
            {
                Debug.Log($"Failed to write to {fullPatch} with exception {e}");
                return false;
            }
        }

        public static bool LoadFromFile(string fileName, out string result)
        {
            var fullPath = Path.Combine(Application.persistentDataPath, fileName);
            
            if(File.Exists(fullPath) == false)
                File.WriteAllText(fullPath,"");
            
            try
            {
                result = File.ReadAllText(fullPath);
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to read from {fullPath} with exception {e}");
                result = "";
                return false;
            }
        }
    }
}
