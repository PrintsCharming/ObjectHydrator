<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerFakeLookup.aspx.cs" Inherits="SampleWebApplication.CustomerFakeLookup" %>

<%@ Register src="Menu.ascx" tagname="Menu" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <uc1:Menu ID="Menu1" runat="server" />
        Search for a customer by last name (returns a 'faked' list for purposes of testing)<br />
        <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Search" />
        <br />
        
        <asp:GridView ID="GridView1" runat="server">
        </asp:GridView>
        <br />
    
    </div>
    </form>
</body>
</html>
