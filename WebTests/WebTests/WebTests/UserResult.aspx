<%@ Page Title="User Result" Language="C#" AutoEventWireup="true"
    CodeBehind="UserResult.aspx.cs" Inherits="WebTests.UserResult" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Result</title>

</head>
<body>
<img src="images/logo.png" height="32px" />     
    <div id="element" style="width: 100%;">
        <span style="font-size: large; font-weight: bold;">
            <p>
                Result of
                <asp:Label ID="LabelTest" runat="server"></asp:Label>

            </p>
            <br />

            <asp:Label ID="LabelDate" runat="server" Text="Label"></asp:Label>
        </span>

        <asp:Panel ID="Panel1" runat="server">
            <asp:Label ID="LabelFn" runat="server"></asp:Label><br />
            <asp:Label ID="LabelLn" runat="server"></asp:Label><br />

            <div style="text-align: center;">

                <span style="font-size: large; font-weight: bold;">Questions
        
                </span>
            </div>

        </asp:Panel>

        <asp:Label ID="Label1" runat="server"></asp:Label><br />
        <asp:Label ID="LabelProcent" runat="server"></asp:Label>
        <br />

    </div>
</body>
</html>
