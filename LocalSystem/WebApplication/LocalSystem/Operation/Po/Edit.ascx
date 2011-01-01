<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Edit.ascx.cs" Inherits="MasterData_Operation_Po_Edit" %>

<script language="javascript" type="text/javascript" src="Js/DatePicker/WdatePicker.js"></script>

<div id="divFV" runat="server">
    <fieldset>
        <legend>${MasterData.Po.UpdatePo}</legend>
        <table class="mtable">
            <tr>
                <td class="td01">
                    ${MasterData.Po}:
                </td>
                <td class="td02">
                    <asp:Label ID="lblCode" runat="server" />
                </td>
                <td class="td01">
                    ${MasterData.Po.Status}:
                </td>
                <td class="td02">
                    <asp:Label ID="lblStatus" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="td01">
                    ${MasterData.Po.PlantCode}:
                </td>
                <td class="td02">
                    <asp:Label ID="lblPlantCode" runat="server" />
                </td>
                <td class="td01">
                    ${MasterData.Po.PlantName}:
                </td>
                <td class="td02">
                    <asp:Label ID="lblPlantName" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="td01">
                    ${MasterData.Po.PlantAddress}:
                </td>
                <td class="td02" colspan="3">
                    <asp:TextBox ID="tbPlantAddress" runat="server" ReadOnly="true" Width="300" />
                </td>
            </tr>
            <tr>
                <td class="td01">
                    ${MasterData.Po.PlantPhone}:
                </td>
                <td class="td02">
                    <asp:TextBox ID="tbPlantPhone" runat="server" ReadOnly="true" />
                </td>
                <td class="td01">
                    ${MasterData.Po.PlantFax}:
                </td>
                <td class="td02">
                    <asp:TextBox ID="tbPlantFax" runat="server" ReadOnly="true" />
                </td>
            </tr>
            <tr>
                <td class="td01">
                    ${MasterData.Po.PlantContanct}:
                </td>
                <td class="td02">
                    <asp:TextBox ID="tbPlantContanct" runat="server" ReadOnly="true" />
                </td>
                <td class="td01">
                    ${MasterData.Po.PlantDock}:
                </td>
                <td class="td02">
                    <asp:TextBox ID="tbPlantDock" runat="server" ReadOnly="true" />
                </td>
            </tr>
        </table>
        <br />
        <table class="mtable">
            <tr>
                <td class="td01">
                    ${MasterData.Po.Supplier}:
                </td>
                <td class="td02">
                    <asp:Label ID="lblSupplier" runat="server" />
                </td>
                <td class="td01">
                    ${MasterData.Po.RefNo}:
                </td>
                <td class="td02">
                    <asp:Label ID="lblRefPo" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="td01">
                    ${MasterData.Po.SupplierAddress}:
                </td>
                <td class="td02" colspan="3">
                    <asp:TextBox ID="tbSupplierAddress" runat="server" ReadOnly="true" Width="300" />
                </td>
            </tr>
            <tr>
                <td class="td01">
                    ${MasterData.Po.SupplierPhone}:
                </td>
                <td class="td02">
                    <asp:TextBox ID="tbSupplierPhone" runat="server" ReadOnly="true" />
                </td>
                <td class="td01">
                    ${MasterData.Po.SupplierFax}:
                </td>
                <td class="td02">
                    <asp:TextBox ID="tbSupplierFax" runat="server" ReadOnly="true" />
                </td>
            </tr>
            <tr>
                <td class="td01">
                    ${MasterData.Po.SupplierContanct}:
                </td>
                <td class="td02">
                    <asp:TextBox ID="tbSupplierContant" runat="server" ReadOnly="true" />
                </td>
                <td class="td01">
                    ${Common.Business.LotNo}:
                </td>
                <td class="td02">
                    <asp:TextBox ID="tbSupplierLotNo" runat="server" ReadOnly="true" />
                </td>
            </tr>
            <tr>
                <td class="td01">
                    到货时间:
                </td>
                <td class="td02">
                    <asp:TextBox ID="tbWinTime" runat="server" onClick="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm'})" />
                </td>
                <td class="td01">
                    ${MasterData.Po.OutBoundDate}:
                </td>
                <td class="td02">
                    <asp:Label ID="lblOutBoundDate" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="td01">
                    <asp:Literal ID="ltlCreateUser" runat="server" Text="${MasterData.Common.CreateUser}:" />
                </td>
                <td class="td02">
                    <asp:Label ID="lbCreateUser" runat="server" />
                </td>
                <td class="td01">
                    <asp:Literal ID="ltlCreateDate" runat="server" Text="${MasterData.Common.CreateDate}:" />
                </td>
                <td class="td02">
                    <asp:Label ID="lbCreateDate" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="td01">
                    <asp:Literal ID="ltlLastModifyUser" runat="server" Text="${MasterData.Common.LastModifyUser}:" />
                </td>
                <td class="td02">
                    <asp:Label ID="lbLastModifyUser" runat="server" />
                </td>
                <td class="td01">
                    <asp:Literal ID="ltlLastModifyDate" runat="server" Text="${MasterData.Common.LastModifyDate}:" />
                </td>
                <td class="td02">
                    <asp:Label ID="lbLastModifyDate" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="td01">
                </td>
                <td class="td02">
                </td>
                <td class="td01">
                </td>
                <td class="td02">
                    <div class="buttons">
                        <asp:Button ID="btnSave" runat="server" Text="${Common.Button.Save}" OnClick="btnSave_Click"
                            CssClass="apply" ValidationGroup="vgSave" />
                        <asp:Button ID="btnCancel" runat="server" Text="${Common.Button.Cancel}" OnClick="btnCancel_Click"
                            CssClass="apply" ValidationGroup="vgSave" />
                        <asp:Button ID="btnSubmit" runat="server" Text="${Common.Button.Submit}" OnClick="btnSubmit_Click"
                            CssClass="apply" ValidationGroup="vgSave" />                        
                        <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click"
                            CssClass="back" />
                    </div>
                </td>
            </tr>
        </table>
    </fieldset>
    <fieldset>
        <legend>${MasterData.Po.Detail}</legend>
        <asp:GridView ID="GV_List" runat="server" DataKeyNames="Id" Width="100%" AutoGenerateColumns="false"
            OnRowDataBound="GV_List_RowDataBound" OnRowDeleting="GV_List_RowDeleting" OnRowEditing="GV_List_RowEditing"
            OnRowCancelingEdit="GV_List_RowCancelingEdit" OnRowUpdating="GV_List_RowUpdating">
            <Columns>
                <asp:BoundField DataField="ItemCode" HeaderText="${MasterData.PoDetail.Item.Code}"
                    SortExpression="ItemCode" ReadOnly="true" />
                <asp:BoundField DataField="ItemDescription" HeaderText="${MasterData.PoDetail.Item.Description}"
                    SortExpression="ItemDescription" ReadOnly="true" />
                <asp:BoundField DataField="Uom" HeaderText="${MasterData.PoDetail.Item.Uom}" SortExpression="Uom"
                    ReadOnly="true" />
                <asp:BoundField DataField="UC" HeaderText="${MasterData.PoDetail.Item.UC}" SortExpression="UC"
                    DataFormatString="{0:0.##}" ReadOnly="true" />
                <asp:TemplateField HeaderText="${MasterData.PoDetail.Qty}" SortExpression="Qty" ItemStyle-Width="100">
                    <EditItemTemplate>
                        <asp:TextBox ID="tbValue" runat="server" Text='<%# Bind("Qty","{0:0.########}") %>'
                            Width="100" MaxLength="100" />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblValue" runat="server" Text='<%# Eval("Qty","{0:0.########}") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="True" ItemStyle-Width="80px" HeaderText="${Common.GridView.Action}"
                    ShowDeleteButton="true" />
            </Columns>
        </asp:GridView>
    </fieldset>
</div>
