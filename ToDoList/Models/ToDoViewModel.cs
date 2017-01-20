using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ToDoList.Domain;

namespace ToDoList.Models
{
    public class ToDoListViewModel
    {
        public ToDoListViewModel(Domain.ToDoListItem mdl)
        {
            this.id = mdl.id;
            this.description = mdl.description;
            this.isCompleted = mdl.isCompleted;
        }
        public ToDoListViewModel()
        {

        }

        public ToDoListViewModel(ToDoListItem mdl, ToDoListViewModel vm)
        {
            mdl.id = vm.id;
            mdl.description = vm.description;
            mdl.isCompleted = vm.isCompleted;
        }

        public Guid id { get; set; }

        [DisplayName("Completed")]
        public bool isCompleted { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [DisplayName("Description")]
        public string description { get; set; }

    }

}