<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>22 лучших формы входа и регистрации | Vladmaxi.net</title>
    <link rel="stylesheet" href="<?=\config\DbConfig::$config['public_url']?>stylesheets/style.css">
    <link rel="icon" href="http://vladmaxi.net/favicon.ico" type="image/x-icon">
    <link rel="shortcut icon" href="http://vladmaxi.net/favicon.ico" type="image/x-icon">
    <script type="application/javascript" src="<?=\config\DbConfig::$config["public_url"]?>javascripts/jquery-3.3.1.min.js"></script>
    <script type="application/javascript" src="<?=\config\DbConfig::$config["public_url"]?>javascripts/glm-ajax.js"></script>
    <script type="application/javascript" src="<?=\config\DbConfig::$config["public_url"]?>javascripts/public.js"></script>
</head>

<body>

<form id="login" method="post">
    <h1>Форма входа</h1>
    <fieldset id="inputs">
        <input id="username" name="login" type="text" placeholder="Логин" value="admin@domain.com" autofocus required>
        <input id="password" name="password" type="password" placeholder="Пароль" value="QwertY123@" required>
    </fieldset>
    <fieldset id="actions">
        <input type="submit" id="submit" value="ВОЙТИ">
        <!--a href="">Забыли пароль?</a--><a href="">Регистрация</a>
    </fieldset>
</form>
<script type="application/javascript" src="<?=\config\DbConfig::$config["public_url"]?>javascripts/views/account/login.js"></script>
</body>
</html>
