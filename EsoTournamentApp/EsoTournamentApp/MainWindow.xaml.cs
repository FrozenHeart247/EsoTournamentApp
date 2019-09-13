using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using System.Data.SQLite;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;



namespace EsoTournamentApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window

    {
        DataSet ds;
        private String dbFileName;
        private SQLiteConnection m_dbConn;
        private SQLiteCommand m_sqlCmd;
        private SQLiteDataReader myDataReader;

        public MainWindow()
        {
            InitializeComponent();
            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            dbFileName = "sample.sqlite";
            lbStatusText.Content = "Disconnected";
        }

        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {/*
            OpenFileDialog openFileDialog = new OpenFileDialog();
           // openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() == true)
            {
                //txtEditor.Text = File.ReadAllText(openFileDialog.FileName);
               // txtEditor.Text = "";
                String fileName = openFileDialog.FileName;
                SQLiteConnection sqlCon = new SQLiteConnection();
               
                    sqlCon.ConnectionString = "Data Source=\"" + fileName + "\""; ;
                    sqlCon.Open();
                    sqlCon.Close();
            }
               */
            OpenFileDialog opf = new OpenFileDialog();
            
            if (opf.ShowDialog()==true)
            {
                dbFileName = opf.FileName;
                m_dbConn = new SQLiteConnection("Data Source =\"" + dbFileName + "\"");
                m_dbConn.Open();

                if (m_dbConn.State != ConnectionState.Open)
                {
                    MessageBox.Show("Open connection with database");
                    return;
                }
                else
                    lbStatusText.Content = "Connected";

               
                /*  if (m_dbConn.State == System.Data.ConnectionState.Open)
                  {
                      lbStatusText.Content = "Connected";
                  }                               //проверка на подключение, хз может не работает, не тестил
                  else
                  {
                      lbStatusText.Content = "нихуя не работает";
                  }
                  */
            }
                                            
        }

        private void btnLoadFile_Click(object sender, RoutedEventArgs e)
        {
            /*
            DataTable dTable = new DataTable();s
            String sqlQuery;
            sqlQuery = "SELECT * FROM TournamentDB";
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
            adapter.Fill(dTable);
            dgvViewer.DataContext = sqlQuery;
            */// m_sqlCmd.CommandText=("SELECT * FROM TournamentDB");
           // String c1= "SELECT * FROM TournamentDB";
            //SQLiteCommand cmd1 = new SQLiteCommand(c1,m_dbConn);
            DataTable dTable = new DataTable();
            String c1 = "SELECT * FROM Players";
            using (SQLiteCommand cmd = new SQLiteCommand(c1, m_dbConn))
            {
                SQLiteCommand cmd1 = new SQLiteCommand(c1, m_dbConn);
                SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
                da.Fill(dTable);
                dgvViewer.ItemsSource = dTable.AsDataView();
            }
        }

        private void btnGetPic_Click(object sender, RoutedEventArgs e)
        {
            #region commentaryAndNotWorkingCode
            /*
            BitmapImage BImg = new BitmapImage();
            Image Img = new Image();
            m_sqlCmd = new SQLiteCommand("SELECT Build FROM Players WHERE Id = 1");
            SQLiteDataReader myDataReader = SQLiteCommand.ExecuteReader();
            */
            /////////////////////////////////////////////--------/////////////
            ///
            /// 
            /*
            SQLiteCommand getPiccommand = new SQLiteCommand("SELECT Build FROM Players WHERE Id=1", m_dbConn); //Получение фотографии пользователя по ID.
            SQLiteDataReader picReader1 = getPiccommand.ExecuteReader();
            if (picReader1.Read())
            {
                byte[] imgData1 = (byte[])picReader1[0];
                using (MemoryStream ms = new MemoryStream(imgData1))
                {
                    pictureBox. = ms;
                    picReader1.Close();
                }*/
            //////////////////////??????????????????????????????//
            ///
            /*
        DataTable dataTable = ds.Tables[0];

        foreach (DataRow row in dataTable.Rows)
        {

                //Store binary data read from the database in a byte array
                byte[] blob = (byte[])row[1];
                MemoryStream stream = new MemoryStream();
                stream.Write(blob, 0, blob.Length);
                stream.Position = 0;

                System.Drawing.Image img = System.Drawing.Image.FromStream(stream);
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();

                MemoryStream ms = new MemoryStream();
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                ms.Seek(0, SeekOrigin.Begin);
                bi.StreamSource = ms;
                bi.EndInit();
                imageBox.Source = bi;
                 }
        */
            #endregion
            string query = "SELECT Build from Players WHERE Id=1";
            SQLiteCommand cmd2 = new SQLiteCommand(query, m_dbConn);
            SQLiteDataReader dataReader2 = cmd2.ExecuteReader();
            while (dataReader2.Read())
            {
                Byte[] bindata = (Byte[])dataReader2["Build"];
                MemoryStream strm = new MemoryStream();
                strm.Write(bindata, 0, bindata.Length);
                strm.Position = 0;
                System.Drawing.Image img = System.Drawing.Image.FromStream(strm);
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                MemoryStream ms = new MemoryStream();
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                ms.Seek(0, SeekOrigin.Begin);
                bi.StreamSource = ms;
                bi.EndInit();
                imageBox.Source = bi;
            }

        }

    }
}
