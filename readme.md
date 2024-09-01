//Disable Setting button
https://www.youtube.com/watch?v=hCmcjPtpf4o&ab_channel=NewbieComputer

root directory confg.txt:

{
    "getImageFolderName": "KwongwahServer",   //Server folder name
    "adminPassword": "000000",    // change to admin mode password
    "isLogined": false,  //current login status
    "fullGameTime": 1200.0, // gameTime: 20 minutes
    "onlyHuabaoTime": 600.0, // skip to Huabao duration 10 minutes
    "goodByePageDuration": 150.0, //good bye page duration 2.5 minutes
    "latestUploadPhotoWithinTime": 5.0, // check photo last update intervel
    "topMostEnable": false,  //game focus on the toppest layer
    "showServerInitErrorBox": false  //status allow to show server log error box
}

Installation steps:
1. install xampp to pc;
2. setup the maximum upload config in php.ini;
3. extract the KwongwahServer and put to xampp htdocs directory;
4. start xampp for testing;
5. config xampp for auto startup in settings;
6. extract the unity exe from Kwongwah.zip folder and copy desktop shortcut to window startup;
7. config the config.txt to enable topMostEnable before daily usage;