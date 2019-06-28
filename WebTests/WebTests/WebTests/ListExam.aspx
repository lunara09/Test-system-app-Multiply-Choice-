<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListExam.aspx.cs" Inherits="WebTests.ListExam" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">

<head id="Head12" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title> "Exam"</title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .auto-style1 {
            width: 270px;
            height: 261px;
        }
    </style>
</head>
<body>
    <form id="Form12" runat="server">
        <div class="page">
            <div class="header">
                <div class="title">
                    <p>
                        <img src="images/logo.png" height="60px" />&nbsp;

                        <asp:Label runat="server" ID="lblUserName" />&nbsp;This is your EXAM section, Please choose the Test. 
                    </p>
                </div>
                <div class="loginDisplay">
                    <asp:LoginView ID="HeadLoginVie" runat="server" EnableViewState="false">

                        <LoggedInTemplate>
                            Welcome <span class="bold">
                                <asp:LoginName ID="HeadLoginNam" runat="server" />
                            </span>!
                        [
                            <asp:LoginStatus ID="HeadLoginStatu" runat="server" LogoutAction="Redirect" LogoutText="Log out" LogoutPageUrl="~/" />
                            ]
                        </LoggedInTemplate>
                    </asp:LoginView>
                </div>
                <div class="clear hideSkiplink">
                    <asp:Menu ID="NavigationMen" runat="server" CssClass="menu" EnableViewState="false" IncludeStyleBlock="false" Orientation="Horizontal">
                        <Items>
                            <asp:MenuItem NavigateUrl="~/Default.aspx" Text="To main page" />
                            <asp:MenuItem NavigateUrl="~/ListTrialTest.aspx" Text="Training" />
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
                                <th>Test</th>
                            </tr>
                    </HeaderTemplate>

                    <ItemTemplate>
                        <tr>
                            <td bgcolor="#CCFFCC">
                                <asp:HyperLink runat="server" ID="Label1"
                                    Text='<%# ((int)DataBinder.Eval(Container,"ItemIndex")+1) %>'
                                    NavigateUrl='<%# String.Format("ExamQuestion.aspx?testId={0}",Eval("TESTID")) %>' />
                            </td>
                            <td bgcolor="#CCFFCC">
                                <%--  <a href='<%#"ExamQuestion.aspx?testId="+Request.QueryString["id"]%><%#Eval("id") %>'> <%#Eval("name") %> </a>--%>
                                <asp:HyperLink runat="server" ID="Label2"
                                    Text='<%# Eval("TESTTITLE") %>'
                                    NavigateUrl='<%# String.Format("ExamQuestion.aspx?testId={0}",Eval("TESTID")) %>' />

                            </td>
                        </tr>
                    </ItemTemplate>

                    <AlternatingItemTemplate>
                        <tr>
                            <td>
                                <asp:HyperLink runat="server" ID="Label3"
                                    Text='<%#((int)DataBinder.Eval(Container,"ItemIndex")+1) %>'
                                    NavigateUrl='<%# String.Format("ExamQuestion.aspx?testId={0}",Eval("TESTID")) %>' />
                            </td>
                            <td>
                                <asp:HyperLink runat="server" ID="Label4"
                                    Text='<%# Eval("TESTTITLE") %>'
                                    NavigateUrl='<%# String.Format("ExamQuestion.aspx?testId={0}",Eval("TESTID")) %>' />
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
                    ConnectionString="<%$ ConnectionStrings:testdb2Entities %>"
                    ID="SqlDataSource1" runat="server"
                    SelectCommand="SELECT * FROM [TEST]">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="true" Name="status" Type="Boolean" />
                    </SelectParameters>
                </asp:SqlDataSource>


                     <img alt="" class="auto-style1" src="images/exam.png" /><br />
                &nbsp;</div>



            <div class="clear">
            </div>
        </div>
        <div class="footer">
        </div>
    </form>
</body>
</html>

