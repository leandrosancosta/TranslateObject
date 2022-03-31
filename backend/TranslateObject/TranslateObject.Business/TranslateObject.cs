using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranslateObject.Core.Object;


namespace TranslateObject.Business
{
    public static class TranslateObject
    {
        private static bool IsAvaliable(string source, string destination)
        {
            if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(destination))
                return false;

            TranslateObjectsAccept objectAccept;
            //Validate if values are avaliable in translate code
            return (Enum.TryParse<TranslateObjectsAccept>(Convert.ToString(source.ToLower()), true, out objectAccept)) && (Enum.TryParse<TranslateObjectsAccept>(Convert.ToString(destination.ToLower()), true, out objectAccept));
        }

        private static string CreateObjectTypeScritpToCSharp(string source, string destination, bool hasHeranca, string content, string className)
        {
            if (!IsAvaliable(source, destination))
                return "Não foi possível converter";

            string[] contentLines = content.Split('\n');
            string contentConvert = string.Empty;

            foreach (string line in contentLines)
            {
                int HasAccessModifier = -1;
                string newLine = line;

                if (newLine.ToLower() == "public")
                {
                    HasAccessModifier = 0;
                    newLine = newLine.Replace("public", "");
                }
                else if (line.ToLower() == "protected")
                {
                    HasAccessModifier = 1;
                    newLine = newLine.Replace("protected", "");
                }
                else if (line.ToLower() == "private")
                {
                    HasAccessModifier = 2;
                    newLine = newLine.Replace("private", "");
                }

                //Get Variable Name

                string varName = newLine.Substring(0, newLine.IndexOf(':'));
                string type = newLine.Substring(varName.Length + 2);
                type = type.Substring(0, type.Length - 1);

                if (type == "number")
                {
                    type = "float";
                }


                //Construct line;
                switch (HasAccessModifier)
                {
                    case 0: contentConvert += "\t\tpublic "; break;
                    case 1: contentConvert += "\t\tprotected "; break;
                    case 2: contentConvert += "\t\tprivate "; break;
                    default: contentConvert += "\t\tpublic "; break;
                }

                varName = varName.Substring(0, 1).ToUpper() + varName.Substring(1, varName.Length - 1);

                contentConvert += $"{type} {varName} {{get; set;}}\n";




            }
            string ret = $"public class {className} {{\n{contentConvert}\n}}";


            return ret;
        }
    }
}
