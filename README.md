# CleanArchitecture

This project is a demo project for creating a Clean architecture based framework. The application is supposed to be a set of applications for a small webshop owner

This containts some major parts:

0. the framework itself

1. Backoffice: an offline (WPF) application used to administrate all data inside the application (products, categories, languages, currencies, ...), monitor user administration (loss of password, ...) and operational management (reporting, logging, ...)

2. Webshop: the real webshop where users can buy products.

3. OrderPicking: a mobile, cross platform app to pick the ordered products. This is intentionally designed to use mobile devices since small business owner cannot afford large order picking software.

4. Authentication service: this is used to allow users to authenticate themselves in the webshop

5. WindowsAuthentication service: this is used to allow users (for the backoffice) to use windows authentication. This is to produce tokens that can also be used to authenticate to the webshop.

