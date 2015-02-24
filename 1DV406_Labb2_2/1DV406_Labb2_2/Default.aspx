<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="_1DV406_Labb2_2.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Marco - Labb 2.2</title>
    <link rel="stylesheet" href="Style.css" media="screen"/>
</head>
<body>
    <form id="form1" runat="server">

        <div id="inramning"> 
            
                <div id="TitleLabel">
               
            <h1>Marcos Kontakter</h1>
            </div>

            <!-- Visar att upplandning lyckats -->
            <div id="Close">

                <div id="StatusMessage" class="green" runat="server" visible="false">
                        <h2><asp:Literal ID="StatusLitteral" runat="server"></asp:Literal></h2>
                        <a href="#" id="CloseMessage">Stäng meddelande</a> 
                </div>     
                       
            </div>        

                    <!-- Visar alla samlade fel meddelanden -->
                <div id="val">
                    <asp:ValidationSummary runat="server" HeaderText="Följande fel inträffade vid din begäran" CssClass="validation-summary-error" />
                    </div>    


            <div class="grey">
              
                    <!-- ListView Används för att lada hem och presenterar information man fåt från data bas -->
                    <asp:ListView ID="ContactListView" runat="server" ItemType="_1DV406_Labb2_2.Models.Contact" SelectMethod="ContactListView_GetData" DataKeyNames="ContactID" 
                        InsertMethod="ContactListView_InsertItem" UpdateMethod="ContactListView_UpdateItem" DeleteMethod="ContactListView_DeleteItem" InsertItemPosition="FirstItem">
                         
                        <LayoutTemplate>
                            <!--Strukturerar up och presenterar den när laddade informationen-->  
                            <table class="normalTable">
                                <tr class="normalTableHeader">
                                    <th>
                                        Förnamn
                                    </th>
                                    <th>
                                        Efternamn
                                    </th>
                                    <th>
                                        Epost
                                    </th>
                                    <th>
                                        Hantering
                                    </th>
                                </tr>

                        <!-- skapar Platshållare i tabellen -->
                        <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                            </table>
                            <div id="sitePaging">
                                <!-- Meny ruta med fram back, till sista till första  -->
                                <asp:DataPager ID="DataPager" runat="server" PageSize="20" QueryStringField="Page">
                                    <Fields>
                                        <asp:NextPreviousPagerField ShowFirstPageButton="True" FirstPageText=" Första " ShowNextPageButton="False" ShowPreviousPageButton="True" ButtonType="Button"  />
                                        <asp:NumericPagerField ButtonType="Link" />
                                        <asp:NextPreviousPagerField ShowLastPageButton="True" LastPageText=" Sista " ShowNextPageButton="True" ShowPreviousPageButton="False" ButtonType="Button"  />
                                    </Fields>
                                </asp:DataPager>
                            </div>
                        </LayoutTemplate>

      
                        <ItemTemplate>
                             <!-- Mall för varje rad i databasen med andra ord varje rad i tabellen som loopas fram-->
                            <tr>
                                 <td>
                                <asp:Label ID="ItemFNameLabel" runat="server" Text='<%#: Item.FirstName %>'></asp:Label>

                            </td>
                            <td>
                                <asp:Label ID="ItemLNameLabel" runat="server" Text='<%#: Item.LastName %>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="ItemEmailLabel" runat="server" Text='<%#: Item.EmailAddress %>'></asp:Label>
                            </td>
                                <td>
                                    <!-- Knappar för att ta bort och redigera Informations raden i tabellen     -->
                                    <asp:LinkButton runat="server" CommandName="Delete" Text="Ta bort" CausesValidation="false" OnClientClick='<%# Eval("FirstName", "return confirm(\"Vill du radera {0} permanent?\");") %>' />
                                    <asp:LinkButton runat="server" CommandName="Edit" Text="Redigera" CausesValidation="false" />
                                </td>
                            </tr>

                        </ItemTemplate>
                                                    
                      
                        <EmptyDataTemplate>
                            <!-- Om ingen användare finns registrerad i systemet visas den här texten-->
                            <p>inga personer är registrerade i systemet</p>
                        </EmptyDataTemplate>

 
                        <InsertItemTemplate>
                            <!-- Template för att lägga till nya anändare med andra ord nya rader i databasen -->
                            <tr>
                                <td>
                                    <asp:TextBox ID="FirstName" runat="server" Text="<%# BindItem.FirstName %>" MaxLength="50"></asp:TextBox>

                                </td>
                                <td>
                                    <asp:TextBox ID="LastName" runat="server" Text="<%# BindItem.LastName %>" MaxLength="50"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="EmailAddress" runat="server" Text="<%# BindItem.EmailAddress %>" MaxLength="50"></asp:TextBox>
                                </td>
                                <td>
                                    <!-- Knappar för att lägga till kontakter. -->
                                    <asp:LinkButton runat="server" CommandName="Insert" Text="Lägg till"  /> 
                                    <asp:LinkButton runat="server" CommandName="Cancel" Text="Avbryt" CausesValidation="false" />
                                </td>
                            </tr>
                        </InsertItemTemplate>

  
                        <EditItemTemplate>
                             <!-- Template för att redigera rader i databasen -->
                            <tr>
                                <td>
                                    <asp:TextBox ID="FirstName" runat="server" Text="<%# BindItem.FirstName %>" MaxLength="50"></asp:TextBox>
   
                                </td>
                                <td>
                                    <asp:TextBox ID="LastName" runat="server" Text="<%# BindItem.LastName %>" MaxLength="50"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="EmailAddress" runat="server" Text="<%# BindItem.EmailAddress %>" MaxLength="50"></asp:TextBox>
                                </td>
                                <td>
                                    <!-- Knappar för att redigera kontakter/databas-->
                                    <asp:LinkButton runat="server" CommandName="Update" Text="Spara" /> 
                                    <asp:LinkButton runat="server" CommandName="Cancel" Text="Avbryt" CausesValidation="false" />
                                </td>
                            </tr>
                        </EditItemTemplate>

                    </asp:ListView>
            
            </div>
        </div>
        
    </form>

     <!-- tar bort ladda upp meddelande-->
    <script type="text/javascript" src="js/Script.js"></script>

</body>
</html>
