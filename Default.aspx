<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_PayrollStatus" %>

<!DOCTYPE html>
<%--<%@ Register Src="~/Include/wucConfirmBoxBS5.ascx" TagName="wucConfirmBox" TagPrefix="wucConfirmBox" %>--%>

<%@ Register Src="~/Include/sFooter.ascx" TagName="sFooter" TagPrefix="uc3" %>

<html lang="en">
<head id="Head1" runat="server">

    <meta charset="utf-8" />
    <meta content="width=device-width, initial-scale=1.0" name="viewport" />

    <link rel="Shortcut Icon" href="~/favicon.ico" type="image/x-icon" />

    <title>Payroll Management System</title>

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


    <script src="Scripts/block/blockui.js"></script>
    <link href="Scripts/mycss/mycss.css" rel="stylesheet" />

    <style type="text/css">
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


        @media (max-width: 991px) {
            #divFloatImg {
                display: none;
            }
        }
    </style>

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
                    <!--  
            <img src="assets/img/imgs1.png" alt=""> -->


                    <nav id="navbar" class="navbar nav ms-auto mb-2 mb-lg-0">

                        <ul style="color: black;" class="">
                            <li><a class="nav-link active" href="Default.aspx">Home</a></li>
                            <li><a class="nav-link" href="paycheck_register.aspx">Register</a></li>
                            <li><a class="nav-link" href="payroll_check.aspx">Check Payroll</a></li>
                            

                        </ul>
                        <i class="bi bi-list mobile-nav-toggle"></i>
                    </nav>
                    <!-- .navbar -->

                </div>
            </header>
            <!-- End Header -->

            <section id="hero" class="d-flex align-items-center mb-5">
                <div class="container">
                    <div class="row">
                        <div class="col-lg-8 d-flex flex-column justify-content-center pt-lg-0 order-2 order-lg-1" data-aos="fade-up" data-aos-delay="200">
                            <h1>PAYROLL MANAGEMENT SYSTEM</h1>
                            <h2>Employee Payroll Monitoring</h2>

                            <div class="d-flex justify-content-center justify-content-lg-start">
                                 <a href="paycheck_register.aspx" class="btn-get-started1">REGISTER <i class="bi bi-arrow-right"></i></a>
                                <a href="Payroll_check.aspx" class="btn-get-started">Check Payroll<i class="bi bi-arrow-right"></i></a>
                               

                            </div>

                        </div>
                        <div class="col-lg-4 order-1 order-lg-2 hero-img" data-aos="zoom-in" data-aos-delay="200" id="divFloatImg">
                            <img src="Images/PMSMonitoring.png" class="img-fluid animated" alt="">
                        </div>
                    </div>
                   
                </div>

            </section>
            <!-- End Hero -->
            
         <div>
            <uc3:sFooter ID="sFooter" runat="server" />
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
                <%--<wucConfirmBox:wucConfirmBox runat="server" ID="thisMsgBox" />--%>
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

