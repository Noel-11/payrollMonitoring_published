<%@ Control Language="VB" AutoEventWireup="false" CodeFile="wucCaptcha.ascx.vb" Inherits="Include_wucCaptcha" %>
<%--<table>
    <tr>
        <td>
            <asp:TextBox runat="server" ID="txtLastName" SkinID="txtInputFormUC" MaxLength="50"></asp:TextBox>
            &nbsp<asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="txtLastName"
                SetFocusOnError="true" Font-Bold="true" Font-Size="16pt" Display="Static" Text="*" />
            <br />
            <asp:Label ID="Label1" runat="server" SkinID="lbl12B" Text="(Last)" />

        </td>
        <td>

            <asp:TextBox runat="server" ID="txtFirstName" SkinID="txtInputFormUC" MaxLength="50"></asp:TextBox>
            &nbsp<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFirstName"
                SetFocusOnError="true" Font-Bold="true" Font-Size="16pt" Display="Static" Text="*" />
            <br />
            <asp:Label ID="lblFN" runat="server" SkinID="lbl12B" Text="(First)"></asp:Label>
        </td>
        <td>
              <asp:TextBox runat="server" ID="txtMiddleName" SkinID="txtInputFormUC" MaxLength="50"></asp:TextBox>
              <br />
            <asp:Label ID="Label3" runat="server" SkinID="lbl12B" Text="(Middle)"></asp:Label>
        </td>
    </tr>



</table>--%>
<style >
.container {
  display: table;
}
.row {
  display: table-row;
}
.column {
  display: table-cell;
}
</style>

<div class="container">
    <div class="row">
        <div class="column">Column 1</div>
        <div class="column">Column 2</div>
        <div class="column">Column 3</div>
    </div>
    <div class="row">
        <div class="column">Column 1</div>
        <div class="column">Column 2</div>
        <div class="column">Column 3</div>
    </div>
</div>
