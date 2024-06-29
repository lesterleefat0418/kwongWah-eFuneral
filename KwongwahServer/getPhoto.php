<?php
$uploadRootFolder = 'uploads/people/';

// Delete all date folders that are not for the current date
deleteNonCurrentDateFolders($uploadRootFolder);

// Create the date folder if it does not exist
$dateFolder = date('Ymd');
$uploadFolder = $uploadRootFolder . $dateFolder . '/';
if (!is_dir($uploadFolder)) {
    if (!mkdir($uploadFolder, 0777, true) && !is_dir($uploadFolder)) {
        die("Failed to create upload directory: " . $uploadFolder);
    }
}

$latestImageFile = getLatestImageFile($uploadFolder);
if ($latestImageFile !== null) {
    $imageUrl = 'http://localhost/kwongwahServer/' . $uploadFolder . $latestImageFile;
    $creationTime = filectime($uploadFolder . $latestImageFile);
    echo "NEW_IMAGE:" . $imageUrl. "," . $creationTime;
} else {
    echo "NO_NEW_IMAGE"; // Return an empty string if no new image is available
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

function deletePreviousDayFolder($uploadRootFolder) {
    $previousDayFolder = date('Ymd', strtotime('-1 day'));
    $previousDayFolderPath = $uploadRootFolder . $previousDayFolder . '/';

    if (is_dir($previousDayFolderPath)) {
        $files = array_diff(scandir($previousDayFolderPath), array('.', '..'));

        foreach ($files as $file) {
            $filePath = $previousDayFolderPath . $file;
            if (is_file($filePath)) {
                unlink($filePath);
            }
        }

        rmdir($previousDayFolderPath);
    }
}

function deleteNonCurrentDateFolders($uploadRootFolder) {
    $currentDateFolder = date('Ymd');
    $folders = scandir($uploadRootFolder);

    foreach ($folders as $folder) {
        $folderPath = $uploadRootFolder . $folder;

        if ($folder !== $currentDateFolder && is_dir($folderPath) && preg_match('/^\d{8}$/', $folder)) {
            $files = array_diff(scandir($folderPath), array('.', '..'));

            foreach ($files as $file) {
                $filePath = $folderPath . '/' . $file;
                if (is_file($filePath)) {
                    unlink($filePath);
                }
            }

            rmdir($folderPath);
        }
    }
}
?>