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




namespace EsoTournamentApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window

    {
        private String dbFileName;
        private SQLiteConnection m_dbConn;
        private SQLiteCommand m_sqlCmd;

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
    }
}
