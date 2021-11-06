using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTaskNotify
{
    class TaskDo
    {
        public string Id { get; set; }
        public string TaskName { get; set; }
        public long BeginTime { get; set; }
        public long DeadLine { get; set; }
        public Status Status { get; set; }
        public LEVEL Level { get; set; }
    }

    enum Status
    {
        DONE, UN_DONE
    }
    enum LEVEL
    {
        NI_NU, I_NU, NI_U, I_U
    }
}
