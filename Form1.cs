using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PdfSharp.Pdf;
using PdfSharp;
using PdfSharp.Drawing;
using System.Diagnostics;
using System.Xml.Serialization;
using System.Data.SqlClient;
using hrmg_ackowledgements.Properties;




namespace hrmg_ackowledgements
{
    public partial class Form1 : Form
    {
        
        string inputPath;
        string saveDir;
        string savepath;
        string savePDFAs;
        string newSavePDFAs;
        string agency;

        string font;
        int font_size;
        int top_margin;

        private BackgroundWorker bw;

        List<Exceptions> exceptions_list = new List<Exceptions>();

        XFont fontForPDF;

        public class Exceptions
        {
            public string Clients { get; set; }
            public int NumChars { get; set; }
        }


        public Form1()
        {
            InitializeComponent();

            // Enable drag/drop controls
            this.AllowDrop = true;
            this.DragEnter += new DragEventHandler(Form1_DragEnter);
            this.DragDrop += new DragEventHandler(Form1_DragDrop);

            this.bw = new BackgroundWorker();
            this.bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            this.bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            this.bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            this.bw.WorkerReportsProgress = true;
            this.bw.WorkerSupportsCancellation = true;


        }

        public void getExceptions()
        {
            string sql;
            string cs = Settings.Default.exceptionsdbConnectionString;
            SqlConnection con = new SqlConnection(cs);
            Exceptions[] allRecords = null;
            if (agency == "CMRE")
            {
                sql = @"SELECT ClientCMRE,NumCharsCMREs from [TableCMRE]";
            }
            else
            {
                sql = @"SELECT Client,NumChars from [Table]";
            }
            using (var command = new SqlCommand(sql, con))
            {
                con.Open();
                using (var reader = command.ExecuteReader())
                {
                    
                    while (reader.Read())
                        exceptions_list.Add(new Exceptions { Clients = reader.GetString(0), NumChars = reader.GetInt32(1) });
                    allRecords = exceptions_list.ToArray();
                }
            }

            con.Close();
        }
        


        private void Form1_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
                e.Effect = DragDropEffects.All;
        }

        // Place file path of source in TextBox1
        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            inputPath = string.Join("", FileList);
            textBox1.Text = inputPath;

        }

        

        private void Form1_Load(object sender, EventArgs e)
        {
            if (cmreButton.Checked)
            {
                if (File.Exists("data.xml"))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(Information));
                    FileStream read = new FileStream("data.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
                    Information info = (Information)xs.Deserialize(read);
                    saveDir = info.SaveDirCMRE;
                    savePDFAs = info.SavePDFAsCMRE;
                    top_margin = info.TopMarginCMRE;
                    read.Close();
                    if (top_margin == 0)
                    {
                        top_margin = 20;
                    }

                }
                else
                {
                    saveDir = "";
                    savePDFAs = "[CLI]-CMRESPAckRD[MMddyy]";
                    top_margin = 20;
                }
            }
            else
            {
                if (File.Exists("data.xml"))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(Information));
                    FileStream read = new FileStream("data.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
                    Information info = (Information)xs.Deserialize(read);
                    saveDir = info.SaveDir;
                    savePDFAs = info.SavePDFAs;
                    top_margin = info.TopMargin;
                    read.Close();
                    if (top_margin == 0)
                    {
                        top_margin = 20;
                    }

                }
                else
                {
                    saveDir = "";
                    savePDFAs = "[CLI]-HRMGSPAckRD[MMddyy]";
                    top_margin = 20;
                }
            }
            font = "Consolas";
            font_size = 11;
            textBox2.Text = saveDir;
            progressBar1.Visible = false;
            cancelbutton.Hide();
        }

        // Browse save directory button click
        private void button1_Click(object sender, EventArgs e)
        {
            saveDir = "";
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                saveDir = folderBrowserDialog1.SelectedPath;
            }
            textBox2.Text = saveDir;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            // check if source file exists
            if (textBox1.Text == "")
            {
                label5.Text = "No Source File Selected";
            }
        
            // check if save directory exists
            else if(textBox2.Text ==  "")
            {
                label5.Text = "No Save Directory Selected";
            }
           

            else
            {
                saveDir = textBox2.Text;
                progressBar1.Visible = true;
                button2.Hide();
                cancelbutton.Show();
                if (!this.bw.IsBusy)
                {
                    this.bw.RunWorkerAsync();
                }
            }
        }

        private void menuquit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void menupreferences_Click(object sender, EventArgs e)
        {
            var pref_form = new Preferences();
            pref_form.Show();
            pref_form.FormClosed += new FormClosedEventHandler(pref_form_FormClosed);

        }

        void pref_form_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (cmreButton.Checked)
            {
                if (File.Exists("data.xml"))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(Information));
                    FileStream read = new FileStream("data.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
                    Information info = (Information)xs.Deserialize(read);
                    saveDir = info.SaveDirCMRE;
                    savePDFAs = info.SavePDFAsCMRE;
                    top_margin = info.TopMarginCMRE;
                    read.Close();
                }
                else
                {
                    saveDir = "";
                    savePDFAs = "[CLI]-CMRESPAckRD[MMddyy]";
                    top_margin = 20;
                }
            }
            else
            {
                if (File.Exists("data.xml"))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(Information));
                    FileStream read = new FileStream("data.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
                    Information info = (Information)xs.Deserialize(read);
                    saveDir = info.SaveDir;
                    savePDFAs = info.SavePDFAs;
                    top_margin = info.TopMargin;
                    read.Close();
                }
                else
                {
                    saveDir = "";
                    savePDFAs = "[CLI]-HRMGSPAckRD[MMddyy]";
                    top_margin = 20;
                }
            }
            textBox2.Text = saveDir;
        }


        private void menufont_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog1 = new FontDialog();
            fontDialog1.Font = new Font("Consolas", 11);
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                font = fontDialog1.Font.Name.ToString();
                font_size = (int)fontDialog1.Font.Size;
            }
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                Success.Text = "Task Cancelled";
            }
            else if (e.Error != null)
            {
                Success.Text = "Error";
            }
            else
            {
                this.Success.Text = e.Result.ToString();
                var suc_pop = new SuccessPop();
                suc_pop.Show();
                button2.Show();
            }

            cancelbutton.Hide();
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            this.Success.Text = e.ProgressPercentage.ToString() + "% complete";
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            
            if (cmreButton.Checked)
            {
                agency = "CMRE";
            }
            else 
            {
                agency = "HRMG";
            }
            // populate exceptions list from Form2 -> ExceptionsDB
            getExceptions();


            // file extension
            string ext = inputPath.Substring(inputPath.Length - 4);

                // Start PDF split here

                label5.Text = "";
            if (agency == "CMRE")
            {
                if (File.Exists("data.xml"))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(Information));
                    FileStream read = new FileStream("data.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
                    Information info = (Information)xs.Deserialize(read);
                    savePDFAs = info.SavePDFAsCMRE;
                    top_margin = info.TopMarginCMRE;
                    read.Close();
                }
                else
                {
                    savePDFAs = "[CLI]-" + agency + "SPAckRD[MMddyy]";
                    top_margin = 20;
                }
            }
            else
            {
                if (File.Exists("data.xml"))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(Information));
                    FileStream read = new FileStream("data.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
                    Information info = (Information)xs.Deserialize(read);
                    savePDFAs = info.SavePDFAs;
                    top_margin = info.TopMargin;
                    read.Close();
                }
                else
                {
                    savePDFAs = "[CLI]-" + agency + "SPAckRD[MMddyy]";
                    top_margin = 20;
                }
            }


                try
                {
                    // local variables
                    bool first_page = true;
                    bool first_pdf = true;
                    int yPoint = top_margin;
                    int page = 0;
                    int pdf_number = 0;
                    string clientcode = null;
                    string clientprefix = null;

               
                    // create list of PDFs
                    List<PdfDocument> pdf_list = new List<PdfDocument>();

                    //create first PDF
                    PdfDocument pdf = new PdfDocument();
                    pdf_list.Add(pdf);

                    // title of PDF
                    pdf_list[pdf_number].Info.Title = agency +" Acknowledgement " + DateTime.Now.ToString("MMddyy") + "(" + pdf_number +")";

                    
                    // create new list of pages for pdf
                    List<PdfPage> pdf_page_list = new List<PdfPage>();  
                    
                    // create new page for pdf -- add to list
                    PdfPage pdfpage = new PdfPage();
                    pdf_page_list.Add(pdfpage);
                    pdf_page_list[page] = pdf_list[pdf_number].AddPage();
                    XGraphics graph = XGraphics.FromPdfPage(pdf_page_list[page]);

                    

                    // font of PDF
                    fontForPDF = new XFont(font, font_size, XFontStyle.Regular);

                    Success.ForeColor = Color.Black;

                    Success.Text = "Reading File";
                    // import .lst file to List lines
                    List<string> lines = System.IO.File.ReadAllLines(@inputPath).ToList();

                    // read through all lines
                    for (int i = 0; i < lines.Count(); i++)
                    {
                        // report your progress    
                        bw.ReportProgress((i*100)/lines.Count());
                        
                        // sleep to assure progress bar works
                        System.Threading.Thread.Sleep(1);

                        if (bw.CancellationPending)
                        {
                            // Set the e.Cancel flag so that the WorkerCompleted event
                            // knows that the process was cancelled.
                            e.Cancel = true;
                            bw.ReportProgress(0);
                            return;
                        }

                    // check if line contains PDF header
                    if ((agency == "HRMG" && lines[i].Contains("HEALTHCARE REVENUE MGT. GROUP")) || (agency == "CMRE" && lines[i].Contains("CMRE")))
                        {
                            if (first_pdf == true)
                            {
                                first_pdf = false;
                                
                                // print line
                                graph.DrawString(lines[i], fontForPDF, XBrushes.Black, new XRect(40, yPoint, pdf_page_list[page].Width.Point, pdf_page_list[page].Height.Point), XStringFormats.TopLeft);
                                yPoint = yPoint + 11;
                            }
                            else
                            {
                                // num of Chars in client code that need to be the same to save the page in the same PDF
                                int numChars;
                                if (agency == "HRMG")
                                    numChars = 3;
                                else // CMRE
                                    numChars = 6;

                                // check to see if next PDF is an exception
                                for (int j = i; j < lines.Count(); j++)
                                {
                                if ((agency == "HRMG" && lines[j].Contains("T755")) || (agency == "CMRE" && lines[j].Contains("T7")))
                                    {
                                        string newClientprefix = lines[j].Substring(lines[j].IndexOf('T') + 6, 3);

                                        // if new page is part of the exceptions list, numChars = exception type
                                        for (int k = 0; k < exceptions_list.Count; k++)
                                        {
                                            if (exceptions_list[k].Clients == newClientprefix && newClientprefix == clientprefix)
                                            {
                                                numChars = exceptions_list[k].NumChars;
                                            }
                                                
                                        }

                                        if (newClientprefix == clientprefix && numChars == 6)
                                        {
                                            // if prefix is 6 exception then save previous and make new PDF
                                            newSavePDFAs = savePDFAs.Replace("[CLI]", clientcode);
                                            newSavePDFAs = newSavePDFAs.Replace("[MMddyy]", DateTime.Now.ToString("MMddyy"));
                                            savepath = saveDir + @"\" + newSavePDFAs + ".pdf";
                                            pdf_list[pdf_number].Save(savepath);
                                            pdf_number++;

                                            // Start new PDF
                                            pdf = new PdfDocument();
                                            pdf_list.Add(pdf);

                                            pdf_list[pdf_number].Info.Title = agency + " Acknowledgement" + DateTime.Now.ToString("MMddyy");
                                            yPoint = top_margin;
                                            page = 0;

                                            pdf_page_list = new List<PdfPage>();
                                            pdfpage = new PdfPage();
                                            pdf_page_list.Add(pdfpage);
                                            pdf_page_list[page] = pdf_list[pdf_number].AddPage();
                                            graph = XGraphics.FromPdfPage(pdf_page_list[page]);
                                        
                                            first_page = true;
                                            break;
                                        }
                                        else if (clientprefix == newClientprefix)
                                        {
                                            // 4 char exception check
                                            if (numChars == 4)
                                            {
                                                if (lines[j].Substring(lines[j].IndexOf('T') + 6, 4) + "00" == clientcode)
                                                {
                                                    page++;
                                                    first_page = true;
                                                    yPoint = top_margin;

                                                    pdfpage = new PdfPage();
                                                    pdf_page_list.Add(pdfpage);
                                                    pdf_page_list[page] = pdf_list[pdf_number].AddPage();
                                                    graph = XGraphics.FromPdfPage(pdf_page_list[page]);

                                                    break;
                                                } else
                                                {
                                                    newSavePDFAs = savePDFAs.Replace("[CLI]", clientcode);
                                                    newSavePDFAs = newSavePDFAs.Replace("[MMddyy]", DateTime.Now.ToString("MMddyy"));
                                                    savepath = saveDir + @"\" + newSavePDFAs + ".pdf";
                                                    pdf_list[pdf_number].Save(savepath);
                                                    pdf_number++;
                                                    
                                                    // Start new PDF
                                                    pdf = new PdfDocument();
                                                    pdf_list.Add(pdf);

                                                    pdf_list[pdf_number].Info.Title = agency + " Acknowledgement" + DateTime.Now.ToString("MMddyy");
                                                    yPoint = top_margin;
                                                    page = 0;

                                                    pdf_page_list = new List<PdfPage>();
                                                    pdfpage = new PdfPage();
                                                    pdf_page_list.Add(pdfpage);
                                                    pdf_page_list[page] = pdf_list[pdf_number].AddPage();
                                                    graph = XGraphics.FromPdfPage(pdf_page_list[page]);

                                                    first_page = true;
                                                    break;
                                                }
                                            } else if (numChars == 5)
                                            {
                                                if (lines[j].Substring(lines[j].IndexOf('T') + 6, 5) + "0" == clientcode)
                                                {
                                                    page++;
                                                    first_page = true;
                                                    yPoint = top_margin;

                                                    pdfpage = new PdfPage();
                                                    pdf_page_list.Add(pdfpage);
                                                    pdf_page_list[page] = pdf_list[pdf_number].AddPage();
                                                    graph = XGraphics.FromPdfPage(pdf_page_list[page]);

                                                    break;
                                                }
                                                else
                                                {
                                                    newSavePDFAs = savePDFAs.Replace("[CLI]", clientcode);
                                                    newSavePDFAs = newSavePDFAs.Replace("[MMddyy]", DateTime.Now.ToString("MMddyy"));
                                                    savepath = saveDir + @"\" + newSavePDFAs + ".pdf";
                                                    pdf_list[pdf_number].Save(savepath);
                                                    pdf_number++;

                                                    // Start new PDF
                                                    pdf = new PdfDocument();
                                                    pdf_list.Add(pdf);

                                                    pdf_list[pdf_number].Info.Title = agency + " Acknowledgement" + DateTime.Now.ToString("MMddyy");
                                                    yPoint = top_margin;
                                                    page = 0;

                                                    pdf_page_list = new List<PdfPage>();
                                                    pdfpage = new PdfPage();
                                                    pdf_page_list.Add(pdfpage);
                                                    pdf_page_list[page] = pdf_list[pdf_number].AddPage();
                                                    graph = XGraphics.FromPdfPage(pdf_page_list[page]);

                                                    first_page = true;
                                                    break;
                                                }
                                            }

                                            else {

                                                    page++;
                                                    first_page = true;
                                                    yPoint = top_margin;
 
                                                    pdfpage = new PdfPage();
                                                    pdf_page_list.Add(pdfpage);
                                                    pdf_page_list[page] = pdf_list[pdf_number].AddPage();
                                                    graph = XGraphics.FromPdfPage(pdf_page_list[page]);

                                                    break;
                                                }
                                            }
                                        else
                                        {
                                            // if prefix is different, save previous and make new PDF
                                            newSavePDFAs = savePDFAs.Replace("[CLI]", clientcode);
                                            newSavePDFAs = newSavePDFAs.Replace("[MMddyy]", DateTime.Now.ToString("MMddyy"));
                                            savepath = saveDir + @"\" + newSavePDFAs + ".pdf";
                                            pdf_list[pdf_number].Save(savepath);
                                            pdf_number++;

                                            // Start new PDF
                                            pdf = new PdfDocument();
                                            pdf_list.Add(pdf);

                                            pdf_list[pdf_number].Info.Title = agency + " Acknowledgement" + DateTime.Now.ToString("MMddyy");
                                            yPoint = top_margin;
                                            page = 0;





                                           
                                            pdf_page_list = new List<PdfPage>();
                                            pdfpage = new PdfPage();
                                            pdf_page_list.Add(pdfpage);
                                            pdf_page_list[page] = pdf_list[pdf_number].AddPage();
                                            graph = XGraphics.FromPdfPage(pdf_page_list[page]);





                                            first_page = true;
                                            break;
                                        }    
                                    } 
                                }

                                graph.DrawString(lines[i], fontForPDF, XBrushes.Black, new XRect(40, yPoint, pdf_page_list[page].Width.Point, pdf_page_list[page].Height.Point), XStringFormats.TopLeft);
                                yPoint = yPoint + 11;


                            }
                        }

                        else if ((agency == "HRMG" && lines[i].Contains("T755")) || (agency == "CMRE" && lines[i].Contains("T7")))
                        {
                            if (first_page == true)
                            {
                                first_page = false;
                                graph.DrawString(lines[i], fontForPDF, XBrushes.Black, new XRect(40, yPoint, pdf_page_list[page].Width.Point, pdf_page_list[page].Height.Point), XStringFormats.TopLeft);
                                yPoint = yPoint + 11;

                                clientprefix = lines[i].Substring(lines[i].IndexOf('T') + 6, 3);

                                // Normal Clients
                                if(agency == "HRMG")
                                    clientcode = clientprefix;
                                else //CMRE
                                    clientcode = lines[i].Substring(lines[i].IndexOf('T') + 6, 6);

                            // Handle Client Exceptions
                            for (int j = 0; j < exceptions_list.Count(); j++)
                                {
                                    if (clientprefix == exceptions_list[j].Clients)
                                    {
                                        if(exceptions_list[j].NumChars == 4)
                                        {
                                            clientcode = lines[i].Substring(lines[i].IndexOf('T') + 6, 4) + "00";
                                        }
                                        else if (exceptions_list[j].NumChars == 5)
                                        {
                                            clientcode = lines[i].Substring(lines[i].IndexOf('T') + 6, 5) + "0";
                                        }
                                        else if (exceptions_list[j].NumChars == 6)
                                        {
                                            clientcode = lines[i].Substring(lines[i].IndexOf('T') + 6, 6);
                                        }
                                        // CMRE exceptions
                                        else if (exceptions_list[j].NumChars == 3)
                                        {
                                            clientcode = clientprefix;
                                        }   

                                }
                                    
                                }


                          
                            }
                            else
                            {
                                page++;
                                yPoint = top_margin;


                                
                                pdfpage = new PdfPage();
                                pdf_page_list.Add(pdfpage);
                                pdf_page_list[page] = pdf_list[pdf_number].AddPage();
                                graph = XGraphics.FromPdfPage(pdf_page_list[page]);



                                graph.DrawString(lines[i], fontForPDF, XBrushes.Black, new XRect(40, yPoint, pdf_page_list[page].Width.Point, pdf_page_list[page].Height.Point), XStringFormats.TopLeft);
                                //graph.DrawString(lines[i], fontForPDF, XBrushes.Black, new XRect(40, yPoint, pdfpage[page].Width.Point, pdfpage[page].Height.Point), XStringFormats.TopLeft);
                                yPoint = yPoint + 11;
                            }
                        }
                        else
                        {
                            graph.DrawString(lines[i], fontForPDF, XBrushes.Black, new XRect(40, yPoint, pdf_page_list[page].Width.Point, pdf_page_list[page].Height.Point), XStringFormats.TopLeft);
                            yPoint = yPoint + 11;
                        }
                      }

                    newSavePDFAs = savePDFAs.Replace("[CLI]", clientcode);
                    newSavePDFAs = newSavePDFAs.Replace("[MMddyy]", DateTime.Now.ToString("MMddyy"));
                    savepath = saveDir + @"\" + newSavePDFAs + ".pdf";

                    pdf_list[pdf_number].Save(savepath);

                   

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            bw.ReportProgress(100);
            e.Result = "100% Complete";
            Success.ForeColor = Color.Green;
            
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void menuabout_Click(object sender, EventArgs e)
        {
            var about_pop = new About();
            about_pop.Show();
        }

        private void cancelbutton_Click(object sender, EventArgs e)
        {
            if (bw.IsBusy)
            {

                // Notify the worker thread that a cancel has been requested.

                // The cancel will not actually happen until the thread in the

                // DoWork checks the bw.CancellationPending flag. 

                bw.CancelAsync();
            }
            button2.Show();
        }

        private void cmreButton_CheckedChanged(object sender, EventArgs e)
        {
            if (cmreButton.Checked)
            {
                if (File.Exists("data.xml"))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(Information));
                    FileStream read = new FileStream("data.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
                    Information info = (Information)xs.Deserialize(read);
                    saveDir = info.SaveDirCMRE;
                    savePDFAs = info.SavePDFAsCMRE;
                    top_margin = info.TopMarginCMRE;
                    read.Close();
                    if (top_margin == 0)
                    {
                        top_margin = 20;
                    }

                }
                else
                {
                    saveDir = "";
                    savePDFAs = "[CLI]-CMRESPAckRD[MMddyy]";
                    top_margin = 20;
                }
            }
            else
            {
                if (File.Exists("data.xml"))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(Information));
                    FileStream read = new FileStream("data.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
                    Information info = (Information)xs.Deserialize(read);
                    saveDir = info.SaveDir;
                    savePDFAs = info.SavePDFAs;
                    top_margin = info.TopMargin;
                    read.Close();
                    if (top_margin == 0)
                    {
                        top_margin = 20;
                    }

                }
                else
                {
                    saveDir = "";
                    savePDFAs = "[CLI]-HRMGSPAckRD[MMddyy]";
                    top_margin = 20;
                }
            }
            textBox2.Text = saveDir;
        }
    }
}
