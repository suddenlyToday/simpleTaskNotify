using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTaskNotify
{
    class TaskDto
    {
        public string Id { get; set; }
        public string TaskName { get; set; }
        public string BeginTime { get; set; }
        public string DeadLine { get; set; }
        public int Level { get; set; }
    }
}
