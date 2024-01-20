<?php
if ($_FILES['image']['error'] === UPLOAD_ERR_OK) {
    $file = $_FILES['image']['tmp_name'];
    $filename = $_FILES['image']['name'];
    $destination = 'uploads/people/' . urlencode($filename);

    if (move_uploaded_file($file, $destination)) {
        echo 'Image uploaded successfully';
    } else {
        echo 'Error uploading image';
    }
} else {
    echo 'Error: ' . $_FILES['image']['error'];
}
?>