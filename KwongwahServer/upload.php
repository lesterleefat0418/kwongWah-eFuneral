<?php

include 'success.php';

if ($_FILES['image']['error'] === UPLOAD_ERR_OK) {
    $file = $_FILES['image']['tmp_name'];
    $originalFilename = $_FILES['image']['name'];

    // Get the current date in the format 'yyyymmdd'
    $dateFolder = date('Ymd');

    $uniqueCode = uniqid('', true);

    // Construct the new file name
    $newFileName = date('YmdHis') . '-' . $uniqueCode . '.' . pathinfo($originalFilename, PATHINFO_EXTENSION);

    // Destination path for the uploaded file
    $destination = 'uploads/people/' . $dateFolder . '/' . $newFileName;

    // Set the desired maximum width and height for the resized image
    $maxWidth = 1920;
    $maxHeight = 1920;

    // Load the original image
    $image = imagecreatefromstring(file_get_contents($file));

    // Extract EXIF data to get the image orientation
    //$exif = exif_read_data($file);
    //$orientation = isset($exif['Orientation']) ? $exif['Orientation'] : 1;
    $imageInfo = getimagesize($file);
    $orientation = $imageInfo[2];

    switch ($orientation) {
        case IMAGETYPE_JPEG:
        case IMAGETYPE_JPEG2000:
            if (function_exists('exif_read_data')) {
                $exif =  @exif_read_data($file);
                $orientation = isset($exif['Orientation']) ? $exif['Orientation'] : 1;
            } else {
                $orientation = 1;
            }
            break;
        case IMAGETYPE_TIFF_II:
        case IMAGETYPE_TIFF_MM:
            if (function_exists('exif_read_data')) {
                $exif = exif_read_data($file);
                $orientation = isset($exif['Orientation']) ? $exif['Orientation'] : 1;
            } else {
                $orientation = 1;
            }
            break;
        default:
            $orientation = 1;
            break;
    }

    // Adjust the image orientation if needed
    switch ($orientation) {
        case 3:
            $image = imagerotate($image, 180, 0);
            break;
        case 6:
            $image = imagerotate($image, -90, 0);
            break;
        case 8:
            $image = imagerotate($image, 90, 0);
            break;
    }

    // Get the original image's dimensions after orientation adjustment
    $originalWidth = imagesx($image);
    $originalHeight = imagesy($image);

    // Calculate the new dimensions while maintaining the aspect ratio
    $ratio = min($maxWidth / $originalWidth, $maxHeight / $originalHeight);
    $newWidth = $originalWidth * $ratio;
    $newHeight = $originalHeight * $ratio;

    // Create the date folder if it doesn't exist
    if (!is_dir('uploads/people/' . $dateFolder)) {
        mkdir('uploads/people/' . $dateFolder, 0777, true);
    }

    // Create a new blank image with the desired dimensions
    $resizedImage = imagecreatetruecolor($newWidth, $newHeight);

    // Resize and compress the image
    imagecopyresampled($resizedImage, $image, 0, 0, 0, 0, $newWidth, $newHeight, $originalWidth, $originalHeight);
    imagejpeg($resizedImage, $destination, 80); // Adjust the compression quality (80 is just an example)

    // Free up memory by destroying the images
    imagedestroy($image);
    imagedestroy($resizedImage);

    //echo "Image uploaded successfully: " . $uniqueFilename;
    //echo "<br>成功上載!";
} else {
    echo 'Upload Error: ' . $_FILES['image']['error'];
}