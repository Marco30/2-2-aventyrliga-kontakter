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

        public void InsertContact(Contact contact)// lägger in kontakt i databasen
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    var cmd = new SqlCommand("Person.uspAddContact", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // parametrar som måste skickas med för att lägga till en ny kontakt i databasen
                    cmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 50).Value = contact.FirstName;
                    cmd.Parameters.Add("@LastName", SqlDbType.VarChar, 50).Value = contact.LastName;
                    cmd.Parameters.Add("@EmailAddress", SqlDbType.VarChar, 50).Value = contact.EmailAddress;

                    cmd.Parameters.Add("@ContactID", SqlDbType.Int, 4).Direction = ParameterDirection.Output; // sett referensen ID till kunden på databasen som nys laggt till   

                    conn.Open();// Öppnar anslutningen till databasen.

                    cmd.ExecuteNonQuery();// läger in parametrarna i en ny rad i tabellen Contact

                  
                    contact.ContactID = (int)cmd.Parameters["@ContactID"].Value;// Hämtar ID och tilldelar detta till använt Contactobjekt.
                }
                catch
                {
                    throw new ApplicationException("fel inträffade vid försökt att koma åt information i databasen");
                }
            }
        }

        public void UpdateContact(Contact contact)// updaterar kontakt i databas
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    var cmd = new SqlCommand("Person.uspUpdateContact", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Skapar här de parametrar som måste skickas med för att lägga till en ny kontakt i databasen.
                    cmd.Parameters.Add("@ContactID", SqlDbType.VarChar, 50).Value = contact.ContactID;
                    cmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 50).Value = contact.FirstName;
                    cmd.Parameters.Add("@LastName", SqlDbType.VarChar, 50).Value = contact.LastName;
                    cmd.Parameters.Add("@EmailAddress", SqlDbType.VarChar, 50).Value = contact.EmailAddress;


                    // Öppnar anslutningen till databasen.
                    conn.Open();


                    cmd.ExecuteNonQuery();//skjuter in, en ny rad i tabellen Contact.
                }
                catch
                {
                    throw new ApplicationException("fel inträffade vid försökt att koma åt information i databasen");
                }
            }
        }

        public void DeleteContact(int contactID)// tar bort kontatk från databas 
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

        public IEnumerable<Contact> GetContactsPageWise(int maximumRows, int startRowIndex, out int totalRowCount) // Hämtar kontakter en sida i taget om 20 kontakter 
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    var contacts = new List<Contact>(100);

                    var cmd = new SqlCommand("Person.uspGetContactsPageWise", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@PageSize", SqlDbType.Int, 4).Value = maximumRows;
                    cmd.Parameters.Add("@PageIndex", SqlDbType.Int, 4).Value = (startRowIndex / maximumRows) + 1;                 
                    cmd.Parameters.Add("@RecordCount", SqlDbType.Int, 4).Direction = ParameterDirection.Output;

                    conn.Open();// Öppnar anslutningen till databasen.

                    cmd.ExecuteNonQuery();

                    totalRowCount = (int)cmd.Parameters["@RecordCount"].Value;

                    using (var reader = cmd.ExecuteReader())// Skapar referens till data utläst från databasen.
                    {
                        // Tar här reda på vilket index min databas olika kolumner har.
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

        public Contact GetContactByID(int contactID)// Hämtar enskild kontakt från databasen
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    var cmd = new SqlCommand("Person.uspGetContact", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ContactID", SqlDbType.Int, 4).Value = contactID;

                    conn.Open();// Öppnar anslutningen till databasen.


                    using (var reader = cmd.ExecuteReader()) // Skapar referens till data utläst från databasen
                    {
                        // Tar här reda på vilket index min databas olika kolumner har
                        var contactIdIndex = reader.GetOrdinal("ContactID");
                        var firstNameIndex = reader.GetOrdinal("FirstName");
                        var lastNameIndex = reader.GetOrdinal("LastName");
                        var emailAddressIndex = reader.GetOrdinal("EmailAddress");

                      
                        if (reader.Read())// Eftersom jag bara vill läsa ut en post, körs en if-sats om något finns att läsas ur databasen
                        {
                            return new Contact
                            {
                                ContactID = reader.GetInt32(contactIdIndex),
                                FirstName = reader.GetString(firstNameIndex),
                                LastName = reader.GetString(lastNameIndex),
                                EmailAddress = reader.GetString(emailAddressIndex)
                            };
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
                catch
                {
                    throw new ApplicationException("fel inträffade vid försökt att koma åt information i databasen");
                }
            }
        }

       
    }
}