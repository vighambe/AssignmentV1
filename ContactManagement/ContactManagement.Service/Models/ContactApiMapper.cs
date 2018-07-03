using ContactManagement.Lib.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactManagement.Service.Models
{
    public class ContactApiMapper
    {
        public static Contact MapToEntiry(ContactApiModel contactApiModel, bool createRequest = true)
        {
            if (contactApiModel == null)
                return (null);
            Contact contact = new Contact();
            if (createRequest)
                contact.Id = -1;
            else
                contact.Id = contactApiModel.Id;
            contact.FirstName = contactApiModel.FirstName;
            contact.LastName = contactApiModel.LastName;
            contact.Email = contactApiModel.Email;
            contact.PhoneNumber = contactApiModel.PhoneNumber;
            contact.Status = contactApiModel.Status;
            return (contact);
        }

        public static ContactApiModel MapToApiModel(Contact contact)
        {
            if (contact == null)
                return (null);
            ContactApiModel contactApiModel = new ContactApiModel();
            contactApiModel.Id = contact.Id;
            contactApiModel.FirstName = contact.FirstName;
            contactApiModel.LastName = contact.LastName;
            contactApiModel.Email = contact.Email;
            contactApiModel.PhoneNumber = contact.PhoneNumber;

            return (contactApiModel);
        }


    }
}
