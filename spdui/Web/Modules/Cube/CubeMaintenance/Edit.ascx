<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Edit.ascx.cs" Inherits="Modules_Cube_Cube_Edit" %>
<%@ Register Src="NewRule.ascx" TagName="NewRule" TagPrefix="uc1" %>
<%@ Register Src="NewOperator.ascx" TagName="NewOperator" TagPrefix="uc2" %>
<%@ Register Src="NewDimension.ascx" TagName="NewDimension" TagPrefix="uc3" %>
<%@ Register Src="NewMDX.ascx" TagName="NewMDX" TagPrefix="uc4" %>
<%@ Register Src="NewMeasure.ascx" TagName="NewMeasure" TagPrefix="uc5" %>
<%@ Import Namespace="Dndp.Persistence.Entity.Cube" %>
<asp:Panel ID="pnlMain" runat="server">
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
<h2>Cube Maintenance:</h2>
    <p class="formBtnBoard">
        <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" CssClass="btn" />
        <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click"  CssClass="btn"  OnClientClick="return ButtonWarning('Delete')"/>
        <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" CssClass="btn" />
    </p>
<div class="BGform">
    <table border="0">
        <tr>
            <td>
                <asp:Label ID="lblMessage" runat="server" Visible="false" CssClass="error"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="txtForm">Description:</td>
            <td colspan="5"><asp:TextBox ID="txtDescription" runat="server" Width="400"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="txtForm">Process Cube Name:</td>
            <td><asp:TextBox ID="txtProcessCubeName" runat="server"></asp:TextBox></td>           
             <td class="txtForm">Process Server Address:</td>
            <td><asp:TextBox ID="txtProcessServerAddr" runat="server" Width="400"></asp:TextBox></td>
       </tr>
        <tr>
            <td class="txtForm">Process Database Name:</td>
            <td><asp:TextBox ID="txtProcessDatabaseName" runat="server"></asp:TextBox></td>          
            <td class="txtForm">Process Cube Backup Folder:</td>
            <td colspan="3"><asp:TextBox ID="txtProcessBackupFolder" runat="server" Width="400"></asp:TextBox></td>
	    </tr>
        <tr>
	        <td class="txtForm">Release Cube Name:</td>
            <td><asp:TextBox ID="txtReleaseCubeName" runat="server"></asp:TextBox></td>      
            <td class="txtForm">Release Server Address:</td>
            <td><asp:TextBox ID="txtReleaseServerAddr" runat="server" Width="400"></asp:TextBox></td>
        </tr>
        <tr>            
            <td class="txtForm">Release Database Name:</td>
            <td><asp:TextBox ID="txtReleaseDatabaseName" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td valign="top" class="txtForm">Parameters:</td>
            <td>
                &nbsp;<asp:DropDownList ID="ddlParameter" runat="server" Style="width:120;border-color:Red;"></asp:DropDownList>
            </td>
            <td><asp:Button ID="btnInsertParameter" runat="server" Text="Insert" CssClass="btn" OnClick="btnInsertParameter_Click"/></td>
            <td></td>
        </tr>
	    <tr>
            <td valign="top" class="txtForm">SQL Before Process Run:</td>
            <td colspan="3">
                &nbsp;<asp:TextBox ID="txtPreProcessSQL" runat="server"  Columns="100" Rows="15" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
	    <tr>
	        <td valign="top" class="txtForm">SQL After Process Run:</td>
            <td colspan="3">
                &nbsp;<asp:TextBox ID="txtPostProcessSQL" runat="server"  Columns="100" Rows="15" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>   
        <tr>
            <td valign="top" class="txtForm">SQL Before Release Run:</td>
            <td colspan="3">
                &nbsp;<asp:TextBox ID="txtPreReleaseSQL" runat="server"  Columns="100" Rows="15" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
	    <tr>
	        <td valign="top" class="txtForm">SQL After Release Run:</td>
            <td colspan="3">
                &nbsp;<asp:TextBox ID="txtPostReleaseSQL" runat="server"  Columns="100" Rows="15" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td valign="top" class="txtForm">SQL Before UpdateRole Run:</td>
            <td colspan="3">
                &nbsp;<asp:TextBox ID="txtPreUpdateRoleSQL" runat="server"  Columns="100" Rows="15" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
	    <tr>
	        <td valign="top" class="txtForm">SQL After UpdateRole Run:</td>
            <td colspan="3">
                &nbsp;<asp:TextBox ID="txtPostUpdateRoleSQL" runat="server"  Columns="100" Rows="15" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td valign="top" class="txtForm">SQL Before UpdateDescription Run:</td>
            <td colspan="3">
                &nbsp;<asp:TextBox ID="txtPreUpdateDescriptionSQL" runat="server"  Columns="100" Rows="15" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
	    <tr>
	        <td valign="top" class="txtForm">SQL After UpdateDescription Run:</td>
            <td colspan="3">
                &nbsp;<asp:TextBox ID="txtPostUpdateDescriptionSQL" runat="server"  Columns="100" Rows="15" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>          
    </table>
</div>

<b>Operators List:</b>
<p class="formBtnBoard"><asp:Button ID="btnAddOperator" runat="server" Text="Add Operator" OnClick="btnAddOperator_Click" Width="100px" CssClass="btn"/>
<asp:Button ID="btnDeleteOperator" runat="server" Text="Delete" OnClick="btnDeleteOperator_Click" cssclass="btn" OnClientClick="return ButtonWarning('Delete')"/>
</p>

<asp:GridView runat="server" ID="gvOperatorList" AutoGenerateColumns="False" PageSize="20" DataKeyNames="Id" CellPadding="4" CssClass="list" GridLines="Horizontal" >
         <HeaderStyle CssClass="listheader" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="cbSelect" runat="server" CssClass="radio" />
                        </ItemTemplate>
                        <HeaderStyle Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Operator Name">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "TheUser.UserName") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Email">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "TheUser.Email") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="AllowType" HeaderText="Allow Type" />
                </Columns>
    <AlternatingRowStyle CssClass="listA" />
</asp:GridView>
            
<b>Validation Rules:</b>
<p class="formBtnBoard"><asp:Button ID="btnAddRule" runat="server" Text="Add Rule" Width="100px" OnClick="btnAddRule_Click" CssClass="btn" />

<asp:Button ID="btnDeleteRule" runat="server" Text="Delete" OnClick="btnDeleteRule_Click" CssClass="btn"  OnClientClick="return ButtonWarning('Delete')"/>
</p>
<asp:GridView runat="server" ID="gvRuleList" AutoGenerateColumns="False" PageSize="20" DataKeyNames="Id" CellPadding="4" CssClass="list" GridLines="Horizontal" OnRowDataBound="gvRule_OnRowDataBound">
         <HeaderStyle CssClass="listheader" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="cbSelect" runat="server" CssClass="radio" />
                        </ItemTemplate>
                        <HeaderStyle Width="3%" />
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="#" />
                    <asp:TemplateField HeaderText="Name">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnRuleName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Name") %>' CommandName="Select" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' OnClick="lbtnRuleName_Click"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Description" HeaderText="Description" />
                    <asp:BoundField DataField="ValidationTarget" HeaderText="Validation Target" />
                    <asp:BoundField DataField="Type" HeaderText="Type" />
                    <asp:BoundField DataField="CreateDate" DataFormatString="{0:d}" HeaderText="Create Date" />
                    <asp:TemplateField HeaderText="Create By">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "CreateUser.UserName") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="UpdateDate" DataFormatString="{0:d}" HeaderText="Last Update Date" />
                    <asp:TemplateField HeaderText="Last Update By">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "UpdateUser.UserName") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
    <AlternatingRowStyle CssClass="listA" />
            </asp:GridView>

<b>Measure Definitions:</b>
<p class="formBtnBoard">
    <asp:Button ID="btnAddMeasure" runat="server" Text="Add Measure" Width="100px" OnClick="btnAddMeasure_Click" CssClass="btn" />
    <asp:Button ID="btnDeleteMeasure" runat="server" Text="Delete" OnClick="btnDeleteMeasure_Click" CssClass="btn"  OnClientClick="return ButtonWarning('Delete')"/>
</p>
<asp:GridView runat="server" ID="gvMeasureList" AutoGenerateColumns="False" PageSize="20" DataKeyNames="Id" CellPadding="4" CssClass="list" GridLines="Horizontal" >
         <HeaderStyle CssClass="listheader" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="cbSelect" runat="server" CssClass="radio" />
                        </ItemTemplate>
                        <HeaderStyle Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Measure Name">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnMeasureName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Name") %>' CommandName="Select" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' OnClick="lbtnMeasureName_Click"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="DisplayName" HeaderText="Measure Display Name" />
                </Columns>
               <AlternatingRowStyle CssClass="listA" />
            </asp:GridView>

<b>Dimension Definitions:</b>
<p class="formBtnBoard"><asp:Button ID="btnAddDimension" runat="server" Text="Add Dimension" Width="100px" OnClick="btnAddDimension_Click" CssClass="btn" />

<asp:Button ID="btnDeleteDimension" runat="server" Text="Delete" OnClick="btnDeleteDimension_Click" CssClass="btn"  OnClientClick="return ButtonWarning('Delete')"/>
</p>
<asp:GridView runat="server" ID="gvDimensionList" AutoGenerateColumns="False" PageSize="20" DataKeyNames="Id" CellPadding="4" CssClass="list" GridLines="Horizontal" >
         <HeaderStyle CssClass="listheader" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="cbSelect" runat="server" CssClass="radio" />
                        </ItemTemplate>
                        <HeaderStyle Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Dimension Name">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnDimensionName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DimensionName") %>' CommandName="Select" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' OnClick="lbtnDimensionName_Click"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="AttributeName" HeaderText="Attribute Name" />
                    <asp:BoundField DataField="SetDimensionName" HeaderText="Set Dimension Name" />
                    <asp:BoundField DataField="SetAttributeName" HeaderText="Set Attribute Name" />
                    <asp:BoundField DataField="RelatedDimensionName" HeaderText="Related Dimension Name" />
                    <asp:BoundField DataField="RelatedAttributeName" HeaderText="Related Attribute Name" />
                </Columns>
               <AlternatingRowStyle CssClass="listA" />
            </asp:GridView>
            
            <b>Warm MDX:</b>
<p class="formBtnBoard"><asp:Button ID="btnAddMDX" runat="server" Text="Add MDX" Width="100px" OnClick="btnAddMDX_Click" CssClass="btn" />

<asp:Button ID="btnDeleteMDX" runat="server" Text="Delete" OnClick="btnDeleteMDX_Click" CssClass="btn"  OnClientClick="return ButtonWarning('Delete')"/>
</p>
<asp:GridView runat="server" ID="gvMDXList" AutoGenerateColumns="False" PageSize="20" DataKeyNames="Id" CellPadding="4" CssClass="list" GridLines="Horizontal" >
         <HeaderStyle CssClass="listheader" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="cbSelect" runat="server" CssClass="radio" />
                        </ItemTemplate>
                        <HeaderStyle Width="3%" />
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="#" />
                    <asp:BoundField DataField="SequenceNo" HeaderText="Sequence No" />
                    <asp:TemplateField HeaderText="Description">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnDescription" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Description") %>' CommandName="Select" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' OnClick="lbtnMDX_Click"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>                 
                </Columns>
    <AlternatingRowStyle CssClass="listA" />
            </asp:GridView>         
</asp:Panel>         
<!-- Modified By Vincent On 2006-9-2 End -->
<uc1:NewRule id="NewRule1" runat="server" Visible="False"/>
<uc2:NewOperator id="NewOperator1" runat="server" Visible="False"/>
<uc3:NewDimension id="NewDimension1" runat="server" Visible="False"/>
<uc4:NewMDX id="NewMDX1" runat="server" Visible="False"/>
<uc5:NewMeasure id="NewMeasure1" runat="server" Visible="False"/>