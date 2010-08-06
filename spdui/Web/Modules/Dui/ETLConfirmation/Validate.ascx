<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Validate.ascx.cs" Inherits="Modules_Dui_DSUpload_Validate" %>
<script type="text/javascript" src="Popup/lhgcore.min.js"></script>
<script type="text/javascript" src="Popup/lhgdialog.js"></script>
<script language="vbscript">
function ButtonWarning(Action)
    Dim ReturnVal
    Select Case Action
        case "Confirm"
            ReturnVal = msgbox("Are you sure you want to confirm the record?",33)
            If ReturnVal = 1 then
                ButtonWarning = true
            else
                ButtonWarning = false
            end if 
        case "UnConfirm"
            ReturnVal = msgbox("Are you sure you want to unconfirm the record?",33)
            If ReturnVal = 1 then
                ButtonWarning = true
            else
                ButtonWarning = false
            end if 
    end select
end function
</script>
<h2>Data Confirmation - Data Validation</h2>
<p class="formBtnBoard"> <%if (TheDataSourceUpload.ProcessStatus.Equals("OWNER_CONFIRMED")) {%>
            <input type="button" name="btnValidateAll" value="Validate All" onclick="validateAll();" class=btn />        
            <input type="button" name="btnValidateSelected" value="Validate Selected" onclick="validateSelected();" class=btn />
            <%}%>
            <asp:Button ID="btnConfirm" Text="Confirm" runat="server" OnClick="btnConfirm_Click" OnClientClick="return ButtonWarning('Confirm')" class=btn />
            <asp:Button ID="btnUnConfirm" Text="UnConfirm" runat="server" OnClick="btnUnConfirm_Click" OnClientClick="return ButtonWarning('UnConfirm')" class=btn />
            <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" Text="Back"  class=btn />
            <asp:Button ID="btnDownload" runat="server" OnClick="btnDownload_Click" Text="Download Data"  class=btn />
        
        <span style="display:none">
            <asp:Button ID="btnInValidation" runat="server" OnClick="btnInValidation_Click" Text="Continue" class=btn/>  
            <asp:HiddenField ID="txtValidationIds" runat="server" />
        </span>  </p>
<table border="0" class=list>
   
	<tr>
        <td  class="TXTform">Data Source Name:</td>
        <td>
            <asp:Label ID="txtName" runat="server"></asp:Label>
            <asp:HiddenField ID="txtId" runat="server" />
        </td>
        <td  class="TXTform">Category:</td>
        <td>
            <asp:Label ID="txtCategory" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td  class="TXTform">Subject:</td>
        <td>
            <asp:Label ID="txtSubject" runat="server"></asp:Label>
        </td>
        <td  class="TXTform">Status:</td>
        <td>
            <asp:Label ID="txtStatus" runat="server"></asp:Label>
        </td>        
    </tr>
    <tr>
        <td  class="TXTform">File Name:</td>
        <td>
            <asp:Label ID="txtFileNm" runat="server"></asp:Label>
        </td>  
        <td  class="TXTform">Size:</td>
        <td>
            <asp:Label ID="txtSize" runat="server"></asp:Label>bytes
        </td>       
    </tr>	
</table>
<asp:Label ID="lblMessage" Font-Bold="true" ForeColor="blue" Text="Now rule validation is in progress, maybe it will take a few minutes, please wait..." runat="server" Visible="false" CssClass=error></asp:Label>
<hr/>

<b>Data Validation List for This data source</b>
<!-- Modified By Vincent On 2006-9-1 Begin -->
<!--Modify CheckBox Style-->
<table border="0" width="100%">
    <tr>
        <td width="33%" valign="top">
            <asp:GridView runat="server" ID="gvErrorValidationRule" AutoGenerateColumns="False" DataKeyNames="Id" meta:resourcekey="gvErrorValidationRuleResource" OnSelectedIndexChanged="gvErrorValidationRule_SelectedIndexChanged" CellPadding="4" CssClass="list" GridLines="Horizontal" >
                <HeaderStyle CssClass="listheader" />                
                <Columns>                    
                    <asp:TemplateField HeaderText="Validation Rule(Error)" meta:resourcekey="TemplateFieldResource1" >
                        <ItemTemplate>  
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td valign=top>
                                    <input type="checkbox" class=radio name="cbSelect" value='<%# DataBinder.Eval(Container.DataItem, "Id")%>' />                           
                                </td>
                                <td valign="top">
                                     <%# DataBinder.Eval(Container.DataItem, "TheDataSourceRule.Name")%>                       
                                </td>    
                            </tr>
                        </table>
                          </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnErrorRuleStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ValidationStatus") == null ? DataBinder.Eval(Container.DataItem, "Status") : DataBinder.Eval(Container.DataItem, "ValidationStatus") == "In Progress" ? "In Progress" : "Waiting"%>' CommandName="Select" meta:resourcekey="lbtnErrorRuleStatusResource" ForeColor='<%# DataBinder.Eval(Container.DataItem, "ValidationStatus") == null ? (DataBinder.Eval(Container.DataItem, "Status") != null ? (DataBinder.Eval(Container.DataItem, "Status").Equals("Passed") ? System.Drawing.Color.Green : System.Drawing.Color.Red) : System.Drawing.Color.Black) : DataBinder.Eval(Container.DataItem, "ValidationStatus") == "In Progress" ? System.Drawing.Color.Blue : System.Drawing.Color.Gray %>'></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <AlternatingRowStyle CssClass="listA" />
            </asp:GridView>            
        </td>
        <td width="33%" valign="top">
            <asp:GridView runat="server" ID="gvProblemValidationRule" AutoGenerateColumns="False" DataKeyNames="Id" meta:resourcekey="gvProblemValidationRuleResource" OnSelectedIndexChanged="gvProblemValidationRule_SelectedIndexChanged" CellPadding="4" CssClass="list" GridLines="Horizontal" >
                <HeaderStyle CssClass="listheader" />                
                <Columns>                    
                    <asp:TemplateField HeaderText="Validation Rule(Problem)" meta:resourcekey="TemplateFieldResource2">
                        <ItemTemplate>
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td valign=top>
                                    <input type="checkbox" class=radio name="cbSelect" value='<%# DataBinder.Eval(Container.DataItem, "Id")%>' />                            
                                </td>
                                <td valign="top">
                                    <%# DataBinder.Eval(Container.DataItem, "TheDataSourceRule.Name")%>
                                </td>    
                            </tr>
                        </table>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnErrorRuleStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ValidationStatus") == null ? DataBinder.Eval(Container.DataItem, "Status") : DataBinder.Eval(Container.DataItem, "ValidationStatus") == "In Progress" ? "In Progress" : "Waiting"%>' CommandName="Select" meta:resourcekey="lbtnErrorRuleStatusResource" ForeColor='<%# DataBinder.Eval(Container.DataItem, "ValidationStatus") == null ? (DataBinder.Eval(Container.DataItem, "Status") != null ? (DataBinder.Eval(Container.DataItem, "Status").Equals("Passed") ? System.Drawing.Color.Green : System.Drawing.Color.Red) : System.Drawing.Color.Black) : DataBinder.Eval(Container.DataItem, "ValidationStatus") == "In Progress" ? System.Drawing.Color.Blue : System.Drawing.Color.Gray %>'></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <AlternatingRowStyle CssClass="listA" />
            </asp:GridView>
        </td>
        <td width="33%" valign="top">
            <asp:GridView runat="server" ID="gvWarningValidationRule" AutoGenerateColumns="False" DataKeyNames="Id" meta:resourcekey="gvWarningValidationRuleResource" OnSelectedIndexChanged="gvWarningValidationRule_SelectedIndexChanged" CellPadding="4" CssClass="list" GridLines="Horizontal" >
                <HeaderStyle CssClass="listheader" />                
                <Columns>                    
                    <asp:TemplateField HeaderText="Validation Rule(Warning)" meta:resourcekey="TemplateFieldResource3">
                        <ItemTemplate>
                            <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td valign=top>
                                    <input type="checkbox" class="radio" name="cbSelect" value='<%# DataBinder.Eval(Container.DataItem, "Id")%>' />                            
                                </td>
                                <td valign=top>
                                    <%# DataBinder.Eval(Container.DataItem, "TheDataSourceRule.Name")%>
                                </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnErrorRuleStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ValidationStatus") == null ? DataBinder.Eval(Container.DataItem, "Status") : DataBinder.Eval(Container.DataItem, "ValidationStatus") == "In Progress" ? "In Progress" : "Waiting"%>' CommandName="Select" meta:resourcekey="lbtnErrorRuleStatusResource" ForeColor='<%# DataBinder.Eval(Container.DataItem, "ValidationStatus") == null ? (DataBinder.Eval(Container.DataItem, "Status") != null ? (DataBinder.Eval(Container.DataItem, "Status").Equals("Passed") ? System.Drawing.Color.Green : System.Drawing.Color.Red) : System.Drawing.Color.Black) : DataBinder.Eval(Container.DataItem, "ValidationStatus") == "In Progress" ? System.Drawing.Color.Blue : System.Drawing.Color.Gray %>'></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <AlternatingRowStyle CssClass="listA" />
            </asp:GridView>
        </td>
    </tr>  
</table>

<!-- Modified By Vincent On 2006-9-1 End -->
<script language="javascript" type="text/javascript">
    function validateAll()
    {
        var validationIds = null;
        var obj = document.getElementsByName("cbSelect");
        //fetch all checkbox in the page
        for(i=0; i<obj.length; i++)
        {
           if (validationIds != null)
           {
                validationIds += "," + obj[i].value;
           }
           else
           {
                validationIds = obj[i].value;
           }        
        }
        if (validationIds != null) 
        {
            openValidateDialog(validationIds);
        }
    }
    
    function validateSelected()
    {
        var validationIds = null;
        var obj = document.getElementsByName("cbSelect");
        //fetch all checkbox in the page
        for(i=0; i<obj.length; i++)
        {
            if (obj[i].checked) {
               if (validationIds != null)
               {
                    validationIds += "," + obj[i].value;
               }
               else
               {
                    validationIds = obj[i].value;
               }        
            }
        }
        
        if (validationIds != null) 
        {
            openValidateDialog(validationIds);
        }
    }
    
    function openValidateDialog(validationIds)
    {
	    var cusfn = function()
	    {
		    J.dialog.inwin['ValidationRule'].J('#xbtn').click( function(){ 
		        document.getElementById("<%= btnInValidation.ClientID %>").click(); 
		        J.dialog.inwin['ValidationRule'].cancel();
		    } );
		    
	    };
	    
	    var fn = function()
	    {
	        document.getElementById("<%= btnInValidation.ClientID %>").click(); 
		    J.dialog.inwin['ValidationRule'].cancel();
	    };
	
        var dsUploadId = document.getElementById("<%= txtId.ClientID %>").value;
	    J.dialog.get( "ValidationRule", {  top: 90, cover: true, custom: fn, closeWin: cusfn, title : "Data Validation", page: "ValidationRule.aspx?dsUploadId=" + dsUploadId + "&validationIds=" + validationIds } );      
    }
<%
    if (TheDataSourceUpload.IsInValidation)
    {
%>     

    function doSubmit()
    {                
        document.getElementById("<%= btnInValidation.ClientID %>").click();
    }  
    document.body.onload = doSubmit;               
<%        
    }
%>
</script> 