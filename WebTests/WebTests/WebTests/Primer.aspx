<%@ Page Title="Домашняя страница" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Primer.aspx.cs" Inherits="WebTests.Primer" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <center>
   <asp:HyperLink ID="HyperLink1"  runat="server">Results of Exam</asp:HyperLink> <br /><br />
    <div>
    <div>
     <table><tr><td class="td"> 
     <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
     </td></tr></table>
      </div>
   
    </div>
   
   
     </center>
    
</asp:Content>
   