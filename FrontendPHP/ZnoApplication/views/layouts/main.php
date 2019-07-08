<html lang="en">

<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <!-- The above 3 meta tags *must* come first in the head; any other head content must come *after* these tags-->
    <title>E-SHOP HTML Template</title>
    <!-- Google font-->
    <link href="https://fonts.googleapis.com/css?family=Hind:400,700" rel="stylesheet" />
    <!-- Bootstrap-->
    <link type="text/css" rel="stylesheet" href="<?=PUBLIC_URL?>site/css/bootstrap.min.css" />
    <!-- Slick-->
    <link type="text/css" rel="stylesheet" href="<?=PUBLIC_URL?>site/css/slick.css" />
    <link type="text/css" rel="stylesheet" href="<?=PUBLIC_URL?>site/css/slick-theme.css" />
    <!-- nouislider-->
    <link type="text/css" rel="stylesheet" href="<?=PUBLIC_URL?>site/css/nouislider.min.css" />
    <!-- Font Awesome Icon-->
    <link rel="stylesheet" href="<?=PUBLIC_URL?>site/css/font-awesome.min.css" />
    <!-- Custom stlylesheet-->
    <link type="text/css" rel="stylesheet" href="<?=PUBLIC_URL?>site/css/style.css" />
    <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries-->
    <!-- WARNING: Respond.js doesn't work if you view the page via file://-->
    <!--if lt IE 9script(src='https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js')
script(src='https://oss.maxcdn.com/respond/1.4.2/respond.min.js')-->
</head>

<body>
<header>
    <!-- header-->
    <div id="header">
        <div class="container">
            <div class="pull-left">
                <!-- Logo-->
                <div class="header-logo"><a class="logo" href="#"><img src="<?=PUBLIC_URL?>site/img/logo.png" alt=""/></a></div>
                <!-- /Logo-->
                <!-- Search-->
                <div class="header-search">
                    <!-- <form>-->
                    <!-- <input class="input search-input" type="text" placeholder="Enter your keyword">-->
                    <!-- <select class="input search-categories">-->
                    <!-- <option value="0">All Categories</option>-->
                    <!-- <option value="1">Category 01</option>-->
                    <!-- <option value="1">Category 02</option>-->
                    <!-- </select>-->
                    <!-- <button class="search-btn"><i class="fa fa-search"></i></button>-->
                    <!-- </form>-->
                    <!-- /Search-->
                </div>
            </div>
            <div class="pull-right"></div>
        </div>
        <!-- header-->
        <!-- container-->
    </div>
</header>
<div id="navigation">
    <!-- container-->
    <div class="container">
        <div id="responsive-nav">
            <!-- category nav-->
            <div class="category-nav show-on-click"><span class="category-header">Categories <i class="fa fa-list"></i></span>
                <ul class="category-list">
                    <?= /** @var \models\db\mysql\tables\Category[] $categories */
                    \models\widjets\CategoryWidjet::render($categories)?>
                </ul>
            </div>
            <!-- /category nav-->
            <!-- menu nav-->
            <div class="menu-nav"><span class="menu-header">Menu <i class="fa fa-bars"></i></span></div>
            <!-- menu nav-->
            <!-- /container-->
        </div>
    </div>
</div>
<div class="section">
    <div class="container">
        <?php /** @var string $content */
        include_once $content?>
    </div>
</div>
<footer class="section section-grey" id="footer">
    <!-- container-->
    <div class="container">
        <!-- row-->
        <div class="row">
            <!-- footer widget-->
            <div class="col-md-3 col-sm-6 col-xs-6">
                <div class="footer">
                    <!-- footer logo-->
                    <div class="footer-logo"><a class="logo" href="#"><img src="<?=PUBLIC_URL?>site/img/logo.png" alt=""/></a></div>
                    <!-- /footer logo-->
                    <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna</p>
                    <!-- footer social-->
                    <ul class="footer-social">
                        <li><a href="#"><i class="fa fa-facebook"></i></a></li>
                        <li><a href="#"><i class="fa fa-twitter"></i></a></li>
                        <li><a href="#"><i class="fa fa-instagram"></i></a></li>
                        <li><a href="#"><i class="fa fa-google-plus"></i></a></li>
                        <li><a href="#"><i class="fa fa-pinterest"></i></a></li>
                    </ul>
                    <!-- /footer social-->
                    <!-- /footer widget-->
                    <!-- /row-->
                </div>
            </div>
            <hr/>
            <div class="row">
                <div class="col-md-8 col-md-offset-2 text-center">
                    <div class="footer-copyright">Copyright ©
                        <script>
                            document.write(new Date().getFullYear());
                        </script>2019 All rights reserved | This template is made with <i class="fa fa-heart-o" aria-hidden="true"></i> by <a href="https://colorlib.com" target="_blank">Colorlib</a></div>
                </div>
            </div>
        </div>
    </div>
</footer>
<script src="<?=PUBLIC_URL?>site/js/jquery.min.js"></script>
<script src="<?=PUBLIC_URL?>site/js/bootstrap.min.js"></script>
<!-- <script src="<?=PUBLIC_URL?>site/js/slick.min.js"></script> -->
<script src="<?=PUBLIC_URL?>site/js/nouislider.min.js"></script>
<script src="<?=PUBLIC_URL?>site/js/jquery.zoom.min.js"></script>
<script src="<?=PUBLIC_URL?>site/js/main.js"></script>
</body>

</html>