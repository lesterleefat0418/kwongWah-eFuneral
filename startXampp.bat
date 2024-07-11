cd /d "C:\xampp\"
Start "" /b xampp-control.exe
timeout /T 1 /nobreak >nul
powershell (ps xampp-control).CloseMainWindow()