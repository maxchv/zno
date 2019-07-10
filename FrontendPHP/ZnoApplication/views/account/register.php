<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Zno Osvita Ua</title>
    <link rel="stylesheet" href="<?=\config\DbConfig::$config['public_url']?>stylesheets/style.css">
    <link rel="icon" href="http://vladmaxi.net/favicon.ico" type="image/x-icon">
    <link rel="shortcut icon" href="http://vladmaxi.net/favicon.ico" type="image/x-icon">
    <script type="application/javascript" src="<?=\config\DbConfig::$config["public_url"]?>javascripts/jquery-3.3.1.min.js"></script>
    <script type="application/javascript" src="<?=\config\DbConfig::$config["public_url"]?>javascripts/glm-ajax.js"></script>
    <script type="application/javascript" src="<?=\config\DbConfig::$config["public_url"]?>javascripts/public.js"></script>
</head>

<body>

<form id="register" method="post">
    <h1>Register</h1>
    <fieldset id="inputs">
        <input id="phone" name="phone" type="tel" placeholder="Phone" value="+380123456789" autofocus required><br/>
        <input id="password" name="password" type="password" placeholder="Password" value="QwertY123@" required><br/>
        <input id="confirmPassword" name="confirmPassword" type="password" placeholder="Confirm Password" value="QwertY123@" required><br/>
        <input id="email" name="email" type="email" placeholder="Email" value="mail@domen.ua" required>
    </fieldset>
    <div class="errors">

    </div>
    <fieldset id="actions">
        <input type="submit" id="submit" value="REGISTER">
        <!--a href="">Забыли пароль?</a--><a href="<?=Application::getUrl("account", "login")?>">Вход</a>
    </fieldset>
</form>
<script type="application/javascript" src="<?=\config\DbConfig::$config["public_url"]?>javascripts/views/account/register.js"></script>
</body>
</html>
