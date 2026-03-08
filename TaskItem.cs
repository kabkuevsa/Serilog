using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serilog
{

    public class TaskItem
    {
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }

        public TaskItem(string title)
        {
            Title = title;
            CreatedDate = DateTime.Now;
        }

        public override string ToString()
        {
            return Title;
        }
    }
}
