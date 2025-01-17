using System;
using System.IO;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

using R5T.Extensions;
using R5T.L0089.T000;
using R5T.T0132;


namespace R5T.F0020.N000
{
    [FunctionalityMarker]
	public partial interface IProjectFileXmlOperator : IFunctionalityMarker
	{
		public XElement GetProjectElement(XDocument projectDocument)
		{
			var hasProjectElement = this.HasProjectElement(projectDocument);

			var projectElement = hasProjectElement.Get_Result_OrExceptionIfNotFound(
				"No project element found.");

			return projectElement;
		}

		public WasFound<XElement> HasProjectElement(XDocument projectDocument)
		{
			var exists = projectDocument.HasElement(xDocument =>
			{
				var rootElementIsProject = xDocument.Root?.Name.LocalName == "Project";

				var output = rootElementIsProject
					? xDocument.Root
					: default
					;

				return output;
			},
			out var element);

			var output = WasFound.From(
				exists,
				element);

			return output;
		}

		public async Task<TOutput> InProjectFileXDocumentContext<TOutput>(
			string projectFilePath,
			Func<XDocument, Task<TOutput>> functionOnProjectXDocument)
        {
			var projectXDocument = await this.LoadProjectDocument(projectFilePath);

			var output = await functionOnProjectXDocument(projectXDocument);
			return output;
        }

        public async Task<TOutput> InProjectFileXDocumentContext<TOutput>(
            string projectFilePath,
            Func<XDocument, TOutput> functionOnProjectXDocument)
        {
            var projectXDocument = await this.LoadProjectDocument(projectFilePath);

            var output = functionOnProjectXDocument(projectXDocument);
            return output;
        }

        public TOutput InProjectFileXDocumentContext_Synchronous<TOutput>(
			string projectFilePath,
			Func<XDocument, TOutput> functionOnProjectXDocument)
		{
			var projectXDocument = this.LoadProjectDocument_Synchronous(projectFilePath);

			var output = functionOnProjectXDocument(projectXDocument);
			return output;
		}

        public async Task<XElement> LoadProjectElement(
            string projectFilePath)
        {
            var projectDocument = await this.LoadProjectDocument(projectFilePath);

            var projectElement = this.GetProjectElement(projectDocument);
            return projectElement;
        }

        public XElement LoadProjectElement_Synchronous(
            string projectFilePath)
        {
            var projectDocument = this.LoadProjectDocument_Synchronous(projectFilePath);

            var projectElement = this.GetProjectElement(projectDocument);
            return projectElement;
        }

        public async Task<XDocument> LoadProjectDocument(
			Stream stream)
        {
			var projectXDocument = await XDocument.LoadAsync(
				stream,
				LoadOptions.None,
				CancellationToken.None);

			return projectXDocument;
		}

		public XDocument LoadProjectDocument_Synchronous(
			Stream stream)
		{
			var projectXDocument = XDocument.Load(
				stream,
				LoadOptions.None);

			return projectXDocument;
		}

		public async Task<XDocument> LoadProjectDocument(
			string projectFilePath)
		{
			using var fileStream = Instances.FileStreamOperator.Open_Read(projectFilePath);

			var projectXDocument = await this.LoadProjectDocument(fileStream);
			return projectXDocument;
		}

		public XDocument LoadProjectDocument_Synchronous(
			string projectFilePath)
		{
			using var fileStream = Instances.FileStreamOperator.Open_Read(projectFilePath);

			var projectXDocument = this.LoadProjectDocument_Synchronous(fileStream);
			return projectXDocument;
		}

		public void SaveProject(
			string filePath,
			Project project)
        {
			this.SaveProjectElement_Synchronous(
				filePath,
				project.Element);
        }

        public async Task SaveProjectElement(
            string filePath,
            XElement projectElement)
        {
            var document = Instances.ProjectFileXDocumentOperator.GetProjectDocument(projectElement);

            await Instances.ProjectFileXmlOperator.SaveProjectDocument(
                filePath,
                document);
        }

        public void SaveProjectElement_Synchronous(
			string filePath,
			XElement projectElement)
		{
			var document = Instances.ProjectFileXDocumentOperator.GetProjectDocument(projectElement);

			Instances.ProjectFileXmlOperator.SaveProjectFile_Synchronous(
				filePath,
				document);
		}

		public async Task SaveProjectDocument(
			string filePath,
			XDocument xDocument)
		{
			using var outputFileStream = Instances.FileStreamOperator.Open_Write(filePath);

			using var xmlWriter = Instances.XmlWriterOperator.New(outputFileStream);

			await xDocument.SaveAsync(
				xmlWriter,
				CancellationToken.None);
		}

		public void SaveProjectFile_Synchronous(
			string filePath,
			XDocument xDocument)
		{
			Instances.XmlOperator.Write(
				xDocument,
				filePath);

			using var outputFileStream = Instances.FileStreamOperator.Open_Write(filePath);

			using var xmlWriter = Instances.XmlWriterOperator.New(outputFileStream);

			xDocument.Save(xmlWriter);
		}

        public Task SaveProjectFile(
            string filePath,
            XDocument xDocument)
		{
			return this.SaveProjectDocument(
				filePath,
				xDocument);
		}

    }
}