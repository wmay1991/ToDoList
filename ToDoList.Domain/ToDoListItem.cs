using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ToDoList.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.Domain
{
    [Table("ToDoList")]
    public class ToDoListItem //: IToDoListItem
    {
       /* public string _description { get; set; }
        public bool _isCompleted { get; set; }

        // used to mock
       /* public ToDoListItem(string description, bool isCompleted)
        {
            _description = description;
            _isCompleted = isCompleted;
        }*/

        public ToDoListItem()
        {
        }

        // give an item a unique secure key
        public Guid id { get; set; }
        public bool isCompleted { get; set; }
        public string description { get; set; }

    }
}
