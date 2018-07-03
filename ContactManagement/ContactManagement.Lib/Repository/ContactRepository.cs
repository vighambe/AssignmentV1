using ContactManagement.Lib.AbstractRepository;
using ContactManagement.Lib.Context;
using ContactManagement.Lib.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactManagement.Lib.Repository
{
    
    public class ContactRepository : BaseRepository<Contact>, IContactRepository
    {
        public ContactRepository(ContactContext context)
            : base(context)
        { }
    }
}
