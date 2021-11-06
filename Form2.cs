using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleTaskNotify
{
    public partial class addForm : Form
    {
        public addForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string tName = textBox1.Text;
            DateTime bd = dateTimePicker1.Value;
            DateTime dd = dateTimePicker2.Value;
            LEVEL lv = getLevel();
            TaskDo ret = new TaskDo { Id = Guid.NewGuid().ToString(), Status = Status.UN_DONE, TaskName = tName, BeginTime = bd.Ticks, DeadLine = dd.Ticks, Level = lv };
            DataGridDataSource.TASK_LIST.Add(ret);
            foreach (TaskDto taskDto in DataGridDataSource.Convert2Dto(new List<TaskDo> { ret }))
            {
                DataGridDataSource.TASK_DTO_LIST.Add(taskDto);
            }
            this.Close();
        }

        private LEVEL getLevel()
        {
            if (radioButton1.Checked)
            {
                return LEVEL.I_U;
            }
            if (radioButton2.Checked)
            {
                return LEVEL.NI_U;
            }
            if (radioButton3.Checked)
            {
                return LEVEL.I_NU;
            }
            if (radioButton4.Checked)
            {
                return LEVEL.NI_NU;
            }
            return LEVEL.NI_NU;
        }

        private void addForm_Load(object sender, EventArgs e)
        {
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "MM月dd日HH点mm分";
            dateTimePicker2.ShowUpDown = true;
            dateTimePicker2.Value = DateTime.Now;

            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "MM月dd日HH点mm分";
            dateTimePicker1.ShowUpDown = true;
            dateTimePicker1.Value = DateTime.Now;
        }
    }
}
