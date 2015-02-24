using _1DV406_Labb2_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _1DV406_Labb2_2// Marco Villegas
{
    public partial class Default : System.Web.UI.Page
    {
        private Service _service;

        private Service Service// Egenskap
        {
            get { return _service ?? (_service = new Service()); } //  om Service är null så Skapar nytt object och lägger den i _service 
        }

        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (Session["lyckades"] != null)// Om en session av typen lyckades finns så ska ett meddelande med dess innehåll visas
            {
                StatusLitteral.Text = Session["lyckades"].ToString();
                StatusMessage.Visible = true;
                Session["lyckades"] = null;
                //Session.Remove("lyckades");
            }
        }



        public IEnumerable<Contact> ContactListView_GetData(int maximumRows, int startRowIndex, out int totalRowCount)//Hämtar alla kontakter som finns lagrade i databasen
        {
            return Service.GetContactsPageWise(maximumRows, startRowIndex, out totalRowCount);
        }


        public void ContactListView_InsertItem(Contact contact)// Lägger till ny medlem i systemet med andra ord Sparar kontakt i databasen
        {
                if (ModelState.IsValid)
                {
                    try
                    {
                        // sparar kontakten i databasen och presenterar ett meddelande om att man lyckats sparar till databas
                        Service.SaveContact(contact);
                        Session["lyckades"] = "Kontakten Sparad!";
                        Response.Redirect(Request.UrlReferrer.ToString());
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError("", "Fel inträffade då kontakten skulle läggas till");
                    }
                }
        }


        public void ContactListView_UpdateItem(int contactID) //Uppdaterar en kontakt i databasen
        {
            try
            {
                var contact = Service.GetContact(contactID);
                if (contact == null)// Om if satsen körs så har man inte hittar kontakten man vill uppdatera, ett fel har då uppstått  
                {
                    
                    ModelState.AddModelError("", String.Format("kontakt {0} hittades inte", contactID));
                    return;
                }

                if (TryUpdateModel(contact))
                {
                    // Uppdaterar kontakten i databas och presenterar ett meddelande
                    Service.SaveContact(contact);
                    Session["lyckades"] = "Kontakten har uppdaterats!";
                    Response.Redirect(Request.UrlReferrer.ToString());
                }
            }
            catch (Exception)// Fel som uppstått hanteras här    
            {
                ModelState.AddModelError("", "fel inträffade då kontakten skulle uppdateras.");
            }
        }

      
        public void ContactListView_DeleteItem(int contactID)// Raderar kontakt i databasen
        {
            try
            {
                // Raderar kontakten och presenterar ett meddelande om att kontakt tagit bort 
                Service.DeleteContact(contactID);
                Session["lyckades"] = "Kontakten har raderats!";
                Response.Redirect(Request.UrlReferrer.ToString());
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "fel inträffade då kontakten skulle raderas.");
            }
        }
    }
}