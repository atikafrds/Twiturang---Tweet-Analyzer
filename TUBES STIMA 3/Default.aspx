<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TUBES_STIMA_3.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    
    <meta name="viewport" content="width=dev<a href='favicon.ico'>favicon.ico</a>ice-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>Twiturang - Social Platform for Bandung</title>

    <!-- Bootstrap Core CSS -->
    <link href="css/bootstrap.min.css" rel="stylesheet">

    <!-- Custom CSS -->
    <link href="css/agency.css" rel="stylesheet">

    <!-- Custom Fonts -->
    <link href="font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css">
    <link href="https://fonts.googleapis.com/css?family=Montserrat:400,700" rel="stylesheet" type="text/css">
    <link href='https://fonts.googleapis.com/css?family=Kaushan+Script' rel='stylesheet' type='text/css'>
    <link href='https://fonts.googleapis.com/css?family=Droid+Serif:400,700,400italic,700italic' rel='stylesheet' type='text/css'>
    <link href='https://fonts.googleapis.com/css?family=Roboto+Slab:400,100,300,700' rel='stylesheet' type='text/css'>

</head>

<body id="page-top" class="index">
     <form id="form2" runat="server">
    <!-- Navigation -->
    <nav class="navbar navbar-default navbar-fixed-top">
        <div class="container">
            <!-- Brand and toggle get grouped for better mobile display -->
            <div class="navbar-header page-scroll">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1">
                    <span class="sr-only">Toggle navigation</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <a class="navbar-brand page-scroll" href="#page-top">Twiturang</a>
            </div>

            <!-- Collect the nav links, forms, and other content for toggling -->
            <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
                <ul class="nav navbar-nav navbar-right">
                    <li class="hidden">
                        <a href="#page-top"></a>
                    </li>
                    <li>
                        <a class="page-scroll" href="#services">Services</a>
                    </li>
                    
                
                    <li>
                        <a class="page-scroll" href="#Launch">Launch</a>
                    </li>
                    <asp:Literal ID="Literal5" runat="server" />
                    <li>
                        <a class="page-scroll" href="#team">Team</a>
                    </li>
                </ul>
            </div>
        </div>
    </nav>

    <!-- Header -->
    <header>
        <div class="container">
            <div class="intro-text">
                <img src="img/twiturang-logo.png" alt ="Twitturang" class="img-responsive img-centered" style="width:10%;height:auto;opacity:0.9;" />
                <div class="intro-lead-in">Social Platform for Bandung</div>
                <div class="intro-heading">Twiturang</div>
                
                <asp:Literal ID="Literal4" runat="server" />
            </div>
        </div>
    </header>

    <!-- Services Section -->
    <section id="services">
        <div class="container">
            <div class="row">
                <div class="col-lg-12 text-center">
                    <h2 class="section-heading">Our Services</h2>
                    <h3 class="section-subheading text-muted">We give you very special services</h3>
                </div>
            </div>
            <div class="row text-center">
                <div class="col-md-4">
                    <span class="fa-stack fa-4x">
                        <i class="fa fa-circle fa-stack-2x text-primary"></i>
                        <i class="fa fa-tachometer fa-stack-1x fa-inverse"></i>
                    </span>
                    <h4 class="service-heading">Fast</h4>
                    <p class="text-muted">Providing you actual information in fast service.</p>
                </div>
                <div class="col-md-4">
                    <span class="fa-stack fa-4x">
                        <i class="fa fa-circle fa-stack-2x text-primary"></i>
                        <i class="fa fa-laptop fa-stack-1x fa-inverse"></i>
                    </span>
                    <h4 class="service-heading">Responsive Design</h4>
                    <p class="text-muted">Working in every device.</p>
                </div>
                <div class="col-md-4">
                    <span class="fa-stack fa-4x">
                        <i class="fa fa-circle fa-stack-2x text-primary"></i>
                        <i class="fa fa-twitter fa-stack-1x fa-inverse"></i>
                    </span>
                    <h4 class="service-heading">Actual Tweets</h4>
                    <p class="text-muted">Giving informations based on last updated tweets.</p>
                </div>
                
            </div>
        </div>
    </section>

    <!-- Launch Section -->
    <section id="Launch" class ="bg-darkest-gray">
        <div class="container">
            <font color='white'>
            <div class="row">
                <div class="col-lg-12 text-center">
                    <h2 class="section-heading">Launch The Project</h2>
                    <h3 class="section-subheading text-muted">Insert Twitter Keyword</h3>
                </div>
            </div>
            <div class="row">
                <!-- Textbox untuk menerima input keyword tweets -->
                <div class="col-sm-offset-4 col-sm-4">
                        <h4>Keyword Twitter</h4>
                      <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control input-lg" placeholder="contoh: Ridwan Kamil"></asp:TextBox>
                      <p style="margin-top:5px;">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"  ErrorMessage="* Required Field" CssClass="text-danger" ControlToValidate="TextBox1" Display="Dynamic"></asp:RequiredFieldValidator>
                      </p>
                </div>
                <br />
                <br />
                <!-- Textbox untuk menerima input keyword DBMP Bandung -->
                <div class="col-sm-offset-4 col-sm-4">
                        <h4>Keyword DBMP Bandung</h4>
                      <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control input-lg" placeholder="contoh: Taman;Sungai;Trotoar;"></asp:TextBox>
                      <p style="margin-top:5px;">
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"  ErrorMessage="* Required Field" CssClass="text-danger" ControlToValidate="TextBox2" Display="Dynamic"></asp:RequiredFieldValidator>
                      </p>
                </div>
                <br />
                <br />
                <!-- Textbox untuk menerima input keyword Dinas Pendidikan -->
                <div class="col-sm-offset-4 col-sm-4">
                        <h4>Keyword Dinas Pendidikan</h4>
                      <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control input-lg" placeholder="contoh: Sekolah;Siswa;"></asp:TextBox>
                      <p style="margin-top:5px;">
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"  ErrorMessage="* Required Field" CssClass="text-danger" ControlToValidate="TextBox2" Display="Dynamic"></asp:RequiredFieldValidator>
                      </p>
                </div>
                <br />
                <br />
                <!-- Textbox untuk menerima input keyword Dinas Kebakaran -->
                <div class="col-sm-offset-4 col-sm-4">
                        <h4>Keyword Dinas Kebakaran</h4>
                      <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control input-lg" placeholder="contoh: Kebakaran;Bencana;"></asp:TextBox>
                      <p style="margin-top:5px;">
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"  ErrorMessage="* Required Field" CssClass="text-danger" ControlToValidate="TextBox2" Display="Dynamic"></asp:RequiredFieldValidator>
                      </p>
                </div>
                <br />
                <br />
                <!-- Textbox untuk menerima input keyword PDAM Bandung -->
                <div class="col-sm-offset-4 col-sm-4">
                        <h4>Keyword PDAM Kota Bandung</h4>
                      <asp:TextBox ID="TextBox5" runat="server" CssClass="form-control input-lg" placeholder="contoh: Air;Pipa;"></asp:TextBox>
                      <p style="margin-top:5px;">
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"  ErrorMessage="* Required Field" CssClass="text-danger" ControlToValidate="TextBox2" Display="Dynamic"></asp:RequiredFieldValidator>
                      </p>
                </div>
                <br />
                <br />
                <!-- Textbox untuk menerima input keyword Dinas Perhubungan -->
                <div class="col-sm-offset-4 col-sm-4">
                        <h4>Keyword Dinas Perhubungan</h4>
                      <asp:TextBox ID="TextBox6" runat="server" CssClass="form-control input-lg" placeholder="contoh: Lalu lintas;Jalan;"></asp:TextBox>
                      <p style="margin-top:5px;">
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"  ErrorMessage="* Required Field" CssClass="text-danger" ControlToValidate="TextBox2" Display="Dynamic"></asp:RequiredFieldValidator>
                      </p>
                </div>
                <br />
                <br />
                <div class="col-sm-offset-4 col-sm-4">
                    <p>
                <div class="btn-group" data-toggle="buttons">
                    <!-- Button untuk memilih metode string matching: Knuth-Morris-Pratt -->
                    <label class="btn btn-warning">
                        <asp:RadioButton runat="server" id="radioB1" GroupName="radioB" AutoPostBack="True"/>
                        Knuth – Morris – Pratt               
                    </label>
                    <!-- Textbox untuk memilih metode string matching: Boyer Moore Algorithm -->
                    <label class="btn btn-warning">
                        <asp:RadioButton runat="server" id="radioB2" GroupName="radioB" AutoPostBack="True" />
                        Boyer Moore Algorithm
                    </label>
                </div>
                        </p>
                    <br />
                </div>
                <br />
                <div class="col-sm-offset-4 col-sm-4">
                      <!-- Button untuk melakukan proses string matching yang terdapat pada prosedur Run_Click() -->
                      <asp:Button ID="Run" runat="server" Text="Analyze" CssClass="btn-primary btn btn-block"  OnClick="Run_Click"/>
                      <br />
                      <br />
                         
                     
                </div>
                <div class ="col-sm-offset-2 col-sm-8 ">
                    
                <asp:Literal ID="Literal1" runat="server" />
               
               
                </div>    
            </div>
            </font>
        </div>

    </section>
    
        <asp:Literal ID="Literal2" runat="server" />

    <!-- Team Section -->
    <section id="team" class="bg-darkest-gray">
        <div class="container">
            <font color='white'> 
            <div class="row">
                <div class="col-lg-12 text-center">
                    <h2 class="section-heading">Our Amazing Team</h2>
                    <h3 class="section-subheading text-muted">People behind this awesome project.</h3>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-4">
                    <div class="team-member">
                        <img src="img/team/1.jpg" class="img-responsive img-circle" alt="" style="width:70%;height:auto">
                        <h4>Ari Pratama Zhorifiandi</h4>
                        <p class="text-muted">Programmer</p>
                        <ul class="list-inline social-buttons">
                            <li><a href="https://twitter.com/arizhopratama" target="_blank"><i class="fa fa-twitter"></i></a>
                            </li>
                            <li><a href="https://www.facebook.com/arizhopratama" target="_blank"><i class="fa fa-facebook"></i></a>
                            </li>
                            <li><a href="https://plus.google.com/117874164476873286610/" target="_blank"><i class="fa fa-google-plus"></i></a>
                            </li>
                        </ul>
                    </div>
                </div>
                <div class="col-sm-4">
                    <div class="team-member">
                        <img src="img/team/2.jpg" class="img-responsive img-circle" alt="" style="width:70%;height:auto">
                        <h4>Atika Firdaus</h4>
                        <p class="text-muted">Programmer</p>
                        <ul class="list-inline social-buttons">
                            <li><a href="https://twitter.com/atikafrds" target="_blank"><i class="fa fa-twitter"></i></a>
                            </li>
                            <li><a href="https://www.facebook.com/atikafrds" target="_blank"><i class="fa fa-facebook"></i></a>
                            </li>
                            <li><a href="https://plus.google.com/114343977574383325637/" target="_blank"><i class="fa fa-google-plus"></i></a>
                            </li>
                        </ul>
                    </div>
                </div>
                <div class="col-sm-4">
                    <div class="team-member">
                        <img src="img/team/3.jpg" class="img-responsive img-circle" alt="" style="width:70%;height:auto">
                        <h4>Anwar Ramadha</h4>
                        <p class="text-muted">Programmer</p>
                        <ul class="list-inline social-buttons">
                            <li><a href="https://twitter.com/AnwarRamadha" target="_blank"><i class="fa fa-twitter"></i></a>
                            </li>
                            <li><a href="https://www.facebook.com/anwar.ramadha" target="_blank"><i class="fa fa-facebook"></i></a>
                            </li>
                            <li><a href="https://plus.google.com/100815411808297500579/" target="_blank"><i class="fa fa-google-plus"></i></a>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-8 col-lg-offset-2 text-center">
                     </div>
            </div>
      </font> 
       </div>
    </section>
    
    <footer>
        <div class="container">
            <div class="row">
                <div class="col-md-4">
                    <span class="copyright">Copyright &copy; Twiturang 2016</span>
                </div>
                <div class="col-md-4">
                    <ul class="list-inline social-buttons">
                        <li><a href="https://www.twitter.com/arizhopratama" target="_blank"><i class="fa fa-twitter"></i></a>
                        </li>
                        <li><a href="https://www.facebook.com/anwar.ramadha" target="_blank"><i class="fa fa-facebook"></i></a>
                        </li>
                        <li><a href="https://plus.google.com/114343977574383325637/" target="_blank"><i class="fa fa-google-plus"></i></a>
                        </li>
                    </ul>
                </div>
                <div class="col-md-4">
                    <ul class="list-inline quicklinks">
                        <li><a href="http://www.itb.ac.id" target="_blank">Institut Teknologi Bandung</a>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    </footer>

    <!-- Portfolio Modals -->
    <!-- Page untuk menampilkan hasil pengelompokan pada Dinas Bina Marga dan Pengairan Kota Bandung -->
    <div class="portfolio-modal modal fade" id="portfolioModal1" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-content">
            <div class="close-modal" data-dismiss="modal">
                <div class="lr">
                    <div class="rl">
                    </div>
                </div>
            </div>
            <div class="container">
                <div class="row">
                    <div class="col-lg-8 col-lg-offset-2">
                        <div class="modal-body">
                            <!-- Project Details Go Here -->
                            <h2>Dinas Bina Marga dan Pengairan</h2>
                            <p class="item-intro text-muted">Tweet dari masyarakat untuk Dinas Bina Marga dan Pengairan Kota Bandung</p>
                            <asp:Literal ID="Literal3" runat="server" />
                            <button type="button" class="btn btn-primary" data-dismiss="modal"><i class="fa fa-times"></i> Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Page untuk menampilkan hasil pengelompokan pada Dinas Pendidikan Kota Bandung -->
    <div class="portfolio-modal modal fade" id="portfolioModal2" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-content">
            <div class="close-modal" data-dismiss="modal">
                <div class="lr">
                    <div class="rl">
                    </div>
                </div>
            </div>
            <div class="container">
                <div class="row">
                    <div class="col-lg-8 col-lg-offset-2">
                        <div class="modal-body">
                             <h2>Dinas Pendidikan</h2>
                            <p class="item-intro text-muted">Tweet dari masyarakat untuk Dinas Pendidikan Kota Bandung</p>
                            <asp:Literal ID="Literal6" runat="server" />
                            
                            <button type="button" class="btn btn-primary" data-dismiss="modal"><i class="fa fa-times"></i> Close</button>
                       </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Page untuk menampilkan hasil pengelompokan pada Dinas Kebakaran Kota Bandung -->    
    <div class="portfolio-modal modal fade" id="portfolioModal3" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-content">
            <div class="close-modal" data-dismiss="modal">
                <div class="lr">
                    <div class="rl">
                    </div>
                </div>
            </div>
            <div class="container">
                <div class="row">
                    <div class="col-lg-8 col-lg-offset-2">
                        <div class="modal-body">
                             <h2>Dinas Kebakaran</h2>
                            <p class="item-intro text-muted">Tweet dari masyarakat untuk Dinas Kebakaran Kota Bandung</p>
                            <asp:Literal ID="Literal9" runat="server" />
                            
                            <button type="button" class="btn btn-primary" data-dismiss="modal"><i class="fa fa-times"></i> Close</button>
                       </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Page untuk menampilkan hasil pengelompokan pada PDAM Kota Bandung -->
    <div class="portfolio-modal modal fade" id="portfolioModal4" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-content">
            <div class="close-modal" data-dismiss="modal">
                <div class="lr">
                    <div class="rl">
                    </div>
                </div>
            </div>
            <div class="container">
                <div class="row">
                    <div class="col-lg-8 col-lg-offset-2">
                        <div class="modal-body">
                             <h2>PDAM</h2>
                            <p class="item-intro text-muted">Tweet dari masyarakat untuk PDAM Kota Bandung</p>
                            <asp:Literal ID="Literal10" runat="server" />
                            
                            <button type="button" class="btn btn-primary" data-dismiss="modal"><i class="fa fa-times"></i> Close</button>
                       </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Page untuk menampilkan hasil pengelompokan pada Dinas Perhubungan Kota Bandung -->    
    <div class="portfolio-modal modal fade" id="portfolioModal5" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-content">
            <div class="close-modal" data-dismiss="modal">
                <div class="lr">
                    <div class="rl">
                    </div>
                </div>
            </div>
            <div class="container">
                <div class="row">
                    <div class="col-lg-8 col-lg-offset-2">
                        <div class="modal-body">
                             <h2>Dinas Perhubungan</h2>
                            <p class="item-intro text-muted">Tweet dari masyarakat untuk Dinas Perhubungan Kota Bandung</p>
                            <asp:Literal ID="Literal8" runat="server" />
                            
                            <button type="button" class="btn btn-primary" data-dismiss="modal"><i class="fa fa-times"></i> Close</button>
                       </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Page untuk menampilkan hasil yang tidak masuk pada kategori apapun -->    
    <div class="portfolio-modal modal fade" id="portfolioModal6" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-content">
            <div class="close-modal" data-dismiss="modal">
                <div class="lr">
                    <div class="rl">
                    </div>
                </div>
            </div>
            <div class="container">
                <div class="row">
                    <div class="col-lg-8 col-lg-offset-2">
                        <div class="modal-body">
                            <!-- Project Details Go Here -->
                        <h2>Unknown</h2>
                            <p class="item-intro text-muted">Tweet dari masyarakat yang tidak ditemukan departemen yang cocok</p>
                            <asp:Literal ID="Literal7" runat="server" />
                                       
                            <button type="button" class="btn btn-primary" data-dismiss="modal"><i class="fa fa-times"></i> Close</button>
                      </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- jQuery -->
    <script src="js/jquery.js"></script>

    <!-- Bootstrap Core JavaScript -->
    <script src="js/bootstrap.min.js"></script>

    <!-- Plugin JavaScript -->
    <script src="jquery.easing.min.js"></script>
    <script src="js/classie.js"></script>
    <script src="js/cbpAnimatedHeader.js"></script>

    <!-- Contact Form JavaScript -->
    <script src="js/jqBootstrapValidation.js"></script>
    <script src="js/contact_me.js"></script>

    <!-- Custom Theme JavaScript -->
    <script src="js/agency.js"></script>

    <script>
        $('.modal').on('hidden.bs.modal', function (e) {
            if ($('.modal').hasClass('in')) {
                $('body').addClass('modal-open');
            }
        });
    </script>

</form>
</body>

</html>