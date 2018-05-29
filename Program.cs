using S4;
using System;
using System.IO;

namespace test_version_s4
{
    class Program
    {
        static TS4App S4App;
        static void Main(string[] args)
        {
            int DocID = 629992;
            int VersionID = 0;
            string Path = @"D:\SEARCHWORK\!test\"+DateTime.Now.ToString("hh_mm_ss")+".doc";
            string BackupPath = @"D:\SEARCHWORK\!test\" + DateTime.Now.ToString("hh_mm_ss") + "_.doc";
            using (File.Open(Path,FileMode.OpenOrCreate))
            S4App = new TS4AppClass();
            S4App.Login();
            S4App.OpenDocument(DocID);                          CheckError(" в (OpenDocument)");
            VersionID = S4App.GetDocMaxVersionID(DocID);        CheckError(" в (GetDocMaxVersionID)");
            
            File.Move(Path, BackupPath);

            S4App.CreateDocVersion(
                        DocID
                        , VersionID
                        , (++VersionID) .ToString()
                        , ""
                        , Path
                        , 1
                        , (sbyte)VersionID);                    CheckError(" в (CreateDocVersion)");
            S4App.SaveChanges();                                CheckError(" в (SaveChanges)");
            S4App.SetDocType("DOC");                            CheckError(" в (SetDocType)");

            File.Delete(Path);
            File.Move(BackupPath, Path);
            S4App.CheckIn();                                    CheckError(" в (CheckIn)");
            
            Console.WriteLine("*************************end");
            Console.ReadKey();
        }

        private static void CheckError(string message)
        {
            if (S4App.ErrorCode == 1)
                Console.WriteLine("Ошибка " + message + ":" + S4App.ErrorMessage);
        }
    }
}
