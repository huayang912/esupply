<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewJobReport.ascx.cs" Inherits="Modules_OffLineReport_JobExecution_NewJobReport" %>
<!-- Modified By Vincent On 2006-9-3 Begin -->

<h2>
    Report</h2>
<p class="formBtnBoard">
    <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click"  CssClass="btn"/>
    <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click"  CssClass="btn"/>
</p>
<asp:HiddenField ID="txtReportJobId" runat="server" />
<asp:gridview id="gvReportList" runat="server" DataKeyNames="Id" AutoGenerateColumns="false"
    CellPadding="4" CssClass="list" GridLines="Horizontal">
    <HeaderStyle CssClass="listhead" HorizontalAlign="Left" />
    <Columns>
        <asp:TemplateField HeaderText="Report">
            <ItemTemplate>
                <asp:CheckBox ID="cbSelect" runat="server" Checked='<%# hasData((int)DataBinder.Eval(Container.DataItem, "TheReport.Id")) %>' />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Id">
            <ItemTemplate>
                <asp:Label ID="reportId" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TheReport.Id") %>' />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Report Name">
            <ItemTemplate>
                <%# DataBinder.Eval(Container.DataItem, "TheReport.Name") %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Report Type">
            <ItemTemplate>
                <%# DataBinder.Eval(Container.DataItem, "TheReport.ReportType") %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Description">
            <ItemTemplate>
                <%# DataBinder.Eval(Container.DataItem, "TheReport.Description") %>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:gridview>
<asp:Label ID="lblMessage" runat="server" Visible="false" CssClass="error"></asp:Label>


