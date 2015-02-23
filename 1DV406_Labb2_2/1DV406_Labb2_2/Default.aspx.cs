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
                Session.Remove("lyckades");
            }
        }



        public IEnumerable<Contact> ContactListView_GetData(int maximumRows, int startRowIndex, out int totalRowCount)//Hämtar alla kontakter som finns lagrade i databasen
        {
            return Service.GetContactsPageWise(maximumRows, startRowIndex, out totalRowCount);
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