using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskService
{
    public class FileHandler
    {
        public const string relativePath = @"H:\VictoryLink Tasks\Task 5.0\Files\";
        public static List<Request> GetANI(string filePath, char splitter)
        {
            using var reader = new StreamReader(filePath);
            List<Request> requests = new List<Request>();
            try
            {
                while (!reader.EndOfStream)
                {
                    Request request = new Request();
                    var line = reader.ReadLine();
                    var values = line.Split(splitter);
                    request.MobileNumber = int.Parse(values[0]);
                    request.RequestDate = DateTime.Parse(values[1]);
                    requests.Add(request);
                }
                return requests;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                return null;
            }
        }

        public bool AppendToFile(string fileName, string data)
        {
            try
            {
                string filePath = GetFileDirectory(fileName);
                File.AppendAllText(filePath, data + Environment.NewLine);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                return false;
            }
        }


        public string GetFileDirectory(string fileName)
        {
            try
            {
                return Path.GetFullPath(relativePath + fileName);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                return null;
            }
        }


        public bool MoveFiles(string directoryTo, string exception)
        {

            if (Directory.Exists(relativePath))
            {
                foreach (var file in new DirectoryInfo(relativePath).GetFiles())
                {
                    if (file.Name != exception)
                    {
                        string destination = $@"{relativePath + directoryTo}";
                        if (Directory.Exists(destination))
                        {
                            Directory.Delete(destination, true);
                            Directory.CreateDirectory(destination);
                            Directory.Move(relativePath + file.Name, destination + "\\" + file.Name);
                        }
                        else
                        {
                            Directory.Move(relativePath + file.Name, destination + "\\" + file.Name);
                        }
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
