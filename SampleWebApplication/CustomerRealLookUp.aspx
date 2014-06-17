<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerRealLookUp.aspx.cs" Inherits="SampleWebApplication.CustomerRealLookUp" %>

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
        Search by last name:<br />
        <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
        <asp:Button ID="Search" runat="server" onclick="Search_Click" Text="Button" />
        <br />
        <asp:GridView ID="GridView1" runat="server">
        </asp:GridView>
    
    </div>
    </form>
</body>
</html>
