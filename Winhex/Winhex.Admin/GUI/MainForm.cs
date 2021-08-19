using DevExpress.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using Winhex.Admin.GUI;
using Winhex.Admin.Models;

namespace Winhex.Admin
{
    public partial class MainForm : Form
    {
        private List<UserLog> _users;
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            SizeChanged += MainForm_SizeChanged;
            LoadUsersFromServer();
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            comboBox_ValueChanged(null, null);
        }

        private void LoadUsersFromServer()
        {
            comboBox.Items.Clear();
            string userListSerial = WebRequester.Get($"{AppConfig.LoadConfig().Url}download/", "");
            if (!string.IsNullOrEmpty(userListSerial))
            {
                _users = JsonConvert.DeserializeObject<List<UserLog>>(userListSerial);
                _users.ForEach(x => comboBox.Items.Add(x.CustomNote != "" ? x.CustomNote : x.CompName));
            }
        }

        private void comboBox_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                string username = comboBox.SelectedItem?.ToString();
                var curUser = _users.FirstOrDefault(x => x.CompName == username || x.CustomNote == username);

                if (curUser == null) return;
                string serialUser = WebRequester.Get($"{AppConfig.LoadConfig().Url}download/{curUser.Id}/{AppConfig.LoadConfig().Key}", "");//"http://www.ihih.somee.com/download/"
                var logs = JsonConvert.DeserializeObject<UserLog>(serialUser)?.Logs;

                gridControl.DataSource = logs;
                gridView1.Columns["Id"].Visible = false;

                gridView1.Columns[1].DisplayFormat.FormatType = FormatType.DateTime;
                gridView1.Columns[1].DisplayFormat.FormatString = "dd/MM/yyyy hh:mm:ss";
                gridView1.Columns[1].Caption = "Date/Time"; 
                gridView1.Columns[1].BestFit();

                gridView1.Columns[2].Caption = "App title"; 
                gridView1.Columns[2].BestFit();
                gridControl.RefreshDataSource();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadUsersFromServer();
        }

        private void установитьКличкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Enabled = false;
            SetNoteForm form = new SetNoteForm();

            form.FormClosed += (o, args) => Enabled = true;
            form.OnGetNote += s =>
            {
                try
                {
                    WebRequester.Post(new UserLog()
                        {
                            Id = _users.FirstOrDefault(x => x.CustomNote == comboBox.SelectedItem.ToString() ||
                                                            x.CompName == comboBox.SelectedItem.ToString()).Id,
                            CustomNote = s
                        }, AppConfig.LoadConfig().Url + "download");
                }
                catch
                {
                    MessageBox.Show("Выбери пользователя!");
                }
            };
            form.Show();
        }

        private void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            comboBox_ValueChanged(null, null);
        }
    }
}
