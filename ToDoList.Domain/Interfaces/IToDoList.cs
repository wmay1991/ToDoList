using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Domain.Interfaces
{
    interface IToDoListItem
    {
        string _description { get; set; }
        bool _isCompleted { get; set; }
    }
}
