<%@ Control Language="VB" AutoEventWireup="false" CodeFile="wucReport.ascx.vb" Inherits="Include_wucReport" %>
<asp:HiddenField runat="server" ID="hdDummy" />
    <asp:HiddenField runat="server" ID="hfModalType" />
    <ajaxToolkit:ModalPopupExtender ID="mpeMessageBox" runat="server" PopupControlID="pnlMessageBox"
        TargetControlID="hdDummy" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="pnlMessageBox" runat="server" CssClass="modalPopup" Style="display: none" Width="1000px">
        <div class="header">
            <asp:Label runat="server" ID="lblMsgBoxHeader" SkinID="lbl12BW" Text="Report"></asp:Label><asp:ImageButton
                runat="server" ID="btnmodalClose" ImageUrl="~/images/close.png" ImageAlign="Right" />
        </div>
        <div class="body">
            <asp:Literal ID="ltEmbed" runat="server" />
        </div>
    </asp:Panel>