<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Payroll_check.aspx.vb" Inherits="Payroll_check" Theme="Skins" %>

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

                    <nav id="navbar" class="navbar nav ms-auto mb-2 mb-lg-0">

                        <ul style="color: black;" class="">
                            <li><a class="nav-link" href="Default.aspx">Home</a></li>
                            <li><a class="nav-link" href="paycheck_register.aspx">Register</a></li>
                            <li><a class="nav-link active" href="payroll_check.aspx">Check Payroll</a></li>

                        </ul>

                        <i class="bi bi-list mobile-nav-toggle"></i>
                    </nav>

                </div>
            </header>
            <!-- End Header -->

            <section id="hero" class="d-flex align-items-center mb-5" style="max-height: 600px; overflow-y: scroll;">


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
                                        <h1>
                                            <img src="Images/search.png" class="img-fluid" />Check Employee Payroll</h1>

                                    </div>

                                    <%-- <div class="card-body">--%>
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRegEmployeeID" SetFocusOnError="true" Font-Bold="false" Font-Italic="true" Font-Size="10pt" Display="Dynamic" Text="ID is Required" ValidationGroup="DOCRETRIEVE" />
                                            <asp:TextBox runat="server" ID="txtRegEmployeeID" CssClass="form-control mb-3" placeholder="Employee ID* "></asp:TextBox>
                                        </div>
                                    </div>



                                    <div class="row" runat="server" visible="false">
                                        <div class="col-lg-12">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtRegLastName" SetFocusOnError="true" Font-Bold="false" Font-Italic="true" Font-Size="10pt" Display="Dynamic" Text="Last Name is Required" ValidationGroup="DOCRETRIEVE" />
                                            <asp:TextBox runat="server" ID="txtRegLastName" CssClass="form-control mb-3" placeholder="Last Name*"></asp:TextBox>
                                        </div>

                                    </div>


                                    <div class="row" runat="server" visible="false">
                                        <div class="col-lg-12">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtRegFirstName" SetFocusOnError="true" Font-Bold="false" Font-Italic="true" Font-Size="10pt" Display="Dynamic" Text="First Name is Required" ValidationGroup="DOCRETRIEVE" />
                                            <asp:TextBox runat="server" ID="txtRegFirstName" CssClass="form-control mb-3" placeholder="First Name*"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="row" runat="server" visible="false">
                                        <div class="col-lg-12">
                                            <asp:DropDownList runat="server" CssClass="form-select mb-3" ID="txtExtName" placeholder="Ext">
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
                                        <div class="col-md-12">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtEmployeePassword" SetFocusOnError="true" Font-Bold="true" Font-Italic="true" Font-Size="10pt" Display="Dynamic" Text="Password is Required" ValidationGroup="DOCRETRIEVE" />
                                            <asp:TextBox runat="server" CssClass="form-control mb-3" ID="txtEmployeePassword" TextMode="Password" MaxLength="50" placeholder="Password* "></asp:TextBox>
                                             
                                        </div>
                                          
                                    </div>

                                     <div class="row">
                                         <div class="col-md-6">
                                            <a runat="server" id="aForgotP" href="#" style="font-size: 14px;text-decoration:underline;color:blue;">Forgot password? Click Here</a>
                                         </div>
                                           <div class="col-md-6">
                                            <a runat="server" id="a1" href="PayCheck_register.aspx" class="float-end" style="font-size: 14px;text-decoration:underline;color:blue;">Click me to Register</a>
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
                                            <button type="button" class="btn btn-primary mb-3" id="btnRegRetrieve" runat="server" validationgroup="DOCRETRIEVE">RETRIEVE <i class="bi bi-arrow-right"></i></button>
                                        </div>
                                    </div>



                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>


            </section>

            <!-- End Hero -->


            <%--MODAL PAYROLL--%>
            <div id="mdlPayroll" role="dialog" class="modal fade show" data-bs-backdrop="false" data-bs-keyboard="false">
                <div class="modal-dialog modal-xl">
                    <!-- Modal content-->
                    <div class="modal-content" style="border-top-left-radius: 15px; border-top-right-radius: 15px;">
                        <div class="modal-header" style="border-top-left-radius: 10px; border-top-right-radius: 10px; background-color: #3366cc; text-align: center;">

                            <%--<div class="row">
                                <div class="col-md-12">--%>
                            <img src="Images/archive.png" class="img-fluid" />
                            <h3>
                                <asp:Label runat="server" ID="Label1" Text="Employee Payroll" Style="color: yellow;"></asp:Label>
                            </h3>
                            <%-- </div>
                            </div>--%>

                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body" style="padding: 10px 10px; border: solid 5px black;">
                            <asp:UpdatePanel runat="server" ID="upList">
                                <ContentTemplate>

                                    <div class="row">
                                        <div class="col-md-12 mb-1">
                                            <div class="input-group">
                                                <span class="input-group-text border-secondary" style="background-color: white; color: black">Office</span>
                                                <asp:Label runat="server" ID="lblOffice" CssClass="form-control border-secondary" Style="text-transform: uppercase"></asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">

                                        <div class="col-md-12 mb-1">
                                            <div class="input-group">
                                                <span class="input-group-text border-secondary" style="background-color: white; color: black">Name</span>
                                                <asp:Label runat="server" ID="lblName" CssClass="form-control border-secondary" Style="text-transform: uppercase"></asp:Label>
                                            </div>

                                        </div>

                                        <%--    <div class="col-md-4 mb-1">
                                            <div class="input-group">
                                                <span class="input-group-text border-secondary" style="background-color: white; color: black">First Name</span>
                                                <asp:Label runat="server" ID="lblFirstName" CssClass="form-control border-secondary" Style="text-transform: uppercase"></asp:Label>
                                            </div>

                                        </div>

                                        <div class="col-md-2 mb-1">
                                            <div class="input-group">
                                                <span class="input-group-text border-secondary" style="background-color: white; color: black">Ext.</span>
                                                <asp:Label runat="server" ID="lblExtName" CssClass="form-control border-secondary" Style="text-transform: uppercase"></asp:Label>
                                            </div>

                                        </div>

                                        <div class="col-md-2 mb-1">
                                            <div class="input-group">
                                                <span class="input-group-text border-secondary" style="background-color: white; color: black">Middle Name</span>
                                                <asp:Label runat="server" ID="lblMiddleName" CssClass="form-control border-secondary" Style="text-transform: uppercase"></asp:Label>
                                            </div>
                                        </div>--%>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-4 mb-1">
                                            <div class="input-group">
                                                <span class="input-group-text border-secondary" style="background-color: white; color: black">Employee ID</span>
                                                <asp:Label runat="server" ID="lblAgencyId" CssClass="form-control border-secondary" Style="text-transform: uppercase"></asp:Label>
                                            </div>

                                        </div>

                                        <div class="col-md-4 mb-1">
                                            <div class="input-group">
                                                <span class="input-group-text border-secondary" style="background-color: white; color: black">Position</span>
                                                <asp:Label runat="server" ID="lblEmployeePosition" CssClass="form-control border-secondary" Style="text-transform: uppercase"></asp:Label>
                                            </div>

                                        </div>

                                        <div class="col-md-4 mb-1">
                                            <div class="input-group">
                                                <span class="input-group-text border-secondary" style="background-color: white; color: black">Step</span>
                                                <asp:Label runat="server" ID="lblEmployeePositionStep" CssClass="form-control border-secondary" Style="text-transform: uppercase"></asp:Label>
                                            </div>

                                        </div>
                                    </div>


                                    <div class="row">

                                        <div class="col-md-4 mb-1">
                                            <div class="input-group">
                                                <span class="input-group-text border-secondary" style="background-color: white; color: black">Employee Status</span>
                                                <asp:Label runat="server" ID="lblEmployeeStatus" CssClass="form-control border-secondary" Style="text-transform: uppercase"></asp:Label>
                                            </div>
                                        </div>

                                    </div>

                                    <div class="row" style="max-width: 1190px; overflow-x: scroll;">
                                        <div class="col-md-12 mb-1">
                                            <asp:GridView runat="server" ID="_gvLedger" HeaderStyle-Font-Size="14px" CssClass="gridviewGray"
                                                ShowFooter="false" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                AutoGenerateColumns="false" GridLines="None" Font-Names="Arial" Font-Size="14px"
                                                ForeColor="#000000" AllowPaging="true" PageSize="20">
                                                <Columns>
                                                    <asp:BoundField DataField="payroll_period" HeaderText="Period" HeaderStyle-Width="10%" ItemStyle-Font-Size="14px" ItemStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="reference_code" HeaderText="Reference#" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="center" />
                                                    <asp:BoundField DataField="payroll_compensation" HeaderText="Type" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="payroll_classification" HeaderText="Classification" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="payroll_status" HeaderText="Status" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="trans_datetime" HeaderText="DateTime" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:TemplateField HeaderText="Check" HeaderStyle-Width="2%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:ImageButton runat="server" ID="lnkCheck" ImageUrl="~/images/open.png" OnCommand="cmdGVCheck"
                                                                CommandArgument='<%# Bind("transaction_header_id")%>' period='<%# Eval("payroll_period")%>' refCode='<%# Eval("reference_code")%>' empStatus='<%# Eval("payroll_classification")%>' payrollType='<%# Eval("payroll_compensation")%>' ToolTip="Click to Check Payroll" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Logs" HeaderStyle-Width="2%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:ImageButton runat="server" ID="lnkLogs" ImageUrl="~/images/log.png" OnCommand="cmdGVLogs"
                                                                CommandArgument='<%# Bind("transaction_header_id")%>' period='<%# Eval("payroll_period")%>' refCode='<%# Eval("reference_code")%>' empStatus='<%# Eval("payroll_classification")%>' payrollType='<%# Eval("payroll_compensation")%>' ToolTip="Click to View logs" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-12">
                                            <label class="badge bg-primary">The system displays only the five(5) most recently processed payrolls.</label>
                                        </div>
                                    </div>

                                </ContentTemplate>
                            </asp:UpdatePanel>

                            <div class="modal-footer ">
                                <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                    <ContentTemplate>
                                        <asp:Button runat="server" ID="btnPayrollClose" class="btn btn-danger pull-right " Text="Close" data-bs-dismiss="modal" CausesValidation="false" />

                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <%--MODAL PAYROLL END--%>

            <%--MDL CHECK PAYROLL--%>

            <div id="mdlCheck" role="dialog" class="modal fade show" data-bs-backdrop="false" data-bs-keyboard="false">
                <div class="modal-dialog modal-xl">
                    <!-- Modal content-->
                    <div class="modal-content" style="border-top-left-radius: 15px; border-top-right-radius: 15px;">
                        <asp:UpdatePanel ID="updatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="modal-header" style="border-top-left-radius: 10px; border-top-right-radius: 10px; background-color: steelblue; color: white; text-align: center; font-family: garamond; font-weight: bold; font-size: large;" runat="server" id="div2">
                                    <img src="Images/ledger.png" class="img-fluid" />
                                    <h3>
                                        <asp:Label runat="server" ID="Label2" Text="Payroll Ledger" ForeColor="White"></asp:Label></h3>

                                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                                    <span class="glyphicon glyphicon-alt-list"></span>
                                </div>
                                <div class="modal-body" style="padding: 10px 10px; border: solid 5px black;">

                                    <div class="row">
                                        <div class="col-md-12 mb-1">
                                            <div class="input-group">
                                                <span class="input-group-text border-secondary" style="background-color: white; color: black">Name</span>
                                                <asp:Label runat="server" ID="lblLedgerEmpName" CssClass="form-control border-secondary" Style="text-transform: uppercase"></asp:Label>
                                                <span class="input-group-text border-secondary" style="background-color: white; color: black">
                                                    <asp:UpdatePanel ID="updatePanel4" runat="server">
                                                        <ContentTemplate>
                                                            <asp:ImageButton runat="server" ID="ibLogs" ImageUrl="~/images/log.png"
                                                                ToolTip="Click to View logs" />
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </span>

                                            </div>
                                        </div>

                                    </div>

                                    <div class="row">
                                        <div class="col-md-12 mb-1">
                                            <div class="input-group">
                                                <span class="input-group-text border-secondary" style="background-color: white; color: black">Period</span>
                                                <asp:Label runat="server" ID="lblLedgerPeriod" CssClass="form-control border-secondary" Style="text-transform: uppercase"></asp:Label>
                                            </div>
                                        </div>

                                    </div>

                                    <div class="row">
                                        <div class="col-md-4 mb-1">
                                            <div class="input-group">
                                                <span class="input-group-text border-secondary" style="background-color: white; color: black">Reference#</span>
                                                <asp:Label runat="server" ID="lblLedgerRef" CssClass="form-control border-secondary" Style="text-transform: uppercase"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="col-md-4 mb-1">
                                            <div class="input-group">
                                                <span class="input-group-text border-secondary" style="background-color: white; color: black">Type</span>
                                                <asp:Label runat="server" ID="lblLedgerType" CssClass="form-control border-secondary" Style="text-transform: uppercase"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="col-md-4 mb-1">
                                            <div class="input-group">
                                                <span class="input-group-text border-secondary" style="background-color: white; color: black">Classification</span>
                                                <asp:Label runat="server" ID="lblLedgerClassification" CssClass="form-control border-secondary" Style="text-transform: uppercase"></asp:Label>
                                            </div>
                                        </div>

                                    </div>

                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:GridView runat="server" ID="_gvCheck" HeaderStyle-Font-Size="14px" CssClass="gridviewBlue"
                                                ShowFooter="false" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                AutoGenerateColumns="false" GridLines="None" Font-Names="Arial" Font-Size="14px"
                                                ForeColor="#000000" AllowPaging="false" PageSize="20" OnRowDataBound="OnRowDataBound">
                                                <Columns>
                                                    <asp:BoundField DataField="department" HeaderText="Dept Code" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="center" />

                                                    <asp:BoundField DataField="salary" HeaderText="Salary" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N}" />
                                                    <asp:TemplateField HeaderStyle-Width="15%" HeaderText="Additional Income" ItemStyle-VerticalAlign="Top">
                                                        <ItemTemplate>

                                                            <asp:GridView runat="server" ID="_gvAddIncome" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="black" CssClass="gridviewGray table-bordered table-responsive " PageSize="15" EmptyDataText=""
                                                                PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="" AutoGenerateColumns="false" AllowSorting="false"
                                                                GridLines="None" Font-Names="Arial" Font-Size="14px" ForeColor="#000000" AllowPaging="false">
                                                                <Columns>
                                                                    <asp:BoundField DataField="cd_name" HeaderText="Additional Income" HeaderStyle-Width="50%" />
                                                                    <asp:BoundField DataField="cd_amount" HeaderText="Amount" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N}" />

                                                                </Columns>
                                                            </asp:GridView>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="gross_amount" HeaderText="Gross Amount" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N}" />

                                                    <asp:TemplateField HeaderStyle-Width="17%" HeaderText="Deductions" ItemStyle-VerticalAlign="Top">
                                                        <ItemTemplate>

                                                            <div class="row">
                                                                <div class="col-lg-12 mb-2">
                                                                    <asp:GridView runat="server" ID="_gvFixDeductions" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="black" CssClass="gridviewGray table-bordered table-responsive " PageSize="15" EmptyDataText=""
                                                                        PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="false" AllowSorting="false"
                                                                        GridLines="None" Font-Names="Arial" Font-Size="14px" ForeColor="#000000" AllowPaging="false">
                                                                        <Columns>
                                                                            <asp:BoundField DataField="cd_name" HeaderText="Fix" HeaderStyle-Width="50%" />
                                                                            <asp:BoundField DataField="cd_amount" HeaderText="Pers. Share" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N}" />
                                                                            <asp:BoundField DataField="govShare" HeaderText="Gov. Share" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N}" />
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </div>
                                                            </div>

                                                            <div class="row">
                                                                <div class="col-lg-12 mb-2">
                                                                    <asp:GridView runat="server" ID="_gvFixDeductions2" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="black" CssClass="gridviewGray table-bordered table-responsive " PageSize="15" EmptyDataText=""
                                                                        PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="false" AllowSorting="false"
                                                                        GridLines="None" Font-Names="Arial" Font-Size="14px" ForeColor="#000000" AllowPaging="false">
                                                                        <Columns>
                                                                            <asp:BoundField DataField="cd_name" HeaderText="Fix" HeaderStyle-Width="50%" />
                                                                            <asp:BoundField DataField="cd_amount" HeaderText="Amount" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N}" />
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </div>
                                                            </div>


                                                            <div class="row">
                                                                <div class="col-lg-12">
                                                                    <asp:GridView runat="server" ID="_gvDeductions" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="black" CssClass="gridviewGray table-bordered table-responsive " PageSize="15" EmptyDataText=""
                                                                        PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="false" AllowSorting="false"
                                                                        GridLines="None" Font-Names="Arial" Font-Size="14px" ForeColor="#000000" AllowPaging="false">
                                                                        <Columns>
                                                                            <asp:BoundField DataField="cd_name" HeaderText="Other" HeaderStyle-Width="50%" />
                                                                            <asp:BoundField DataField="cd_amount" HeaderText="Amount" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N}" />

                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </div>
                                                            </div>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="net_amount" HeaderText="Net Amount" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N}" />

                                                    <asp:TemplateField HeaderStyle-Width="10%" HeaderText="Posted Date" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblPostedDate"></asp:Label><br />
                                                            <asp:Label runat="server" ID="lblPostedBy" Visible="false"></asp:Label>
                                                            <br />


                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>


                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>

            <asp:UpdatePanel ID="updatePanel6" runat="server">
                <ContentTemplate>

                    <%--LOGS--%>
                    <div id="pnlLogs" role="dialog" class="modal fade show" data-bs-backdrop="false" data-bs-keyboard="false">
                        <div class="modal-dialog modal-lg" style="width: 95%; border-top-left-radius: 2px; border-top-right-radius: 2px; border-color: blue;">
                            <!-- Modal content-->
                            <div class="modal-content" style="border-top-left-radius: 15px; border-top-right-radius: 15px;">
                                <div class="modal-header" style="border-top-left-radius: 10px; border-top-right-radius: 10px; background-color: lightblue; text-align: center; font-family: garamond; font-weight: bold; font-size: large; color: black" runat="server" id="div1">
                                    <img src="Images/log_head.png" class="img-fluid" />
                                    <h3>
                                        <asp:Label runat="server" ID="lblHeaderTitle" Text="Transaction Logs" ForeColor="Black"></asp:Label></h3>

                                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                                    <span class="glyphicon glyphicon-alt-list"></span>
                                </div>
                                <div class="modal-body" style="padding: 10px 10px; border: solid 5px black;">

                                    <div class="row">
                                        <div class="col-md-12 mb-1">
                                            <div class="input-group">
                                                <span class="input-group-text border-secondary" style="background-color: white; color: black">Period</span>
                                                <asp:Label runat="server" ID="lblLogPeriod" CssClass="form-control border-secondary" Style="text-transform: uppercase"></asp:Label>
                                            </div>
                                        </div>

                                    </div>

                                    <div class="row">
                                        <div class="col-md-4 mb-1">
                                            <div class="input-group">
                                                <span class="input-group-text border-secondary" style="background-color: white; color: black">Reference#</span>
                                                <asp:Label runat="server" ID="lblLogRef" CssClass="form-control border-secondary" Style="text-transform: uppercase"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="col-md-4 mb-1">
                                            <div class="input-group">
                                                <span class="input-group-text border-secondary" style="background-color: white; color: black">Type</span>
                                                <asp:Label runat="server" ID="lblLogType" CssClass="form-control border-secondary" Style="text-transform: uppercase"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="col-md-4 mb-1">
                                            <div class="input-group">
                                                <span class="input-group-text border-secondary" style="background-color: white; color: black">Classification</span>
                                                <asp:Label runat="server" ID="lblLogClassification" CssClass="form-control border-secondary" Style="text-transform: uppercase"></asp:Label>
                                            </div>
                                        </div>

                                    </div>

                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:GridView runat="server" ID="_gv" HeaderStyle-Font-Size="11px" CssClass="gridviewGray"
                                                ShowFooter="false" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                AutoGenerateColumns="false" GridLines="None" Font-Names="Arial" Font-Size="12px"
                                                ForeColor="#000000" AllowPaging="true" PageSize="20">
                                                <Columns>

                                                    <asp:BoundField DataField="trans_module" HeaderText="Type" HeaderStyle-Width="5%" HeaderStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="logged_changes" HeaderText="Logs" HeaderStyle-Width="80%" HeaderStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="trans_by" HeaderText="By" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />

                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>


                                </div>
                            </div>
                        </div>
                    </div>
                    <%--LOGS END--%>
                </ContentTemplate>
            </asp:UpdatePanel>



            <div>
                <uc3:sFooter ID="sFooter" runat="server" />
            </div>
        </div>


        <%--FORGOT PASSWORD--%>
        <div id="mdlForgotPassword" role="dialog" class="modal fade" data-bs-backdrop="false" data-bs-keyboard="false" style="z-index: 10">
            <div class="modal-dialog modal-md">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header bg-primary" style="text-align: center">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <asp:Label runat="server" ID="lblFP" Text="Retrieve Account" CssClass="badge text-dark" Font-Size="Large" ForeColor="Yellow"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body" style="padding: 10px 10px;">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>

                                <div class="row">
                                    <div class="col-md-12 mb-2">
                                        <div runat="server" id="pnlContact">
                                            <h4>Verify Contact Number</h4>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtContactNo" SetFocusOnError="true" Font-Bold="false" Font-Italic="true" Font-Size="10pt" Display="Dynamic" Text="Contact Number is Required" ValidationGroup="CEL" />
                                            <asp:TextBox runat="server" ID="txtContactNo" CssClass="form-control mb-3" TextMode="Number" MaxLength="11" placeholder="Contact Number" lenght="11" ValidationGroup="CEL"></asp:TextBox>
                                            <button runat="server" id="btnVerifyCel" type="button" class="btn btn-primary mb-3" validationgroup="CEL">Verify Contact Number <i class="bi bi-arrow-right"></i></button>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-12 mb-2">
                                        <div runat="server" id="pnlPin">
                                            <h4>Verification Code</h4>
                                            <p>
                                                Please type the verification code sent to
                                                <asp:Label runat="server" ID="lblPinContact"></asp:Label>
                                            </p>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtPinCode" SetFocusOnError="true" Font-Bold="false" Font-Italic="true" Font-Size="10pt" Display="Dynamic" Text="Code is Required" ValidationGroup="PIN" />
                                            <asp:TextBox runat="server" ID="txtPinCode" CssClass="form-control mb-3" TextMode="Number" MaxLength="6" placeholder="Payslip PIN" lenght="6" ValidationGroup="PIN"></asp:TextBox>
                                            <button runat="server" id="btnVerify" type="button" class="btn btn-primary mb-3" validationgroup="PIN">Verify PIN <i class="bi bi-arrow-right"></i></button>
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

                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="modal-footer ">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <%--   <asp:Button runat="server" ID="btnSaveMC" class="btn btn-success pull-right" Text="Save" ValidationGroup="DOC" CausesValidation="false" />
                                    <asp:Button runat="server" ID="btnSaveMCAll" class="btn btn-success pull-right" Text="Save to all" ValidationGroup="DOC" CausesValidation="false" />--%>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%--FORGOT PASSWORD END--%>

        <asp:UpdatePanel ID="updatePanel3" runat="server">
            <ContentTemplate>
                <asp:HiddenField runat="server" ID="hfPayrollId"></asp:HiddenField>
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

