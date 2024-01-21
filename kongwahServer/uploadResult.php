<?php
date_default_timezone_set("Asia/Hong_Kong");
$serverIp = '192.168.0.2';
$folderName = date("Ymd");
$directory = __DIR__."/uploads/results/".$folderName;

if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    // Check if an image file was uploaded
    if (isset($_FILES['file']) && $_FILES['file']['error'] === UPLOAD_ERR_OK) {
        // Debug information
        echo 'File name: ' . $_FILES['file']['name'] . '<br>';
        echo 'File type: ' . $_FILES['file']['type'] . '<br>';
        echo 'File size: ' . $_FILES['file']['size'] . '<br>';

        // Create the directory if it doesn't exist
        if (!is_dir($directory)) {
            mkdir($directory, 755, true);
        }

        // Generate a unique filename for the uploaded image
        $filename = uniqid() . '_' . $_FILES['file']['name'];

        // Destination path for the uploaded file
        $destination = $directory . '/' . $filename;

        // Move the uploaded file to the destination folder
        if (move_uploaded_file($_FILES['file']['tmp_name'], $destination)) {
            // Construct the full URL with the IP address and destination path
            $url = 'http://' . $serverIp . substr($_SERVER['PHP_SELF'], 0, strrpos($_SERVER['PHP_SELF'], '/')) . '/uploads/results/' . $folderName . '/' . $filename;

            // Return the full URL as the response
            echo $url;
        } else {
            // Return an error message if the file couldn't be moved
            echo 'Error moving the uploaded file.';
        }
    } else {
        // Debug information
        echo 'Error code: ' . $_FILES['file']['error'] . '<br>';

        // Return an error message if no image file was uploaded
        echo 'No image file uploaded.';
    }
} else {
    // Return an error message for invalid request method
    echo 'Invalid request method.';
}
?>