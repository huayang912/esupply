<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Edit.ascx.cs" Inherits="Modules_Cube_CubeProcess_Edit" %>
<script type="text/javascript" src="Popup/lhgcore.min.js"></script>
<script type="text/javascript" src="Popup/lhgdialog.js"></script>
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
    end select
end function
</script>
<h2>Cube Process</h2>
<b>Basic Information:</b>
<p class="formBtnBoard">
    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="btn"></asp:Button>
    <%
        if (TheCubeProcess.CubeProcessValidationResultList != null
            && TheCubeProcess.CubeProcessValidationResultList.Count > 0) 
        {
            if (TheCubeProcess.Status.Trim().Equals(Dndp.Persistence.Entity.Cube.CubeProcess.PROCESS_STATUS_WaitingValidate)
                || TheCubeProcess.Status.Trim().Equals(Dndp.Persistence.Entity.Cube.CubeProcess.PROCESS_STATUS_ValidatedFailed)
                || TheCubeProcess.Status.Trim().Equals(Dndp.Persistence.Entity.Cube.CubeProcess.PROCESS_STATUS_ValidatedPassed)
                || TheCubeProcess.Status.Trim().Equals(Dndp.Persistence.Entity.Cube.CubeProcess.PROCESS_STATUS_ProcessCancelled)
                || TheCubeProcess.Status.Trim().Equals(Dndp.Persistence.Entity.Cube.CubeProcess.PROCESS_STATUS_ProcessFailed))
            
            {
                if (Editable)
                {
        
     %>
    <input type="button" name="btnValidateAll" value="Validate All" onclick="validateAll();" class="btn" size=""/>           
    <input type="button" name="btnValidateSelected" value="Validate Selected" onclick="validateSelected();" class="btn" size=""/> 
    <%
                }
            }
        }
    %>
    <asp:Button ID="btnProcess" runat="server" Text="Process" OnClick="btnProcess_Click" CssClass="btn"></asp:Button>
    <asp:Button ID="btnReProcess" runat="server" Text="Re-Process" OnClick="btnReProcess_Click" CssClass="btn"></asp:Button>
    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn"></asp:Button>
    <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" CssClass="btn" OnClientClick="return ButtonWarning('Delete')" ></asp:Button>
    <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" CssClass="btn" />
    <span style="display:none">
        <asp:Button ID="btnValidationFinish" runat="server" OnClick="btnValidationFinish_Click" Text="ValidationFinish"/>&nbsp;        
    </span>
</p>
<div class="bgForm">
    <table border="0">
        <tr>
            <td colspan="2" align="left">
                <asp:Label ID="lblMessage" runat="server" Visible="false" CssClass="error"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="txtForm">Cube Description:</td>
            <td><asp:Label ID="lblCubeDescription" runat="server"/></td>
            <td class="txtForm">Cube Name:</td>
            <td><asp:Label ID="lblCubeName" runat="server"/></td>
            <asp:HiddenField ID="txtId" runat="server"/>
            <td class="txtForm">Process Server Address:</td>
            <td><asp:Label ID="lblProcessServerAddr" runat="server"/></td>
        </tr>        
        <tr>
            <td class="txtForm">Process Database Name:</td>
            <td><asp:Label ID="lblProcessDatabaseName" runat="server"/></td>       
            <td class="txtForm">Process Start Date:</td>
            <td><asp:Label ID="lblProcessStartDate" runat="server"/></td>
            <td class="txtForm">Process End Date:</td>
            <td><asp:Label ID="lblProcessEndDate" runat="server"/></td>
        </tr>
        <tr>
            <td class="txtForm">Status:</td>
            <td><asp:Label ID="lblProcessStatus" runat="server"/></td>
            <td class="txtForm">Create User:</td>
            <td><asp:Label ID="lblProcessCreateUser" runat="server"/></td>
            <td class="txtForm">Create Date:</td>
            <td><asp:Label ID="lblProcessCreateDate" runat="server"/></td>
        </tr>
        <tr>
            <td valign="top" class="txtForm">Cube Process Description:</td>
            <td colspan="5">
                &nbsp;<asp:TextBox ID="txtProcessDescription" runat="server"  Columns="100" Rows="5" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
    </table>
</div>
<hr/>
<b>Data Validation List for The Data Source</b>

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
									<%# DataBinder.Eval(Container.DataItem, "TheRule.Name")%>
                                </td>    
                            </tr>
                        </table>                             
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <font color='<%# "Passed".Equals(DataBinder.Eval(Container.DataItem, "Status")) ? "Green" : "Red" %>'>
                                <%# DataBinder.Eval(Container.DataItem, "Status") %>
                            </font>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnErrorRuleDownload" runat="server" Text="Download" CommandName="Select" meta:resourcekey="lbtnErrorRuleStatusResource" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' 
                                Visible='<%# DataBinder.Eval(Container.DataItem, "Status") != null && DataBinder.Eval(Container.DataItem, "Status").Equals("Failed")%>' OnClick="gvValidationRule_Click"/>&nbsp;                            
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Wrap="False" />
                    </asp:TemplateField>
                </Columns>
                <AlternatingRowStyle CssClass="listA" />
            </asp:GridView>            
        </td>
        <td width="33%" valign="top" >
            <asp:GridView runat="server" ID="gvProblemValidationRule" AutoGenerateColumns="False" DataKeyNames="Id" meta:resourcekey="gvProblemValidationRuleResource" CellPadding="4" CssClass="list" GridLines="Horizontal">
                <HeaderStyle CssClass="listheader"/>                
                <Columns>                    
                    <asp:TemplateField HeaderText="Validation Rule(Problem)" meta:resourcekey="TemplateFieldResource1" >
                        <ItemTemplate>
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td valign="top">
                                    <input type="checkbox" class="radio" name="cbSelect" value='<%# DataBinder.Eval(Container.DataItem, "Id")%>' />                           
                                </td>
                                <td valign="top">
                                    <%# DataBinder.Eval(Container.DataItem, "TheRule.Name")%>
                                </td>    
                            </tr>
                        </table>  
                            
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <font color='<%# "Passed".Equals(DataBinder.Eval(Container.DataItem, "Status")) ? "Green" : "Red" %>'>
                                <%# DataBinder.Eval(Container.DataItem, "Status") %>
                            </font>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnProblemRuleDownload" runat="server" Text="Download" CommandName="Select" meta:resourcekey="lbtnProblemRuleStatusResource" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' 
                                Visible='<%# DataBinder.Eval(Container.DataItem, "Status") != null && DataBinder.Eval(Container.DataItem, "Status").Equals("Failed")%>' OnClick="gvValidationRule_Click"/>&nbsp;                           
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Wrap="False" />
                    </asp:TemplateField>
                </Columns>
                <AlternatingRowStyle CssClass="listA" />
            </asp:GridView>
        </td>
        <td width="33%" valign="top" >
            <asp:GridView runat="server" ID="gvWarningValidationRule" AutoGenerateColumns="False" DataKeyNames="Id" meta:resourcekey="gvWarningValidationRuleResource" CellPadding="4" CssClass="list" GridLines="Horizontal">
                <HeaderStyle CssClass="listheader" />                
                <Columns>                    
                    <asp:TemplateField HeaderText="Validation Rule(Warning)" meta:resourcekey="TemplateFieldResource1" >
                        <ItemTemplate>  
                            <input type="checkbox" class="radio" name="cbSelect" value='<%# DataBinder.Eval(Container.DataItem, "Id")%>' />                            
                            <%# DataBinder.Eval(Container.DataItem, "TheRule.Name")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <font color='<%# "Passed".Equals(DataBinder.Eval(Container.DataItem, "Status")) ? "Green" : "Red" %>'>
                                <%# DataBinder.Eval(Container.DataItem, "Status") %>
                            </font>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnWarningRuleDownload" runat="server" Text="Download" CommandName="Select" meta:resourcekey="lbtnWarningRuleStatusResource" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' 
                                Visible='<%# DataBinder.Eval(Container.DataItem, "Status") != null && DataBinder.Eval(Container.DataItem, "Status").Equals("Failed")%>' OnClick="gvValidationRule_Click"/>&nbsp;                            
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Wrap="False" />
                    </asp:TemplateField>
                </Columns>
                <AlternatingRowStyle CssClass="listA" />
            </asp:GridView>
        </td>
    </tr>
</table>
<hr/>
<b>Process Parameters:</b>

<asp:GridView runat="server" ID="gvParameterList" AutoGenerateColumns="False" PageSize="20" DataKeyNames="TheParameter" CellPadding="4" CssClass="list" GridLines="Horizontal">
    <HeaderStyle CssClass="listheader" HorizontalAlign="Left" />
    <Columns>
        <asp:TemplateField HeaderText="#">
            <ItemTemplate>
                <%# DataBinder.Eval(Container.DataItem, "TheParameter.Id")%>
            </ItemTemplate>
        </asp:TemplateField>      
        
        <asp:TemplateField HeaderText="Parameter Name">
            <ItemTemplate>
                <%# DataBinder.Eval(Container.DataItem, "TheParameter.Name")%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="Value" HeaderText="Parameter Value" /> 
    </Columns>
    <AlternatingRowStyle CssClass="listA" />
</asp:GridView>

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
            //document.getElementById("<%= btnValidationFinish.ClientID %>").click();   
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
		        document.getElementById("<%= btnValidationFinish.ClientID %>").click(); 
		        J.dialog.inwin['ValidationRule'].cancel();
		    } );
		    
	    };
	    
	    var fn = function()
	    {
	        document.getElementById("<%= btnValidationFinish.ClientID %>").click(); 
		    J.dialog.inwin['ValidationRule'].cancel();
	    };
	    	    
        var processId = document.getElementById("<%= txtId.ClientID %>").value;
	    J.dialog.get( "ValidationRule", {  top: 90, cover: true, custom: fn, closeWin: cusfn, title : "Data Validation", page: "ProcessValidationRule.aspx?processId=" + processId + "&validationIds=" + validationIds } );   
    }
</script>