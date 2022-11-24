using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;


namespace Test_6
{
    public class RedactFileExtensions
    {
        public static int MenuLavel = 0;
        //public ArrowMenu ReadLine = new ArrowMenu();

        public static string ext;
        public static string path = "";

        //static Dictionary<int, string> toDict(Struct person)
        //{
        //    Dictionary<int, string> text = new Dictionary<int, string>();

        //    text.Add(0, person.NameOfFile);
        //    text.Add(1, person.DateOfCreate);
        //    text.Add(2, person.FileExtantion);
        //    text.Add(2, Convert.ToString(person.Capacity));
        //    text.Add(2, Convert.ToString(person.LeftSpace));

        //    return text;
        //}

        //static Struct toClass(List<Struct>[] text)
        //{
        //    Struct str = new Struct();

        //    result.NameOfFile = text[0];
        //    result.DateOfCreate = text[1];
        //    result.FileExtantion = text[2];
        //    //result.Capacity = text[3].ToString;
        //    //result.LeftSpace = text[4];

        //    str.NameOfFile = drive.Name;
        //    str.Capacity = TotalSize;
        //    str.LeftSpace = TotalFreeSpace;
        //    ListDisc[i - 2] = new List<Struct>();
        //    ListDisc[i - 2].Add(str);



        //    return str;
        //}
        //public static List<Struct>[] ListDisc = new List<Struct>[3];
        public static void ShowDiscs()
        {
            MenuLavel = 0;

            Console.Clear();

            Console.SetCursorPosition(40, 0);
            Console.WriteLine("Этот компьютер");

            int i = 0;
            int j = 0;
            for (i = 0; i < 119; i++)
            {
                Console.SetCursorPosition(i, 1);
                Console.WriteLine("-");
            }



            DriveInfo[] drives = DriveInfo.GetDrives();
            List<Struct>[] ListDisc = new List<Struct>[DriveInfo.GetDrives().Count()];
            i = 3;
            j = 0;
            int ListCounter = 0;
            float TotalSize = 0;
            string Unit = "";
            string Unit1 = "";
            float TotalFreeSpace = 0;
            foreach (DriveInfo drive in drives)
            {
                if (drive.IsReady)
                {
                    if (drive.TotalSize > 1000000000)
                    {
                        TotalSize = drive.TotalSize / 1000000000;
                        Unit = "Гб";
                    }
                    else
                    {
                        TotalSize = drive.TotalSize / 1000000;
                        Unit = "Мб";
                    }
                    if (drive.TotalSize > 1000000000)
                    {
                        TotalFreeSpace = drive.TotalFreeSpace / 1000000000;
                        Unit1 = "Гб";
                    }
                    else
                    {
                        TotalFreeSpace = drive.TotalFreeSpace / 1000000;
                        Unit1 = "Мб";
                    }


                    //ListDisc = new List<Struct>[j + 1];


                    Struct str = new Struct();
                    str.NameOfFile = drive.Name;
                    str.Capacity = TotalSize;
                    str.LeftSpace = TotalFreeSpace;
                    str.IsFolder = true;
                    //str.DateOfCreate = "";
                    //str.FileExtantion = "";
                    //ListDisc = new List<Struct>[j + 1];
                    ListDisc[j] = new List<Struct>();
                    ListDisc[j].Add(str);



                    foreach (var item in ListDisc[i - 3])
                    {
                        Console.SetCursorPosition(2, i);
                        Console.WriteLine($"{item.NameOfFile}");
                        Console.SetCursorPosition(5, i);
                        Console.WriteLine($"{item.Capacity} {Unit}");
                        Console.SetCursorPosition(20, i);
                        Console.WriteLine($"Свободное пространство: {item.LeftSpace} {Unit1}");
                    }
                    ListCounter++;
                }

                i++;
                j++;

            }
            Console.SetCursorPosition(40, 0);
            Console.WriteLine("Этот компьютер");
            ArrowMenu.Menu(ListDisc, MenuLavel, false);
        }


        public static void ShowHelp()
        {
            Console.SetCursorPosition(90, 3);
            Console.WriteLine("| F1 - Создать папку");
            Console.SetCursorPosition(90, 4);
            Console.WriteLine("| F2 - Создать файл");
            Console.SetCursorPosition(90, 5);
            Console.WriteLine("| F3 - Удалить");
            Console.SetCursorPosition(90, 6);
            Console.WriteLine("| ---------------------------");
        }



    public static string fullpath = "";
        public static void ShowFiles(string path, int MenuLavel, Boolean JastDelited)
        {
            fullpath = path;

            Console.Clear();

            Console.SetCursorPosition(40, 0);
            Console.WriteLine(fullpath);

            int i = 0;
            int j = 0;
            for (i = 0; i < 119; i++)
            {
                Console.SetCursorPosition(i, 1);
                Console.WriteLine("-");
            }

            ShowHelp();


            i = 0;

            var dir = new DirectoryInfo(fullpath);

            List<Struct>[] ListFilesDirectories = new List<Struct>[dir.GetDirectories().Count() + dir.GetFiles().Count()];
            foreach (DirectoryInfo Directories in dir.GetDirectories())
            {
                Struct str = new Struct();
                str.NameOfFile = Directories.FullName;
                str.DateOfCreate = Convert.ToString(Directory.GetCreationTimeUtc(fullpath));
                str.IsFolder = true;

                ListFilesDirectories[i] = new List<Struct>();
                ListFilesDirectories[i].Add(str);

                foreach (var item in ListFilesDirectories[i])
                {

                    Console.SetCursorPosition(2, i + 3);
                    Console.WriteLine(Path.GetFileNameWithoutExtension(Directories.FullName));
                    Console.SetCursorPosition(45, i + 3);
                    Console.WriteLine($"{item.DateOfCreate}");
                    Console.SetCursorPosition(77, i + 3);
                    Console.WriteLine("Папка");

                }

                i++;
            }

            foreach (FileInfo file in dir.GetFiles())
            {
                Struct str = new Struct();
                str.NameOfFile = file.FullName;
                str.DateOfCreate = Convert.ToString(System.IO.File.GetCreationTime(fullpath));
                str.IsFolder = false;

                ListFilesDirectories[i] = new List<Struct>();
                ListFilesDirectories[i].Add(str);


                Console.SetCursorPosition(15, 2);
                Console.WriteLine("Наименование");
                Console.SetCursorPosition(45, 2);
                Console.WriteLine("Дата создания");
                Console.SetCursorPosition(75, 2);
                Console.WriteLine("Разрешение:");
                


                foreach (var item in ListFilesDirectories[i])
                {
                    Console.SetCursorPosition(2, i + 3);
                    Console.WriteLine(Path.GetFileNameWithoutExtension(file.FullName));
                    Console.SetCursorPosition(45, i + 3);
                    Console.WriteLine($"{item.DateOfCreate}");
                    Console.SetCursorPosition(77, i + 3);
                    Console.WriteLine(Path.GetExtension(file.FullName));
                }

                i++;
            }
            Console.SetCursorPosition(40, 0);
            Console.WriteLine(fullpath);

            ArrowMenu.Menu(ListFilesDirectories, MenuLavel, JastDelited);
        }

        static void Main(string[] args)
        {
            ShowDiscs();
        }
    }
}