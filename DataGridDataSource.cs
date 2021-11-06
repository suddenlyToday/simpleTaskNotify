using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTaskNotify
{
    class DataGridDataSource
    {
        public static BindingList<TaskDto> TASK_DTO_LIST = new BindingList<TaskDto>();

        public static List<TaskDo> TASK_LIST = new List<TaskDo>();

        public static List<TaskDto> Convert2Dto(List<TaskDo> Tasks)
        {
            if (Tasks == null)
            {
                return new List<TaskDto>();
            }
            return Tasks.FindAll(task => Status.UN_DONE == task.Status).ConvertAll(task => new TaskDto
            {
                Id = task.Id,
                TaskName = task.TaskName,
                BeginTime = new DateTime(task.BeginTime).ToString("MM-dd HH点mm"),
                DeadLine = new DateTime(task.DeadLine).ToString("MM-dd HH点mm"),
                Level = GetLevel(task.Level)
            }).OrderBy(task => task.Level).ToList();
        }

        static int GetLevel(LEVEL Level)
        {
            switch (Level)
            {
                case LEVEL.NI_NU: return 4;
                case LEVEL.I_NU: return 3;
                case LEVEL.NI_U: return 2;
                case LEVEL.I_U: return 1;
                default: return -1;
            }
        }
    }
}
