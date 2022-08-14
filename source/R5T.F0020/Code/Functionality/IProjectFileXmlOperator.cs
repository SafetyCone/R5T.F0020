using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

using R5T.Magyar.Xml;

using R5T.T0132;


namespace R5T.F0020.N000
{
	[FunctionalityMarker]
	public partial interface IProjectFileXmlOperator : IFunctionalityMarker
	{
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

		public async Task SaveProjectFile(
			string filePath,
			XDocument xDocument)
		{
			using var outputFileStream = FileStreamHelper.NewWrite(filePath);

			using var xmlWriter = XmlWriterHelper.New(outputFileStream);

			await xDocument.SaveAsync(
				xmlWriter,
				CancellationToken.None);
		}

		public void SaveProjectFile_Synchronous(
			string filePath,
			XDocument xDocument)
		{
			using var outputFileStream = FileStreamHelper.NewWrite(filePath);

			using var xmlWriter = XmlWriterHelper.New(outputFileStream);

			xDocument.Save(xmlWriter);
		}
	}
}