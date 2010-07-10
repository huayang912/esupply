<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Validate.ascx.cs" Inherits="Modules_Dui_DSUpload_Validate" %>
<script language="vbscript" type="text/vbscript">
function ButtonWarning(Action)
    Dim ReturnVal
    Select Case Action
        case "Delete"
            ReturnVal = msgbox("Are you sure you want to delete the record?",17)
            If ReturnVal = 1 then
                ButtonWarning = true
            else
                ButtonWarning = false
            end if 
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
<h2>Data Preparation - Data Validation</h2>
<p class="formBtnBoard">
    <asp:Button ID="btnUpdate" Text="Update Subject" runat="server" OnClick="btnUpdate_Click" CssClass="btn"/>
    <%if (TheDataSourceUpload.ProcessStatus.Equals("") || TheDataSourceUpload.ProcessStatus.Equals("OWNER_CONFIRMED")) {%>
        <input type="button" name="btnValidateAll" value="Validate All" onclick="validateAll();" class="btn" size=""/>           
        <input type="button" name="btnValidateSelected" value="Validate Selected" onclick="validateSelected();" class="btn" size=""/> 
    <%}%>
    <asp:Button ID="btnConfirm" Text="Confirm" runat="server" OnClick="btnConfirm_Click" OnClientClick="return ButtonWarning('Confirm')" CssClass="btn"/>
    <asp:Button ID="btnUnConfirm" Text="UnConfirm" runat="server" OnClick="btnUnConfirm_Click" OnClientClick="return ButtonWarning('UnConfirm')" CssClass="btn"/>
    <asp:Button ID="btnDelete" Text="Delete" runat="server" OnClick="btnDelete_Click" OnClientClick="return ButtonWarning('Delete')" CssClass="btn"/>
    <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" Text="Back" CssClass="btn" />
    <asp:Button ID="btnDownload" runat="server" OnClick="btnDownload_Click" Text="Download Data" CssClass="btn" />
    <span style="display:none">
        <asp:Button ID="btnInValidation" runat="server" OnClick="btnInValidation_Click" Text="Continue"/>&nbsp;  
        <asp:HiddenField ID="txtValidationIds" runat="server" />
    </span>
</p>
<table border="0" class="list">
	<tr>
        <td class="TXTform">Data Source Name:</td>
        <td>
            <asp:Label ID="txtName" runat="server"></asp:Label>
            <asp:HiddenField ID="txtId" runat="server"/>
        </td>
        <td class="TXTform">Category:</td>
        <td>
            <asp:Label ID="txtCategory" runat="server"></asp:Label>
        </td>
        <td class="TXTform">Subject:</td>
        <td>
            <asp:TextBox ID="txtSubject" runat="server" Width="200"/>
        </td>
    </tr>
    <tr>
        <td class="TXTform">File Name:</td>
        <td>
            <asp:Label ID="txtFileNm" runat="server"></asp:Label>
        </td>
        <td class="TXTform">Size:</td>
        <td>
            <asp:Label ID="txtSize" runat="server"></asp:Label> bytes
        </td>
        <td class="TXTform">Uploaded Record Rows:</td>
        <td>
            <asp:Label ID="txtRecordRows" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="TXTform">Status:</td>
        <td>
            <asp:Label ID="txtStatus" runat="server"></asp:Label>
        </td>
        <td class="TXTform">Created By:</td>
        <td>
            <asp:Label ID="txtUploadBy" runat="server"></asp:Label>
        </td>
        <td  class="TXTform">Created Date:</td>
        <td>
            <asp:Label ID="txtUploadDate" runat="server"></asp:Label>
        </td>
    </tr>	
    
</table>
<asp:Label ID="lblMessage" Font-Bold="true" ForeColor="blue" Text="Now rule validation is in progress, maybe it will take a few minutes, please wait..." runat="server" Visible="false" cssclass="error"></asp:Label>
<hr/><b>Data Validation List for The Data Source</b>
<!-- Modified By Vincent On 2006-9-1 Begin -->
<!-- Modify CheckBox Style -->

<table border="0" width="100%">
    <tr>
        <td width="33%" valign="top" >
            <asp:GridView runat="server" ID="gvErrorValidationRule" AutoGenerateColumns="False" DataKeyNames="Id" meta:resourcekey="gvErrorValidationRuleResource" CellPadding="4" CssClass="list" GridLines="Horizontal">
                <HeaderStyle CssClass="listheader"  />                
                <Columns>                    
                    <asp:TemplateField HeaderText="Validation Rule(Error)" meta:resourcekey="TemplateFieldResource1" >
                        <ItemTemplate> 
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td valign="top">
									<input type="checkbox" class="radio" name="cbSelect" value='<%# DataBinder.Eval(Container.DataItem, "Id")%>' />                            
                            
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
                            <font color='<%# DataBinder.Eval(Container.DataItem, "ValidationStatus") == null ? (DataBinder.Eval(Container.DataItem, "Status") != null ? (DataBinder.Eval(Container.DataItem, "Status").Equals("Passed") ? "Green" : "Red") : "Black") : (DataBinder.Eval(Container.DataItem, "ValidationStatus") == "In Progress" ? "Blue" : "Gray")%>'>
                                <%# DataBinder.Eval(Container.DataItem, "ValidationStatus") == null ? DataBinder.Eval(Container.DataItem, "Status") : DataBinder.Eval(Container.DataItem, "ValidationStatus") == "In Progress" ? "In Progress" : "Waiting"%>
                            </font>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnErrorRuleDownload" runat="server" Text="Download" CommandName="Select" meta:resourcekey="lbtnErrorRuleStatusResource" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' 
                                Visible='<%# DataBinder.Eval(Container.DataItem, "Status") != null && DataBinder.Eval(Container.DataItem, "Status").Equals("Failed")%>' OnClick="gvValidationRule_Click"/>&nbsp;
                            <asp:LinkButton ID="lbtnErrorRuleUpdate" runat="server" Text="Update" CommandName="Select" meta:resourcekey="lbtnErrorRuleStatusResource" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' 
                                Visible='<%# DataBinder.Eval(Container.DataItem, "Status") != null && DataBinder.Eval(Container.DataItem, "Status").Equals("Failed") && DataBinder.Eval(Container.DataItem, "TheDataSourceRule.UpdateContent") != null && DataBinder.Eval(Container.DataItem, "TheDataSourceRule.UpdateContent").ToString().Trim().Length > 0%>' OnClick="gvValidationRuleUpdate_Click"/>&nbsp;
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Wrap="False" />
                    </asp:TemplateField>
                </Columns>
                <AlternatingRowStyle CssClass="listA" />
            </asp:GridView>            
        </td>
    <!-- Modified By Vincent On 2006-9-1 Begin Rows:120-->
    <!--<ItemStyle HorizontalAlign="Center" />-->
    <!-- Modified By Vincent On 2006-9-1 End --> 
        <td width="33%" valign="top" >
            <asp:GridView runat="server" ID="gvProblemValidationRule" AutoGenerateColumns="False" DataKeyNames="Id" meta:resourcekey="gvProblemValidationRuleResource" CellPadding="4" CssClass="list" GridLines="Horizontal">
                <HeaderStyle CssClass="listheader"/>                
                <Columns>                    
                    <asp:TemplateField HeaderText="Validation Rule(Problem)" meta:resourcekey="TemplateFieldResource1" >
                        <ItemTemplate>
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td valign=top>
                                    <input type="checkbox" class="radio" name="cbSelect" value='<%# DataBinder.Eval(Container.DataItem, "Id")%>' />                           
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
                            <font color='<%# DataBinder.Eval(Container.DataItem, "ValidationStatus") == null ? (DataBinder.Eval(Container.DataItem, "Status") != null ? (DataBinder.Eval(Container.DataItem, "Status").Equals("Passed") ? "Green" : "Red") : "Black") : (DataBinder.Eval(Container.DataItem, "ValidationStatus") == "In Progress" ? "Blue" : "Gray")%>'>
                                <%# DataBinder.Eval(Container.DataItem, "ValidationStatus") == null ? DataBinder.Eval(Container.DataItem, "Status") : DataBinder.Eval(Container.DataItem, "ValidationStatus") == "In Progress" ? "In Progress" : "Waiting"%>
                            </font>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnProblemRuleDownload" runat="server" Text="Download" CommandName="Select" meta:resourcekey="lbtnProblemRuleStatusResource" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' 
                                Visible='<%# DataBinder.Eval(Container.DataItem, "Status") != null && DataBinder.Eval(Container.DataItem, "Status").Equals("Failed")%>' OnClick="gvValidationRule_Click"/>&nbsp;
                            <asp:LinkButton ID="lbtnProblemRuleUpdate" runat="server" Text="Update" CommandName="Select" meta:resourcekey="lbtnProblemRuleStatusResource" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' 
                                Visible='<%# DataBinder.Eval(Container.DataItem, "Status") != null && DataBinder.Eval(Container.DataItem, "Status").Equals("Failed") && DataBinder.Eval(Container.DataItem, "TheDataSourceRule.UpdateContent") != null && DataBinder.Eval(Container.DataItem, "TheDataSourceRule.UpdateContent").ToString().Trim().Length > 0%>' OnClick="gvValidationRuleUpdate_Click"/>&nbsp;
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Wrap="False" />
                    </asp:TemplateField>
                </Columns>
                <AlternatingRowStyle CssClass="listA" />
            </asp:GridView>
        </td>
    <!-- Modified By Vincent On 2006-9-1 Begin Rows:153-->
    <!--<ItemStyle HorizontalAlign="Center" />-->
    <!-- Modified By Vincent On 2006-9-1 End --> 
        <td width="33%" valign="top" >
            <asp:GridView runat="server" ID="gvWarningValidationRule" AutoGenerateColumns="False" DataKeyNames="Id" meta:resourcekey="gvWarningValidationRuleResource" CellPadding="4" CssClass="list" GridLines="Horizontal">
                <HeaderStyle CssClass="listheader" />                
                <Columns>                    
                    <asp:TemplateField HeaderText="Validation Rule(Warning)" meta:resourcekey="TemplateFieldResource1" >
                        <ItemTemplate>  
                            <input type="checkbox" class=radio name="cbSelect" value='<%# DataBinder.Eval(Container.DataItem, "Id")%>' />                            
                            <%# DataBinder.Eval(Container.DataItem, "TheDataSourceRule.Name")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <font color='<%# DataBinder.Eval(Container.DataItem, "ValidationStatus") == null ? (DataBinder.Eval(Container.DataItem, "Status") != null ? (DataBinder.Eval(Container.DataItem, "Status").Equals("Passed") ? "Green" : "Red") : "Black") : (DataBinder.Eval(Container.DataItem, "ValidationStatus") == "In Progress" ? "Blue" : "Gray")%>'>
                                <%# DataBinder.Eval(Container.DataItem, "ValidationStatus") == null ? DataBinder.Eval(Container.DataItem, "Status") : DataBinder.Eval(Container.DataItem, "ValidationStatus") == "In Progress" ? "In Progress" : "Waiting"%>
                            </font>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnWarningRuleDownload" runat="server" Text="Download" CommandName="Select" meta:resourcekey="lbtnWarningRuleStatusResource" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' 
                                Visible='<%# DataBinder.Eval(Container.DataItem, "Status") != null && DataBinder.Eval(Container.DataItem, "Status").Equals("Failed")%>' OnClick="gvValidationRule_Click"/>&nbsp;
                            <asp:LinkButton ID="lbtnWarningRuleUpdate" runat="server" Text="Update" CommandName="Select" meta:resourcekey="lbtnWarningRuleStatusResource" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' 
                                Visible='<%# DataBinder.Eval(Container.DataItem, "Status") != null && DataBinder.Eval(Container.DataItem, "Status").Equals("Failed") && DataBinder.Eval(Container.DataItem, "TheDataSourceRule.UpdateContent") != null && DataBinder.Eval(Container.DataItem, "TheDataSourceRule.UpdateContent").ToString().Trim().Length > 0%>' OnClick="gvValidationRuleUpdate_Click"/>&nbsp;
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Wrap="False" />
                    </asp:TemplateField>
                </Columns>
                <AlternatingRowStyle CssClass="listA" />
            </asp:GridView>
        </td>
    </tr>  
        <!-- Modified By Vincent On 2006-9-1 Begin Rows:186-->
    <!--<ItemStyle HorizontalAlign="Center" />-->
    <!-- Modified By Vincent On 2006-9-1 End --> 
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
        var dsUploadId = document.getElementById("<%= txtId.ClientID %>").value;
        
        windowprops	= "height=300px,width=400px,location=no,scrollbars=yes,status=no,menubars=no,toolbars=no";
	    window.showModalDialog(
	        "Popup/ValidationRule.aspx?dsUploadId=" + dsUploadId + "&validationIds=" + validationIds
	        , null
	        , "dialogWidth:400px;dialogHeight:300px;status:no;help:no;scroll:yes");
	    
	    document.getElementById("<%= btnInValidation.ClientID %>").click();      
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