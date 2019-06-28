<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TrialTestQuestion.aspx.cs" Inherits="WebTests.TrialTestQuestion" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Training</title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        #msg {
            color: Red;
            display: none;
        }
    </style>
    <script type="text/javascript">
        function requestParam(name) {
            if (name = (new RegExp('[?&]' + encodeURIComponent(name) + '=([^&]*)')).exec(location.search))
                return decodeURIComponent(name[1]);
        }

        window.onload = function () {
            var object = document.getElementById("QuestionsPerPart");
            object.onchange = function () {
                var object = document.getElementById("QuestionsPerPart");
                var questId = object.options[object.selectedIndex].value;
                var testId = requestParam("testId");
                window.location.href = 'TrialTestQuestion.aspx?testId=' + testId + '&questId=' + questId;
            };
        }
        function validForm(f) {
            if (isDigit(f.value)) document.getElementById("msg").style.display = "none";
            else document.getElementById("msg").style.display = "inline";

        }

        function isDigit(data) {
            var numStr = "0123456789,. "
            var k = 0;
            for (i = 0; i < data.length; i++) {
                thisChar = data.substring(i, i + 1);
                if (numStr.indexOf(thisChar) != -1) k++;
            }
            if (k == data.length) return 1;
            else return 0;
        }
        function pageLoad(sender, args) {
            $addHandler(document, "keydown", OnKeyPress);
        }

        function OnKeyPress(args) {
            if (args.keyCode == Sys.UI.Key.esc) {
                $find("ModalPopupExtender1").hide();
            }
        };



    </script>
</head>
<body>
    <form id="Form1" runat="server" submitdisabledcontrols="True">
        <div class="page">
            <div class="header">

                <div class="title">
                    <p>
                        <img src="images/logo.png" height="60px" />
                        <asp:Label runat="server" ID="lblUserName" />, Training:
                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                    </p>
                </div>
                <div class="clear hideSkiplink">
                    <asp:Menu ID="NavigationMenu" runat="server" CssClass="menu" EnableViewState="false"
                        IncludeStyleBlock="false" Orientation="Horizontal">
                        <Items>
                            <asp:MenuItem NavigateUrl="~/Default.aspx" Text="To main page" />
                            <asp:MenuItem NavigateUrl="~/ListExam.aspx" Text="Exam" />
                            <asp:MenuItem NavigateUrl="~/ListTrialTest.aspx" Text="Choose another test" />
                        </Items>
                    </asp:Menu>
                </div>
            </div>
            <div class="main">
                <%-- <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>--%>
                <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                </ajaxToolkit:ToolkitScriptManager>
                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" PopupControlID="Panel1"
                    TargetControlID="Button3" runat="server" CancelControlID="Button2" Enabled="true"
                    BackgroundCssClass="content" DropShadow="True">
                </ajaxToolkit:ModalPopupExtender>
                <ajaxToolkit:AnimationExtender ID="AnimationExtender1" runat="server" TargetControlID="Panel1">
                    <Animations>
     <OnLoad>
     <Sequence>

     <Parallel AnimationTarget="Panel1" Duration=".3" Fps="25">
         <Move Horizontal="120" Vertical="100" />
            <Resize Height="200" Width="280" />
              <Color AnimationTarget="Panel1" PropertyKey="backgroundColor"
                StartValue="#AAAAAA" EndValue="#00FFFF" />
     </Parallel>
  <FadeIn AnimationTarget="Panel3" Duration=".3"/>
      <Parallel AnimationTarget="Panel3" Duration=".3">
        <Color PropertyKey="color"
        StartValue="#246ACF" EndValue="#00FFFF" />
        <Color PropertyKey="borderColor"
        StartValue="#246ACF" EndValue="#00FFFF" />
     </Parallel>
<Parallel AnimationTarget="Panel3" Duration=".2">
<Color PropertyKey="color"
StartValue="#246ACF" EndValue="#00FFFF" />
<Color PropertyKey="borderColor"
StartValue="#FF0000" EndValue="#00FFFF" />

</Parallel>
</Sequence>
         <%-- <FadeIn Duration=".5" Fps="25" />--%>
            
        </OnLoad>
   
                    </Animations>
                </ajaxToolkit:AnimationExtender>
                <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup1" Style="display: none">
                    <br />
                    <br />
                    <br />
                    <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="Panel3" runat="server">
                                <div style="text-align: center">
                                    <asp:Label ID="Result" runat="server" Font-Bold="True" Font-Size="14pt" ForeColor="#00CC00"></asp:Label><br />
                                    <br />
                                    <br />
                                    <br />
                                </div>
                                <div class="footer">
                                    <asp:Button ID="Button2" CssClass="hand" runat="server" Text="Ok" />
                                    <asp:Button ID="Button4" CssClass="hand" runat="server" Text="Next" OnClick="Next" />
                                </div>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
                <br />
                <div class="myframe" style="width: 120px;">
                    <asp:ListBox ID="QuestionsPerPart" CssClass="hand" runat="server" Style="font-weight: bold; height: 500px; border: none;"></asp:ListBox>
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <div class="panelka" style="width: 750px;">
                            <h2>
                                <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                            </h2>
                            <fieldset>
                                <legend>
                                    <asp:Label ID="Labvarotv" runat="server" Text="Label">Answer choice:</asp:Label>
                                </legend>
                                <asp:Panel ID="Panel2" runat="server">
                                </asp:Panel>
                            </fieldset>
                            <asp:TextBox ID="TextBox1" Visible="false" onkeyup="validForm(this)"
                                runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                            <%--<asp:Button ID="Button1" CssClass="hand" Visible="false" Enabled="true" runat="server"
                            Text="Изменить порядок" OnClick="Butizmpor_Click" />--%>
                            <asp:Label ID="Label3" runat="server" Visible="false" Text="Label">Example: 2,1,3</asp:Label><br />
                            <center>
                            <span id="msg">Please enter a number</span></center>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="submitButton" style="margin-bottom: 20px;">
                <asp:Button ID="BackButton" CssClass="hand" runat="server" OnClick="Prev" Text="Back"
                    Width="82px" />
                <asp:Button ID="NextButton" CssClass="hand" runat="server" OnClick="Next" Text="Next"
                    Width="82px" />
                <asp:Button ID="AnswerButton" CssClass="hand" runat="server" OnClick="Answer" Text="Submit"
                    Width="82px" />
                <asp:Button ID="Button3" runat="server" Text="Button" Style="display: none" />
            </div>
        </div>
    </form>
</body>
</html>
