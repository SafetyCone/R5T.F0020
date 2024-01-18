using System;
using System.Linq;
using System.Xml.Linq;

using R5T.T0132;
using R5T.T0221;


namespace R5T.F0020.Internal
{
    [FunctionalityMarker]
	public partial interface IProjectXmlOperator
	{
		public XElement AcquirePropertyGroupWithChildElement(XElement projectElement,
			string propertyGroupIdentifyingChildElementName)
		{
            var propertyGroupWithChildWasFound = this.HasPropertyGroupWithChildElement(
                projectElement,
                propertyGroupIdentifyingChildElementName);

            var propertyGroup = propertyGroupWithChildWasFound
                ? propertyGroupWithChildWasFound.Result
                : Instances.ProjectXmlOperator.AddPropertyGroup(projectElement)
                ;

            return propertyGroup;
        }

		public XElement AcquirePropertyGroupChildElement(XElement projectElement,
			string propertyGroupChildElementName)
		{
			// Use the same child name to identify the property group.
			var childElement = this.AcquirePropertyGroupChildElement(projectElement,
				propertyGroupChildElementName,
				propertyGroupChildElementName);

			return childElement;
		}

		public XElement AcquireFirstPropertyGroup(XElement projectElement)
		{
			var firstPropertyGroupOrDefault = projectElement.Children()
				.WhereNameIs(ElementNames.Instance.PropertyGroup)
				.FirstOrDefault();

			var firstPropertyGroupWasFound = firstPropertyGroupOrDefault != default;

			var firstPropertyGroup = firstPropertyGroupWasFound
				? firstPropertyGroupOrDefault
				: Instances.ProjectXmlOperator.AddPropertyGroup(projectElement)
				;

			return firstPropertyGroup;
		}

		/// <summary>
		/// Allows using one property group child element name to identify the property group of interest, while selecting a different child element from the property group.
		/// </summary>
		public XElement AcquirePropertyGroupChildElement(XElement projectElement,
			string propertyGroupIdentifyingChildElementName,
			string propertyGroupChildElementName)
        {
			var propertyGroupWithChildWasFound = this.HasPropertyGroupWithChildElement(
				projectElement,
				propertyGroupIdentifyingChildElementName);

			var propertyGroup = propertyGroupWithChildWasFound
				? propertyGroupWithChildWasFound.Result
				: this.AcquireFirstPropertyGroup(projectElement)
				;

			var childWasFound = propertyGroup.HasChild(propertyGroupChildElementName);
			if (!childWasFound)
			{
				var childElement = Instances.XElementOperator.AddChild(propertyGroup, propertyGroupChildElementName);
				return childElement;
			}

			return childWasFound.Result;
		}

		public WasFound<XElement> HasPropertyGroupChildElement(XElement projectElement,
			string propertyGroupIdentifyingChildElementName,
			string propertyGroupChildElementName)
		{
			var propertyGroupWithChildWasFound = this.HasPropertyGroupWithChildElement(
				projectElement,
				propertyGroupIdentifyingChildElementName);

			if (!propertyGroupWithChildWasFound)
			{
				return WasFound.NotFound<XElement>();
			}

			var propertyGroup = propertyGroupWithChildWasFound.Result;

			var child = propertyGroup.HasChild(propertyGroupChildElementName);
			return child;
		}

		public WasFound<XElement> HasPropertyGroupChildElement(XElement projectElement,
			string propertyGroupChildElementName)
        {
			var hasPropertyGroupChildElement = this.HasPropertyGroupChildElement(projectElement,
				propertyGroupChildElementName,
				propertyGroupChildElementName);

			return hasPropertyGroupChildElement;
        }

		public XElement GetPropertyGroupChildElement(XElement projectElement,
			string propertyGroupIdentifyingChildElementName,
			string propertyGroupChildElementName)
		{
			var hasPropertyGroupChildElement = this.HasPropertyGroupChildElement(
				projectElement,
				propertyGroupIdentifyingChildElementName,
				propertyGroupChildElementName);

			if(!hasPropertyGroupChildElement)
            {
				throw new Exception($"Unable to find property group with child element: {propertyGroupIdentifyingChildElementName}");
            }

			return hasPropertyGroupChildElement;
		}

		public XElement GetPropertyGroupChildElement(XElement projectElement,
			string propertyGroupChildElementName)
		{
			// Use the same child name to identify the property group.
			var childElement = this.GetPropertyGroupChildElement(projectElement,
				propertyGroupChildElementName,
				propertyGroupChildElementName);

			return childElement;
		}

		/// <summary>
		/// Gets the property group element with the specified child element, if it exists.
		/// </summary>
		public WasFound<XElement> HasPropertyGroupWithChildElement(XElement projectElement, string propertyGroupChildElementName)
		{
			// Assume just one property group with the element name.
			var wasFound = Instances.XElementOperator.HasChildWithChild_Single(
				projectElement,
				Instances.ElementNames.PropertyGroup,
				propertyGroupChildElementName);

			return wasFound;
		}

		/// <summary>
		/// Allows using one property group child element name to identify the property group of interest, while selecting a different child element from the property group.
		/// </summary>
		public void SetPropertyGroupChildElementValue(XElement projectElement,
			string propertyGroupIdentifyingChildElementName,
			string propertyGroupChildElementName,
			string value)
		{
			var propertyGroupChildElement = this.AcquirePropertyGroupChildElement(projectElement,
				propertyGroupIdentifyingChildElementName,
				propertyGroupChildElementName);

			propertyGroupChildElement.Value = value;
		}

		public string GetPropertyGroupChildElementValue(XElement projectElement,
			string propertyGroupChildElementName)
		{
			var propertyGroupChildElement = this.GetPropertyGroupChildElement(projectElement,
				propertyGroupChildElementName);

			var value = propertyGroupChildElement.Value;
			return value;
		}

		public WasFound<string> HasPropertyGroupChildElementValue(XElement projectElement,
			string propertyGroupChildElementName)
        {
			var hasPropertyGroupChildElement = this.HasPropertyGroupChildElement(projectElement,
				propertyGroupChildElementName);

			var valueWasFound = hasPropertyGroupChildElement.Convert(x => x.Value);
			return valueWasFound;
		}

		public void SetPropertyGroupChildElementValue(XElement projectElement,
			string propertyGroupChildElementName,
			string value)
        {
			var propertyGroupChildElement = this.AcquirePropertyGroupChildElement(projectElement,
				propertyGroupChildElementName);

			propertyGroupChildElement.Value = value;
        }
	}
}