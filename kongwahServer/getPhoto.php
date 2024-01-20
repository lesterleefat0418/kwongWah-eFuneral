<?php
$latestImageFile = getLatestImageFile();
if ($latestImageFile !== null) {
    $imageUrl = 'http://localhost/kongwahServer/uploads/people/' . $latestImageFile;
    echo $imageUrl;
} else {
    echo ""; // Return an empty string if no new image is available
}

function getLatestImageFile() {
    $uploadFolder = 'uploads/people/';
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