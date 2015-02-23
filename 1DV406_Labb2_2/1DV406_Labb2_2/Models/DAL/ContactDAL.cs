using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace _1DV406_Labb2_2.Models.DAL
{
    public class ContactDAL : DALBase// Dataåtkomstklassen
    {




        public void DeleteContact(int contactID)
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    var cmd = new SqlCommand("Person.uspRemoveContact", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ContactID", SqlDbType.Int, 4).Value = contactID;


                    conn.Open();// Öppnar anslutningen till databasen.


                    cmd.ExecuteNonQuery();//skjuter in, en ny rad i tabellen Contact.
                }
                catch
                {
                    throw new ApplicationException("fel inträffade vid försökt att koma åt information i databasen");
                }
            }
        }


        public IEnumerable<Contact> GetContacts()// Hämtar alla kontakter i databasen, Samlingsobjekt för att loopa ut referenser till kunder i databasen
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    var contacts = new List<Contact>(100);

                    var cmd = new SqlCommand("Person.uspGetContacts", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();


                    using (var reader = cmd.ExecuteReader())// Skapar referens till data man läst från databasen.
                    {
                        // här tar man reda på vilket index databasen olika kolumner har.
                        var contactIdIndex = reader.GetOrdinal("ContactID");
                        var firstNameIndex = reader.GetOrdinal("FirstName");
                        var lastNameIndex = reader.GetOrdinal("LastName");
                        var emailAddressIndex = reader.GetOrdinal("EmailAddress");

                        while (reader.Read())
                        {
                            contacts.Add(new Contact
                                {
                                    ContactID = reader.GetInt32(contactIdIndex),
                                    FirstName = reader.GetString(firstNameIndex),
                                    LastName = reader.GetString(lastNameIndex),
                                    EmailAddress = reader.GetString(emailAddressIndex)
                                });
                        }
                    }

                    contacts.TrimExcess();
                    return contacts;
                }
                catch
                {
                    throw new ApplicationException("fel inträffade vid försökt att koma åt information i databasen");
                }
            }
        }


    }
}