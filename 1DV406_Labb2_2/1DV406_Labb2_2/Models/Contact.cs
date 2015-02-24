using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _1DV406_Labb2_2.Models// Marco villegas
{

    public class Contact //Klass för kontaktobjekt och Validering i affärslogiklagret med hjälp av dataannotations för att validera inmatad data 
    {
        public int ContactID { get; set; }

        [Required(ErrorMessage = "En epostadress måste anges")]
        [StringLength(50)]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "E-postadressen är inte giltig, exmpel Marco.Villegas@live")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage="Ett förnamn måste anges")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required(ErrorMessage="Ett efternamn måste anges")]
        [StringLength(50)]
        public string LastName { get; set; }
    }
}