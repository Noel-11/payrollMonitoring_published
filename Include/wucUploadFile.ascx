<%@ Control Language="VB" AutoEventWireup="false" CodeFile="wucUploadFile.ascx.vb" Inherits="Include_wucUploadFile" %>
 <asp:Button runat="server" ID="btnSave" CssClass="swisbutton" Text="Save"/>&nbsp;&nbsp;
 <asp:Button runat="server" ID="btnCancel" CssClass="swisbutton" Text="Cancel"/>

 <asp:HiddenField runat="server" ID="hdDummy" />
    <asp:HiddenField runat="server" ID="hfModalType" />
    <ajaxToolkit:ModalPopupExtender ID="mpeMessageBox" runat="server" PopupControlID="pnlMessageBox"
        TargetControlID="hdDummy" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="pnlMessageBox" runat="server" CssClass="modalPopup" Style="display: none">
        <div class="header">
            <asp:Label runat="server" ID="lblMsgBoxHeader" SkinID="lbl12BW"  Text="CONFIRMATION"></asp:Label>
        </div>
        <div class="body">
            <table>
                <tr>
                    <td></td>
                    <td><asp:FileUpload runat="server" ID="fuClaims" SkinID="fileUpload" /></td>
                </tr>
            </table>
        </div>
        <div class="footer" align="center">
            <asp:Button ID="btnMsgBoxYes" runat="server" Text="Yes" CssClass="yes" />
            <asp:Button ID="btnMsgBoxNo" runat="server" Text="No" CssClass="no" CausesValidation="false" />
        </div>
    </asp:Panel>