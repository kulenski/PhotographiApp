# PhotographiApp
The purpose of this app is to serve as photo management system with some social functionalitie like comments, discussions, faves.
![Mock](https://github.com/kulenski/PhotographiApp/blob/master/PhotographiShowcase.png?raw=true)
# Features
Application allow users access and perform the following functionalities: 
* Upload photos.
* Organize photos in albums.
* Mark as favorite other people photos.
* Comment other and/or own photos.
* Create discussions.
* View other people's profiles.
# Technical specification
Applications is written in ASP.NET Core 5 and is using the following technologies:
* MVC
* EntityFrameworkCore
# Services
The following services are used:
* Cloudinary
# Other libraries
* MetadataExtractor
# Database / Entities
Application uses Microsoft SQL Server and EntityFrameworkCore to manage the business entities.
They are:
* Album
* Photo
* PhotoAlbum
* PhotoFavorite
* Comment
* Topic
* TopicReply
* License
# Backend
The web project contains:
* 2 areas: Identity, Administration
* 10+ controllers
* 20+ views
* 8 data services
* 2 additional services

