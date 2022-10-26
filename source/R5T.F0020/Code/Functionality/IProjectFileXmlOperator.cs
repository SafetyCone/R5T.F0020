using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

using R5T.F0000;
using R5T.T0132;


namespace R5T.F0020.N000
{
    [FunctionalityMarker]
	public partial interface IProjectFileXmlOperator : IFunctionalityMarker
	{
		public WasFound<XElement> HasProjectElement(XDocument projectXDocument)
		{
			var wasFound = projectXDocument.HasElement(xDocument =>
			{
				var rootElementIsProject = xDocument.Root?.Name.LocalName == "Project";

				var output = rootElementIsProject
					? xDocument.Root
					: default
					;

				return output;
			});

			return wasFound;
		}

		public async Task<TOutput> InProjectFileXDocumentContext<TOutput>(
			string projectFilePath,
			Func<XDocument, Task<TOutput>> functionOnProjectXDocument)
        {
			var projectXDocument = await this.LoadProjectFile(projectFilePath);

			var output = await functionOnProjectXDocument(projectXDocument);
			return output;
        }

		public TOutput InProjectFileXDocumentContext_Synchronous<TOutput>(
			string projectFilePath,
			Func<XDocument, TOutput> functionOnProjectXDocument)
		{
			var projectXDocument = this.LoadProjectFile_Synchronous(projectFilePath);

			var output = functionOnProjectXDocument(projectXDocument);
			return output;
		}

		public async Task<XDocument> LoadProjectFile(
			Stream stream)
        {
			var projectXDocument = await XDocument.LoadAsync(
				stream,
				LoadOptions.None,
				CancellationToken.None);

			return projectXDocument;
		}

		public XDocument LoadProjectFile_Synchronous(
			Stream stream)
		{
			var projectXDocument = XDocument.Load(
				stream,
				LoadOptions.None);

			return projectXDocument;
		}

		public async Task<XDocument> LoadProjectFile(
			string projectFilePath)
		{
			using var fileStream = Instances.ProjectFileOperator.NewRead(projectFilePath);

			var projectXDocument = await this.LoadProjectFile(fileStream);
			return projectXDocument;
		}

		public XDocument LoadProjectFile_Synchronous(
			string projectFilePath)
		{
			using var fileStream = Instances.ProjectFileOperator.NewRead(projectFilePath);

			var projectXDocument = this.LoadProjectFile_Synchronous(fileStream);
			return projectXDocument;
		}

		public void Save(
			string filePath,
			Project project)
        {
			this.Save(
				filePath,
				project.Element);
        }

		public void Save(
			string filePath,
			XElement projectElement)
		{
			var document = Instances.ProjectFileXDocumentOperator.GetProjectDocument(projectElement);

			Instances.ProjectFileXmlOperator.SaveProjectFile_Synchronous(
				filePath,
				document);
		}

		public async Task SaveProject(
			string filePath,
			XDocument xDocument)
		{
			using var outputFileStream = F0000.FileStreamOperator.Instance.NewWrite(filePath);

			using var xmlWriter = F0000.XmlWriterOperator.Instance.New(outputFileStream);

			await xDocument.SaveAsync(
				xmlWriter,
				CancellationToken.None);
		}

		public void SaveProjectFile_Synchronous(
			string filePath,
			XDocument xDocument)
		{
			F0000.Instances.XmlOperator.Write(
				xDocument,
				filePath);

			using var outputFileStream = F0000.FileStreamOperator.Instance.NewWrite(filePath);

			using var xmlWriter = F0000.XmlWriterOperator.Instance.New(outputFileStream);

			xDocument.Save(xmlWriter);
		}
	}
}