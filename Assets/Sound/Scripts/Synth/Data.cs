/* --- Libraries --- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monet;
using Monet.IO;

namespace Monet.IO {

    public class Data {

        public static void SaveJSON(Data data, string filename, string directory, string format) {
            string json = JsonUtility.ToJson(data);
            string filepath = DataPath + directory + filename + format;
            using (StreamWriter o = new StreamWriter(filepath)) {
                o.Write(json);
            }
        }

        public static object OpenJSON(string filename, string directory, string format) {
            string json = "";
            string filepath = DataPath + directory + filename + format;
            using (StreamReader r = new StreamReader(filepath)) {
                json = r.ReadToEnd();
            }
            return JsonUtility.FromJson(json);
        }

        public static void SaveBinary(Data data, string filename, string directory, string format) {
            // Concatenate the path.
            string filepath = DataPath + directory + filename + format;

            // Format the data.
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(filepath, FileMode.Create);
            formatter.Serialize(fileStream, data);

            // Close the file.
            fileStream.Close();

        }

        public static object OpenBinary(string filename, string directory, string format) {
            // Concatenate the path.
            string filepath = DataPath + path + filename + filetype;

            if (File.Exists(fullPath)) {

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

    }
}