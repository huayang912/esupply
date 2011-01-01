<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="Operation_BarCode_Main" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>--%>

<script language="javascript" type="text/javascript" src="Js/DatePicker/WdatePicker.js"></script>

<script type="text/javascript" language="javascript">
    function GVCheckClick() {
        if ($(".GVHeader input:checkbox").attr("checked") == true) {
            $(".GVRow input:checkbox").attr("checked", true);
            $(".GVAlternatingRow input:checkbox").attr("checked", true);
        }
        else {
            $(".GVRow input:checkbox").attr("checked", false);
            $(".GVAlternatingRow input:checkbox").attr("checked", false);
        }
    }
</script>

<fieldset>
    <table class="mtable">
        <tr>
            <td class="ttd01">
                ${Common.Business.LotNo}:
            </td>
            <td class="td02">
                <asp:TextBox ID="tbLotNo" runat="server" Visible="true" />
            </td>
            <td class="td01">
                ${MasterData.PoDetail.Item.Code}:
            </td>
            <td class="td02">
                <asp:TextBox ID="tbItemCode" runat="server" Visible="true" />
            </td>
        </tr>
        <tr>
            <td class="ttd01">
                ${Common.Business.Supplier}:
            </td>
            <td class="td02">
                <asp:TextBox ID="tbSupplier" runat="server" Visible="true" />
            </td>
            <td class="td01">
                ${Common.Business.CreateUser}:
            </td>
            <td class="td02">
                <asp:TextBox ID="tbCreateUser" runat="server" Visible="true" />
            </td>
        </tr>
        <tr>
            <td class="ttd01">
                ${Common.Business.StartTime}:
            </td>
            <td class="td02">
                <asp:TextBox ID="tbStartDate" runat="server" onClick="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm'})" />
            </td>
            <td class="td01">
                ${Common.Business.StartDate}:
            </td>
            <td class="td02">
                <asp:TextBox ID="tbEndDate" runat="server" onClick="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm'})" />
            </td>
        </tr>
        <tr>
            <td class="td01">
                ${MasterData.Po.Status}:
            </td>
            <td class="td02">
                <asp:CheckBoxList ID="cbStatus" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Create" Value="Create" Selected="True" />
                    <asp:ListItem Text="Warning" Value="Warning" Selected="True" />
                    <asp:ListItem Text="Close" Value="Close" />
                    <asp:ListItem Text="Error" Value="Error" />
                </asp:CheckBoxList>
            </td>
            <td />
            <td class="td02">
                <asp:Button ID="btnSearch" runat="server" Text="${Common.Button.Search}" OnClick="btnSearch_Click"
                    CssClass="button2" />
                <asp:Button ID="btnOutBound" runat="server" Text="${Common.Button.OutBound}" OnClick="btnOutBound_Click" />
                <asp:Button ID="btnCreate" runat="server" Text="${Common.Button.Create}" OnClick="btnCreate_Click" />
            </td>
        </tr>
    </table>
</fieldset>
<fieldset id="fldList" runat="server">
    <div class="GridView">
        <asp:GridView ID="GV_List" runat="server" DataKeyNames="Id" Width="100%" AutoGenerateColumns="false"
            OnRowDataBound="GV_List_RowDataBound" OnRowDeleting="GV_List_RowDeleting" OnRowEditing="GV_List_RowEditing"
            OnRowCancelingEdit="GV_List_RowCancelingEdit" OnRowUpdating="GV_List_RowUpdating">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <div onclick="GVCheckClick()">
                            <asp:CheckBox ID="CheckAll" runat="server" />
                        </div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:HiddenField runat="server" Value='<%# Bind("Id") %>' ID="hfId" />
                        <asp:CheckBox ID="CheckBoxGroup" name="CheckBoxGroup" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="BarCode" HeaderText="${Inventory.PrintHu.LocationLotDetail.HuId}" />
                <%--<asp:BoundField DataField="LotNo" HeaderText="${Common.Business.LotNo}" />--%>
                <asp:BoundField DataField="ItemCode" HeaderText="${MasterData.PoDetail.Item.Code}" />
                <asp:BoundField DataField="ItemDescription" HeaderText="${MasterData.PoDetail.Item.Description}" />
                <%--                <asp:BoundField DataField="Uom" HeaderText="${MasterData.PoDetail.Item.Uom}" />
                <asp:BoundField DataField="UC" HeaderText="${MasterData.PoDetail.Item.UC}" DataFormatString="{0:0.##}" />--%>
                <asp:BoundField DataField="Qty" HeaderText="${MasterData.PoDetail.Qty}" DataFormatString="{0:0.##}" />
                <asp:BoundField DataField="SupplierCode" HeaderText="${Common.Business.Supplier}" />
                <asp:BoundField DataField="CreateDate" HeaderText="${MasterData.Common.CreateDate}"
                    DataFormatString="{0:yyyy-MM-dd HH:mm}" />
                <asp:BoundField DataField="CreateUser" HeaderText="${MasterData.Common.CreateUser}" />
                <asp:BoundField DataField="Status" HeaderText="${MasterData.Po.Status}" />
                <%--<asp:TemplateField HeaderText="${MasterData.Po.Status}">
                    <ItemTemplate>
                        <asp:Label ID="lblStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Status")%>' />
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:BoundField DataField="Memo" HeaderText="${MasterData.OutBound.Memo}" />
                <asp:CommandField ItemStyle-Width="80px" HeaderText="${Common.GridView.Action}" ShowDeleteButton="true"  
                DeleteText="&lt;span onclick=&quot;JavaScript:return confirm('${Common.Delete.Confirm}?')&quot;&gt;${Common.Button.Delete}&lt;/span&gt;"/>
            </Columns>
        </asp:GridView>
    </div>
</fieldset>
