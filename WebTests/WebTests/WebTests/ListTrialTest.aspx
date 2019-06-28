<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListTrialTest.aspx.cs" Inherits="WebTests.ListTrialTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Training</title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .auto-style1 {
            height: 244px;
        }
    </style>
</head>
<body>
    <form id="Form1" runat="server">
        <div class="page">
            <div class="header">
                <div class="title">
                    <p>
                        <img src="images/logo.png" height="60px" />

                        <asp:Label runat="server" ID="lblUserName" />  This is Your Training section. Please choose the Test

                    </p>
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
                            <asp:MenuItem NavigateUrl="~/ListExam.aspx" Text="Exam" />
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
                                    Text='<%# ((int) DataBinder.Eval(Container,"ItemIndex")) +1 %>'
                                    NavigateUrl='<%# String.Format("TrialTestQuestion.aspx?testId={0}&questid=1",Eval("TESTID")) %>'></asp:HyperLink>
                            </td>
                            <td bgcolor="#CCFFCC">

                                <asp:HyperLink runat="server" ID="Label2"
                                    Text='<%# Eval("TESTTITLE") %>'
                                    NavigateUrl='<%# String.Format("TrialTestQuestion.aspx?testId={0}&questid=1",Eval("TESTID")) %>' />

                            </td>
                        </tr>
                    </ItemTemplate>

                    <AlternatingItemTemplate>
                        <tr>
                            <td>
                                <asp:HyperLink runat="server" ID="Label3"
                                    Text='<%# ((int) DataBinder.Eval(Container,"ItemIndex")) +1 %>'
                                    NavigateUrl='<%# String.Format("TrialTestQuestion.aspx?testId={0}&questid=1",Eval("TESTID")) %>'></asp:HyperLink>
                            </td>
                            <td>
                                <asp:HyperLink runat="server" ID="Label4"
                                    Text='<%# Eval("TESTTITLE") %>'
                                    NavigateUrl='<%# String.Format("TrialTestQuestion.aspx?testId={0}&questid=1",Eval("TESTID")) %>' />
                            </td>
                        </tr>
                    </AlternatingItemTemplate>

                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>

                <asp:SqlDataSource
                    ConnectionString="<%$ ConnectionStrings:testdb2Entities %>"
                    ID="SqlDataSource1" runat="server"
                    SelectCommand="SELECT * FROM [TEST]">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="true" Name="status" Type="Boolean" />
                    </SelectParameters>
                </asp:SqlDataSource>


                <img alt="" class="auto-style1" src="images/online-examination.jpg" /></div>
            <div class="clear">
            </div>
        </div>
        <div class="footer">
        </div>
    </form>
</body>
</html>
