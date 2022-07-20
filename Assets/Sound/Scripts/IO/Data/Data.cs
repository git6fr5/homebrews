// Libraries.
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Monet;
using Monet.IO;

namespace Monet.IO {

    [System.Serializable]
    public class Data {

        public static string Path = "Assets/Resources/Data/";

        #region JSON

        public static void SaveJSON(Data data, string filename, string directory, string format) {
            string json = JsonUtility.ToJson(data);
            string filepath = Path + directory + filename + format;
            using (StreamWriter o = new StreamWriter(filepath)) {
                o.Write(json);
            }
        }

        public static string OpenJSON(string filename, string directory, string format) {
            string json = "";
            string filepath = Path + directory + filename + format;
            using (StreamReader r = new StreamReader(filepath)) {
                json = r.ReadToEnd();
            }
            return json;
        }

        #endregion

        #region Binary

        public static void SaveBinary(Data data, string filename, string directory, string format) {
            // Concatenate the path.
            string filepath = Path + directory + filename + format;

            // Format the data.
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(filepath, FileMode.Create);
            formatter.Serialize(fileStream, data);

            // Close the file.
            fileStream.Close();

        }

        public static object OpenBinary(string filename, string directory, string format) {
            // Concatenate the path.
            string filepath = Path + directory + filename + format;

            if (File.Exists(filepath)) {

                // Read the data.
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream fileStream = new FileStream(filepath, FileMode.Open);
                object data = formatter.Deserialize(fileStream);

                // Close the file.
                fileStream.Close();

                return data;
            }

            return null;
        }

        #endregion

    }
}