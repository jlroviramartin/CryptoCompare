# CryptoCompare
C# cryptocompare.com REST client

This library implements a C# client of the REST API cryptocompare.com.
It is composed of several projects.
- BuildCryptoCompare
  This project builds a skeleton of the client using NJsonSchema and NSwag code generators. The json schema was created using [https://studio.restlet.com].

- CryptoCompare
  This project contains the code generated by BuildCryptoCompare and other classes. It implements the REST client.
