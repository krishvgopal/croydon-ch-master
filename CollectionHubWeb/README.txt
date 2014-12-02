

IMPORTANT!
------------------------------------------------------------------------------------
Ensure the following call is applied on the webservers to ensure json is compressed. 
This has a _MASSIVE_ impact on performance.

////
cd \Windows\System32\inetsrv
appcmd.exe set config -section:system.webServer/httpCompression /+"dynamicTypes.[mimeType='application/json',enabled='True']" /commit:apphost
////


IMPORTANT!
------------------------------------------------------------------------------------
You will need to ensure that ASP.NET State Server Is Running On The Web Servers