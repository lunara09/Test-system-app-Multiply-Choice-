<%@ Page Title="Протокол" Language="C#"  AutoEventWireup="true"
    CodeBehind="About.aspx.cs" Inherits="WebTests.About" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Протокол</title>
 
    </head>
<body>  
   
   <div id="element" style="width: 100%;">
	<span style="font-size: large; font-weight: bold;">Results of Examа by 
        <asp:Label ID="Labeldate" runat="server" Text="Label"></asp:Label>
    </span>

        <asp:Panel ID="Panel1" runat="server">
          <%--  <asp:Label ID="LabeChlenComis" runat="server" ></asp:Label><br />--%>
               <asp:Label ID="Labelfio" runat="server" ></asp:Label><br />
               <asp:Label ID="Labeltod" runat="server" ></asp:Label><br />
               <asp:Label ID="Labeldol" runat="server"></asp:Label><br />         
               <asp:Label ID="LabelPart" runat="server" ></asp:Label><br />
            
               <div style="text-align: center;">

		<span style="font-size: large; font-weight: bold;">Questions
        
        </span> </div>
          
        </asp:Panel>

      <asp:Label ID="Label1" runat="server" ></asp:Label><br />
            <asp:Label ID="LabelProcent" runat="server" ></asp:Label> <br />

       <asp:Label ID="LabelFoterSday" runat="server"  ></asp:Label>

       <asp:Label ID="LabelFoterKomis" runat="server"  style="margin-left:100px;"></asp:Label>
    </div>
</body>
</html>