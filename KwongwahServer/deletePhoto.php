<?php
$uploadRootFolder = 'uploads/people/';

if (isset($_GET['filename'])) {
    $filename = basename($_GET['filename']); // Sanitize the filename
    $dateFolder = date('Ymd');
    $filePath = $uploadRootFolder . $dateFolder . '/' . $filename;

    if (file_exists($filePath)) {
        unlink($filePath);
        echo "Image deleted successfully.";
    } else {
        echo "Image not found.";
    }
} else {
    echo "No filename provided.";
}
?>