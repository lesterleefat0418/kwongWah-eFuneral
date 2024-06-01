<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width">
    <link rel="stylesheet" href="./css/success.css">
    <title>Image Upload Success</title>
</head>
<body>
    <img class="logo" src="./image/logo_kwh.png" alt="Logo">
    <br><br>
    <div class="container">
        <?php
        // Your PHP code here
        echo '<div><h1>你的圖片已上載!</h1></div>';
        echo '<div><p>可按返回重新上載</p></div>';
        echo '<div><button class="home-button" onclick="goToHome()" ontouchstart="goToHome()">返回</button></div>';
        ?>
    </div>

    <script>
        function goToHome() {
            window.location.href = './'; // Replace with the actual path to your home page
        }
    </script>
</body>
</html>