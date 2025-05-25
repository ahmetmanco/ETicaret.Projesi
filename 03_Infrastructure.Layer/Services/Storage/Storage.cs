using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _03_Infrastructure.Layer.Operations;

namespace _03_Infrastructure.Layer.Services.Storage
{
    public class Storage
    {
        protected delegate bool HasFile(string pathOrContainer, string fileName);
        protected async Task<string> FileRenameAsync(string fileName, string pathOrContainer, HasFile hasFile, bool first = true)
        {
            string extension = Path.GetExtension(fileName);
            string newFileName;

            if (first)
            {
                string oldName = Path.GetFileNameWithoutExtension(fileName);
                newFileName = $"{NameOperations.CharacterRegulator(oldName)}{extension}";
            }
            else
            {
                newFileName = fileName;
                int index1 = newFileName.IndexOf('.');

                if (index1 == -1)
                {
                    newFileName = $"{Path.GetFileNameWithoutExtension(newFileName)}-2{extension}";
                }
                else
                {
                    int lastIndex = 0;
                    while (true)
                    {
                        lastIndex = index1;
                        index1 = newFileName.IndexOf("-", index1 + 1);
                        if (index1 == -1)
                        {
                            index1 = lastIndex;
                            break;
                        }
                    }

                    int index2 = newFileName.IndexOf(".");
                    string fileNo = newFileName.Substring(index1 + 1, index2 - index1 - 1);

                    if (int.TryParse(fileNo, out int _fileNo))
                    {
                        _fileNo++;
                        newFileName = newFileName.Remove(index1 + 1, index2 - index1 - 1).Insert(index1 + 1, _fileNo.ToString());
                    }
                    else
                    {
                        newFileName = $"{Path.GetFileNameWithoutExtension(newFileName)}-2{extension}";
                    }
                }
            }

            if (hasFile(pathOrContainer, newFileName))
            {
                return await FileRenameAsync(newFileName, pathOrContainer, hasFile, false);
            }

            return newFileName;
        }


    }
}
