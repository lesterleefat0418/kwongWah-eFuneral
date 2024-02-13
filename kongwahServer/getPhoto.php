<?php
$uploadRootFolder = 'uploads/people/';

// Create the date folder if it does not exist
$dateFolder = date('Ymd');
$uploadFolder = $uploadRootFolder . $dateFolder . '/';
if (!is_dir($uploadFolder)) {
    mkdir($uploadFolder, 0777, true);
}

$latestImageFile = getLatestImageFile($uploadFolder);
if ($latestImageFile !== null) {
    $imageUrl = 'http://localhost/kongwahServer/' . $uploadFolder . $latestImageFile;
    echo $imageUrl;
} else {
    echo ""; // Return an empty string if no new image is available
}

function getLatestImageFile($uploadFolder) {
    $latestFile = null;
    $latestTime = 0;

    $files = scandir($uploadFolder);

    foreach ($files as $file) {
        $filePath = $uploadFolder . $file;

        if (is_file($filePath) && filectime($filePath) > $latestTime) {
            $latestFile = $file;
            $latestTime = filectime($filePath);
        }
    }

    return $latestFile;
}
?>