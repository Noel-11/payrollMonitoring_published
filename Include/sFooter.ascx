<%@ Control Language="VB" AutoEventWireup="false" CodeFile="sFooter.ascx.vb" Inherits="Include_sFooter" %>

<%--<link href="<%=ResolveClientUrl("~/Scripts/mycss/footerStyle.css")%>" rel="stylesheet" />--%>

<div id="fb-root"></div>
<script async defer crossorigin="anonymous" src="https://connect.facebook.net/en_LA/sdk.js#xfbml=1&version=v3.3"></script>


<style>
    .site-footer {
        background-color: #e7e7e7;
    }

    #more {
        display: none;
    }

    /*body {
        background-color: #e7e7e7 !important;
    }*/
</style>

<script>
    function myFunction() {
        var dots = document.getElementById("dots");
        var moreText = document.getElementById("more");
        var btnText = document.getElementById("myBtn");
        if (dots.style.display === "none") {
            dots.style.display = "inline";
            btnText.innerHTML = "Read more";
            moreText.style.display = "none";
        } else {
            dots.style.display = "none";
            btnText.innerHTML = "Read less";
            moreText.style.display = "inline";
        }
    }
</script>

<div class="container-fluid footer" style="background-color: #e7e7e7;">
    <div class="row">
        <div class="col-md-12 position-relative">
            <footer class="site-footer">
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-sm-12 col-md-6 mt-2">
                            <div class="client-info">
                                <%-- <img src="<%=ResolveClientUrl("~/Images/clenro.png")%>" alt="CLENRO Logo" width="50" />--%>

                                <h3 class="fs-5">PAYROLL MANAGEMENT SYSTEM</h3>
                                <br />
                                <p>
                                    The Payroll Management System serves as the primary tool for processing salary and additional income for both permanent and temporary personnel within the Cagayan de Oro City Government. <span id="dots" style="display: block">...</span>
                                    <span id="more">
                                        <%--<br />--%>
                                        &nbsp;
                                        It encompasses all stages from preparation to audit and booking, culminating in the generation of essential reports for remittances, bookkeeping, and the creation of payslips/paychecks.
                                        <br />
                                        <br />
                                        <strong>Developed by:</strong> CMISID Team - Zelfred Anthony T. Cocon / Noel S. Moreno
                                    </span>
                                </p>

                                <button type="button" id="myBtn" onclick="myFunction()">Read more</button>

                                <h6>CITY MANAGEMENT INFORMATION SYSTEMS AND INNOVATION DEPARTMENT</h6>
                                <p>
                                    <strong>Vision:</strong> A highly competitive Information and Communications Technology driven City Government.
                                    <br />
                                    <br />
                                    <strong>Mission:</strong> To serve the City Government of Cagayan de Oro by providing a reliable, accurate and efficient ICT solutions and services while continuously innovates technology to meet the needs of our clients and stakeholders.
                                </p>

                            </div>
                        </div>
                        <div class="col-xs-6 col-md-3 other-systems">
                            <h6>other Related Systems</h6>
                            <ul class="footer-links">

                                <li><a href="http://online.cagayandeoro.gov.ph/dtr" style="color: #5390ad;">Attendance Management System</a></li>
                                <li><a href="https://services.cagayandeoro.gov.ph:8087/payroll" style="color: #5390ad;">Payroll Management System</a></li>
                                <li><a href="https://services.cagayandeoro.gov.ph:8087/payslip" style="color: #5390ad;">View Payslip</a></li>

                                <br />
                                <li>If you have issues encountered and inquiries:<a href="https://services.cagayandeoro.gov.ph/helpdesk/" style="color: #5390ad;">
                                    <br />
                                    CMISID Helpdesk</a></li>
                            </ul>
                        </div>
                        <div class="col-xs-6 col-md-3 other-systems image-block">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="brand-container">
                                        <img class="w-70" src="assets/img/seallogo.png" style="float: left;">
                                    </div>
                                    <a href="https://cagayandeoro.gov.ph/" target="_blank" class='link' name='link' style="float: right;">Visit Official Website</a>
                                </div>
                                <div class="col-md-12">
                                    <div class="brand-container">
                                        <img src="assets/img/risebig.png">
                                    </div>
                                    <a href="https://cagayandeoro.gov.ph/index.php/news/the-city-mayor/rise1.html" target="_blank" class='link2' name='link2' style="float: right;">Learn RISE Platform</a>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12 separator"></div>
                    </div>
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-md-11 col-sm-6 col-xs-12">
                                <div class="d-flex justify-content-center">
                                    <div>
                                        <img src="assets/img/ict.png">
                                    </div>
                                </div>
                                <div class="d-flex justify-content-center">
                                    <div>
                                        <span style="padding-left: 10px;">Powered by: City Management Information Systems and Innovation Department</span><br>
                                    </div>
                                </div>
                                <div class="d-flex justify-content-center">
                                    <div>
                                        <span>&nbsp;&nbsp;<div class="fb-like fb_iframe_widget" data-href="https://www.facebook.com/City-Management-Information-Systems-Office-LGU-CdeO-568493593557935/" data-width="" data-layout="button_count" data-action="like" data-size="small" data-share="true" data-show-faces="false"></div>
                                        </span>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-1 col-sm-6 col-xs-12 version">
                                Version 1.0
                            </div>
                        </div>
                    </div>
                </div>
            </footer>
        </div>
    </div>
</div>

<script async defer crossorigin="anonymous" src="https://connect.facebook.net/en_LA/sdk.js#xfbml=1&version=v14.0" nonce="wcxFHnry"></script>
