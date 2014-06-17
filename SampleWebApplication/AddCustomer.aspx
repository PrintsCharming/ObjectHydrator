<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddCustomer.aspx.cs" Inherits="SampleWebApplication.AddCustomer" %>

<%@ Register src="Menu.ascx" tagname="Menu" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <uc1:Menu ID="Menu1" runat="server" />
    <div>
    
        First Name<asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
        <br />
        Last Name<asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
        <br />
        Street Address<asp:TextBox ID="txtStreetAddress" runat="server"></asp:TextBox>
        <br />
        City<asp:TextBox ID="txtCity" runat="server"></asp:TextBox>
        <br />
        State<asp:TextBox ID="txtState" runat="server"></asp:TextBox>
        <br />
        Zip<asp:TextBox ID="txtZip" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Save" />
    
    </div>
    </form>
</body>
</html>
