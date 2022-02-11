using ContactsAPI._1.Interface.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsAPI._1.Interface.ViewModels
{
    public class ContactViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public SexEnum Sex { get; set; }

        public bool Active { get; set; }
    }
}
