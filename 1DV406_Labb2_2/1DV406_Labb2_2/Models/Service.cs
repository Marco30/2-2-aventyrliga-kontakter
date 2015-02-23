using _1DV406_Labb2_2.Models.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _1DV406_Labb2_2.Models
{
    public class Service
    {
        private ContactDAL _contactDAL;

        private ContactDAL ContactDAL
        {
            get { return _contactDAL ?? (_contactDAL = new ContactDAL()); }
        }

        // Hämtar alla kontakter.
        public IEnumerable<Contact> GetContacts()
        {
            return ContactDAL.GetContacts();
        }

        public IEnumerable<Contact> GetContactsPageWise(int maximumRows, int startRowIndex, out int totalRowCount)// Hämtar kontakter en sida i taget om 20 kontakter 
        {
            return ContactDAL.GetContactsPageWise(maximumRows, startRowIndex, out totalRowCount);
        }

  
        // Raderar en kontakt
        public void DeleteContact(int contactID)
        {
            ContactDAL.DeleteContact(contactID);
        }
    }
}