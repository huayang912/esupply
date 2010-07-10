<%@ Control Language="C#" AutoEventWireup="true" CodeFile="New.ascx.cs" Inherits="Modules_Cube_Cube_New" %>
<h2>Cube Maintenance:</h2>
    <p class="formBtnBoard">
        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="btn" />
        <asp:Button ID="btnBack" runat="server" Text="Cancel" OnClick="btnBack_Click" CssClass="btn" />
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