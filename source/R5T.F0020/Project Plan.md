# R5T.F0020
Project file related functionality.


## Implementation Details

Specific:

* All paths are project-element relative. Values are in: IProjectElementRelativeXPaths.
* Operations are performed in IProjectXmlOperator.

General:

* System.Xml.Linq types are used for the model.(Due to easy de/serialization from the XML that a project file actually is.)
* All operations will be performed on the Project XElement.
* When de/serialized, only then will an XDocument be used.


## Links

* <LangVersion> https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/configure-language-version

