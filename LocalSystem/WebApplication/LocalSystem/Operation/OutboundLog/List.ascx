<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="MasterData_OutboundLog_List" %>
<%@ Register Assembly="com.LocalSystem.Control" Namespace="com.LocalSystem.Control" TagPrefix="cc1" %>
<fieldset>
    <div class="GridView">
        <cc1:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
            ShowSeqNo="true" AllowSorting="true" OnRowDataBound="GV_List_RowDataBound">
            <Columns>
                <asp:BoundField DataField="PoCode" HeaderText=" ${MasterData.Po}" SortExpression="PoCode" />
                <asp:BoundField DataField="FileName" HeaderText="${MasterData.OutBound.FileName}" SortExpression="FileName" />
                <asp:BoundField DataField="CreateDate" HeaderText="${MasterData.Common.CreateDate}" SortExpression="CreateDate" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
                <asp:BoundField DataField="CreateUser" HeaderText="${MasterData.Common.CreateUser}" SortExpression="CreateUser" />
                <asp:BoundField DataField="Memo" HeaderText="${MasterData.OutBound.Memo}" SortExpression="Memo" />
                <asp:BoundField DataField="OutBoundResult" HeaderText="${MasterData.OutBound.OutBoundResult}" SortExpression="OutBoundResult" />
            </Columns>
        </cc1:GridView>
        <cc1:GridPager ID="gp" runat="server" GridViewID="GV_List" PageSize="10">
        </cc1:GridPager>
    </div>
</fieldset>
