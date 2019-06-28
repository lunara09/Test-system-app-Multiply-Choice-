<%@ Register Assembly="AjaxControlToolkit"  Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Page Language="C#" AutoEventWireup="true"  CodeBehind="EkzQuest.aspx.cs" Inherits="WebTests.WebForm1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Пробный Exam</title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
    
   <style type="text/css">
      #msg
      {color:Red;
       display:none;
      	}
    </style>
    <script type="text/javascript" language="javascript">

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
                window.location.href = 'EkzQuest.aspx?testId=' + testId + '&questId=' + questId;
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
        };

       

    </script>
</head>
<body oncontextmenu="return false;">

    <form id="Form1" runat="server" submitdisabledcontrols="True">
    <div class="page">
        <div class="header">
            <div class="title">
                <h1>
               Пробный Exam : <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                </h1>
            </div>
            <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
            </asp:ScriptManager>
            <div class="clear hideSkiplink">
                <asp:Menu ID="NavigationMenu" runat="server" CssClass="menu" EnableViewState="false"
                    IncludeStyleBlock="false" Orientation="Horizontal">
                    <Items>
                     <asp:MenuItem NavigateUrl="~/Default.aspx" Text="To main page" />
                          <asp:MenuItem NavigateUrl="~/ekzamen.aspx" Text="Choose another test" />
                       
                    </Items>
                  
                </asp:Menu>
            </div>
        </div>
        <div class="main">

            <div class="myframe" style="width: 120px;">
                <asp:ListBox ID="QuestionsPerPart" runat="server" Style="font-weight: bold; height: 500px;
                    border: none; overflow: auto;"></asp:ListBox>
            </div> 
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="panelka" style="width: 750px;">
                        <h2>
                            <asp:Label ID="LabelVoprosa" runat="server" Text="Label"></asp:Label>
                        </h2>
                        <fieldset>
                            <legend>
                                <asp:Label ID="Labvarotv" runat="server" Text="Label">Answer choice:</asp:Label>
                            </legend>
                            <asp:Panel ID="Panel2" CssClass="hand" runat="server">
                            </asp:Panel>
                        </fieldset>
                        <asp:TextBox ID="TextBoxVariant" onkeyup="validForm(this)"  Visible="false" AutoPostBack="true" runat="server" 
                            ontextchanged="TextBoxVariant_TextChanged"></asp:TextBox>
<%--                        <asp:Button ID="ButtonPOrIzmen" Visible="false" runat="server" Text="Изменить порядок" 
                            onclick="ButtonPOrIzmen_Click" />--%>
                        <asp:Label ID="Primer" runat="server" Visible="false" Text="Label">For example: 2,3,1</asp:Label>
                         <center><span id="msg">Please enter a number</span></center>
                       </div>
                </ContentTemplate>
            </asp:UpdatePanel>



           <div class="clock" style="width:100px; left:1000px; font-weight: bold;">
        <center>
          
                Prior to the completion of exams left<br />
                 <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                 <ContentTemplate>
                                 <asp:Label ID="Labeltimer" runat="server" 
                    ClientIDMode="Static" Text=""></asp:Label>  
                    <asp:Timer ID="Timer1" Interval="1000" runat="server"   >
                    </asp:Timer>
                 </ContentTemplate>
                 </asp:UpdatePanel>                      
                    <br /> <br /><br />                                     

                 <asp:UpdatePanel ID="UpdatePanel2" runat="server"  UpdateMode="Conditional">
                 <ContentTemplate>
                  <asp:Button ID="AnswerButton" runat="server" Style="font-weight: bold; cursor:pointer;" OnClick="Ans" Visible="true" Enabled="false" Text="Ответить"/>       
                    
                  </ContentTemplate>
                 </asp:UpdatePanel> 
                 <br />
           <%-- <asp:Button ID="Buttonnaekz" Visible="true" Enabled="false" 
                Style="font-weight: bold; cursor:pointer;"  runat="server" Text="Exam" 
                onclick="Buttonnaekz_Click" />     --%>            
                </center> 
                </div>             
                </div>  
                 </div>
        </div>
       <!--[if gte IE 9]><script type="text/javascript">
 createPopup().show( 0, 0, 0, 0, 0 );
  </script><![endif]--> 
    </form>
</body>
</html>
