<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
	<meta name="viewport" content="width=device-width">
    <script src="https://use.fontawesome.com/3a2eaf6206.js"></script>
    <title>Image Upload Success</title>
    <style>
        body {
            display: flex;
            flex-direction: column;
            align-items: center;
            justify-content: center;
            height: 100vh;
        }

        p {
            text-align: center;
            font-size: 1.2rem;
        }

        button {
            padding: 10px 20px;
            font-size: 1.2rem;
        }
    </style>
</head>
<body>
    <?php
    // Your PHP code here
    echo '<p>Image uploaded successfully</p>';
    echo '<a href="./" style="text-decoration: none;"><button>Return to Home</button></a>';
    ?>
</body>
</html>