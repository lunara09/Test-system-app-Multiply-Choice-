<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebTests.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">
<head id="Head11" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Log in</title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .auto-style1 {
            width: 250px;
            height: 250px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="page">
            <div class="header">
                <div class="title">
                    <p>
                        <img src="images/logo.png" height="60px" />
                        Log in
                    </p>
                </div>
                <div class="clear hideSkiplink">
                    <asp:Menu ID="NavigationMenu" runat="server" CssClass="menu" EnableViewState="false"
                        IncludeStyleBlock="false" Orientation="Horizontal">
                        <Items>
                        </Items>
                    </asp:Menu>
                </div>
            </div>
            <center>
        <div class="main">
            <h2>
              
            </h2>
            <div class="accountInfo">
                <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                    <Services>
                        <asp:ServiceReference Path="~/WebService.asmx" />
                    </Services>
                </ajaxToolkit:ToolkitScriptManager>
                <fieldset>
                    <legend>Log in</legend>
                      <cc1:AutoCompleteExtender TargetControlID="TextBoxFIOSDAushego" EnableCaching="true" ServiceMethod="GetSugguestions"
                        ID="AutoCompleteExtender2" runat="server" MinimumPrefixLength="1" CompletionSetCount="20"
                        ServicePath="WebService.asmx" ShowOnlyCurrentWordInCompletionListItem="true"
                        CompletionInterval="100" BehaviorID="AutoCompleteEx">
                        <Animations> 
  <OnShow>
  <Sequence>

  <OpacityAction Opacity="0" />
  <HideAction Visible="true" />

 
  <ScriptAction Script="// Cache the size and setup the initial size
                                var behavior = $find('AutoCompleteEx');
                                if (!behavior._height) {
                                    var target = behavior.get_completionList();
                                    behavior._height = target.offsetHeight - 2;
                                    target.style.height = '0px';
                                }" />
 
  <Parallel Duration=".4">
  <FadeIn />
  <Length PropertyKey="height" StartValue="0" 
	EndValueScript="$find('AutoCompleteEx')._height" />
  </Parallel>
  </Sequence>
  </OnShow>
  <OnHide>
 
  <Parallel Duration=".4">
  <FadeOut />
  <Length PropertyKey="height" StartValueScript=
	"$find('AutoCompleteEx')._height" EndValue="0" />
  </Parallel>
  </OnHide>
                        </Animations>
                    </cc1:AutoCompleteExtender>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="failureNotification"
                        ValidationGroup="LoginUserValidationGroup" />
                    <table border="0">
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="Label">Name</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBoxFIOSDAushego" runat="server"   Width="300px"></asp:TextBox>
                           
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LabelDolnSdaush" runat="server" Text="Label">Password</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBoxDolSdaush" runat="server" Width="300px"></asp:TextBox>
                              
                            </td>
                        </tr>
                       <%-- <tr>
                            <td>
                                <asp:Label ID="LabelOtdSdaush" runat="server" Text="Label">Отдел</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBoxOtdSdaush" runat="server"  Width="300px"></asp:TextBox>
                            </td>
                        </tr>--%>
                        <tr>
                            <td>&nbsp;<img alt="" class="auto-style1" src="images/login.png" />&nbsp;&nbsp;&nbsp;&nbsp; </td>
                        </tr>
                    </table>
                </fieldset>
              <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="TextBoxfio"
                    runat="server" ErrorMessage="Поле ''Имя пользователя'' является обязательным."
                    ValidationGroup="LoginUserValidationGroup"> 
                  
                     </asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                    CssClass="failureNotification" ErrorMessage="Поле ''Пароль'' является обязательным."
                    ValidationGroup="LoginUserValidationGroup"></asp:RequiredFieldValidator>--%>
                <p class="submitButton">
                    <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Log in"
                        ValidationGroup="LoginUserValidationGroup" OnClick="LoginButton_Click" />
                </p>
                <asp:Label ID="Label1" runat="server" Visible="false" Text="Label"></asp:Label>
                <br />
            </div>
        </div>
            </center>
        </div>
    </form>
</body>
</html>
