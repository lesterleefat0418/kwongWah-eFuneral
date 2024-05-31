<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
	<meta name="viewport" content="width=device-width">
    <script src="https://use.fontawesome.com/3a2eaf6206.js"></script>
    <link rel="stylesheet" href="./css/style.css">
    <title>Image Upload Success</title>
    <style>
body, html {
    margin: 0;
    padding: 0;
    font-family: Arial, sans-serif;
    background-color: #f2f2f2;
}

/* Center the content */
body {
    display: flex;
    justify-content: center;
    align-items: center;
    height: 100vh;
}

/* Container styles */
.container {
    background-color: white;
    padding: 30px;
    border-radius: 10px;
    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
    text-align: center;
}

/* Heading styles */
h1 {
    color: #333;
    font-size: 24px;
    margin-bottom: 20px;
}

/* Paragraph styles */
p {
    color: #666;
    font-size: 16px;
    margin-bottom: 30px;
}

/* Button styles */
#Return {
    background-color: rgb(173 145 113);
    color: white;
    border: none;
    padding: 10px 20px;
    text-decoration: none;
    font-size: 16px;
    border-radius: 5px;
    cursor: pointer;
    transition: background-color 0.3s ease;
}

#Return:hover {
    background-color: #0056b3;
}
    </style>
</head>
<body>
    <?php
    // Your PHP code here
    echo '<p>照片成功上載.</p>';
    echo '<a href="./" style="text-decoration: none;"><button id="Return">Return to Home</button></a>';
    ?>
</body>
</html>