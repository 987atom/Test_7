using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_6;
 
namespace Test_6
{
    public static class KreateAndDelite
    {
        public static Boolean JastDelited = false;

        public static void ShowHintDirectory()
        {
            Console.SetCursorPosition(90, 7);
            Console.WriteLine("Введите имя папки,");
            Console.SetCursorPosition(90, 8);
            Console.WriteLine("которая будет создана...");
        }
        public static void ShowHintFile()
        {
            Console.SetCursorPosition(90, 7);
            Console.WriteLine("Введите имя файла,");
            Console.SetCursorPosition(90, 8);
            Console.WriteLine("который будет создан...");
        }
        public static  void CreateDirectory(int MenuLavel, string ParentPath)
        {

            ShowHintDirectory();

            Console.SetCursorPosition(90, 9);
            string name = Console.ReadLine();
            string PathAndName = ParentPath + "\\" + name;

            System.IO.Directory.CreateDirectory(PathAndName);



            RedactFileExtensions.ShowFiles(ParentPath, MenuLavel, false);
        }

        public static void CreateFile(int MenuLavel, string ParentPath)
        {

            ShowHintFile();

            Console.SetCursorPosition(90, 9);
            string name = Console.ReadLine();
            string PathAndName = ParentPath + "\\" + name;

            using (System.IO.File.Create(PathAndName));



            RedactFileExtensions.ShowFiles(ParentPath, MenuLavel, false);
        }

        public static List<Struct>[] DeleteFolderOrFile(List<Struct>[] text, int MenuLavel, string ParentPath, int uppos)
        {

            string path = "";

            foreach (var item in text[uppos - 3])
            {
                if (MenuLavel >= 0)
                {
                    if (item.IsFolder == true)
                    {
                        JastDelited = true;
                        path = item.NameOfFile;
                        Directory.Delete(path);
                        RedactFileExtensions.ShowFiles(ParentPath, MenuLavel, JastDelited);
                    }
                    else if (item.IsFolder == false)
                    {
                        JastDelited = true;
                        //ParentPath = System.IO.Directory.GetParent(path).FullName;
                        path = item.NameOfFile;
                        File.Delete(path);
                        RedactFileExtensions.ShowFiles(ParentPath, MenuLavel, JastDelited);
                    }
                }
            }

            return text;
        }
    }
}
