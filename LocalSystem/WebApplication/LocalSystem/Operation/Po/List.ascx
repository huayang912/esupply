<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="MasterData_Operation_Po_List" %>
<%@ Register Assembly="com.LocalSystem.Control" Namespace="com.LocalSystem.Control"
    TagPrefix="cc1" %>
<fieldset>
    <div class="GridView">
        <cc1:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" DataKeyNames="Code"
            SkinID="GV" AllowMultiColumnSorting="false" AutoLoadStyle="false" SeqNo="0" SeqText="No."
            ShowSeqNo="true" AllowSorting="True" AllowPaging="True" PagerID="gp" Width="100%"
            CellMaxLength="10" TypeName="com.LocalSystem.Web.CriteriaMgrProxy" SelectMethod="FindAll"
            SelectCountMethod="FindCount" OnRowDataBound="GV_List_RowDataBound" DefaultSortExpression="Code"
            DefaultSortDirection="Descending">
            <Columns>
                <asp:BoundField DataField="Code" HeaderText="${MasterData.Po}" SortExpression="Code" />
                <asp:TemplateField HeaderText="${MasterData.Po.Supplier}" SortExpression="Supplier">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "SupplierName")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="CreateDate" HeaderText="${MasterData.Common.CreateDate}"
                    DataFormatString="{0:yyyy-MM-dd HH:mm}" SortExpression="CreateDate" />
                <asp:BoundField DataField="WinTime" HeaderText="到货时间"
                    DataFormatString="{0:yyyy-MM-dd HH:mm}" SortExpression="WinTime" />
                <asp:BoundField DataField="RefPo" HeaderText=" ${MasterData.Po.RefNo}" SortExpression="RefPo" />
                <asp:BoundField DataField="Status" HeaderText="${MasterData.Po.Status}" SortExpression="Status" />
                <asp:BoundField DataField="CreateUser" HeaderText="${MasterData.Common.CreateUser}"
                    SortExpression="CreateUser" />
                <asp:TemplateField HeaderText="${Common.GridView.Action}">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Code") %>'
                            Text="${Common.Button.Edit}" OnClick="lbtnEdit_Click">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </cc1:GridView>
        <cc1:GridPager ID="gp" runat="server" GridViewID="GV_List" PageSize="10">
        </cc1:GridPager>
    </div>
</fieldset>
