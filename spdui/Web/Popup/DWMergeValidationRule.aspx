<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DWMergeValidationRule.aspx.cs" Inherits="Popup_DWMergeValidationRule" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Rule Validation</title>
    <link href="../style.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript">
    var P = window.parent, E = P.setDialog();

    window.onload = function()
    {
	    P.S.auto();
    }
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="0" width="100%">
            <tr>
                <td colspan="2" align="right">
                    <input type="button" id="pause" name="pause" value="Pause" onclick="onPause();" style="display:inline"/>&nbsp;
                    <input type="button" id="start" name="start" value="Start" onclick="onStart();" style="display:none"/>&nbsp;
                    <input type="button" id="close" name="close" value="Close" onclick="onClose();"/>&nbsp;
                </td>
            </tr>
            <tr>
                <td style="display:none">
                    <asp:Button ID="btnInValidation" runat="server" OnClick="btnInValidation_Click" Text="Continue" CssClass="btn"/>&nbsp;  
                    <asp:HiddenField ID="txtValidationIds" runat="server"/>
                    <asp:HiddenField ID="txtValidationResults" runat="server"/>
                </td>  
                <td valign="top">
                    <asp:GridView width="100%" runat="server" ID="gvValidationRule" AutoGenerateColumns="False" DataKeyNames="Id" meta:resourcekey="gvValidationRuleResource" 
                     CellPadding="4" CssClass="list" GridLines="Horizontal"  >
                         <HeaderStyle CssClass="listheader" />
                           <Columns>                    
                            <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Center" meta:resourcekey="TemplateFieldResource1" >
                                <ItemTemplate>                      
                                    <asp:CheckBox ID="cbSelect" runat="server" meta:resourcekey="cbSelectResource1" Visible='<%# DataBinder.Eval(Container.DataItem, "ValidationStatus") != null && DataBinder.Eval(Container.DataItem, "ValidationStatus").Equals("Waiting")%>' Checked='<%# IsChecked(DataBinder.Eval(Container.DataItem, "Id").ToString()) %>'/>                                
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rule Name">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "Name")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rule Type">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "RuleType")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <font style='display:<%# DataBinder.Eval(Container.DataItem, "ValidationStatus") != null ? "inline" : "none" %>' color='<%# DataBinder.Eval(Container.DataItem, "ValidationStatus") == null ? (DataBinder.Eval(Container.DataItem, "Status") != null ? (DataBinder.Eval(Container.DataItem, "Status").Equals("Passed") ? "Green" : "Red") : "") : DataBinder.Eval(Container.DataItem, "ValidationStatus") == "In Progress" ? "Blue" : "Gray" %>'>                            
                                        <%# DataBinder.Eval(Container.DataItem, "ValidationStatus") == null ? DataBinder.Eval(Container.DataItem, "Status") : DataBinder.Eval(Container.DataItem, "ValidationStatus") == "In Progress" ? "In Progress" : "Waiting"%>                                
                                    </font>
                                    <asp:LinkButton ID="lbtnRuleStatus" runat="server" Visible='<%# DataBinder.Eval(Container.DataItem, "ValidationStatus") == null%>' Text='<%# DataBinder.Eval(Container.DataItem, "ValidationStatus") == null ? DataBinder.Eval(Container.DataItem, "Status") : DataBinder.Eval(Container.DataItem, "ValidationStatus") == "In Progress" ? "In Progress" : "Waiting"%>' CommandName="Select" meta:resourcekey="lbtnRuleStatusResource" ForeColor='<%# DataBinder.Eval(Container.DataItem, "ValidationStatus") == null ? (DataBinder.Eval(Container.DataItem, "Status") != null ? (DataBinder.Eval(Container.DataItem, "Status").Equals("Passed") ? System.Drawing.Color.Green : System.Drawing.Color.Red) : System.Drawing.Color.Black) : DataBinder.Eval(Container.DataItem, "ValidationStatus") == "In Progress" ? System.Drawing.Color.Blue : System.Drawing.Color.Gray %>'></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>            
                </td>
             </tr>
          </table>    
        </div>
    </form>
</body>
</html>
<script language="javascript" type="text/javascript">   
    var isPause = false;
    var intervalId = null;
    if (document.getElementById("<%= txtValidationIds.ClientID %>").value != "") 
    {           
       intervalId =  window.setInterval("autoSubmit()", 1000);                       
    }   
    else
    {
        intervalId =  window.setInterval("autoClose()", 1000);    
    }
    
    function onPause()
    {
        isPause = true;
        document.getElementById("pause").style.display = "none";
        document.getElementById("start").style.display = "inline";
    } 
    
    function onStart()
    {
        isPause = false;
        document.getElementById("pause").style.display = "inline";
        document.getElementById("start").style.display = "none";
    }
    
    function autoSubmit()
    {
        if (!isPause) {
            window.clearInterval(intervalId);
            document.getElementById("pause").disabled = true;
            document.getElementById("<%= btnInValidation.ClientID %>").click();
        }
    }     
    
    function autoClose()
    {
        onClose();
    } 
    
    function onClose()
    {
       E.J('#hfvr').val( E.J('#<%= txtValidationResults.ClientID %>',document).val() )
       P.A().custom();
       
    }            

</script>