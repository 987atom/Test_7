using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_6
{
    public static class ArrowMenu
    {

        public static int maxznach;
        public static string path = "";
        public static string ParentPath = "";

        public static List<Struct>[] Menu(List<Struct>[] text, int MenuLavel, Boolean JastDelited)
        {
            int uppos = 3;
            Console.SetCursorPosition(0, uppos);
            Console.WriteLine("->");

            uppos = 3;
            maxznach = text.Length+2;
            int i;

            while (true)
            {
                
                ConsoleKeyInfo key = Console.ReadKey();

                switch (key.Key)
                {

                    case ConsoleKey.UpArrow:
                        if (uppos <= 3)
                        {
                            uppos = maxznach;

                            cleerArrow();

                            Console.SetCursorPosition(0, uppos);
                            Console.WriteLine("->");
                        }
                        else
                        {
                            uppos = uppos - 1;

                            cleerArrow();

                            Console.SetCursorPosition(0, uppos);
                            Console.WriteLine("->");
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (uppos == maxznach)
                        {
                            uppos = 3;

                            cleerArrow();

                            Console.SetCursorPosition(0, uppos);
                            Console.WriteLine("->");
                        }
                        else
                        {
                            uppos = uppos + 1;
                            cleerArrow();

                            Console.SetCursorPosition(0, uppos);
                            Console.WriteLine("->");
                        }
                        break;
                    case ConsoleKey.Enter:
                        if (uppos >= 3)
                        {
                            MenuLavel++;

                            
                            //int Disc = uppos - 2;
                            foreach (var item in text[uppos - 3])
                            {
                                if (MenuLavel >= 0)
                                {
                                    if (item.IsFolder == true || MenuLavel==0)
                                    {
                                        path = item.NameOfFile;
                                        RedactFileExtensions.ShowFiles(path, MenuLavel, false);
                                    }
                                    else if (item.IsFolder == false)
                                    {
                                        path = item.NameOfFile;

                                        Process.Start(new ProcessStartInfo { FileName = path, UseShellExecute = true});
                                    } 
                                }
                            }
                        }
                        break;
                    case ConsoleKey.Escape:
                        if (uppos >= 3)
                        {
                            MenuLavel--;

                            //int Disc = uppos - 2;
                            try
                            {
                                if (text.Count() == 0)
                                {
                                    ParentPath = System.IO.Directory.GetParent(path).FullName;
                                    if (JastDelited == true)
                                    {
                                        ParentPath = System.IO.Directory.GetParent(ParentPath).FullName;
                                        JastDelited = false;
                                    }

                                    RedactFileExtensions.ShowFiles(ParentPath, MenuLavel, false);
                                }
                                else
                                {
                                    foreach (var item in text[uppos - 3])
                                    {
                                        switch (MenuLavel)
                                        {
                                            case > 0:
                                                {
                                                    path = item.NameOfFile;

                                                    ParentPath = System.IO.Directory.GetParent(path).FullName;
                                                    ParentPath = System.IO.Directory.GetParent(ParentPath).FullName;
                                                    RedactFileExtensions.ShowFiles(ParentPath, MenuLavel, false);
                                                    break;
                                                }
                                            case 0:
                                                {
                                                    path = item.NameOfFile;

                                                    RedactFileExtensions.ShowDiscs();
                                                    break;
                                                }
                                                break;
                                            case < 0:
                                                {
                                                    Console.Clear();
                                                    Environment.Exit(0);
                                                }
                                                break;
                                        }
                                    }
                                }
                            }

                            catch (Exception e)
                            {

                            }



                        }
                        break;

                        case ConsoleKey.F1:
                            if (MenuLavel == 0)
                            {
                                break;
                            }
                            else
                            {
                                if (text.Count() == 0)
                                {
                                    ParentPath = System.IO.Directory.GetParent(path).FullName;
                                    KreateAndDelite.CreateDirectory(MenuLavel, path);
                                }
                                else
                                {
                                    foreach (var item in text[uppos - 3])
                                    {
                                        path = item.NameOfFile;

                                        ParentPath = System.IO.Directory.GetParent(path).FullName;
                                        //ParentPath = System.IO.Directory.GetParent(ParentPath).FullName;

                                        KreateAndDelite.CreateDirectory(/*text,*/ MenuLavel, ParentPath);
                                    }
                                }
                            }
                            break;
                        case ConsoleKey.F2:
                        if (MenuLavel == 0)
                        {
                            break;
                        }
                        else
                        {
                            if (text.Count() == 0)
                            {
                                ParentPath = System.IO.Directory.GetParent(path).FullName;
                                KreateAndDelite.CreateFile(MenuLavel, path);
                            }
                            else
                            {
                                foreach (var item in text[uppos - 3])
                                {
                                    path = item.NameOfFile;

                                    ParentPath = System.IO.Directory.GetParent(path).FullName;

                                    KreateAndDelite.CreateFile(MenuLavel, ParentPath);
                                }
                            }
                        }
                        break;

                        case ConsoleKey.F3:
                        if (MenuLavel == 0)
                        {
                            break;
                        }
                        else
                        {
                            foreach (var item in text[uppos - 3])
                            {
                                path = item.NameOfFile;

                                ParentPath = System.IO.Directory.GetParent(path).FullName;

                                KreateAndDelite.DeleteFolderOrFile(text, MenuLavel, ParentPath, uppos);
                            }
                        }
                        break;
                } 
            }
            return text;
        }

        static void cleerArrow()
        {
            for (int o = 3; o <= maxznach; o++)
            {
                Console.SetCursorPosition(0, o);
                Console.WriteLine("  ");
            }
        }
    }
}
