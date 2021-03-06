﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace CowsaySharp.Library
{
    static public class GetCow
    {
        static private StringBuilder cow;

        static public string ReturnCow(string cowFile, IBubbleChars bubbles, CowFace face)
        {
            StreamReader sr = new StreamReader(cowFile);
            cow = new StringBuilder(sr.ReadToEnd().ToString());
            bool threeEyes = false;
            if (cow.ToString().Contains("($extra x 2)"))
                threeEyes = true;

            cow = removeExtraCowLines(cow);

            cow.Replace("\\\\", "\\");

            cow.Replace("$thoughts", bubbles.Bubble);
            if (threeEyes)
            {
                string eyesForReplacement = new String(face.Eyes[0], 3);
                cow.Replace("$eyes", eyesForReplacement);
                cow.Replace("${eyes}", eyesForReplacement);
            }
            else
            {
                string eyesForReplacement = face.Eyes;
                cow.Replace("$eyes", eyesForReplacement);
                cow.Replace("${eyes}", eyesForReplacement);
            }
            cow.Replace("$tongue", face.Tongue);

            if (cow.ToString().Substring(0, 1) == "\n")
                cow.Remove(0, 1);

            return cow.ToString().TrimEnd();
        }

        static private StringBuilder removeExtraCowLines(StringBuilder cow)
        {
            string cowString = cow.ToString();
            StringBuilder cowToReturn = new StringBuilder();
            List<string> cowList = new List<string>();

            while (cowString.Length > 0)
            {
                string sub = cowString.Substring(0, cowString.IndexOf("\n"));

                cowList.Add(sub);

                cow.Remove(0, cowString.IndexOf("\n") + 1);
                cowString = cow.ToString();
            }

            foreach (string line in cowList)
            {
                if (!(line.StartsWith("#") || line.StartsWith("$") || line.StartsWith("EOC")))
                    cowToReturn.Append(line + Environment.NewLine);
            }

            return cowToReturn;
        }
    }

   
}
