<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ekzamen.aspx.cs" Inherits="WebTests.OTPB" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">

<head id="Head12" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Режим "Exam"</title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />  
</head>
<body>
    <form id="Form12" runat="server">
    <div class="page">
        <div class="header">
            <div class="title">
                <h1>
                You chose exam. Select the test
                </h1>
            </div>
            <div class="loginDisplay">
                <asp:LoginView ID="HeadLoginVie" runat="server" EnableViewState="false">
                 
                    <LoggedInTemplate>
                        Welcome <span class="bold"><asp:LoginName ID="HeadLoginNam" runat="server" /></span>!
                        [ <asp:LoginStatus ID="HeadLoginStatu" runat="server" LogoutAction="Redirect" LogoutText="Log out" LogoutPageUrl="~/"/> ]
                    </LoggedInTemplate>
                </asp:LoginView>
            </div>
            <div class="clear hideSkiplink">
                <asp:Menu ID="NavigationMen" runat="server" CssClass="menu" EnableViewState="false" IncludeStyleBlock="false" Orientation="Horizontal">
                    <Items>
                       <asp:MenuItem NavigateUrl="~/Default.aspx" Text="To main page" />  
                        <asp:MenuItem NavigateUrl="~/OTPB.aspx" Text="Training"/>
                         <%--<asp:MenuItem NavigateUrl="~/probekzamen.aspx" Text="Пробный Exam"/>--%>
                        <%--<asp:MenuItem NavigateUrl="~/About.aspx" Text="О программе"/>--%>
                    </Items>
                </asp:Menu>
              
            </div>
           
        </div>
        <div class="main">
       
           <asp:Repeater ID="Repeater1" runat="server" 
          DataSourceID="SqlDataSource1">
          <HeaderTemplate>
              <table>
              <tr>
                 <th>№</th>
                 <th>Раздел</th>
              </tr>
          </HeaderTemplate>
              
          <ItemTemplate>
          <tr>
              <td bgcolor="#CCFFCC">
                <asp:Hyperlink runat="server" ID="Label1" 
                    text= '<%# DataBinder.Eval(Container,"ItemIndex") %>' 
                        NavigateUrl='<%# String.Format("EkzQuest.aspx?testId={0}",Eval("TESTID")) %>'  /> 
              </td>
              <td bgcolor="#CCFFCC">
                <%--  <a href='<%#"EkzQuest.aspx?testId="+Request.QueryString["id"]%><%#Eval("id") %>'> <%#Eval("name") %> </a>--%>
                  <asp:Hyperlink runat="server" ID="Label2" 
                      text='<%# Eval("TESTTITLE") %>' 
                        NavigateUrl='<%# String.Format("EkzQuest.aspx?testId={0}",Eval("TESTID")) %>'  />                     
                    
              </td>
          </tr>
          </ItemTemplate>

          <AlternatingItemTemplate>
          <tr>
              <td >
                <asp:Hyperlink runat="server" ID="Label3" 
                    text='<%# DataBinder.Eval(Container,"ItemIndex") %>'
                     NavigateUrl='<%# String.Format("EkzQuest.aspx?testId={0}",Eval("TESTID")) %>'  /> 
              </td>
              <td >
                 <asp:Hyperlink runat="server" ID="Label4" 
                     text='<%# Eval("TESTTITLE") %>' 
                   NavigateUrl='<%# String.Format("EkzQuest.aspx?testId={0}",Eval("TESTID")) %>'  />    
              </td>
          </tr>
          </AlternatingItemTemplate>

          <FooterTemplate>
              </table>
          </FooterTemplate>
      </asp:Repeater>

   <%--          <asp:SqlDataSource 
          ConnectionString=
              "<%$ ConnectionStrings:testdbConnectionString %>"
          ID="SqlDataSource1" runat="server" 
          SelectCommand="SELECT [id], [name]
               FROM [TEST] WHERE [status] = 'true' ">
      </asp:SqlDataSource>--%>
            
            <asp:SqlDataSource 
                ConnectionString=
                "<%$ ConnectionStrings:testdbConnectionString %>"
                ID="SqlDataSource1" runat="server" 
                SelectCommand="SELECT * FROM [TEST]">
                <SelectParameters>
                    <asp:Parameter DefaultValue="true" Name="status" Type="Boolean" />
                </SelectParameters>
            </asp:SqlDataSource>
            

        </div>



        <div class="clear">
        </div>
    </div>
    <div class="footer">
        
    </div>
    </form>
</body>
</html>

