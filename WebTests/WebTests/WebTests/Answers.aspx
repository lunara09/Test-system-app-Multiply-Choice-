<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Answers.aspx.cs" Inherits="WebTests.Answers1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Exam</title>
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
//     function validForm() {

//                     var key = window.event.keyCode;
//                     if (key < 44 || key > 57)
//                         window.event.returnValue = false;
//     }

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
             window.location.href = 'Answers.aspx?testId=' + testId + '&questId=' + questId;
         };
     };

    </script>
</head>
<body oncontextmenu="return false;">

    <form id="Form11" runat="server" submitdisabledcontrols="True">
    <div class="page">
        <div class="header">
            <div class="title">
                <h1>
                Exam по теме: <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                </h1>
            </div>
           <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
            </asp:ScriptManager>
            <div class="clear hideSkiplink">
                <asp:Menu ID="NavigationMenu" runat="server" CssClass="menu" EnableViewState="false"
                    IncludeStyleBlock="false" Orientation="Horizontal">
                    <Items>
                     <%--<asp:MenuItem NavigateUrl="~/Default.aspx" Text="To main page" />
                          <asp:MenuItem NavigateUrl="~/ekzamen.aspx" Text="Choose another test" />--%>
                       
                    </Items>
                  
                </asp:Menu>
            </div>
        </div>
        <div class="main">
      
            <div class="myframe" style="width: 120px;">
                <asp:ListBox ID="QuestionsPerPart" runat="server" Style="font-weight: bold; height: 400px;
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
                                <asp:Label ID="Labvarotv" runat="server" Text="Label">Варианты ответов:</asp:Label>
                            </legend>
                            <asp:Panel ID="Panel2" CssClass="hand" runat="server">
                            </asp:Panel>
                        </fieldset>
                        <asp:Label ID="Label3"  Visible="false" runat="server"  Text="Label">Ответ:</asp:Label>
                        <asp:TextBox ID="TextBox1"  Visible="false"  
                            ValidationGroup="group1"   runat="server" ></asp:TextBox>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="None"  SetFocusOnError="true" runat="server" ValidationGroup="group1" ControlToValidate="TextBox1" ErrorMessage="Поле ''Ответ'' является обязательным" EnableClientScript="True"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator
                            ID="RegularExpressionValidator1" runat="server"  Display="None" SetFocusOnError="true"  ValidationGroup="group1" ControlToValidate="TextBox1" ErrorMessage="Неверный формат данных.Посмотрите пример заполнения"></asp:RegularExpressionValidator>
                       
                        <asp:Label ID="Label2" runat="server" Visible="false" Text="Label">Пример: 2,1,3</asp:Label>                       
                         
                        
                         <asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="group1" CssClass="failureNotification" runat="server" />              
                        </div>
                </ContentTemplate>
            </asp:UpdatePanel>
           <div class="clock" style="width:100px; left:1000px; font-weight: bold;">
        <center>          
                Prior to the completion of exams left<br />
                 

                 <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                 <ContentTemplate>
                                 <asp:Label ID="Labeltimer" runat="server" 
                    ClientIDMode="Static" Text=""></asp:Label>  
                    <asp:Timer ID="Timer1" Interval="10" runat="server" Enabled="true" ontick="Timer1_Tick" >
                    </asp:Timer>
                 </ContentTemplate>
                 </asp:UpdatePanel>                                                  
                
                    <br /> <br /><br />                                     
            
                 <asp:UpdatePanel ID="UpdatePanel2" runat="server"  UpdateMode="Conditional">
                 <ContentTemplate>
                  <asp:Button ID="AnswerButton" ValidationGroup="group1" runat="server"  Style="font-weight: bold; cursor:pointer;" OnClick="Ans" Visible="true" Enabled="false" Text="Ответить"/>       
                    
                        
                  </ContentTemplate>
                 </asp:UpdatePanel> 
                 <br />         
                 </center> 
                </div>  
             
                 </div>
        </div>
       <!--[if gte IE 9]><script type="text/javascript">
 createPopup().show( 0, 0, 0, 0, 0 );
  </script><![endif]--> 
    </form>
</body>
</html>
