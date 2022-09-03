using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using WinSCP;

namespace webroam_uploader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Multiselect = true;
            if(fd.ShowDialog()!= DialogResult.Cancel)
            {
                foreach(var f in fd.FileNames)
                listBox1.Items.Add(f);
            }
        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Delete)
            {
                if(listBox1.SelectedIndices.Count > 0)
                foreach(int itm in listBox1.SelectedIndices)
                {
                        listBox1.Items.RemoveAt(itm);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button7_Click(null, null);
            if (String.IsNullOrWhiteSpace(textBox9.Text))
            {
                return;
            }
                if (textBox1.Text.Trim() != "" && textBox2.Text.Trim() != "" && textBox3.Text.Trim() != "" && textBox6.Text.Trim() != "" && textBox8.Text.Trim() != "" && textBox9.Text!="")
            {
                /*if (!Directory.Exists("output"))
                    Directory.CreateDirectory("output");
               */
                string directory = "\\" + textBox9.Text.Trim().Trim('/').Trim('\\') + "\\";//String.Join("/", (textBox6.Text.TrimEnd('/') + "/").Split('/').Skip(3));
                /*if (!Directory.Exists("output" + directory))
                {
                    Directory.CreateDirectory("output" + directory);
                }*/
                
                int dirs = Directory.EnumerateDirectories("output" + directory).Count() + 1;
                string outdir = "output" + directory + dirs;
                string updateFile = outdir + "\\" + textBox8.Text.Trim();

                
                if (!Directory.Exists(outdir))
                {
                    Directory.CreateDirectory(outdir);
                }
                Task.Run(() =>
                {
                    try
                    {
                        this.Invoke(new MethodInvoker(delegate () {
                            string wrav = "/wrav/";
                            string text1 = (textBox1.Text.TrimEnd('/') + "/");                            
                            string url = text1;
                            List<string> folders = directory.Trim('/').Split('/').Skip(1).ToList();
                            if (File.Exists(textBox4.Text.Trim()))
                            {
                                XmlDocument xdoc = new XmlDocument();
                                xdoc.LoadXml(File.ReadAllText(textBox4.Text.Trim()));

                                if (xdoc.SelectNodes("item/url").Count > 0)
                                {
                                    string stx = xdoc.SelectNodes("item/url").Item(0).InnerText.Trim().Substring(0, xdoc.SelectNodes("item/url").Item(0).InnerText.Trim().LastIndexOf("/"));
                                    folders.Add(stx.Substring(stx.LastIndexOf('/') + 1));
                                }
                            }

                            if (true)
                            {
                                if (File.Exists(textBox4.Text.Trim()))
                                {
                                    XmlDocument xdoc = new XmlDocument();
                                    xdoc.LoadXml(File.ReadAllText(textBox4.Text.Trim()));

                                    if (xdoc.SelectNodes("item/url").Count > 0)
                                    {
                                        string stx = xdoc.SelectNodes("item/url").Item(0).InnerText.Trim().Substring(0, xdoc.SelectNodes("item/url").Item(0).InnerText.Trim().LastIndexOf("/"));
                                        stx = stx.Substring(stx.IndexOf(wrav) + wrav.Length);
                                        url += stx;
                                        //  MessageBox.Show(text1+stx);
                                        WebRequest request = WebRequest.Create(text1 + stx);

                                        request.Method = WebRequestMethods.Ftp.MakeDirectory;

                                        request.Credentials = new NetworkCredential(textBox2.Text.Trim(), textBox3.Text.Trim());
                                        try
                                        {
                                            using (var resp = (FtpWebResponse)request.GetResponse())
                                            {
                                                Console.WriteLine(resp.StatusCode);
                                            }
                                        }
                                        catch (WebException we) { /*MessageBox.Show(text1+stx+"\n"+we.Message);*/ }
                                    }
                                }
                            }
                            {
                                //   MessageBox.Show(url);
                                WebRequest request = WebRequest.Create(url);

                                request.Method = WebRequestMethods.Ftp.MakeDirectory;

                                request.Credentials = new NetworkCredential(textBox2.Text.Trim(), textBox3.Text.Trim());
                                try
                                {
                                    using (var resp = (FtpWebResponse)request.GetResponse())
                                    {
                                        Console.WriteLine(resp.StatusCode);
                                    }
                                }
                                catch (WebException we) { /*MessageBox.Show(url+"\n"+we.Message);*/ }
                            }

                            /*
                                                try
                                                {
                                                    WebRequest request = WebRequest.Create($"{url}/{textBox8.Text.Trim()}");
                                                    request.Method = WebRequestMethods.Ftp.DeleteFile;
                                                    request.Credentials = new NetworkCredential(textBox2.Text.Trim(), textBox3.Text.Trim());
                                                    //  MessageBox.Show($"{url}{textBox8.Text.Trim()}");
                                                    FtpWebResponse response1 = (FtpWebResponse)request.GetResponse();
                                                }
                                                catch (WebException we) { }*/
                            // FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(textBox1.Text.Trim());
                            // MessageBox.Show(textBox1.Text.Trim().Split('/')[2]);
                            SessionOptions sessionOptions = new SessionOptions
                            {
                                Protocol = Protocol.Ftp
                                ,
                                FtpSecure = FtpSecure.Explicit
                            ,
                                HostName = textBox1.Text.Trim().Split('/')[2]
                            ,
                                UserName = textBox2.Text
                            ,
                                Password = textBox3.Text
                            ,
                                PortNumber = 21
                             ,
                                GiveUpSecurityAndAcceptAnyTlsHostCertificate = true
                             ,
                            };
                            // ftpWebRequest.Credentials = new System.Net.NetworkCredential(textBox2.Text.Trim(), textBox3.Text.Trim());
                            //   ftpWebRequest.KeepAlive = false;
                            //  ftpWebRequest.UsePassive = true;
                            // ftpWebRequest.EnableSsl = true;
                            //   ftpWebRequest = (FtpWebRequest)FtpWebRequest.Create($"{url}/{textBox8.Text.Trim()}");
                            //ftpWebRequest.Method = WebRequestMethods.Ftp.UploadFile;


                            if (listBox1.Items.Count > 0)
                            {
                                List<string> files = new List<string>();
                                foreach (string itm in listBox1.Items)
                                {
                                    files.Add(itm);
                                }
                                // MessageBox.Show(String.Join("/", url.Split('/').Skip(3)));
                                
                            
                               
                                File.WriteAllText(outdir + "\\update.txt", textBox7.Text);
                                files.Add(outdir + "\\update.txt");
                                File.WriteAllText(outdir + "\\" + textBox4.Text, textBox5.Text);


                                // MessageBox.Show(new FileInfo(updateFile).FullName);
                                BackUpSettings.getBackUpfromFiles(updateFile, files.ToArray());
                                using (Session session = new Session())
                                {
                                    // Connect
                                    session.Open(sessionOptions);
                                    // MessageBox.Show(session.HomePath);
                                    // Upload files
                                    TransferOptions transferOptions = new TransferOptions();
                                    transferOptions.TransferMode = TransferMode.Binary;
                                    transferOptions.OverwriteMode = OverwriteMode.Overwrite;
                                    TransferOperationResult transferResult;
                                    //XmlDocument xdoc = new XmlDocument();
                                    //xdoc.LoadXml(textBox5.Text);
                                    //url = xdoc.SelectSingleNode("/item")["url"].InnerText.Substring(0, xdoc.SelectSingleNode("/item")["url"].InnerText.LastIndexOf("/"));
                                    string[] stx = url.Split('/').Skip(3).ToArray();
                                    stx[stx.Length - 1] = dirs.ToString();
                                  //  MessageBox.Show(String.Join("/",stx)); 
                                    transferResult =
                                        session.PutFilesToDirectory(outdir, String.Join("/", stx), null, false, transferOptions);

                                    // Throw on any error
                                    transferResult.Check();

                                    // Print results
                                    foreach (TransferEventArgs transfer in transferResult.Transfers)
                                    {
                                        MessageBox.Show(String.Format("Upload of {0} : {1} succeeded", transfer.FileName, transfer.Destination));
                                    }
                                    File.Copy("profile.save", outdir + "\\profile.save");
                                    Form1_Load(null, null);
                                }

                                //  MessageBox.Show($"{url}/{textBox8.Text.Trim()}");
                                //Task.Run(() => client.UploadFile($"{url}/{textBox8.Text.Trim()}", WebRequestMethods.Ftp.UploadFile, updateFile)).ContinueWith((t) => { MessageBox.Show("Success!"); });
                            }
  }));
                        }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        var fs = Directory.EnumerateFiles(outdir);
                        foreach (var f in fs)
                            File.Delete(f);
                        Directory.Delete(outdir);
                    }
              
                });
            }
            else
            {
                MessageBox.Show("Fill required fields!");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "*.xml|*.xml";
            if (fd.ShowDialog() != DialogResult.Cancel)
            {
                textBox4.Text = fd.FileName;
                LoadXml(listBox2.SelectedIndex == -1 ? textBox4.Text.Trim() : listBox2.SelectedItem.ToString() + "\\" + textBox4.Text.Trim());
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FtpWebRequest requestDir = (FtpWebRequest)FtpWebRequest.Create(textBox1.Text.Trim());
            requestDir.Credentials = new NetworkCredential(textBox2.Text.Trim(), textBox3.Text.Trim());
            requestDir.Method = WebRequestMethods.Ftp.ListDirectory;
            //requestDir.EnableSsl = true;
          //  System.Net.ServicePointManager.ServerCertificateValidationCallback += (sender2, certificate, chain, sslPolicyErrors) => true;

            try
            {
                WebResponse response = requestDir.GetResponse();
                MessageBox.Show("Connection OK!");
            }
            catch(Exception em)
            {
                MessageBox.Show("Connection failed!\n"+em.Message);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrWhiteSpace(textBox9.Text))
            {
                MessageBox.Show("Fill the Folder (Number) Field!");
                return;
            }
            if (!Directory.Exists("output"))
                Directory.CreateDirectory("output");

            string directory = "\\" + textBox9.Text.Trim().Trim('/').Trim('\\') + "\\";//String.Join("/", (textBox6.Text.TrimEnd('/') + "/").Split('/').Skip(3));
            if (!Directory.Exists("output" + directory))
            {
                Directory.CreateDirectory("output" + directory);
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(textBox1.Text.Trim());
            sb.AppendLine(",");
            sb.AppendLine(textBox2.Text.Trim());
            sb.AppendLine(",");
            sb.AppendLine(textBox3.Text.Trim());
            sb.AppendLine(",");
            sb.AppendLine(textBox4.Text.Trim());
            sb.AppendLine(",");
            sb.AppendLine(textBox5.Text.Trim());
            sb.AppendLine(",");
            sb.AppendLine(textBox6.Text.Trim('/')+"/"+textBox9.Text.TrimStart('/'));
            sb.AppendLine(",");
            sb.AppendLine(textBox7.Text.Trim());
            sb.AppendLine(",");
            sb.AppendLine(textBox8.Text.Trim());
            sb.AppendLine(",");
   //         sb.AppendLine();
            foreach (string itm in listBox1.Items)
            {
                sb.AppendLine(itm.Trim());
            }
     //       sb.AppendLine();
            sb.AppendLine(",");
            foreach (string itm in listBox2.Items)
            {
                sb.AppendLine(itm.Trim());
            }
       //     sb.AppendLine();
            sb.AppendLine(",");
            
            //MessageBox.Show(sb.ToString());
           
            // File.WriteAllText(textBox4.Text.Trim(), textBox5.Text.Trim());
            if (sender != null)
            { 
                File.WriteAllText("profile.save", sb.ToString());
                SaveXml();
                MessageBox.Show("Saved!");
                
            }
            Form1_Load(null, null);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if(listBox2.Items.Count==1)
            {
                Form1_Load(null, null);
                return;
            }
            if (listBox2.SelectedIndex > -1 && File.Exists($"{listBox2.SelectedItem.ToString()}\\profile.save"))
            {
                LoadSaved($"{listBox2.SelectedItem.ToString()}\\profile.save");               
               // button7_Click(null, null);
            }
           
        }
        void LoadSaved(string file= "profile.save")
        {
            if (File.Exists(file))
            {
                string wrav = "/wrav/";
                this.Text += "...";
                textBox5.Text = "";
                string[] text = File.ReadAllText(file).Split(',');
                var lt = text.ToList();
                lt.ForEach((s) => { s = s.Replace("\r", "").Replace("\n", ""); });
                text = lt.ToArray();
                if (!String.IsNullOrWhiteSpace(text[0].Trim()))
                    textBox1.Text = text[0].Trim();
                if (!String.IsNullOrWhiteSpace(text[1].Trim()))
                    textBox2.Text = text[1].Trim();
                if (!String.IsNullOrWhiteSpace(text[2].Trim()))
                    textBox3.Text = text[2].Trim();
                if (!String.IsNullOrWhiteSpace(text[3].Trim()))
                    textBox4.Text = text[3].Trim();
                if (!String.IsNullOrWhiteSpace(text[4].Trim()))
                    textBox5.Text = text[4].Replace("\r", "").Replace("\n", "").Replace(">", ">\r\n").Replace("<", "\r\n<").Replace("\r\n\r\n", "\r\n").Trim();// text[4].Trim();
                if (!String.IsNullOrWhiteSpace(text[5].Trim()))
                    textBox6.Text = text[5].Trim().Substring(0, text[5].IndexOf(wrav)-2) + wrav;
                if (!String.IsNullOrWhiteSpace(text[6].Trim()))
                    textBox7.Text = text[6].Trim();
                if (!String.IsNullOrWhiteSpace(text[7].Trim()))
                    textBox8.Text = text[7].Trim();
                if (text[5].IndexOf(wrav) != -1)
                    textBox9.Text = text[5].Substring(text[5].IndexOf(wrav) + wrav.Length);//"/"+String.Join("/", (text[5].TrimEnd('/')).Split('/').Skip(4));

                //     label2.Text = $"URL ({textBox9.Text.Split('/')[1]}):";
                int i = 8;

                listBox1.Items.Clear();
                string[] tt1 = text[i].Trim().Split('\n');
                string t1 = tt1[0].Trim();
                int j = 1;
                do
                {
                    if (!t1.Any(c => Path.GetInvalidPathChars().Contains(c)) && !String.IsNullOrWhiteSpace(t1))
                    {
                        listBox1.Items.Add(t1);
                    }
                    if (j == tt1.Length)
                        break;
                    t1 = tt1[j].Trim();
                    j++;

                }
                while (t1 != ",");
                i++;

                listBox2.Items.Clear();
                listBox2.Items.Add("Latest (Current)");
                /*string[] tt2 = text[i].Trim().Split('\n');
                string t2 = tt2[0].Trim();
                j = 1;
                do
                {               
                    listBox2.Items.Add(t2);
                        if (j == tt2.Length)
                        break;
                    t2 = tt2[j].Trim();
                    j++;
                   
                }
                while (t2 != ",");*/
            }
            

            


            this.Text = this.Text.Replace("...", "");
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadSaved();
            
            LoadXml(listBox2.SelectedIndex == -1 ? textBox4.Text.Trim() : listBox2.SelectedItem.ToString() + "\\" + textBox4.Text.Trim());
            if (Directory.Exists("output\\" + textBox9.Text.Trim()))
            {
                var folders = Directory.EnumerateDirectories("output\\" + textBox9.Text.Trim(), "*", SearchOption.TopDirectoryOnly);
                if (folders.Count() > 0)
                {
                    foreach (var f in folders)
                    {
                        if (File.Exists($"{f}\\profile.save"))
                            listBox2.Items.Add(f);
                    }
                }
            }
            if (e != null)
            {
              // listBox2.Items.Add("Latest (Current)");
                listBox2.SelectedIndex = listBox2.Items.Count - 1;                
            }
        }
        void LoadXml(string file)
        {
            if (File.Exists(file))
            {
                XmlDocument xdoc = new XmlDocument();
                xdoc.LoadXml(File.ReadAllText(file));

                if (xdoc.SelectNodes("item/url").Count > 0)
                {
                    string stx = xdoc.SelectNodes("item/url").Item(0).InnerText.Trim();
                    if(stx?.IndexOf("/wrav/") > -1)
                    textBox6.Text = stx.Substring(0, stx.IndexOf("/wrav/")) + "/wrav/";
                    int dirs = 1;
                    if (Directory.Exists("output\\" + textBox9.Text.Trim()))
                    {
                        dirs = Directory.EnumerateDirectories("output\\" + textBox9.Text.Trim()).Count() + 1;

                        string[] stsp = stx.Split('/');
                        string s = (/*Int32.Parse(stsp[stsp.Length - 2]) + 1*/dirs + 1).ToString();
                        stsp[stsp.Length - 3] = textBox9.Text.Trim();
                        stsp[stsp.Length - 2] = s;
                        stsp[stsp.Length - 1] = textBox4.Text;

                        textBox7.Text = String.Join("/", stsp);
                   
                        string s2 = (/*Int32.Parse(stsp[stsp.Length - 2]) + 1*/dirs).ToString();
                        stsp[stsp.Length - 2] = s2;
                    }
                    string[] stsp2 = stx.Split('/');
                    if (stsp2.Length > 2)
                    {
                        stsp2[stsp2.Length - 2] = dirs.ToString();
                        stsp2[stsp2.Length - 3] = textBox9.Text.Trim();
                        string it = String.Join("/", stsp2);
                        xdoc.SelectSingleNode("/item")["url"].InnerText = it;
                        textBox5.Text = xdoc.InnerXml.Replace("\r", "").Replace("\n", "").Replace(">", ">\r\n").Replace("<", "\r\n<").Replace("\r\n\r\n", "\r\n").Trim();
                        textBox8.Text = stx.Substring(stx.LastIndexOf("/") + 1);
                    }
                }
            }
        }

        void SaveXml()
        {
            if (File.Exists(textBox4.Text.Trim()))
            {                
                XmlDocument xdoc = new XmlDocument();
                xdoc.LoadXml(File.ReadAllText(listBox2.SelectedIndex == -1 ? textBox4.Text.Trim() : listBox2.SelectedItem.ToString() + "\\" + textBox4.Text.Trim()));

                if (xdoc.SelectNodes("item/url").Count > 0)
                {
                    xdoc.SelectSingleNode("/item")["url"].InnerText = textBox7.Text;//textBox6.Text.Trim('/') + "/" + textBox9.Text.Trim('/') + "/" + textBox8.Text.Trim('/');
                    string stx = xdoc.SelectNodes("item/url").Item(0).InnerText.Trim();
                    textBox6.Text = stx.Substring(0, stx.LastIndexOf("/") + 1);

                    // string[] stsp = stx.Split('/');
                    //string s = (Int32.Parse(stsp[stsp.Length - 3]) + 1).ToString();
                    //stsp[stsp.Length - 3] = s;
                    
                    //textBox8.Text = stx.Substring(stx.LastIndexOf("/") + 1);
                //   MessageBox.Show(xdoc.InnerXml.ToString());
                   
                    //textBox5.Text = ;
                    
                        File.WriteAllText(textBox4.Text.Trim(), xdoc.InnerXml.ToString());
                }

            }
        }
        private void listBox2_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void listBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (listBox2.SelectedIndices.Count > 0)
                    foreach (int itm in listBox2.SelectedIndices)
                    {
                        listBox2.Items.RemoveAt(itm);
                    }
            }
        }

        private void listBox2_KeyDown_1(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Delete)
            {
                Directory.Delete(listBox2.SelectedItem.ToString(), true);
                Form1_Load(null, null);
            }
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            //LoadXml(listBox2.SelectedIndex==-1?textBox4.Text.Trim(): listBox2.SelectedItem.ToString() + "\\"+textBox4.Text.Trim());            
        }

        private void textBox9_SelectedIndexChanged(object sender, EventArgs e)
        {
            button7_Click(null, null);
        }
    }
}
