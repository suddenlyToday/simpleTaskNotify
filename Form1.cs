using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleTaskNotify
{
    public partial class Form1 : Form
    {
        private const string FILE_NAME = "task.json";
        private const int THIRTY = 30;
        private const long SLEEP_MINUTES_TICKS = THIRTY * 60 * 1000L * ONE_MILLSECOND_TICKS;
        private const int TIMER_INTERVAL = 300000;
        private const int ONE_MILLSECOND_TICKS = 10000;
        private addForm addForm = new addForm();
        private long doNotNotifyTime = DateTime.Now.Ticks;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.timer1.Interval = TIMER_INTERVAL;
            if (File.Exists(FILE_NAME))
            {
                string Content = File.ReadAllText(FILE_NAME).IfEmptyThenGet("[]");
                DataGridDataSource.TASK_LIST = JsonConvert.DeserializeObject<List<TaskDo>>(Content);
                DataGridDataSource.TASK_DTO_LIST = new BindingList<TaskDto>(DataGridDataSource.Convert2Dto(DataGridDataSource.TASK_LIST));
            }
            else
            {
                string contents = string.Empty;
                File.WriteAllText(FILE_NAME, contents);
                DataGridDataSource.TASK_LIST = new List<TaskDo>();
                DataGridDataSource.TASK_DTO_LIST = new BindingList<TaskDto>();

            }
            dataGridView1.DataSource = DataGridDataSource.TASK_DTO_LIST;
            DataGridDataSource.TASK_DTO_LIST.ListChanged += listChanged;
            dataGridView1.ClearSelection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            addForm.ShowDialog();
        }

        void listChanged(object sender, ListChangedEventArgs e)
        {
            File.WriteAllText(FILE_NAME, JsonConvert.SerializeObject(DataGridDataSource.TASK_LIST));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var selectRows = dataGridView1.SelectedRows;
            foreach (DataGridViewRow row in selectRows)
            {
                string id = (string)row.Cells[0].Value;
                dataGridView1.Rows.RemoveAt(row.Index);
                DataGridDataSource.TASK_LIST.RemoveAll(task => task.Id.Equals(id));
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            long ticks = DateTime.Now.Ticks;
            if (DataGridDataSource.TASK_LIST.Any(task => task.BeginTime > ticks || task.DeadLine < ticks)
                && ticks > doNotNotifyTime)
            {
                this.Show();
                this.Activate();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.doNotNotifyTime = DateTime.Now.Ticks + SLEEP_MINUTES_TICKS;
            this.Hide();
            this.notifyIcon1.BalloonTipText = $"{THIRTY}分钟内不再提醒";
            this.notifyIcon1.ShowBalloonTip(3000);
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            this.timer1.Stop();
        }

        private void Form1_Deactivate(object sender, EventArgs e)
        {
            this.timer1.Start();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.Activate();
        }
    }
}
