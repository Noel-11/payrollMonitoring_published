<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PayCheck_register.aspx.vb" Inherits="PaySlip_register" %>

<!DOCTYPE html>
<%@ Register Src="~/Include/wucConfirmBoxBS5.ascx" TagName="wucConfirmBox" TagPrefix="wucConfirmBox" %>
<%@ Register Src="~/Include/MyCaptchaWithIncrease.ascx" TagName="Captcha" TagPrefix="site" %>
<%@ Register Src="~/Include/sFooter.ascx" TagName="sFooter" TagPrefix="uc3" %>
<html lang="en">
<head id="Head1" runat="server">

    <meta charset="utf-8">
    <meta content="width=device-width, initial-scale=1.0" name="viewport">

    <title>PAYROLL SYSTEM</title>
    <meta content="" name="description">
    <meta content="" name="keywords">

    <!-- Favicons -->
    <link href="assets/img/cdo-seal.png" rel="icon">
    <link href="assets/img/apple-touch-icon.png" rel="apple-touch-icon">

    <!-- Google Fonts -->
    <link href="https://fonts.googleapis.com/css?family=Open+Sans:300,300i,400,400i,600,600i,700,700i|Jost:300,300i,400,400i,500,500i,600,600i,700,700i|Poppins:300,300i,400,400i,500,500i,600,600i,700,700i" rel="stylesheet">

    <!-- Vendor CSS Files -->
    <link href="assets/vendor/aos/aos.css" rel="stylesheet">
    <link href="assets/vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet">
    <link href="assets/vendor/bootstrap-icons/bootstrap-icons.css" rel="stylesheet">
    <link href="assets/vendor/boxicons/css/boxicons.min.css" rel="stylesheet">
    <link href="assets/vendor/glightbox/css/glightbox.min.css" rel="stylesheet">
    <link href="assets/vendor/remixicon/remixicon.css" rel="stylesheet">
    <link href="assets/vendor/swiper/swiper-bundle.min.css" rel="stylesheet">
    <link href='http://fonts.googleapis.com/css?family=Roboto' rel='stylesheet' type='text/css'>

    <!-- Template Main CSS File -->
    <link href="assets/css/style.css" rel="stylesheet">

    <!-- Template Main CSS File -->
    <%--<link href="assets/css/register-css.css" rel="stylesheet">--%>
    <link href="Scripts/mycss/mycss.css" rel="stylesheet" />

    <script src="Scripts/block/blockui.js"></script>
    <%--  <script src="Scripts/Bootstrap5/jquery.min.js"></script>--%>
    <%-- <script src="Scripts/Bootstrap5/js/bootstrap.bundle.js"></script>--%>

    <style type="text/css">
        /* Mobile-first layout for payroll monitoring */
        body { padding: 12px; background: #f3f6fb; }
        .shadow1 { max-width: 1440px; margin: 0 auto; overflow: hidden; background: #fff; }
        #header .container { flex-wrap: wrap; gap: .5rem; }
        #header .logos { width: auto; max-width: 25%; max-height: 42px; height: auto; object-fit: contain; margin-right: .25rem; }
        #hero { min-height: 0; max-height: none !important; overflow: visible !important; padding: 1rem 0 2rem; align-items: flex-start !important; }
        #hero > .container { width: 100%; }
        #hero .hero-img { min-width: 0; }
        #hero .card-header { border: 0; background: transparent; padding: .75rem 0 1rem; }
        #hero .card-header h1 { display: flex; align-items: center; gap: .65rem; margin: 0; font-size: clamp(1.35rem, 5vw, 2rem); line-height: 1.25; }
        #hero .card-header img { width: 36px; height: 36px; object-fit: contain; }
        #hero .form-control, #hero .form-select { min-height: 48px; }
        #hero .btn { min-height: 46px; padding-inline: 1.25rem; }
        #hero input.form-control, #hero select.form-select, #hero textarea.form-control { width: 100%; max-width: 100%; min-width: 0; }
        #hero button.btn, #hero input.btn, #hero .btn { max-width: 100%; white-space: normal; }
        #hero .input-group { flex-wrap: wrap; }
        #hero .input-group > .form-control { flex: 1 1 12rem; }
        #hero .input-group > span { max-width: 100%; }
        #hero [id*="Validator"] { display: block; max-width: 100%; overflow-wrap: anywhere; }
        #hero a { display: inline-block; padding: .35rem 0; }
        #hero .captcha, #hero img { max-width: 100%; }
        @media (min-width: 576px) { body { padding: 24px; } }
        @media (max-width: 991.98px) {
            #divFloatImg { display: none !important; }
            #hero > .container > .row { justify-content: center; }
            #hero .hero-img { flex: 0 0 100%; max-width: 720px; }
        }
        @media (max-width: 575.98px) {
            body { padding: 0; }
            .shadow1 { border-radius: 0; box-shadow: none; }
            #header { padding: .5rem 0; }
            #header > .container { padding-inline: 12px; }
            #hero > .container { padding-inline: 16px; }
            #hero .row { --bs-gutter-x: .75rem; }
            #hero .row > [class*="col-"] { min-width: 0; }
            #hero .col-md-6 { flex: 0 0 100%; max-width: 100%; }
            #hero .float-end { float: none !important; }
            #hero .btn { width: 100%; }
            #hero .input-group > .btn { flex: 1 1 100%; }
        }

        .footer {
            font-family: "Montserrat", sans-serif;
        }

        .brand-container {
            width: 100%;
            max-width: 150px;
            margin: 10px auto;
            line-height: 0;
            text-align: left;
            display: inline-block;
        }

        .link {
            background: #C0DFF3;
            margin-top: 20px;
            padding: 15px;
            border-radius: 20px;
            color: black;
            box-shadow: 3px 3px #888888;
        }

        .link2 {
            background: #D5E6E1;
            margin-top: 20px;
            padding: 15px;
            border-radius: 20px;
            color: black;
            box-shadow: 3px 3px #888888;
        }

        .other-systems {
            margin-top: 12px;
            font-weight: 300 !important;
        }

            .other-systems h6 {
                color: #2F9ACB;
                font-weight: 600;
            }

        .footer-links {
            padding-left: 0;
            list-style: none;
        }

            .footer-links a {
                color: #737373;
            }

            .footer-links li {
                display: block;
            }

        .site-footer h6 {
            font-size: 16px;
            text-transform: uppercase;
            margin-top: 5px;
            letter-spacing: 2px;
        }

        .footer-links li a {
            color: black;
            text-decoration: none;
            outline: none;
            transition: all 0.2s;
        }

        .image-block a {
            text-decoration: none;
            outline: none;
            transition: all 0.2s;
        }

        .separator {
            margin-top: 1rem;
            margin-bottom: 1rem;
            border: 0;
            border-top: 2px solid rgba(0, 0, 0, 0.1);
        }

        #myBtn {
            background: lightgray;
            display: block;
            color: #878038;
            width: 100px;
            border: none;
            border-radius: 40px;
            padding: 1px 0;
            text-transform: uppercase;
            font-weight: bold;
            margin-bottom: 32px;
            outline: none;
            font-size: 8pt;
        }

        .client-info {
            font-size: 11px;
            line-height: 13px;
        }

    </style>
    <link href="assets/css/payroll-mobile.css" rel="stylesheet" />

</head>

<body>

    <form id="form1" role="form" method="post" runat="server" autocomplete="off">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <%--  <Scripts>
                <asp:ScriptReference Path="~/Scripts/block/UPBlock.js" />
            </Scripts>--%>
        </asp:ScriptManager>

        <!-- ======= Hero Section ======= -->
        <div class="shadow1">

            <!-- ======= Header ======= -->
            <header id="header">
                <div class="container d-flex align-items-center">

                    <img src="assets/img/seallogo.png" class="logos" alt="">
                    <img src="assets/img/risebig.png" class="logos" alt="">
                    <img src="assets/img/ict.png" class="logos" alt="">

                    <nav id="navbar" class="navbar nav ms-auto mb-2 mb-lg-0">

                        <ul style="color: black;" class="">
                            <li><a class="nav-link" href="default.aspx">Home</a></li>
                            <li><a class="nav-link active" href="paycheck_register.aspx">Register</a></li>
                            <li><a class="nav-link" href="payroll_check.aspx">Check Payroll</a></li>
                        </ul>

                        <i class="bi bi-list mobile-nav-toggle"></i>
                    </nav>

                </div>
            </header>
            <!-- End Header -->

            <section id="hero" class="d-flex align-items-center mb-5">


                <div class="container">
                    <div class="row">
                        <div class="col-lg-6 flex-column justify-content-center pt-lg-0 order-1 order-lg-1" id="divFloatImg">
                            <img src="Images/PMSMonitoring2.png" class="img-fluid animated" alt="Floating Image" width="600">
                        </div>
                        <div class="col-lg-6 order-2 hero-img" data-aos="zoom-in" data-aos-delay="200" style="z-index: 10;">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                   
                                    <%-- <div class="card">--%>
                                    <div class="card-header">
                                        <h1>Registration Form</h1>
                                    </div>
                                    <%-- <div class="card-body">--%>

                                    <div class="row">
                                        <div class="col-lg-12">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRegEmployeeID" SetFocusOnError="true" Font-Bold="false" Font-Italic="true" Font-Size="10pt" Display="Dynamic" Text="ID is Required" ValidationGroup="DOCRETRIEVE" />
                                            <asp:TextBox runat="server" ID="txtRegEmployeeID" CssClass="form-control mb-3" placeholder="Employee ID* "></asp:TextBox>

                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-lg-12">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtRegLastName" SetFocusOnError="true" Font-Bold="false" Font-Italic="true" Font-Size="10pt" Display="Dynamic" Text="Last Name is Required" ValidationGroup="DOCRETRIEVE" />
                                            <asp:TextBox runat="server" ID="txtRegLastName" CssClass="form-control mb-3" placeholder="Last Name*"></asp:TextBox>
                                        </div>

                                    </div>


                                    <div class="row">
                                        <div class="col-lg-12">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtRegFirstName" SetFocusOnError="true" Font-Bold="false" Font-Italic="true" Font-Size="10pt" Display="Dynamic" Text="First Name is Required" ValidationGroup="DOCRETRIEVE" />
                                            <asp:TextBox runat="server" ID="txtRegFirstName" CssClass="form-control mb-3" placeholder="First Name*"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-lg-12">
                                            <asp:DropDownList runat="server" CssClass="form-select mb-3" placeholder="Ext">
                                                <asp:ListItem Value="" Text="No Ext."></asp:ListItem>
                                                <asp:ListItem Value="JR" Text="JR"></asp:ListItem>
                                                <asp:ListItem Value="SR" Text="SR"></asp:ListItem>
                                                <asp:ListItem Value="I" Text="I"></asp:ListItem>
                                                <asp:ListItem Value="II" Text="II"></asp:ListItem>
                                                <asp:ListItem Value="III" Text="III"></asp:ListItem>
                                                <asp:ListItem Value="IV" Text="IV"></asp:ListItem>
                                                <asp:ListItem Value="V" Text="V"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>


                                    <div class="row">
                                        <div class="col-md-12 mb-2">
                                            <div class="container-fluid img-responsive" align="center">
                                                <site:Captcha runat="server" ID="Captcha4" EnableClientScript="true" ValidationGroup="DOCRETRIEVE" />
                                            </div>
                                        </div>
                                    </div>



                                    <%-- <button class="btn btn-primary" type="submit">Submit form</button>--%>

                                    <div class="row">
                                        <div class="col-md-12 mb-2">
                                            <button type="button" class="btn btn-primary mb-3" id="btnRegRetrieve" runat="server" validationgroup="DOCRETRIEVE">Get PIN<i class="bi bi-arrow-right"></i></button>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-12 mb-2">
                                            <div runat="server" id="pnlPin">
                                                <h3>Verification Code</h3>
                                                <p>
                                                    Please type the verification code sent to
                                                <asp:Label runat="server" ID="lblPinContact"></asp:Label>
                                                </p>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtPinCode" SetFocusOnError="true" Font-Bold="false" Font-Italic="true" Font-Size="10pt" Display="Dynamic" Text="Code is Required" ValidationGroup="PIN" />
                                                <div class="input-group">
                                                    <asp:TextBox runat="server" ID="txtPinCode" CssClass="form-control mb-3" TextMode="Number" MaxLength="6" placeholder="Payslip PIN" lenght="6" ValidationGroup="PIN"></asp:TextBox>
                                                    <span class="input-group-btn border-secondary" style="background-color: white; color: black">
                                                        <button runat="server" id="btnVerify" type="button" class="btn btn-primary mb-3" validationgroup="PIN">Verify <i class="bi bi-arrow-right"></i></button>
                                                    </span>

                                                </div>

                                            </div>
                                        </div>
                                    </div>


                                    <div class="row">
                                        <div class="col-md-12 mb-2">
                                            <div runat="server" id="pnlPassword">
                                                <asp:TextBox runat="server" ID="txtRegPasword" CssClass="form-control mb-3" TextMode="Password" placeholder="Payslip Password*"></asp:TextBox>
                                                <asp:TextBox runat="server" ID="txtRetypeRegPasword" CssClass="form-control mb-3" TextMode="Password" placeholder="Confirm Password*"></asp:TextBox>
                                                <button runat="server" id="btnRegister" type="button" class="btn btn-primary mb-3">SUBMIT <i class="bi bi-arrow-right"></i></button>
                                            </div>
                                        </div>
                                    </div>

                                    <%--  </div>--%>


                                    <%-- </div>--%>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>


            </section>

            <!-- End Hero -->


            <div>
                <uc3:sfooter id="sFooter" runat="server" />
            </div>


        </div>

        <asp:UpdatePanel ID="updatePanel3" runat="server">
            <ContentTemplate>
                <asp:HiddenField runat="server" ID="hfEmployeeID"></asp:HiddenField>
                <asp:HiddenField runat="server" ID="hfEmployeeName"></asp:HiddenField>
                <asp:HiddenField runat="server" ID="hfEmployeePosition"></asp:HiddenField>
                <asp:HiddenField runat="server" ID="hfDepartment"></asp:HiddenField>
                <asp:HiddenField runat="server" ID="hfCurrRate"></asp:HiddenField>
                <asp:HiddenField runat="server" ID="hfEmpStatus"></asp:HiddenField>
                <asp:HiddenField runat="server" ID="hfPeriod"></asp:HiddenField>
                <asp:HiddenField runat="server" ID="hfContactNo"></asp:HiddenField>
                <wucConfirmBox:wucConfirmBox runat="server" ID="thisMsgBox" />
            </ContentTemplate>
        </asp:UpdatePanel>

    </form>

    <!-- Vendor JS Files -->
    <script src="assets/vendor/aos/aos.js"></script>
    <script src="assets/vendor/bootstrap/js/bootstrap.bundle.min.js"></script>
    <script src="assets/vendor/glightbox/js/glightbox.min.js"></script>
    <script src="assets/vendor/isotope-layout/isotope.pkgd.min.js"></script>
    <script src="assets/vendor/swiper/swiper-bundle.min.js"></script>
    <script src="assets/vendor/waypoints/noframework.waypoints.js"></script>
    <script src="assets/vendor/php-email-form/validate.js"></script>

    <!-- Template Main JS File -->
    <script src="assets/js/main.js"></script>



</body>
</html>

