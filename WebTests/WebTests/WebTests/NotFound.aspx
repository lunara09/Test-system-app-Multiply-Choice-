<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NotFound.aspx.cs" Inherits="WebTests.NotFound" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
   <title>404 - Ошибка: 404</title>
	<style type="text/css">
.k{background:url('1.png');width:200px;height:50px;border:0;}  
.k:hover{background:url('2.png');width:200px;height:50px;border:0;}  

.cc {padding:5;border:1px solid #FFFFFF; width:500;background:#67d6ff;
text-decoration: none; color: #ffffff; font-family:
  Trebuchet MS; font-weight: bold;}
#d{font-size:30;}
</style> 
</head>
<body>
    <form id="form1" runat="server">
        
    <div>
        No questions found for this section<br /><br />
    <asp:HyperLink ID="HyperLink1" runat="server">Back</asp:HyperLink>
  
    </div>
    </form>
</body>
</html>
