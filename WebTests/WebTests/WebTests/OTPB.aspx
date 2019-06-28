<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OTPB.aspx.cs" Inherits="WebTests.OTPB" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Training</title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="Form1" runat="server">
    <div class="page">
        <div class="header">
            <div class="title">
                <h1>
                   You chose Training.Choose section

                </h1>
            </div>
            <div class="loginDisplay">
                <asp:LoginView ID="HeadLoginView" runat="server" EnableViewState="false">
                    <LoggedInTemplate>
                        ����� ���������� <span class="bold">
                            <asp:LoginName ID="HeadLoginName" runat="server" />
                        </span>
                        <asp:LoginStatus ID="HeadLoginStatus" runat="server" LogoutAction="Redirect" LogoutText="�����"
                            LogoutPageUrl="~/" />
                    </LoggedInTemplate>
                </asp:LoginView>
            </div>
            <div class="clear hideSkiplink">
                <asp:Menu ID="NavigationMenu" runat="server" CssClass="menu" EnableViewState="false"
                    IncludeStyleBlock="false" Orientation="Horizontal">
                    <Items>
                        
                         <asp:MenuItem NavigateUrl="~/Default.aspx" Text="To main page" />
                         <%--<asp:MenuItem NavigateUrl="~/probekzamen.aspx" Text="Пробный Exam"/>--%>
                         <asp:MenuItem NavigateUrl="~/ekzamen.aspx" Text="Exam" />
                      <%--  <asp:MenuItem NavigateUrl="~/ekzamen.aspx?ekz=1" Text="�������" />--%>
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
                  text='<%# DataBinder.Eval(Container,"ItemIndex") %>'
                    NavigateUrl='<%# String.Format("PageQuestion.aspx?testId={0}&questid=1",Eval("TESTID")) %>'
                    ></asp:Hyperlink>
              </td>
              <td bgcolor="#CCFFCC">
                
                  <asp:Hyperlink runat="server" ID="Label2" 
                      text='<%# Eval("TESTTITLE") %>' 
                        NavigateUrl='<%# String.Format("PageQuestion.aspx?testId={0}&questid=1",Eval("TESTID")) %>'  />                     
                    
              </td>
          </tr>
          </ItemTemplate>

          <AlternatingItemTemplate>
          <tr>
              <td >
                <asp:Hyperlink runat="server" ID="Label3" 
                   text='<%# DataBinder.Eval(Container,"ItemIndex") %>' 
                     NavigateUrl='<%# String.Format("PageQuestion.aspx?testId={0}&questid=1",Eval("TESTID")) %>'
                    ></asp:Hyperlink>
              </td>
              <td >
                 <asp:Hyperlink runat="server" ID="Label4" 
                     text='<%# Eval("TESTTITLE") %>' 
                   NavigateUrl='<%# String.Format("PageQuestion.aspx?testId={0}&questid=1",Eval("TESTID")) %>'  />    
              </td>
          </tr>
          </AlternatingItemTemplate>

          <FooterTemplate>
              </table>
          </FooterTemplate>
      </asp:Repeater>

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
