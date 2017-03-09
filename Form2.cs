using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;
using System.Data.SqlClient;
using System.Configuration;
using hrmg_ackowledgements.Properties;


namespace hrmg_ackowledgements
{
    public partial class Preferences : Form
    {

        public string saveDirDefault;
        public string savePDFAs;
        public int top_margin = 0;

        public string saveDirDefaultCMRE;
        public string savePDFAsCMRE;
        public int top_marginCMRE = 0;

        DataTable dt = new DataTable();
        DataTable dtCMRE = new DataTable();
        string select = "SELECT * FROM [Table]";
        string selectCMRE = "SELECT * FROM [TableCMRE]";


        public Preferences()
        {
            InitializeComponent();

        }

        // HRMG browse button
        private void button1_Click(object sender, EventArgs e)
        {

            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                saveDirDefault = folderBrowserDialog1.SelectedPath;
            }
            textBox1.Text = saveDirDefault;

        }

        // CMRE browse button
        private void button4_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog2 = new FolderBrowserDialog();
            if (folderBrowserDialog2.ShowDialog() == DialogResult.OK)
            {
                saveDirDefaultCMRE = folderBrowserDialog2.SelectedPath;
            }
            textBox4.Text = saveDirDefaultCMRE;
        }

        // Apply button (HRMG)
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Information info = new Information();
                saveDirDefault = textBox1.Text;
                savePDFAs = textBox2.Text;
                Int32.TryParse(textBox3.Text, out top_margin);
                info.SaveDir = saveDirDefault;
                info.SavePDFAs = savePDFAs;
                info.TopMargin = top_margin;

                saveDirDefaultCMRE = textBox4.Text;
                savePDFAsCMRE = textBox6.Text;
                Int32.TryParse(textBox5.Text, out top_marginCMRE);
                info.SaveDirCMRE = saveDirDefaultCMRE;
                info.SavePDFAsCMRE = savePDFAsCMRE;
                info.TopMarginCMRE = top_marginCMRE;

                SaveXML.SaveData(info, "data.xml");
                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // apply button (CMRE)
        private void applyButtonCMRE_Click(object sender, EventArgs e)
        {
            try
            {
                Information info1 = new Information();
                saveDirDefaultCMRE = textBox4.Text;
                savePDFAsCMRE = textBox6.Text;
                Int32.TryParse(textBox5.Text, out top_marginCMRE);
                info1.SaveDirCMRE = saveDirDefaultCMRE;
                info1.SavePDFAsCMRE = savePDFAsCMRE;
                info1.TopMarginCMRE = top_marginCMRE;

                saveDirDefault = textBox1.Text;
                savePDFAs = textBox2.Text;
                Int32.TryParse(textBox3.Text, out top_margin);
                info1.SaveDir = saveDirDefault;
                info1.SavePDFAs = savePDFAs;
                info1.TopMargin = top_margin;


                SaveXML.SaveData(info1, "data.xml");
                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        // Preferences Page Startup
        private void Preferences_Load(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists("data.xml"))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(Information));
                    FileStream read = new FileStream("data.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
                    Information info = (Information)xs.Deserialize(read);
                    saveDirDefault = info.SaveDir;
                    savePDFAs = info.SavePDFAs;
                    top_margin = info.TopMargin;
                    saveDirDefaultCMRE = info.SaveDirCMRE;
                    savePDFAsCMRE = info.SavePDFAsCMRE;
                    top_marginCMRE = info.TopMarginCMRE;
                    read.Close();
                    if (top_margin == 0)
                    {
                        top_margin = 20;
                    }
                    if (top_marginCMRE == 0)
                    {
                        top_marginCMRE = 20;
                    }
                }
                else
                {
                    saveDirDefault = "";
                    savePDFAs = "[CLI]-HRMGSPAckRD[MMddyy]";
                    top_margin = 20;
                    saveDirDefaultCMRE = "";
                    savePDFAsCMRE = "[CLI]-CMRESPAckRD[MMddyy]";
                    top_marginCMRE = 20;
                }
                textBox1.Text = saveDirDefault;
                textBox2.Text = savePDFAs;
                textBox3.Text = top_margin.ToString();
                textBox4.Text = saveDirDefaultCMRE;
                textBox6.Text = savePDFAsCMRE;
                textBox5.Text = top_marginCMRE.ToString();


                string cs = Settings.Default.exceptionsdbConnectionString;
                // Add Exception
                SqlConnection con = new SqlConnection(cs);
                SqlCommand view = new SqlCommand();
                view.Connection = con;
                view.CommandType = CommandType.Text;
                view.CommandText = select;

                SqlCommand viewCMRE = new SqlCommand();
                viewCMRE.Connection = con;
                viewCMRE.CommandType = CommandType.Text;
                viewCMRE.CommandText = selectCMRE;

                SqlDataAdapter da = new SqlDataAdapter(view);
                SqlDataAdapter daCMRE = new SqlDataAdapter(viewCMRE);

                da.Fill(dt);
                dataGridView1.DataSource = dt;

                daCMRE.Fill(dtCMRE);
                dataGridView2.DataSource = dtCMRE;

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Add Exception (HRMG)
        private void button3_Click(object sender, EventArgs e)
        { 
            try {
                string cs = Settings.Default.exceptionsdbConnectionString;
                
                // Add Exception
                SqlConnection con = new SqlConnection(cs);

                SqlCommand cmd = new SqlCommand("[dbo].[addToDb]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Client", SqlDbType.VarChar).Value = client.Text.ToUpper();
                cmd.Parameters.Add("@NumChars", SqlDbType.Int).Value = numChars.Text;

                SqlCommand view = new SqlCommand();
                view.Connection = con;
                view.CommandType = CommandType.Text;
                view.CommandText = select;

                con.Open();
                cmd.ExecuteNonQuery();

                SqlDataAdapter da = new SqlDataAdapter(view);

                dt.Clear();

                da.Fill(dt);
            
                dataGridView1.DataSource = dt;

                dataGridView1.Refresh();

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Add Exception (CMRE)
        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                string cs = Settings.Default.exceptionsdbConnectionString;

                // Add Exception
                SqlConnection con = new SqlConnection(cs);

                SqlCommand cmd = new SqlCommand("[dbo].[addToDbCMRE]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ClientCMRE", SqlDbType.VarChar).Value = clientCMRE.Text.ToUpper();
                cmd.Parameters.Add("@NumCharsCMREs", SqlDbType.Int).Value = numCharsCMRE.Text;

                SqlCommand viewCMRE = new SqlCommand();
                viewCMRE.Connection = con;
                viewCMRE.CommandType = CommandType.Text;
                viewCMRE.CommandText = selectCMRE;

                con.Open();
                cmd.ExecuteNonQuery();

                SqlDataAdapter daCMRE = new SqlDataAdapter(viewCMRE);

                dtCMRE.Clear();

                daCMRE.Fill(dtCMRE);

                dataGridView2.DataSource = dtCMRE;

                dataGridView2.Refresh();

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Delete Exception (HRMG)
        private void DeleteExc_Click(object sender, EventArgs e)
        {
            try
            {
                string cs = Settings.Default.exceptionsdbConnectionString;
                SqlConnection con = new SqlConnection(cs);

                SqlCommand delete = new SqlCommand();
                delete.CommandType = CommandType.Text;
                delete.Connection = con;

                SqlCommand view = new SqlCommand();
                view.Connection = con;
                view.CommandType = CommandType.Text;
                view.CommandText = select;
            
                con.Open();

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    DataGridViewRow delrow = dataGridView1.Rows[i];
                    string primFieldValue = dataGridView1[0, i].Value.ToString();
                    if (delrow.Selected == true)
                    {
                        try
                        {
                            delete.CommandText = "DELETE FROM [Table] WHERE Client='" + primFieldValue + "'";
                        
                            delete.ExecuteNonQuery();
                            break;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }

                }
                SqlDataAdapter da = new SqlDataAdapter(view);
           

                dt.Clear();

                da.Fill(dt);

                dataGridView1.DataSource = dt;

                dataGridView1.Refresh();

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        // Delete Exception (CMRE)
        private void DeleteExcCMRE_Click(object sender, EventArgs e)
        {
            try
            {
                string cs = Settings.Default.exceptionsdbConnectionString;
                SqlConnection con = new SqlConnection(cs);

                SqlCommand deleteCMRE = new SqlCommand();
                deleteCMRE.CommandType = CommandType.Text;
                deleteCMRE.Connection = con;

                SqlCommand viewCMRE = new SqlCommand();
                viewCMRE.Connection = con;
                viewCMRE.CommandType = CommandType.Text;
                viewCMRE.CommandText = selectCMRE;

                con.Open();

                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {
                    DataGridViewRow delrowCMRE = dataGridView2.Rows[i];
                    string primFieldValueCMRE = dataGridView2[0, i].Value.ToString();
                    if (delrowCMRE.Selected == true)
                    {
                        try
                        {
                            deleteCMRE.CommandText = "DELETE FROM [TableCMRE] WHERE ClientCMRE='" + primFieldValueCMRE + "'";

                            deleteCMRE.ExecuteNonQuery();
                            break;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }

                }
                SqlDataAdapter daCMRE = new SqlDataAdapter(viewCMRE);


                dtCMRE.Clear();

                daCMRE.Fill(dtCMRE);

                dataGridView2.DataSource = dtCMRE;

                dataGridView2.Refresh();

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
