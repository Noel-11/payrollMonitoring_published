<%@ Control Language="VB" AutoEventWireup="false" CodeFile="wucMainMenuBS5.ascx.vb" Inherits="Include_wucMainMenuBS" %>

<!--begin::Brand-->
<div class="center-logo text-center pt-9 pb-7 px-9" id="kt_aside_logo">
   
     <!--begin::Logo-->
    <%-- <a href="#" class="text-center">
        <img src="<%=ResolveClientUrl("~/Images/person.png")%>" width="100" />
    </a>--%>

     <!--end::Logo-->
    <div class="dropdown mb-3">
        <a class="btn btn-secondary dropdown-toggle" href="#" role="button" id="dropdownMenuLink" data-bs-toggle="dropdown" aria-expanded="false">
            <asp:Label runat="server" ID="lblTextUser" Font-Names="Arial" Text="Admin" CssClass="text-wrap" Font-Size="11px" Font-Bold="true"></asp:Label>
        </a>

        <ul class="dropdown-menu" aria-labelledby="dropdownMenuLink">
            <li><a class="dropdown-item" id="logout" href="#" runat="server">Logout</a></li>
            <li><a class="dropdown-item" id="ChangePassword" href="#" runat="server">Change Password</a></li>
        </ul>
    </div>
    

</div>

<!--end::Brand-->
<!--begin::Aside menu-->
<div class="aside-menu flex-column-fluid px-3 px-lg-6">
    <!--begin::Aside Menu-->
    <!--begin::Menu-->
    <div class="menu menu-column menu-pill menu-title-gray-600 menu-icon-gray-400 menu-state-primary menu-arrow-gray-500 menu-active-bg-primary fw-bold fs-5 my-5 mt-lg-2 mb-lg-0" id="kt_aside_menu" data-kt-menu="true">
        <div class="hover-scroll-y me-n3 pe-3" id="kt_aside_menu_scroll" data-kt-scroll="true" data-kt-scroll-activate="{default: false, lg: true}" data-kt-scroll-height="auto" data-kt-scroll-wrappers="#kt_aside_menu" data-kt-scroll-dependencies="#kt_aside_logo, #kt_aside_footer" data-kt-scroll-offset="20px" style="height: 238px;">
            <div runat="server" id="navMenu"></div>
        </div>
    </div>
    <!--end::Menu-->
</div>
<!--end::Aside menu-->






