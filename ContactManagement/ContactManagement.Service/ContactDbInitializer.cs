using ContactManagement.Lib.Context;
using ContactManagement.Lib.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace ContactManagement.Service
{
    public class ContactDbInitializer
    {
        private static ContactContext _context;
        
        public static void Initialize(IApplicationBuilder app)
        {
            //var serviceScope = app.ApplicationServices.CreateScope();
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                _context = (ContactContext)serviceScope.ServiceProvider.GetService(typeof(ContactContext));
                InitializeContacts();
            }
            
        }

        private static void InitializeContacts()
        {
            if (!_context.Contacts.Any())
            {
                for (int i = 0; i < 5; i++)
                {
                    Contact contact = new Contact { FirstName = "FirstName_" + i, LastName = "LastName_" + i, Email = "FirstName_" + i + "." + "LastName_" + i + "@test.com", PhoneNumber = "111111111" + i, Status = ContactStatus.Active };
                    _context.Contacts.Add(contact);
                }
                _context.SaveChanges();
            }
        }
        
    }
}
