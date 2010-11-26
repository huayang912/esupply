<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DWDSMerge.ascx.cs" Inherits="Modules_Dui_DWDSUpdate_DWDSMerge" %>

<script type="text/javascript" src="Popup/lhgcore.min.js"></script>

<script type="text/javascript" src="Popup/lhgdialog.js"></script>

<script language="vbscript" type="text/vbscript">
function ButtonWarning(Action)
    Dim ReturnVal
    Select Case Action
        case "Merge"
            ReturnVal = msgbox("Are you sure you want to merge the record?",33)
            If ReturnVal = 1 then
                ButtonWarning = true
            else
                ButtonWarning = false
            end if 
    end select
end function
</script>

<script language="javascript" type="text/javascript">
function CopyValidationResult()
{
    document.getElementById("<%= hfValidationResult.ClientID %>").value = document.getElementById("hfvr").value;
}
</script>

<h2 runat="server" id="title">
</h2>
<p class="formBtnBoard">
    <asp:Button ID="btnQuery" Text="Query Data" runat="server" OnClick="btnQuery_Click"
        CssClass="btn" />
    <input type="button" name="btnValidateAll" value="Validate All" onclick="validateAll();"
        class="btn" size="" />
    <input type="button" name="btnValidateSelected" value="Validate Selected" onclick="validateSelected();"
        class="btn" size="" />
    <asp:Button ID="btnMerge" Text="Merge" runat="server" OnClick="btnMerge_Click" OnClientClick="return ButtonWarning('Merge')"
        CssClass="btn" />
    <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" Text="Back" CssClass="btn" />
    <span style="display: none">
        <asp:Button ID="btnInValidation" runat="server" OnClick="btnInValidation_Click" Text="Continue"
            OnClientClick="CopyValidationResult()" />&nbsp;
        <asp:HiddenField ID="txtValidationIds" runat="server" />
    </span>
</p>
<table border="0" class="list">
    <tr>
        <td class="TXTform" width="280">
            To-be merged (Delete after merge) Record ID:</td>
        <td>
            <asp:TextBox ID="txtMergeFromId" runat="server" Width="400px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="TXTform" width="280">
            Merged to (Keep after merge) Record ID:</td>
        <td>
            <asp:TextBox ID="txtMergeToId" runat="server" Width="400px"></asp:TextBox>
        </td>
    </tr>
     <tr>
        <td class="TXTform" width="280">
            Item new name after merge:</td>
        <td>
            <asp:TextBox ID="txtNewName" runat="server" Width="400px"></asp:TextBox>
        </td>
    </tr>
</table>
<table>
    <tr>
        <td>
            <asp:Label ID="lblMessage" Font-Bold="true" runat="server" Visible="false" CssClass="error"></asp:Label>
        </td>
    </tr>
</table>
<hr runat="server" id="hr1" />
<b runat="server" id="title2">Master data for merging: </b>
<table border="0" width="100%">
    <tr>
        <td>
            <asp:HiddenField ID="hfMergedRecordRow" runat="server" Value="0" />
            <input type="hidden" id="hfvr" />
            <asp:HiddenField ID="hfValidationResult" runat="server" />
            <asp:GridView ID="gvDWDSMerge" meta:resourcekey="gvDWDSMerge" runat="server" AutoGenerateColumns="True"
                PageSize="20" CellPadding="4" CssClass="list" GridLines="Horizontal" OnRowDataBound="gvDWDSMerge_RowDataBound">
                <HeaderStyle CssClass="listhead" HorizontalAlign="Left" />
                <AlternatingRowStyle CssClass="listA" />
                <Columns>
                    <asp:TemplateField ShowHeader="true" HeaderText="MergedTo">
                        <ItemTemplate>
                            <input type="radio" name="mergedTo" value="<%# Container.DataItemIndex.ToString() %>"
                                disabled="disabled" <%# Container.DataItemIndex == 1 ? "Checked" : "" %>>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </td>
    </tr>
</table>
<hr runat="server" id="hr2" />
<b runat="server" id="title3">Data Validation List for data merging</b>
<table border="0" width="100%">
    <tr>
        <td width="33%" valign="top">
            <asp:GridView runat="server" ID="gvErrorValidationRule" AutoGenerateColumns="False"
                DataKeyNames="Id" meta:resourcekey="gvErrorValidationRuleResource" CellPadding="4"
                CssClass="list" GridLines="Horizontal">
                <HeaderStyle CssClass="listheader" />
                <Columns>
                    <asp:TemplateField HeaderText="Validation Rule(Error)" meta:resourcekey="TemplateFieldResource1">
                        <ItemTemplate>
                            <table cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td valign="top">
                                        <input type="checkbox" class="radio" name="cbSelect" value='<%# DataBinder.Eval(Container.DataItem, "Id")%>'
                                            <%# DataBinder.Eval(Container.DataItem, "DependenceRule") != null ? "disabled='disabled'" : ""%> />
                                    </td>
                                    <td valign="top">
                                        <asp:Label ID="lbName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Name")%>'
                                            ToolTip='<%# DataBinder.Eval(Container.DataItem, "Description")%>' />
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnErrorRuleStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ValidationStatus") == null ? DataBinder.Eval(Container.DataItem, "Status") : DataBinder.Eval(Container.DataItem, "ValidationStatus") == "In Progress" ? "In Progress" : "Waiting"%>'
                                CommandName="Select" meta:resourcekey="lbtnErrorRuleStatusResource" ForeColor='<%# DataBinder.Eval(Container.DataItem, "ValidationStatus") == null ? (DataBinder.Eval(Container.DataItem, "Status") != null ? (DataBinder.Eval(Container.DataItem, "Status").Equals("Passed") ? System.Drawing.Color.Green : System.Drawing.Color.Red) : System.Drawing.Color.Black) : DataBinder.Eval(Container.DataItem, "ValidationStatus") == "In Progress" ? System.Drawing.Color.Blue : System.Drawing.Color.Gray %>'></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnErrorRuleDownload" runat="server" Text="Download" CommandName="Select"
                                meta:resourcekey="lbtnErrorRuleStatusResource" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>'
                                Visible='<%# DataBinder.Eval(Container.DataItem, "Status") != null && DataBinder.Eval(Container.DataItem, "Status").Equals("Failed")%>'
                                OnClick="gvValidationRule_Click" />&nbsp;
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Wrap="False" />
                    </asp:TemplateField>
                </Columns>
                <AlternatingRowStyle CssClass="listA" />
            </asp:GridView>
        </td>
        <td width="33%" valign="top">
            <asp:GridView runat="server" ID="gvProblemValidationRule" AutoGenerateColumns="False"
                DataKeyNames="Id" meta:resourcekey="gvProblemValidationRuleResource" CellPadding="4"
                CssClass="list" GridLines="Horizontal">
                <HeaderStyle CssClass="listheader" />
                <Columns>
                    <asp:TemplateField HeaderText="Validation Rule(Problem)" meta:resourcekey="TemplateFieldResource2">
                        <ItemTemplate>
                            <table cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td valign="top">
                                        <input type="checkbox" class="radio" name="cbSelect" value='<%# DataBinder.Eval(Container.DataItem, "Id")%>'
                                            <%# DataBinder.Eval(Container.DataItem, "DependenceRule") != null ? "disabled='disabled'" : ""%> />
                                    </td>
                                    <td valign="top">
                                        <asp:Label ID="lbName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Name")%>'
                                            ToolTip='<%# DataBinder.Eval(Container.DataItem, "Description")%>' />
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnErrorRuleStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ValidationStatus") == null ? DataBinder.Eval(Container.DataItem, "Status") : DataBinder.Eval(Container.DataItem, "ValidationStatus") == "In Progress" ? "In Progress" : "Waiting"%>'
                                CommandName="Select" meta:resourcekey="lbtnErrorRuleStatusResource" ForeColor='<%# DataBinder.Eval(Container.DataItem, "ValidationStatus") == null ? (DataBinder.Eval(Container.DataItem, "Status") != null ? (DataBinder.Eval(Container.DataItem, "Status").Equals("Passed") ? System.Drawing.Color.Green : System.Drawing.Color.Red) : System.Drawing.Color.Black) : DataBinder.Eval(Container.DataItem, "ValidationStatus") == "In Progress" ? System.Drawing.Color.Blue : System.Drawing.Color.Gray %>'></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnProblemRuleDownload" runat="server" Text="Download" CommandName="Select"
                                meta:resourcekey="lbtnProblemRuleStatusResource" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>'
                                Visible='<%# DataBinder.Eval(Container.DataItem, "Status") != null && DataBinder.Eval(Container.DataItem, "Status").Equals("Failed")%>'
                                OnClick="gvValidationRule_Click" />&nbsp;
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Wrap="False" />
                    </asp:TemplateField>
                </Columns>
                <AlternatingRowStyle CssClass="listA" />
            </asp:GridView>
        </td>
        <td width="33%" valign="top">
            <asp:GridView runat="server" ID="gvWarningValidationRule" AutoGenerateColumns="False"
                DataKeyNames="Id" meta:resourcekey="gvWarningValidationRuleResource" CellPadding="4"
                CssClass="list" GridLines="Horizontal">
                <HeaderStyle CssClass="listheader" />
                <Columns>
                    <asp:TemplateField HeaderText="Validation Rule(Warning)" meta:resourcekey="TemplateFieldResource3">
                        <ItemTemplate>
                            <table cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td valign="top">
                                        <input type="checkbox" class="radio" name="cbSelect" value='<%# DataBinder.Eval(Container.DataItem, "Id")%>'
                                            <%# DataBinder.Eval(Container.DataItem, "DependenceRule") != null ? "disabled='disabled'" : ""%> />
                                    </td>
                                    <td valign="top">
                                        <asp:Label ID="lbName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Name")%>'
                                            ToolTip='<%# DataBinder.Eval(Container.DataItem, "Description")%>' />
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnErrorRuleStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ValidationStatus") == null ? DataBinder.Eval(Container.DataItem, "Status") : DataBinder.Eval(Container.DataItem, "ValidationStatus") == "In Progress" ? "In Progress" : "Waiting"%>'
                                CommandName="Select" meta:resourcekey="lbtnErrorRuleStatusResource" ForeColor='<%# DataBinder.Eval(Container.DataItem, "ValidationStatus") == null ? (DataBinder.Eval(Container.DataItem, "Status") != null ? (DataBinder.Eval(Container.DataItem, "Status").Equals("Passed") ? System.Drawing.Color.Green : System.Drawing.Color.Red) : System.Drawing.Color.Black) : DataBinder.Eval(Container.DataItem, "ValidationStatus") == "In Progress" ? System.Drawing.Color.Blue : System.Drawing.Color.Gray %>'></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnWarningRuleDownload" runat="server" Text="Download" CommandName="Select"
                                meta:resourcekey="lbtnWarningRuleStatusResource" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>'
                                Visible='<%# DataBinder.Eval(Container.DataItem, "Status") != null && DataBinder.Eval(Container.DataItem, "Status").Equals("Failed") && DataBinder.Eval(Container.DataItem, "ResultContent") != null && DataBinder.Eval(Container.DataItem, "ResultContent") != string.Empty%>'
                                OnClick="gvValidationRule_Click" />&nbsp;
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Wrap="False" />
                    </asp:TemplateField>
                </Columns>
                <AlternatingRowStyle CssClass="listA" />
            </asp:GridView>
        </td>
    </tr>
</table>

<script language="javascript" type="text/javascript">
    function validateAll()
    {
        var validationIds = null;
        var obj = document.getElementsByName("cbSelect");
        //fetch all checkbox in the page
        for(i=0; i<obj.length; i++)
        {
            if (!obj[i].disabled) {
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
	
	   J.dialog.get( "ValidationRule", {  top: 90, cover: true, custom: fn, closeWin: cusfn, title : "Data Validation", page: "DWMergeValidationRule.aspx?mergeFromId=<%= this.MergeFromId %>&mergeToId=<%= this.MergeToId %>&validationIds=" + validationIds} );      
    }
</script>

