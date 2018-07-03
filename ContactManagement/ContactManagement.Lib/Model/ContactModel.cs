
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactManagement.Lib.Model
{
    public enum ContactStatus
    {
        Active = 1,
        Inactive = 2
    }

    public class Contact : IModelBase
    {
        
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public ContactStatus Status { get; set; }// (Possible values: Active/Inactive)
    }
}
