<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ExampleWebFormsApplication.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button Text="Feature 1 - exisiting in production" runat="server" />
        <asp:Button ID="Feature2Button" Text="Feature 2 - still in development" runat="server" />
        <p>Note that the feature2 button is not visible at runtime because it is disabled in the app config.</p>
    </div>
    </form>
</body>
</html>
