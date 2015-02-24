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

        
        public Contact GetContact(int contactID) // Hämtar specifik kontakt från databasen
        {
            return ContactDAL.GetContactByID(contactID);
        }

 
        public IEnumerable<Contact> GetContacts()// Hämtar alla kontakter som finns lagrade i databasen
        {
            return ContactDAL.GetContacts();
        }

        public IEnumerable<Contact> GetContactsPageWise(int maximumRows, int startRowIndex, out int totalRowCount)// Hämtar kontakter en sida i taget om 20 kontakter 
        {
            return ContactDAL.GetContactsPageWise(maximumRows, startRowIndex, out totalRowCount);
        }

      
        public void SaveContact(Contact contact)// Sparar till databas, ny kontakt eller med uppdaterad information
        {
            ICollection<ValidationResult> validationResults;
            if (!contact.Validate(out validationResults))
            {
                var ex = new ValidationException("Kontaktobjektet klarade inte datavalideringen.");
                ex.Data.Add("ValidationResults", validationResults);
                throw ex;
            }

            if (contact.ContactID == 0)
            {
                ContactDAL.InsertContact(contact);
            }
            else
            {
                ContactDAL.UpdateContact(contact);
            }           
        }

      
        public void DeleteContact(int contactID)// Raderar kontakt som finn i databasen
        {
            ContactDAL.DeleteContact(contactID);
        }
    }
}