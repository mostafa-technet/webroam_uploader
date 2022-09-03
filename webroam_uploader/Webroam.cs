using SevenZip;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace webroam_uploader
{
    class Webroam
    {
    }
    public class BackUpSettings
    {
        private static FileStream fileStream = null;
        //public static string[] destinationFile = { Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "wrTemp\\Qs.dat") };
        static string password = @"mywrpw1799";

        public static void getBackUpfromFiles(string destination, string[] filename)
        {
            //MessageBox.Show(filename.Split('\\').Last());

            //MessageBox.Show(destinationFile[0]+Environment.NewLine+filename);
            SevenZipBase.SetLibraryPath(
        Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
        "7z64.dll"));
           
            SevenZipCompressor compressor = new SevenZipCompressor();
            if (File.Exists(destination))
                File.Delete(destination);//compressor.CompressionMode = CompressionMode.Append;
            if(true)
            {

                if (!Directory.Exists((destination)))
                    Directory.CreateDirectory(Path.GetDirectoryName(destination));
                //File.Create(destinationFile[0]);
                compressor.CompressionMode = CompressionMode.Create;
            }
            /* if (!File.Exists(destinationFile[0]))
                 File.Create(destinationFile[0]);*/
            compressor.Compressing += Compressor_Compressing;
            compressor.FileCompressionStarted += Compressor_FileCompressionStarted;
            compressor.CompressionFinished += Compressor_CompressionFinished;
            compressor.PreserveDirectoryRoot = false;
            compressor.DirectoryStructure = false;
            /*       if (fileStream != null)
                   {
                       try
                       {
                           fileStream.Unlock(0, fileStream.Length);
                       }
                       catch
                       { }
                   }
                   if (fileStream != null)
                   {
                       fileStream.Close();
                       fileStream = null;
                   }*/
            string[] sourceFiles = filename;//Directory.GetFiles(@"C:\Temp\YourFiles\");


            compressor.CompressionMethod = CompressionMethod.Deflate;
            compressor.CompressionLevel = CompressionLevel.High;
            compressor.ZipEncryptionMethod = ZipEncryptionMethod.Aes256;
            compressor.ArchiveFormat = OutArchiveFormat.Zip;
          //  if (!File.Exists(destination))
                compressor.CompressionMode = CompressionMode.Create;
            //else
              //  compressor.CompressionMode = CompressionMode.Append;
            compressor.EventSynchronization = EventSynchronizationStrategy.AlwaysAsynchronous;
            compressor.FastCompression = false;
            compressor.EncryptHeaders = true;
            compressor.ScanOnlyWritable = true;
            compressor.CompressFilesEncrypted(destination, password, sourceFiles);



            /*   if (fileStream != null)
                   fileStream.Lock(0, fileStream.Length);
           */
        }

        public static void UnBackUpFiles(string destination, string[] filenames)
        {
            SevenZipBase.SetLibraryPath(Path.Combine(
        Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) ?? System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
        "7z64.dll"));
            SevenZip.SevenZipExtractor extractor = new SevenZipExtractor(destination);
            extractor.ExtractArchive(Path.GetDirectoryName(Application.ExecutablePath));
            /*foreach (var file in filenames)
            {
                FileStream fs = new FileStream(file, FileMode.Create);
                extractor.ExtractFile(file, fs);
                fs.Flush();
                fs.Close();
            }*/
        }

        private static void Compressor_FileCompressionStarted(object sender, FileNameEventArgs e)
        {
            /*  if (fileStream != null)
                  fileStream.Unlock(0, fileStream.Length);*/
        }

        private static void Compressor_CompressionFinished(object sender, EventArgs e)
        {
            /*  if (fileStream == null)
              {
                  fileStream = new FileStream(destinationFile[0], FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
              }
              FileSecurity fsecurity = new FileSecurity(destinationFile[0], AccessControlSections.Owner);*/
            //MessageBox.Show("");
            //fsecurity.SetOwner()

        }

        private static void Compressor_Compressing(object sender, SevenZip.ProgressEventArgs e)
        {
            //throw new NotImplementedException();
        }
    }
    public class BackUp
    {
        private static FileStream fileStream = null;
        public static string[] destinationFile = { Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "wrTemp\\Qs.dat") };
        static string password = @"mywrpw1799";
        public static void getBackUpfromFile(string filename)
        {
            //MessageBox.Show(filename.Split('\\').Last());

            //MessageBox.Show(destinationFile[0]+Environment.NewLine+filename);
            SevenZipBase.SetLibraryPath(
        Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
        "7z64.dll"));

            SevenZipCompressor compressor = new SevenZipCompressor();
            compressor.ArchiveFormat = OutArchiveFormat.SevenZip;
            if (File.Exists(destinationFile[0]))
                compressor.CompressionMode = CompressionMode.Append;
            else
            {

                if (!Directory.Exists(Path.GetDirectoryName(destinationFile[0])))
                    Directory.CreateDirectory(Path.GetDirectoryName(destinationFile[0]));
                //File.Create(destinationFile[0]);
                compressor.CompressionMode = CompressionMode.Create;
            }
            /* if (!File.Exists(destinationFile[0]))
                 File.Create(destinationFile[0]);*/
            compressor.Compressing += Compressor_Compressing;
            compressor.FileCompressionStarted += Compressor_FileCompressionStarted;
            compressor.CompressionFinished += Compressor_CompressionFinished;

            /*       if (fileStream != null)
                   {
                       try
                       {
                           fileStream.Unlock(0, fileStream.Length);
                       }
                       catch
                       { }
                   }
                   if (fileStream != null)
                   {
                       fileStream.Close();
                       fileStream = null;
                   }*/
            string[] sourceFiles = new string[] { filename };//Directory.GetFiles(@"C:\Temp\YourFiles\");

            if (String.IsNullOrWhiteSpace(password))
            {
                compressor.CompressFiles(destinationFile[0], sourceFiles);
            }
            else
            {
                //optional
                compressor.CompressionMethod = CompressionMethod.Deflate;
                compressor.CompressionLevel = CompressionLevel.High;
                compressor.ZipEncryptionMethod = ZipEncryptionMethod.Aes256;
                compressor.ArchiveFormat = OutArchiveFormat.Zip;
                if (!File.Exists(destinationFile[0]))
                    compressor.CompressionMode = CompressionMode.Create;
                else
                    compressor.CompressionMode = CompressionMode.Append;
                compressor.EventSynchronization = EventSynchronizationStrategy.AlwaysAsynchronous;
                compressor.FastCompression = false;
                compressor.EncryptHeaders = true;
                compressor.ScanOnlyWritable = true;
                compressor.CompressFilesEncrypted(destinationFile[0], password, sourceFiles);

            }
            /*   if (fileStream != null)
                   fileStream.Lock(0, fileStream.Length);
           */
        }
        public static void RemoveFile(string filename)
        {
            string cmd = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\7z\\7z.exe";
            string args = $"-pmywrpw1799 d {Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\wrTemp\\Qs.dat \"{filename}\" -r";
            System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo(cmd + " " + args);//"netsh", "interface set interface name=\"" + name + "\" admin=DISABLE");
            psi.CreateNoWindow = true;
            psi.Verb = "runas";
            psi.WindowStyle = ProcessWindowStyle.Hidden;
            psi.FileName = cmd;
            psi.Arguments = args;
            Process.Start(psi).WaitForExit();
            // Clipboard.SetText(cmd+" "+args);           
        }


        public static void UnBackUpFile(string filename)
        {
            SevenZipBase.SetLibraryPath(Path.Combine(
        Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) ?? System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
        "7z64.dll"));
            SevenZip.SevenZipExtractor extractor = new SevenZipExtractor(destinationFile[0], password);
            if (File.Exists(destinationFile[0]))
            {
                FileStream fs = new FileStream(filename, FileMode.Create);
                extractor.ExtractFile(filename.Split('\\').Last(), fs);
                fs.Flush();
                fs.Close();
            }
        }

        private static void Compressor_FileCompressionStarted(object sender, FileNameEventArgs e)
        {
            /*  if (fileStream != null)
                  fileStream.Unlock(0, fileStream.Length);*/
        }

        private static void Compressor_CompressionFinished(object sender, EventArgs e)
        {
            /*  if (fileStream == null)
              {
                  fileStream = new FileStream(destinationFile[0], FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
              }
              FileSecurity fsecurity = new FileSecurity(destinationFile[0], AccessControlSections.Owner);*/
            //MessageBox.Show("");
            //fsecurity.SetOwner()

        }

        private static void Compressor_Compressing(object sender, SevenZip.ProgressEventArgs e)
        {
            //throw new NotImplementedException();
        }
    }


}
